using AutoMapper;
using Azure;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.TransactionHistory;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public TransactionService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }

        public async Task<TransactionHistoryReadDTO> CreateAsync(TransactionHistoryWriteDTO dto)
        {
            var transaction = _mapper.Map<TransactionHistory>(dto);

            await _unitOfWork.TransactionHistoryRepository.AddAsync(transaction);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<TransactionHistoryReadDTO>(transaction);
        }

        public async Task<IEnumerable<TransactionHistoryReadDTO>> GetAllAsync()
        {
            var transactions = await _unitOfWork.TransactionHistoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TransactionHistoryReadDTO>>(transactions);
        }

        public async Task<TransactionHistoryReadDTO> GetByIdAsync(string id)
        {
            var existTransaction = await _unitOfWork.TransactionHistoryRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<TransactionHistory>(id);

            return _mapper.Map<TransactionHistoryReadDTO>(existTransaction);
        }

        public async Task<TransactionHistoryReadDTO> UpdateAsync(string id, TransactionHistoryWriteDTO dto)
        {
            var existTransaction = await _unitOfWork.TransactionHistoryRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<TransactionHistory>(id);

            var updatedTransaction = _mapper.Map<TransactionHistory>(dto);

            _unitOfWork.TransactionHistoryRepository.Update(updatedTransaction);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<TransactionHistoryReadDTO>(updatedTransaction);
        }

        public async Task DeleteAsync(string id)
        {
            var existTransaction = await _unitOfWork.TransactionHistoryRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<TransactionHistory>(id);

            _unitOfWork.TransactionHistoryRepository.Delete(existTransaction);

            await _unitOfWork.CommitAsync();
        }

        public async Task<string> TransferAsync(double amount, string refTo, string transferReason)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var fromBal = await _unitOfWork.UserBalanceRepository.FindByAccountIdAsync(accountId!);
            var toBal = await _unitOfWork.UserBalanceRepository.FindByAccountIdAsync(refTo);

            // Check if the person user is transfering to is valid
            if (toBal == null) throw new EntityWithIDNotFoundException<UserBalance>(refTo);

            // Check if user's balance is enough to transact
            if (amount > fromBal!.Balance) throw new AccountNotHavingEnoughBalance();

            fromBal.Balance -= amount;
            toBal.Balance += amount;

            // Proceed to update balances
            _unitOfWork.UserBalanceRepository.Update(fromBal);
            _unitOfWork.UserBalanceRepository.Update(toBal);

            // Proceed to create a transaction history
            var newTrans = new TransactionHistory()
            {
                RefFrom = fromBal.RefUser,
                RefTo = toBal.RefUser,
                Amount = amount
                //Reason = transferReason
            };
            await _unitOfWork.TransactionHistoryRepository.AddAsync(newTrans);

            // Commit to database
            await _unitOfWork.CommitAsync();

            return newTrans.Id;
        }

        public async Task<IEnumerable<TransactionHistoryReadDTO>> GetSelfTransactionHistory(int page)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Get customer's transaction history
            // 10 RECORDS EACH PAGE

            // default all wrong page num to be 1
            if (page < 1) page = 1;

            var trans = await _unitOfWork.TransactionHistoryRepository.GetTransactionHistoryOfCustomer(accountId!, page);

            return _mapper.Map<IEnumerable<TransactionHistoryReadDTO>>(trans);
        }
    }
}
