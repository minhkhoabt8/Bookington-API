
using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.IncomingMatch;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.IdentityModel.Tokens;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public BookingService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }

        /*//Will have to check status of voucher code and the referenced slot
        public async Task<BookingReadDTO> CreateAsync(BookingWriteDTO dto)
        {
            var newBooking = _mapper.Map<Booking>(dto);

            // Check if slot existed
            var existSlot = await _unitOfWork.SlotRepository.FindAsync(dto.RefSlot);

            if (existSlot == null) throw new EntityWithIDNotFoundException<Slot>(dto.RefSlot);

            // Then set original price to booking
            //newBooking.OriginalPrice = existSlot.Price;

            // Check if customer includes a voucher code in the booking or not
            var isVoucherUsed = dto.VoucherCode.IsNullOrEmpty();

            if (!isVoucherUsed)
            {
                var existVoucher = await _unitOfWork.VoucherRepository.FindAsync(dto.VoucherCode);

                if (existVoucher == null) throw new EntityWithIDNotFoundException<Slot>(dto.RefSlot);

                newBooking.VoucherCode = existVoucher.VoucherCode;                

                //Since a voucher is used booking's price = original price * (100 - discount from voucher)
                //newBooking.Price = existSlot.Price * (100 - existVoucher.Discount);                
            }
            else 
            {
                // No voucher used so the price customer has to pay = original price of that slot
                //newBooking.Price = newBooking.OriginalPrice;

                newBooking.VoucherCode = null;
            }            

            await _unitOfWork.BookingRepository.AddAsync(newBooking);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<BookingReadDTO>(newBooking);
        }*/

        // TODO: PHO FIX THIS PLZ
        // NEEDS CHECK FOR BOOKED SLOTS        
        public async Task<IEnumerable<BookingReadDTO>> CreateBookingsAsync(IEnumerable<BookingWriteDTO> dtos)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            if (dtos.Count() == 0) throw new InvalidActionException("You didn't choose a slot to proceed booking!");

            var playDate = DateOnly.FromDateTime(dtos.ElementAt(0).PlayDate);            

            bool isSameDayBooking = false;

            // Check if play date is valid
            if (playDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0) throw new InvalidActionException("Your play date is bullshit!");
            else if (playDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) == 0) isSameDayBooking = true;

            var newBookings = _mapper.Map<IEnumerable<Booking>>(dtos);
            
            var existSlots = new List<Slot>();

            var IsThereAnySlotBooked = false;

            var slotBookedErr = "Some slots you chose have already been booked: ";

            foreach (var dto in dtos)
            {
                // Check if slot existed
                var existSlot = await _unitOfWork.SlotRepository.FindAsync(dto.RefSlot);

                if (existSlot == null) throw new EntityWithIDNotFoundException<Slot>(dto.RefSlot);

                // Check if slot is still active
                if (!existSlot.IsActive) throw new InvalidActionException("Slot " + existSlot.StartTime + "-" + existSlot.EndTime + " is not active right now!");

                // Check for same day booking
                if (isSameDayBooking)
                {
                    var dateSlot = DateTime.Now.Date.AddMinutes(existSlot.StartTime.TotalMinutes);

                    if (DateTime.Now.CompareTo(dateSlot) > 0) throw new InvalidActionException("Slot " + existSlot.StartTime + "-" + existSlot.EndTime + " has passed the booking window!");
                }

                // Check if this slot is a booked slot                
                var isThisSlotBooked = await _unitOfWork.SlotRepository.IsSlotBooked(existSlot.Id, playDate.ToDateTime(TimeOnly.MinValue));

                if (isThisSlotBooked)
                {
                    IsThereAnySlotBooked = true;

                }

                existSlots.Add(existSlot);
            }

            // Create an order to contain bookings
            var bookTime = DateTime.Now;
            var newOrder = new Order()
            {
                OrderAt = bookTime
            };

            // Assign value to each booking                         
            int pos = 0;                        

            foreach (var booking in newBookings)
            {
                booking.RefOrder = newOrder.Id;
                booking.BookBy = accountId!;                
                booking.PlayDate = playDate.ToDateTime(TimeOnly.MinValue);
                booking.BookAt = bookTime;
                booking.Price = existSlots[pos].Price;                

                // Update order's total price
                newOrder.TotalPrice += booking.Price;
                pos++;
            }

            // Proceed to add new order to database
            await _unitOfWork.OrderRepository.AddAsync(newOrder);

            // Proceed to add new bookings to database
            foreach (var booking in newBookings)
            {
                await _unitOfWork.BookingRepository.AddAsync(booking);
            }

            // Commit to database
            await _unitOfWork.CommitAsync();

            return _mapper.Map<IEnumerable<BookingReadDTO>>(newBookings);
        }

        public async Task DeleteAsync(string id)
        {
            var existBooking = await _unitOfWork.BookingRepository.FindAsync(id);

            if (existBooking == null) throw new EntityWithIDNotFoundException<Booking>(id);

            _unitOfWork.BookingRepository.Delete(existBooking);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<BookingReadDTO>> GetAllAsync()
        {
            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<BookingReadDTO>>(bookings);
        }

        public async Task<BookingReadDTO> GetByIdAsync(string id)
        {
            var existBooking = await _unitOfWork.BookingRepository.FindAsync(id);

            if (existBooking == null) throw new EntityWithIDNotFoundException<Court>(id);

            return _mapper.Map<BookingReadDTO>(existBooking);
        }

        public async Task<BookingReadDTO> UpdateAsync(string id, BookingWriteDTO dto)
        {
            var existBooking = await _unitOfWork.BookingRepository.FindAsync(id);

            if (existBooking == null) throw new EntityWithIDNotFoundException<Booking>(id);

            var updatedBooking = _mapper.Map<Booking>(dto);

            _unitOfWork.BookingRepository.Update(updatedBooking);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<BookingReadDTO>(existBooking);
        }

        // Get the whole booking history of a designated court       
        // Not including unpaid booking
        // Order by the most recent booking
        public async Task<IEnumerable<CourtBookingHistoryReadDTO>> GetBookingsOfCourt(string courtId)
        {
            //Check if court exists
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(courtId);

            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(courtId);

            //Get all available sub courts
            var avSubCourts = await _unitOfWork.SubCourtRepository.GetAvailableSubCourtsByCourtId(courtId);

            //From each sub court get its bookings                        
            var subCourtIds = new List<string>();

            foreach (var sc in avSubCourts) subCourtIds.Add(sc.Id);

            var bookings = await _unitOfWork.BookingRepository.GetBookingsOfSubCourts(subCourtIds);

            //Forgot sub court name in database
            foreach (var b in bookings)
            {
                foreach (var sc in avSubCourts)
                {
                    if (sc.Id == b.RefSlotNavigation.RefSubCourt) { }
                    //item.SubCourtName = sc.Name + " - " + sc.CourtType.Content;                    
                }
            }

            //Don't know how to include sub court name in mapper without altering database
            //so i have to make do with this
            var result = _mapper.Map<IEnumerable<CourtBookingHistoryReadDTO>>(bookings);

            return result;
        }

        public async Task<IEnumerable<IncomingMatchReadDTO>> GetIncomingMatchesFromBookingOfUser(string userId)
        {
            if(_userContextService.AccountID.ToString() != userId) throw new ForbiddenException(userId);

            var incomingMatches = await _unitOfWork.BookingRepository.GetIncomingMatchesFromBookingOfUser(userId);

            return _mapper.Map<IEnumerable<IncomingMatchReadDTO>>(incomingMatches);
        }
    }
}
