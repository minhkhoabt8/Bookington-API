using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.CheckOut;
using Bookington.Infrastructure.DTOs.Order;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.DTOs.UserBalance;
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
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionService _transactionService;
        private readonly IUserContextService _userContextService;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, ITransactionService transactionService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
            _userContextService = userContextService;
        }

        public async Task<OrderReadDTO> GetByIdAsync(string id)
        {
            var existOrder = await _unitOfWork.OrderRepository.FindAsync(id);

            if (existOrder == null) throw new EntityWithIDNotFoundException<Order>(id);

            var result = _mapper.Map<OrderReadDTO>(existOrder);

            result.Bookings = await _unitOfWork.BookingRepository.GetBookingsOfOrder(id);

            return result;
        }

        public async Task<IEnumerable<OrderReadDTO>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<OrderReadDTO>>(orders);
        }

        // PHO FIX THIS PLZ
        // TODO: NEEDS VOUCHER CHECK        
        public async Task<string> CheckOutAsync(CheckOutWriteDTO dto)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Check if order existed
            var existOrder = await _unitOfWork.OrderRepository.FindAsync(dto.OrderId);

            if (existOrder == null) throw new EntityWithIDNotFoundException<Order>(dto.OrderId);            

            // Check if it's user's order
            var currOrder = _mapper.Map<OrderReadDTO>(existOrder);

            currOrder.Bookings = await _unitOfWork.BookingRepository.GetBookingsOfOrder(dto.OrderId);

            if (currOrder.Bookings.ElementAt(0).BookBy != accountId) throw new Exception("This is not your order to check out!");

            // Check if voucher there is a voucher in request

            bool hasUsedVoucher = true;

            if (!dto.VoucherCode.IsNullOrEmpty()) hasUsedVoucher = false;

            var existVoucher = new Voucher() { Discount = 0 };            

            // Check if voucher used exists
            if (hasUsedVoucher)
            {
                existVoucher = await _unitOfWork.VoucherRepository.FindByCode(dto.VoucherCode);

                if (existVoucher == null) throw new EntityWithIDNotFoundException<Voucher>(dto.VoucherCode);
            }

            // Some other checks involving voucher here ...            
            // Like usages
            // Or Date based

            var courtOwner = await _unitOfWork.SlotRepository.GetCourtOwnerBySlotId(currOrder.Bookings.ElementAt(0).RefSlot);

            if (courtOwner == null) throw new Exception("Can't find owner id!");

            var courtName = await _unitOfWork.SlotRepository.GetCourtNameBySlotId(currOrder.Bookings.ElementAt(0).RefSlot);

            if (courtName == null) throw new Exception("Can't find court name!");

            // Reason for transaction history
            var reason = "Payment for booking order id " + dto.OrderId + " to (Court Owner: " + courtOwner.FullName + " - " + courtOwner.Phone + ") of (Court: " + courtName + ")";

            // Proceed to complete transaction and commit to database
            var transId = await _transactionService.TransferAsync(currOrder.TotalPrice, courtOwner.Id, reason);

            // And of course you have to update the order with the transaction and new price if user uses voucher
            existOrder.TransactionId = transId;
            existOrder.TotalPrice = existOrder.TotalPrice * (100 - existVoucher.Discount) / 100;
            existOrder.IsPaid = true;

            if (hasUsedVoucher) existOrder.VoucherCode = dto.VoucherCode;

            _unitOfWork.OrderRepository.Update(existOrder);

            await _unitOfWork.CommitAsync();

            return reason;
        }
    }
}
