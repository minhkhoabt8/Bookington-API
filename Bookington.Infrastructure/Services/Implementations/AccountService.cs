﻿using AutoMapper;
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
            //auto mapper
            var account = _mapper.Map<Account>(dto);
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<AccountReadDTO>(account);
        }

        public async Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto)
        {
            var existAccount = await _unitOfWork.AccountRepository.LoginByPhone(dto);

            if (existAccount == null) throw new EntityNotFoundException("User Name Or Password Incorrect");
            //create new Token and return 
            return new AccountLoginOutputDTO
            {
                UserID = existAccount.Id,
                PhoneNumber = existAccount.Phone,
                FullName = existAccount.FullName,
                SysToken = await _tokenService.GenerateTokenAsync(existAccount),
                SysTokenExpires = 120
                //Role = existAccount.Role.RoleName
            };
        }


    }
}
