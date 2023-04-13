using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Enums;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.CheckOut;
using Bookington.Infrastructure.DTOs.Notification;
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
        private readonly IVoucherService _voucherService;
        private readonly ITransactionService _transactionService;
        private readonly IUserContextService _userContextService;
        private readonly INotificationService _notificationService;

        private readonly int PAYMENT_DEADLINE = 5;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, ITransactionService transactionService, IUserContextService userContextService, INotificationService notificationService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _transactionService = transactionService;
            _userContextService = userContextService;
            _notificationService = notificationService;
        }
        
        public async Task<OrderReadDTO> GetByIdAsync(string id)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var account = await _unitOfWork.AccountRepository.FindAsync(accountId!);

            if (account == null) throw new ForbiddenException();

            // Check if order exists
            var existOrder = await _unitOfWork.OrderRepository.FindAsync(id);

            if (existOrder == null) throw new EntityWithIDNotFoundException<Order>(id);

            var result = _mapper.Map<OrderReadDTO>(existOrder);

            var orderCreator = await _unitOfWork.AccountRepository.FindAsync(existOrder.CreateBy!);

            if (orderCreator == null) throw new InvalidActionException("Order's data is corrupted!");

            // If the current user is a customer check if this order is theirs to proceed
            // Otherwise if they are a court owner check if this order is from one of their courts to proceed                        
            if (accountId == AccountRole.customer.ToString())
            {
                if (!_unitOfWork.OrderRepository.IsOrderYours(accountId!, id))
                    throw new InvalidActionException("This order is not yours!");
                result.CreateBy = "You";
            }            

            if (accountId == AccountRole.owner.ToString()) {
                if (!_unitOfWork.OrderRepository.IsOrderFromYourCourts(accountId!, id))
                    throw new InvalidActionException("This order is not from your court(s)!");
                result.CreateBy = orderCreator.FullName + " - " + orderCreator.Phone;
            }                     

            result.Bookings = _mapper.Map<IEnumerable<BookingForOrderReadDTO>>(await _unitOfWork.BookingRepository.GetBookingsOfOrder(id)).ToList();

            if (!result.Bookings.IsNullOrEmpty()) result.CourtName = await _unitOfWork.SubCourtRepository.GetCourtNameBySubCourtId(result.Bookings.First().Id!);

            return result;
        }

        public async Task<IEnumerable<OrderReadDTO>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<OrderReadDTO>>(orders);
        }
              
        public async Task<CheckOutResponse> CheckOutAsync(CheckOutWriteDTO dto)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Check if order existed
            var existOrder = _unitOfWork.OrderRepository.GetOrderDetailsById(dto.OrderId);

            if (existOrder == null) throw new EntityWithIDNotFoundException<Order>(dto.OrderId);            

            // Check if it's user's order                      
            if (existOrder.CreateBy != accountId) throw new InvalidActionException("This is not your order to check out!");

            // Check if order has already been paid or canceled or refunded
            if (existOrder.IsPaid || existOrder.IsCanceled || existOrder.IsRefunded)
            {
                throw new InvalidActionException("This order has already been processed!");
            }

            // Check if request for check out has passed order's payment deadline           
            if (existOrder.OrderAt.AddMinutes(PAYMENT_DEADLINE).CompareTo(DateTime.Now) < 0)
            {
                // Update order's IsCanceled to true
                existOrder.IsCanceled = true;
                _unitOfWork.OrderRepository.Update(existOrder);
                await _unitOfWork.CommitAsync();

                throw new InvalidActionException("5 minutes have passed since this order was made! You can't check out this order anymore!");
            }                

            // Check if voucher there is a voucher in request
            bool hasUsedVoucher = true;

            if (dto.VoucherCode.IsNullOrEmpty()) hasUsedVoucher = false;

            var existVoucher = new Voucher() { Discount = 0 };            

            // Check if voucher used exists and valid
            if (hasUsedVoucher)
            {
                existVoucher = await _unitOfWork.VoucherRepository.FindByCode(dto.VoucherCode);

                if (existVoucher == null) throw new EntityWithIDNotFoundException<Voucher>(dto.VoucherCode);

                var validVoucher = await CheckVoucherValidAsync(existVoucher);

                if (!validVoucher) throw new InvalidActionException("Voucher Is Not Valid");

                //update Voucher Usage

                existVoucher.Usages ++;

                _unitOfWork.VoucherRepository.Update(existVoucher);

            }

            var courtOwner = await _unitOfWork.SubCourtRepository.GetCourtOwnerBySubCourtId(existOrder.Bookings.ElementAt(0).RefSubCourt);

            if (courtOwner == null) throw new EntityNotFoundException("Can't find owner id!");

            var courtName = await _unitOfWork.SubCourtRepository.GetCourtNameBySubCourtId(existOrder.Bookings.ElementAt(0).RefSubCourt);

            if (courtName == null) throw new EntityNotFoundException("Can't find court name!");

            // Reason for transaction history
            var reason = "Payment for booking order id " + dto.OrderId + " to (Court Owner: " + courtOwner.FullName + " - " + courtOwner.Phone + ") of (Court: " + courtName + ")";

            // Update total price according to voucher discount
            existOrder.TotalPrice = existOrder.TotalPrice * (100 - existVoucher.Discount) / 100;

            // Proceed to complete transaction and commit to database
            var transId = await _transactionService.TransferAsync(existOrder.TotalPrice, courtOwner.Id, reason);

            // And of course you have to update the order with the transaction and new price if user uses voucher
            existOrder.TransactionId = transId;            
            existOrder.IsPaid = true;

            if (hasUsedVoucher) existOrder.VoucherCode = dto.VoucherCode;

            _unitOfWork.OrderRepository.Update(existOrder);

            await _unitOfWork.CommitAsync();

            //Create Notification
            var notification = new NotificationWriteDTO
            {
                RefAccount = accountId,
                Content = NotificationFactory.SuccessBooking(),
                IsRead = false
            };

            await _notificationService.CreateNotificationAsync(notification);

            return new CheckOutResponse()
            {
                TransactionId = transId,
                OrderId = existOrder.Id
            };
        }


        private async Task<bool> CheckVoucherValidAsync(Voucher voucher)
        {

            // if no voucher with the matching voucherCode was found, return false
            if (voucher == null)
            {
                return false;
            }

            // check if the voucher has expired
            if (voucher.EndDate != null && voucher.EndDate < DateTime.UtcNow)
            {
                return false;
            }

            // check if the voucher has exceeded the maximum usage or quantity limits
            if (voucher.Usages != null && voucher.MaxQuantity != null &&
                voucher.Usages >= voucher.MaxQuantity)
            {
                return false;
            }

            // check if the voucher is active
            if (voucher.IsActive != true)
            {
                return false;
            }

            // if all checks passed, return true
            return true;

        }
    }
}
