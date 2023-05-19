using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.UserBalance;
using Bookington.Core.Enums;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Bookington.Infrastructure.DTOs.File;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly ISmsService _smsService;
        private readonly IUserBalanceService _userBalanceService;
        private readonly IUserContextService _userContextService;
        private readonly IUploadFileService _uploadFileService;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService, ISmsService smsService, IUserBalanceService userBalanceService, IUserContextService userContextService, IUploadFileService uploadFileService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _smsService = smsService;
            _userBalanceService = userBalanceService;
            _userContextService = userContextService;
            _uploadFileService = uploadFileService;
        }

        public async Task<IEnumerable<AccountReadDTO>> GetAllAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountReadDTO>>(accounts);
        }

        public async Task<AccountReadDTO> CreateAsync(AccountWriteDTO dto)
        {
            //check account phone exist
            var existAccount = await _unitOfWork.AccountRepository.FindAccountByPhoneNumberAsync(dto.Phone);
           
            if(existAccount != null)
            {
                if (!existAccount.IsVerified) throw new InvalidActionException("Account Is Not Verify");

                else
                {
                    throw new UniqueConstraintException<Account>(nameof(Account.Phone), dto.Phone);
                }
            }

            //add account
            var account = _mapper.Map<Account>(dto);

            account.RefAvatar = "";

            await _unitOfWork.AccountRepository.AddAsync(account);

            account.RoleId = ((int)AccountRole.customer).ToString();

            //generate otp and add to OTP table
            var otp = OtpDTO.GenerateOTP();

            var accountOtp = _mapper.Map<AccountOtp>(otp);

            accountOtp.Phone = dto.Phone;

            accountOtp.RefAccount = account.Id;

            await _unitOfWork.OtpRepository.AddAsync(accountOtp);

            //Call Send SMS

            //await _smsService.sendSmsAsync(dto.Phone, accountOtp.OtpCode);

            // Create A Balance For The New User
            var userBalance = new UserBalanceWriteDTO();

            userBalance.RefUser = account.Id;

            userBalance.Balance = 200000;

            await _userBalanceService.CreateAsync(userBalance);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountReadDTO>(account);
        }

        public async Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto)
        {
            var existAccount = await _unitOfWork.AccountRepository.LoginByPhoneAsync(dto);

            if (existAccount == null) throw new EntityNotFoundException("User Phone Or Password Incorrect");

            if (existAccount.IsActive == false) throw new InvalidActionException("Account Not Yet Verify");

            if (existAccount.IsDeleted == true) throw new InvalidActionException("Account Is Deleted!");

            if (existAccount.IsActive == false) throw new InvalidActionException("Account Is Banned!");

            var role = await _unitOfWork.RoleRepository.FindAsync(existAccount.RoleId);

            // Generate new refresh token
            var newRefreshToken = _tokenService.GenerateRefreshToken(existAccount);

            //create new Token and return 
            return new AccountLoginOutputDTO
            {
                UserID = existAccount.Id,
                PhoneNumber = existAccount.Phone,
                FullName = existAccount.FullName!,
                Role = role!.RoleName,
                SysToken = await _tokenService.GenerateTokenAsync(existAccount),
                //SysTokenExpires = 60 * 60 * 4, // 4 hours
                SysTokenExpires = 12000,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpires = newRefreshToken.ExpiresIn,
            };

        }

        public async Task<AccountLoginOutputDTO> LoginWithRefreshTokenAsync(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new InvalidRefreshTokenException();
            }
            var refreshToken = await _unitOfWork.LoginTokenRepository.FindByTokenIncludeAccountAsync(token) ??
                           throw new InvalidRefreshTokenException();

            // Refresh token compromised => revoke all tokens in family
            if (refreshToken.IsRevoked)
            {
                // Travel down family chain
                while (refreshToken.RefreshToken != null)
                {
                    // Descendant token
                    refreshToken =
                        await _unitOfWork.LoginTokenRepository.FindByTokenAsync(refreshToken.RefreshToken);
                    refreshToken!.Revoke();
                }
                await _unitOfWork.CommitAsync();
                throw new InvalidRefreshTokenException();
            }

            // Expired token
            if (refreshToken.IsExpired)
            {
                throw new InvalidRefreshTokenException();
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken(refreshToken.RefAccountNavigation);

            await _unitOfWork.LoginTokenRepository.AddAsync(newRefreshToken);

            refreshToken.ReplaceWith(newRefreshToken);

            await _unitOfWork.CommitAsync();

            return new AccountLoginOutputDTO
            {
                UserID = refreshToken.RefAccountNavigation.Id,

                PhoneNumber = refreshToken.RefAccountNavigation.Phone,

                FullName = refreshToken.RefAccountNavigation.FullName,

                Role = refreshToken.RefAccountNavigation.Role.RoleName,

                SysToken = await _tokenService.GenerateTokenAsync(refreshToken.RefAccountNavigation),

                //SysTokenExpires = 60 * 60 * 4, // 4 hours
                SysTokenExpires = 12000,

                RefreshToken = newRefreshToken.Token,

                RefreshTokenExpires = newRefreshToken.ExpiresIn,
            };

        }

        public async Task VerifyAccount(string phoneNumber, string otp)
        {
            var accountOtp = await _unitOfWork.OtpRepository.VerifyAccountAsync(phoneNumber, otp);

            if (accountOtp == null) throw new EntityNotFoundException("Otp Incorrect");

            var account = await _unitOfWork.AccountRepository.FindAccountByPhoneNumberAsync(accountOtp.Phone);

            if (account == null) throw new EntityNotFoundException("Phone Number Incorrect");

            account.IsActive = true;

            accountOtp.IsConfirmed = true;

            await _unitOfWork.CommitAsync();

        }

        public async Task<AccountReadDTO> UpdateAsync(string id, AccountUpdateDTO dto)
        {
            var existAccount = await _unitOfWork.AccountRepository.FindAsync(id);

            if (existAccount == null || existAccount.IsDeleted == true) throw new EntityWithIDNotFoundException<Account>(id);
            // User can't update other people's profile
            else if (existAccount?.Id != _userContextService.AccountID.ToString()) throw new InvalidActionException("You can't update other people's profile!");

            _mapper.Map(dto, existAccount);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountReadDTO>(existAccount);
        }

        public async Task DeleteAsync(string id)
        {
            var existAccount = await _unitOfWork.AccountRepository.FindAsync(id);

            if (existAccount == null || existAccount.IsDeleted == true) throw new EntityWithIDNotFoundException<Account>(id);
            // Admin can't delete themselves
            else if (existAccount?.Id == _userContextService.AccountID.ToString()) throw new InvalidActionException("Can't delete self!");

            existAccount!.IsDeleted = true;

            _unitOfWork.AccountRepository.Update(existAccount);

            await _unitOfWork.CommitAsync();
        }

        public async Task<AccountReadDTO> GetByIdAsync(string id)
        {
            var existAccount = await _unitOfWork.AccountRepository.FindAsync(id);

            if (existAccount == null) throw new EntityWithIDNotFoundException<Account>(id);

            return _mapper.Map<AccountReadDTO>(existAccount);
        }


        public async Task<PaginatedResponse<AccountReadDTO>> QueryAccountsAsync(AccountQuery query)
        {
            var accounts = (await _unitOfWork.AccountRepository.QueryAsync(query)).ToList();

            var currAccountID = _userContextService.AccountID.ToString();

            var currAccount = new Account();

            foreach (var acc in accounts)
            {
                if (acc.Id == currAccountID) currAccount = acc;
            }

            if (currAccount == null) throw new EntityWithIDNotFoundException<Account>(currAccountID!);

            accounts.Remove(currAccount!);

            return PaginatedResponse<AccountReadDTO>.FromEnumerableWithMapping(
                accounts, query, _mapper);
        }

        //Query Account with Role object
        public async Task<PaginatedResponse<AccountReadDTOModel>> QueryAccountsModelAsync(AccountQuery query)
        {
            var accounts = (await _unitOfWork.AccountRepository.QueryAsync(query)).ToList();

            var currAccountID = _userContextService.AccountID.ToString();

            var currAccount = new Account();

            //filter admin who using this  method
            foreach (var acc in accounts)
            {
                if (acc.Id == currAccountID) currAccount = acc;
            }

            if (currAccount == null) throw new EntityWithIDNotFoundException<Account>(currAccountID!);

            accounts.Remove(currAccount!);


            return PaginatedResponse<AccountReadDTOModel>.FromEnumerableWithMapping(
                accounts, query, _mapper);
        }

        
        public async Task<AccountProfileReadDTO> GetProfileAsync()
        {
            // JWT token check (TRUE to proceed)

            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();


            var profile = await _unitOfWork.AccountRepository.FindAsync(accountId);

            var listImage = new List<string>();

            listImage.Add(profile.RefAvatar);

            var imageFiles = await _uploadFileService.GetImageFilesAsync(listImage, true);

            var accountProfile =  _mapper.Map<AccountProfileReadDTO>(profile);

            if (imageFiles.Any())
            {
                accountProfile.File = imageFiles.ElementAt(0);
            }
            else
            {
                accountProfile.File = null; // or some other default value
            }

            return accountProfile;
        }

        public async Task<AccountProfileReadDTO> GetProfileByIdAsync(string accountId)
        {
            var profile = await _unitOfWork.AccountRepository.FindAsync(accountId);

            if (profile == null) throw new EntityWithIDNotFoundException<Account>(accountId);

            return _mapper.Map<AccountProfileReadDTO>(profile);
        }

        public async Task ReSendVerifyOtp(string phone)
        {
            //check validate otp account phone and is confirmed
            var accountOtp = await _unitOfWork.OtpRepository.FindAccountOtpByPhone(phone);
            if (accountOtp == null) throw new EntityNotFoundException("Phone Not Found");
            //create new otp and update account otp using phone number
            var otp = OtpDTO.GenerateOTP();
            _mapper.Map(accountOtp, otp);
            //send otp to user
            await _smsService.sendSmsAsync(accountOtp.Phone, accountOtp.OtpCode);
            //save updated account otp to db
            await _unitOfWork.CommitAsync();
        }

        public async Task ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var existAccount = await _unitOfWork.AccountRepository.FindAsync(dto.UserId);

            
            if (existAccount == null) throw new EntityWithIDNotFoundException<Account>(dto.UserId);

            else if (existAccount?.Id != _userContextService.AccountID.ToString()) throw new ForbiddenException();

            bool verify = BCrypt.Net.BCrypt.Verify(dto.OldPassword, existAccount.Password);

            if (!verify) throw new Exception("Old Password Not Match");

            if (dto.NewPassword.Equals(dto.ConfirmPassword))
            {
                existAccount.Password = dto.NewPassword;

                await _unitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception("Confirm Password Not Match");
            }
            
              
        }

        public async Task AssignRoleToUserAsync(string userId, string roleId)
        {
            var existAccount = await _unitOfWork.AccountRepository.FindAsync(userId);

            if (existAccount == null) throw new EntityWithIDNotFoundException<Account>(userId);

            existAccount.RoleId = roleId;


            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAccountAvatar(FileUploadDTO dto)
        {
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var existAccount = await  _unitOfWork.AccountRepository.FindAsync(accountId);

            //check  if exist account aldready contain a file. if so, delete that file and upload a new file

            if (existAccount.RefAvatar != null)
            {
                await _uploadFileService.DeleteFileAsync(existAccount.RefAvatar);
            }

            if (existAccount == null) throw new EntityWithIDNotFoundException<Account>(accountId);

            var fileName = await _uploadFileService.UploadFileAsyncReturnFileName(dto.File, dto.IsAccount);

            existAccount.RefAvatar = fileName;

            _unitOfWork.AccountRepository.Update(existAccount);

            await _unitOfWork.CommitAsync();

        }

        public async Task<AccountReadDTO> VerifyPhoneNumber(string phoneNumber)
        {
            //Find account with phone number
            var existAccount = await _unitOfWork.AccountRepository.FindAccountByPhoneNumberAsync(phoneNumber);

            if(existAccount == null) throw new UniqueConstraintException<Account>(nameof(Account.Phone), phoneNumber);

            //Check Phone Number Is Not Verify 
            if (existAccount.IsActive == false) throw new InvalidActionException("This Phone Is Not Yet Verify");

            if (existAccount.IsDeleted == true) throw new InvalidActionException("This Phone Is Deleted!");

            //create OTP for verify and send sms message

            //generate otp and add to OTP table
            var otp = OtpDTO.GenerateOTP();

            var accountOtp = _mapper.Map<AccountOtp>(otp);
            accountOtp.Phone = existAccount.Phone;

            accountOtp.RefAccount = existAccount.Id;

            await _unitOfWork.OtpRepository.AddAsync(accountOtp);

            //Call Send SMS
            await _smsService.sendSmsAsync(existAccount.Phone, accountOtp.OtpCode);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountReadDTO>(existAccount);
        }

        public async Task<AccountReadDTO> UpdatePassword(string phoneNumber, string password)
        {
            // Find account with phone number
            var existAccount = await _unitOfWork.AccountRepository.FindAccountByPhoneNumberAsync(phoneNumber);

            if (existAccount == null) throw new UniqueConstraintException<Account>(nameof(Account.Phone), phoneNumber);

            existAccount.Password = password;

            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountReadDTO>(existAccount);

        }
    }  
}
    


