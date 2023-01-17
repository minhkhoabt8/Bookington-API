using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
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

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, ITokenService tokenService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _tokenService = tokenService;
        }
        public async Task<IEnumerable<AccountReadDTO>> GetAllAsync()
        {
            var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountReadDTO>>(accounts);
        }

        public async Task<AccountReadDTO> CreateAsync(AccountWriteDTO dto)
        {
            //check account phone exist
            var existAccount = await _unitOfWork.AccountRepository.FindAccountByPhoneNumber(dto.Phone);
            if (existAccount != null) throw new UniqueConstraintException<Account>(nameof(Account.Phone), dto.Phone);
            
            //generate otp and add to OTP table
            var otp = OtpDTO.GenerateOTP();

            var accountOtp = _mapper.Map<AccountOtp>(otp);

            accountOtp.Phone = dto.Phone;

            await _unitOfWork.OtpRepository.AddAsync(accountOtp);

            //Call Send SMS Service

            SmsSpeedService smsApi = new SmsSpeedService();

            await smsApi.sendSmsAsync(dto.Phone, accountOtp.Otp);

            //auto mapper
            var account = _mapper.Map<Account>(dto);

            await _unitOfWork.AccountRepository.AddAsync(account);

            account.RoleId = "1";

            await _unitOfWork.CommitAsync();

            return _mapper.Map<AccountReadDTO>(account);
        }

        public async Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto)
        {
            var existAccount = await _unitOfWork.AccountRepository.LoginByPhone(dto);

            if (existAccount == null) throw new EntityNotFoundException("User Name Or Password Incorrect");

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
    }
}
