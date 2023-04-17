using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Enums;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.TransactionHistory;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

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
            var transaction = _mapper.Map<Transaction>(dto);

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

            if (existTransaction == null) throw new EntityWithIDNotFoundException<Transaction>(id);

            return _mapper.Map<TransactionHistoryReadDTO>(existTransaction);
        }

        public async Task<TransactionHistoryReadDTO> UpdateAsync(string id, TransactionHistoryWriteDTO dto)
        {
            var existTransaction = await _unitOfWork.TransactionHistoryRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<Transaction>(id);

            var updatedTransaction = _mapper.Map<Transaction>(dto);

            _unitOfWork.TransactionHistoryRepository.Update(updatedTransaction);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<TransactionHistoryReadDTO>(updatedTransaction);
        }

        public async Task DeleteAsync(string id)
        {
            var existTransaction = await _unitOfWork.TransactionHistoryRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<Transaction>(id);

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
            var newTrans = new Transaction()
            {
                RefFrom = fromBal.RefUser,
                RefTo = toBal.RefUser,
                Amount = amount,
                Reason = transferReason
            };
            await _unitOfWork.TransactionHistoryRepository.AddAsync(newTrans);

            // Other functions will commit to database             

            return newTrans.Id;
        }
                
        public async Task<PaginatedResponse<TransactionHistoryReadDTO>> GetSelfTransactionHistory(TransactionHistoryQuery query)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Get customer's transaction history                        
            var trans = await _unitOfWork.TransactionHistoryRepository.GetTransactionHistoryOfCustomer(accountId!);

            var result = _mapper.Map<IEnumerable<TransactionHistoryReadDTO>>(trans);

            foreach (var record in result)
            {
                var otherParty = new Account();

                if (accountId == record.RefFrom) record.FromUsername = "You";
                else
                {
                    otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefFrom);
                    if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefFrom);

                    // If the other party is an admin the transaction from in transaction will be "System"
                    // Else if they are an owner get their name and the court they own
                    if (otherParty.RoleId == AccountRole.admin.ToString()) record.FromUsername = "System";
                    else if (otherParty.RoleId == AccountRole.owner.ToString())
                    {
                        record.FromUsername += " (Court Owner)";                        
                    }
                }
                if (accountId == record.RefTo) record.ToUsername = "You";
                else
                {
                    otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefTo);
                    if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefFrom);

                    // If the other party is an admin the transaction to in transaction will be "System"
                    // Else if they are an owner get their name and the court they own
                    if (otherParty.RoleId == AccountRole.admin.ToString()) record.FromUsername = "System";
                    else if (otherParty.RoleId == AccountRole.owner.ToString())
                    {
                        record.FromUsername += " (Court Owner)";                         
                    }
                }
            }

            return PaginatedResponse<TransactionHistoryReadDTO>.FromEnumerableWithMapping(
                result, query, _mapper);
        }

        public async Task<TransactionHistoryReadDTO> CreateWithMomoAsync(TransactionHistoryWriteDTO dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);

            await _unitOfWork.TransactionHistoryRepository.AddAsync(transaction);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<TransactionHistoryReadDTO>(transaction);
        }


        public async Task<MomoTransactionReadDTO> CreateMomoTransactionAsync(MomoTransactionWriteDTO dto)
        {
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            //Add MomotransactionTable

            var momoTransaction = _mapper.Map<MomoTransaction>(dto);

            momoTransaction.Content = "Top Up Balance";

            momoTransaction.IsSuccessful = false;

            await _unitOfWork.MomoTransactionRepository.AddAsync(momoTransaction);

            //Add transactionTable

            var transaction = _mapper.Map<Transaction>(dto);

            transaction.RefFrom = "00000000000000000000000000000000000";

            transaction.RefTo = accountId;

            transaction.Reason = "Top Up Balance";

            transaction.RefMomoTransaction = momoTransaction.Id; // it should be its own id or Momo's order id or momo's RequestId???

            await _unitOfWork.TransactionHistoryRepository.AddAsync(transaction);

            // SaveChange DB

            await _unitOfWork.CommitAsync();

            return _mapper.Map<MomoTransactionReadDTO>(transaction);
        }

        //public async Task<PaginatedResponse<TransactionHistoryReadDTO>> GetOwnerTransactionHistory(TransactionHistoryQuery query)
        //{
        //    // Check if account is valid and is an owner
        //    var accountId = _userContextService.AccountID.ToString();

        //    if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

        //    var account = await _unitOfWork.AccountRepository.FindAsync(accountId);
        //    if (account == null) throw new EntityWithIDNotFoundException<Account>(accountId);
        //    if (account.RoleId != AccountRole.owner.ToString()) throw new ForbiddenException();

        //    // Get owner's transaction history                        
        //    var trans = await _unitOfWork.TransactionHistoryRepository.GetTransactionHistoryOfOwner(accountId);

        //    var result = _mapper.Map<IEnumerable<TransactionHistoryReadDTO>>(trans);

        //    foreach (var record in result)
        //    {
        //        var otherParty = new Account();

        //        if (accountId == record.RefFrom) record.FromUsername = "You";
        //        else
        //        {
        //            otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefFrom);
        //            if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefFrom);

        //            // If the other party is an admin the transaction from in transaction will be "System"
        //            // Else if they are an owner get their name and the court they own
        //            if (otherParty.RoleId == AccountRole.admin.ToString()) record.FromUsername = "System";
        //            else if (otherParty.RoleId == AccountRole.owner.ToString())
        //            {
        //                var court = await _unitOfWork.CourtRepository.FindAsync(otherParty.CourtId.ToString());
        //                if (court == null) throw new EntityWithIDNotFoundException<Court>(otherParty.CourtId.ToString());

        //                record.FromUsername = $"{court.Name} ({otherParty.Phone})";
        //            }
        //        }
        //        if (accountId == record.RefTo) record.ToUsername = "You";
        //        else
        //        {
        //            otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefTo);
        //            if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefFrom);

        //            // If the other party is an admin the transaction to in transaction will be "System"
        //            // Else if they are an owner get their name and the court they own
        //            if (otherParty.RoleId == AccountRole.admin.ToString()) record.ToUsername = "System";
        //            else if (otherParty.RoleId == AccountRole.owner.ToString())
        //            {
        //                var court = await _unitOfWork.CourtRepository.FindAsync(otherParty.Id.ToString());
        //                if (court == null) throw new EntityWithIDNotFoundException<Court>(otherParty.Id.ToString());

        //                record.ToUsername = $"{court.Name} ({otherParty.Phone})";
        //            }
        //        }
        //    }

        //    return PaginatedResponse<TransactionHistoryReadDTO>.FromEnumerableWithMapping(
        //        result, query, _mapper);
        //}
    }
}
