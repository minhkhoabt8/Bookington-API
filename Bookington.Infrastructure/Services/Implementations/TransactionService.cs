using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Enums;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Momo;
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
        private readonly IUserBalanceService _userBalanceService;

        public TransactionService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService, IUserBalanceService userBalanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _userBalanceService = userBalanceService;
        }

        public async Task<TransactionHistoryReadDTO> CreateAsync(TransactionHistoryWriteDTO dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);

            await _unitOfWork.TransactionRepository.AddAsync(transaction);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<TransactionHistoryReadDTO>(transaction);
        }

        public async Task<IEnumerable<TransactionHistoryReadDTO>> GetAllAsync()
        {
            var transactions = await _unitOfWork.TransactionRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TransactionHistoryReadDTO>>(transactions);
        }

        public async Task<TransactionHistoryReadDTO> GetByIdAsync(string id)
        {
            var existTransaction = await _unitOfWork.TransactionRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<Transaction>(id);

            return _mapper.Map<TransactionHistoryReadDTO>(existTransaction);
        }

        public async Task<TransactionHistoryReadDTO> UpdateAsync(string id, TransactionHistoryWriteDTO dto)
        {
            var existTransaction = await _unitOfWork.TransactionRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<Transaction>(id);

            var updatedTransaction = _mapper.Map<Transaction>(dto);

            _unitOfWork.TransactionRepository.Update(updatedTransaction);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<TransactionHistoryReadDTO>(updatedTransaction);
        }

        public async Task DeleteAsync(string id)
        {
            var existTransaction = await _unitOfWork.TransactionRepository.FindAsync(id);

            if (existTransaction == null) throw new EntityWithIDNotFoundException<Transaction>(id);

            _unitOfWork.TransactionRepository.Delete(existTransaction);

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
            await _unitOfWork.TransactionRepository.AddAsync(newTrans);

            // Other functions will commit to database             

            return newTrans.Id;
        }


        public async Task TransferForAdminAsync(double amount, string refFrom, string courtName, string orderId)
        {
            var transferReason = $"Order charged for {orderId} with amount: {amount}, reference from: {refFrom} with court name: {courtName}, from date time:{DateTime.Now}.";

            //get Admin account balance

            var adminBalance = await _unitOfWork.UserBalanceRepository.FindAdminAccountBalance();

            if (adminBalance == null) throw new EntityWithIDNotFoundException<UserBalance>(nameof(adminBalance.Id));

            adminBalance.Balance += amount;

            _unitOfWork.UserBalanceRepository.Update(adminBalance);

            // Proceed to create a transaction history for admin
            var newTrans = new Transaction()
            {
                RefFrom = refFrom,
                RefTo = adminBalance.RefUser,
                Amount = amount,
                Reason = transferReason
            };
            await _unitOfWork.TransactionRepository.AddAsync(newTrans);

            await _unitOfWork.CommitAsync();
            // Other functions will commit to database             
        }


        public async Task<PaginatedResponse<TransactionHistoryReadDTO>> GetSelfTransactionHistory(TransactionHistoryQuery query)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Get customer's transaction history                        
            var trans = await _unitOfWork.TransactionRepository.GetTransactionHistoryOfUser(accountId!);

            var result = _mapper.Map<IEnumerable<TransactionHistoryReadDTO>>(trans);

            int currRecord = 0;

            foreach (var record in result)
            {
                var otherParty = new Account();

                if (accountId == record.RefFrom)
                {
                    record.FromUsername = "You";

                    otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefFrom);
                    if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefFrom);

                    // If the other party is an admin the transaction from in transaction will be "System"
                    // Else if they are an owner get their name and the court they own
                    if (int.Parse(otherParty.RoleId) == (int) AccountRole.admin) record.ToUsername = "System";
                    else if (int.Parse(otherParty.RoleId) == (int) AccountRole.owner)
                    {
                        var court = await _unitOfWork.CourtRepository.GetCourtFromTransactionId(record.Id);
                        if (court == null) throw new EntityNotFoundException("There is something wrong with transaction " + record.Id);

                        record.ToUsername = "Court Owner of " + court.Name + " - " + otherParty.Phone;
                    }
                }
                else if (accountId == record.RefTo)
                {
                    record.ToUsername = "You";

                    otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefFrom);
                    if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefFrom);

                    // If the other party is an admin the transaction from in transaction will be "System"
                    // Else if they are an owner get their name and the court they own
                    if (int.Parse(otherParty.RoleId) == (int)AccountRole.admin) record.FromUsername = "System";
                    else if (int.Parse(otherParty.RoleId) == (int)AccountRole.owner)
                    {
                        var court = await _unitOfWork.CourtRepository.GetCourtFromTransactionId(record.Id);
                        if (court == null) throw new EntityNotFoundException("There is something wrong with transaction " + record.Id);

                        record.FromUsername = "Court Owner of " + court.Name + " - " + otherParty.Phone;
                    }
                }     
                
                var order = trans.ElementAt(currRecord).Orders.FirstOrDefault();

                if (order != null) record.OrderId = order.Id;

                currRecord++;
            }

            return PaginatedResponse<TransactionHistoryReadDTO>.FromEnumerableWithMapping(
                result, query, _mapper);
        }

        public async Task<TransactionHistoryReadDTO> CreateWithMomoAsync(TransactionHistoryWriteDTO dto)
        {
            var transaction = _mapper.Map<Transaction>(dto);

            await _unitOfWork.TransactionRepository.AddAsync(transaction);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<TransactionHistoryReadDTO>(transaction);
        }

        //TransactionId is userID
        public async Task<MomoTransactionReadDTO> CreateMomoTransactionAsync(MomoTransactionWriteDTO dto)
        {
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            //Add MomotransactionTable

            var momoTransaction = _mapper.Map<MomoTransaction>(dto);

            momoTransaction.Content = "Top Up Balance";

            momoTransaction.IsSuccessful = false;
            //Id is userID
            momoTransaction.Id = dto.OrderId;

            await _unitOfWork.MomoTransactionRepository.AddAsync(momoTransaction);

            //Add transactionTable

            var transaction = _mapper.Map<Transaction>(dto);

            transaction.RefFrom = "00000000000000000000000000000000000";

            transaction.RefTo = accountId;

            transaction.Reason = "Top Up Balance";

            transaction.RefMomoTransaction = dto.OrderId; // it should be its own id or Momo's order id or momo's RequestId???

            await _unitOfWork.TransactionRepository.AddAsync(transaction);

            // SaveChange DB

            await _unitOfWork.CommitAsync();

            return _mapper.Map<MomoTransactionReadDTO>(transaction);
        }


        public async Task<MomoTransactionReadDTO> ConfirmTopUp(MomoCheckoutResponseDTO dto)
        {
            //1.FindMomoTransaction by dto.OrderId vs Id
            var existMomoTransaction = await _unitOfWork.MomoTransactionRepository.FindAsync(dto.OrderId);

            if (existMomoTransaction == null) throw new EntityWithIDNotFoundException<MomoTransaction>(dto.OrderId);

            existMomoTransaction.IsSuccessful = true;

            //2.Update Status to True
            _unitOfWork.MomoTransactionRepository.Update(existMomoTransaction);

            //3.Get User Account Balance from OrderId of Transaction

            var transaction = await _unitOfWork.TransactionRepository.GetTransactionHstoryByMomoOrderId(existMomoTransaction.Id);

            if (transaction == null) throw new EntityWithIDNotFoundException<Transaction>(existMomoTransaction.Id);

            var selfBalance = await _unitOfWork.UserBalanceRepository.FindByAccountIdAsync(transaction.RefTo);

            if (selfBalance == null) throw new EntityWithIDNotFoundException<UserBalance>(transaction.RefTo);
            //4.Update Balance

            selfBalance.Balance += existMomoTransaction.Amount;

            _unitOfWork.UserBalanceRepository.Update(selfBalance);

            //5.Save Changes

            await _unitOfWork.CommitAsync();

            return _mapper.Map<MomoTransactionReadDTO>(existMomoTransaction);
        }


        public async Task<PaginatedResponse<TransactionHistoryReadDTO>> GetOwnerTransactionHistory(TransactionHistoryQuery query)
        {
            // Check if account is valid and is an owner
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var account = await _unitOfWork.AccountRepository.FindAsync(accountId!);
            if (account == null) throw new EntityWithIDNotFoundException<Account>(accountId!);            

            // Get owner's transaction history                        
            var trans = await _unitOfWork.TransactionRepository.GetTransactionHistoryOfUser(accountId!);

            var result = _mapper.Map<IEnumerable<TransactionHistoryReadDTO>>(trans);

            int currRecord = 0;

            foreach (var record in result)
            {
                var otherParty = new Account();

                // You are ref from and customer or system is ref to
                if (accountId == record.RefFrom)
                {
                    otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefTo);
                    if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefTo);

                    // If the other party is an admin the transaction from in transaction will be "System"
                    // Else if they are an owner get their name and the court they own
                    if (int.Parse(otherParty.RoleId) == (int)AccountRole.admin)
                    {
                        record.FromUsername = "You";
                        record.ToUsername = "System";
                    }
                    else if (int.Parse(otherParty.RoleId) == (int)AccountRole.customer)
                    {
                        var court = await _unitOfWork.CourtRepository.GetCourtFromTransactionId(record.Id);
                        if (court == null) throw new EntityNotFoundException("There is something wrong with transaction" + record.Id);

                        record.FromUsername = "You - " + court.Name;
                        record.ToUsername = otherParty.FullName + " - " + otherParty.Phone;                        
                    }
                }
                // You are ref to and customer or system is ref from
                else
                {
                    otherParty = await _unitOfWork.AccountRepository.FindAsync(record.RefFrom);
                    if (otherParty == null) throw new EntityWithIDNotFoundException<Account>(record.RefFrom);

                    // If the other party is an admin the transaction from in transaction will be "System"
                    // Else if they are an owner get their name and the court they own
                    if (int.Parse(otherParty.RoleId) == (int)AccountRole.admin)
                    {
                        record.FromUsername = "System";
                        record.ToUsername = "You";
                    }
                    else if (int.Parse(otherParty.RoleId) == (int)AccountRole.customer)
                    {
                        var court = await _unitOfWork.CourtRepository.GetCourtFromTransactionId(record.Id);
                        if (court == null) throw new EntityNotFoundException("There is something wrong with transaction " + record.Id);

                        record.FromUsername = otherParty.FullName + " - " + otherParty.Phone;
                        record.ToUsername = "You - " + court.Name;
                    }
                }

                var order = trans.ElementAt(currRecord).Orders.FirstOrDefault();

                if (order != null) record.OrderId = order.Id;

                currRecord++;
            }

            return PaginatedResponse<TransactionHistoryReadDTO>.FromEnumerableWithMapping(
                result, query, _mapper);
        }
    }
}
