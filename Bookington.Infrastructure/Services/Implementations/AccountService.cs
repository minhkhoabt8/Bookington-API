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

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService, ISmsService smsService, IUserBalanceService userBalanceService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _smsService = smsService;
            _userBalanceService = userBalanceService;
            _userContextService = userContextService;
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
            if (existAccount != null) throw new UniqueConstraintException<Account>(nameof(Account.Phone), dto.Phone);
            //generate otp and add to OTP table
            var otp = OtpDTO.GenerateOTP();

            var accountOtp = _mapper.Map<AccountOtp>(otp);
            
            accountOtp.Phone = dto.Phone;

            await _unitOfWork.OtpRepository.AddAsync(accountOtp);

            //auto mapper
            var account = _mapper.Map<Account>(dto);

            //encrypt password
            //account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);

            await _unitOfWork.AccountRepository.AddAsync(account);
            
            account.RoleId = ((int) AccountRole.customer).ToString();

            //Call Send SMS
            await _smsService.sendSmsAsync(dto.Phone, accountOtp.OtpCode);

            // Create A Balance For The New User
            var userBalance = new UserBalanceWriteDTO();

            userBalance.RefUser = account.Id;

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

            if (account==null) throw new EntityNotFoundException("Phone Number Incorrect");

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
            
            if (existAccount == null || existAccount.IsDeleted == true ) throw new EntityWithIDNotFoundException<Account>(id);
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
        //TODO: Profile
        public async Task<AccountProfileReadDTO> GetProfileAsync()
        {
            // JWT token check (TRUE to proceed)

            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // I'll do the avatar thing later

            var profile = await _unitOfWork.AccountRepository.FindAsync(accountId);

            return _mapper.Map<AccountProfileReadDTO>(profile);
        }
        //TODO: Profile
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

            if (dto.NewPassword.Equals(dto.ConfirmPassword))
            {
                existAccount.Password = dto.NewPassword;

                await _unitOfWork.CommitAsync();
            }
            else throw new Exception("Confirm Password Not Match");
        }

        public async Task AssignRoleToUserAsync(string userId, string roleId)
        {
            var existAccount = await _unitOfWork.AccountRepository.FindAsync(userId);

            if(existAccount == null) throw new EntityWithIDNotFoundException<Account>(userId);

            existAccount.RoleId = roleId;
            

            await _unitOfWork.CommitAsync();
        }
    }
}
