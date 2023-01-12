using AutoMapper;
using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class AccountService : IAccountService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
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

        //public async Task<AccountLoginOutputDTO> LoginWithPhoneNumber(AccountLoginInputDTO dto)
        //{
        //    var existAccount = await _unitOfWork.AccountRepository.GetUserUsernameAndPass(dto);


        //    var accessToken = GetAccessToken(existAccount);
        //    return new AccountLoginOutputDTO
        //    {
        //        SysToken = accessToken.Access_token,
        //        SysTokenExpires = accessToken.Expires_in,
        //    };
        //}

        

    }
}
