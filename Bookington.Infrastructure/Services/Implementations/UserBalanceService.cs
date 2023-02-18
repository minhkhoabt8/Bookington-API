using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.UserBalance;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;


namespace Bookington.Infrastructure.Services.Implementations
{
    public class UserBalanceService : IUserBalanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public UserBalanceService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }

        public async Task<UserBalanceReadDTO> CreateAsync(UserBalanceWriteDTO dto)
        {
            var userBalance = _mapper.Map<UserBalance>(dto);

            await _unitOfWork.UserBalanceRepository.AddAsync(userBalance);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserBalanceReadDTO>(userBalance);
        }

        public async Task<IEnumerable<UserBalanceReadDTO>> GetAllAsync()
        {
            var userBalances = await _unitOfWork.UserBalanceRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserBalanceReadDTO>>(userBalances);
        }

        public async Task<UserBalanceReadDTO> GetByIdAsync(string id)
        {
            var existUserBalance = await _unitOfWork.UserBalanceRepository.FindAsync(id);

            if (existUserBalance == null) throw new EntityWithIDNotFoundException<UserBalance>(id);

            return _mapper.Map<UserBalanceReadDTO>(existUserBalance);
        }

        public async Task<UserBalanceReadDTO> GetSelfBalance()
        {
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var selfBalance = await _unitOfWork.UserBalanceRepository.FindByAccountIdAsync(accountId!);

            Debug.WriteLine("Self Balance: " + selfBalance!.ToString());

            return _mapper.Map<UserBalanceReadDTO>(selfBalance);
        }

        public async Task<UserBalanceReadDTO> AddBalanceAsync(UserBalanceWriteDTO dto)
        {
            var existUserBalance = await _unitOfWork.UserBalanceRepository.FindByAccountIdAsync(dto.RefUser);

            if (existUserBalance == null) throw new EntityWithIDNotFoundException<UserBalance>(dto.RefUser);

            var addedBalance = dto.Balance;

            var updatedBalance = existUserBalance.Balance + addedBalance;

            if (updatedBalance < 0) throw new AccountNotHavingEnoughBalance();

            existUserBalance.Balance = updatedBalance;

            _unitOfWork.UserBalanceRepository.Update(existUserBalance);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserBalanceReadDTO>(existUserBalance);
        }
    }
}
