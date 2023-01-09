using AutoMapper;
using Azure;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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


    }
}
