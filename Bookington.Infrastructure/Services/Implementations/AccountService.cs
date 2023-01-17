using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.Extensions.Configuration;


namespace Bookington.Infrastructure.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ISmsService _smsService;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, ITokenService tokenService, ISmsService smsService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenService = tokenService;
            _smsService = smsService;
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

            await _unitOfWork.AccountRepository.AddAsync(account);
            
            account.RoleId = "1";

            //Call Send SMS
            await _smsService.sendSmsAsync(dto.Phone, accountOtp.Otp);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountReadDTO>(account);
        }

        public async Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto)
        {
            var existAccount = await _unitOfWork.AccountRepository.LoginByPhoneAsync(dto);

            if (existAccount == null) throw new EntityNotFoundException("User Phone Or Password Incorrect");
            
            if (existAccount.IsActive == false) throw new InvalidActionException("Account Not Yet Verify");

            var role = await _unitOfWork.RoleRepository.FindAsync(existAccount.RoleId);

            //create new Token and return 
            return new AccountLoginOutputDTO
            {
                UserID = existAccount.Id,
                PhoneNumber = existAccount.Phone,
                FullName = existAccount.FullName,
                Role = role.RoleName,
                SysToken = await _tokenService.GenerateTokenAsync(existAccount),
                SysTokenExpires = 1200
            };
        }

        public async Task VerifyAccount(string phoneNumber, string otp)
        {
            var accountOtp = await _unitOfWork.OtpRepository.VerifyAccountAsync(phoneNumber, otp);

            if (accountOtp == null) throw new EntityNotFoundException("Otp Incorrect");

            var account = await _unitOfWork.AccountRepository.FindAccountByPhoneNumberAsync(accountOtp.Phone);

            account.IsActive = true;

            await _unitOfWork.CommitAsync();

        }

        public async Task<AccountReadDTO> UpdateAsync(int id, AccountWriteDTO dto)
        {
            var existAccount = await _unitOfWork.RoleRepository.FindAsync(id);

            if (existAccount == null) throw new EntityWithIDNotFoundException<Court>(id);

            _mapper.Map(dto, existAccount);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountReadDTO>(existAccount);
        }

        public async Task DeleteAsync(int id)
        {
            var existAccount = await _unitOfWork.AccountRepository.FindAsync(id);

            if (existAccount == null) throw new EntityWithIDNotFoundException<Account>(id);

            _unitOfWork.AccountRepository.Delete(existAccount);

            await _unitOfWork.CommitAsync();
        }

        public async Task<AccountReadDTO> GetByIdAsync(string id)
        {
            var existAccount = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existAccount == null) throw new EntityWithIDNotFoundException<Account>(id);

            return _mapper.Map<AccountReadDTO>(existAccount);
        }
    }
}
