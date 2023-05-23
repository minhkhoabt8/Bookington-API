using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.DTOs.IncomingBooking;
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
            
        public async Task<IEnumerable<BookingReadDTO>> CreateBookingsAsync(IEnumerable<BookingWriteDTO> dtos)
        {            
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            if (dtos.Count() == 0) throw new InvalidActionException("You didn't choose a slot to proceed booking!");

            var playDate = DateOnly.FromDateTime(dtos.ElementAt(0).PlayDate);

            bool isSameDayBooking = false;

            // Check if play date is valid
            if (playDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0) throw new InvalidActionException("Your play date is not available!");
            else if (playDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) == 0) isSameDayBooking = true;

            var newBookings = _mapper.Map<IEnumerable<Booking>>(dtos);

            var existSubCourtSlots = new List<SubCourtSlot>();

            var IsThereAnySlotBooked = false;

            var slotsBookedErr = "Some slots you chose have already been booked: ";

            foreach (var dto in dtos)
            {
                // Check if slot existed
                var existSlot = await _unitOfWork.SlotRepository.FindAsync(dto.RefSlot);

                if (existSlot == null) throw new EntityWithIDNotFoundException<Slot>(dto.RefSlot);

                // Check if sub court uses this slot
                var existSubCourtSlot = await _unitOfWork.SubCourtSlotRepository.FindAsync(dto.RefSubCourt, dto.RefSlot);

                if (existSubCourtSlot == null) throw new EntityNotFoundException("This sub court doesn't have slot (" + existSlot.StartTime + " - " + existSlot.EndTime + ")");

                // Check if slot is still active
                if (!existSubCourtSlot.IsActive) throw new InvalidActionException("Slot " + existSlot.StartTime + "-" + existSlot.EndTime + " is not active right now!");

                // Check for same day booking
                if (isSameDayBooking)
                {
                    var dateSlot = DateTime.Now.Date.AddMinutes(existSlot.StartTime.TotalMinutes);

                    if (DateTime.Now.CompareTo(dateSlot) > 0) throw new InvalidActionException("Slot " + existSlot.StartTime + "-" + existSlot.EndTime + " has passed the booking window!");
                }

                // Check if this slot is a booked slot                
                var isThisSlotBooked = await _unitOfWork.SlotRepository.IsSlotBooked(existSubCourtSlot.RefSlot, playDate.ToDateTime(TimeOnly.MinValue));

                if (isThisSlotBooked)
                {
                    if (IsThereAnySlotBooked) slotsBookedErr += " &";
                    else IsThereAnySlotBooked = true;

                    slotsBookedErr += "(" + existSlot.StartTime + " - " + existSlot.EndTime + ")";
                }

                existSubCourtSlots.Add(existSubCourtSlot);
            }

            // Throw error for booked slots
            if (IsThereAnySlotBooked) throw new InvalidActionException(slotsBookedErr);

            // Create an order to contain bookings
            var bookTime = DateTime.Now;
            var newOrder = new Order()
            {
                CreateBy = accountId!,
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
                booking.Price = existSubCourtSlots[pos].Price;                

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
        public async Task<PaginatedResponse<CourtBookingHistoryReadDTO>> GetBookingsOfCourt(BookingHistoryQuery query)
        {            
            bool isAllQuery = false;
            
            if (query.CourtId == "All") isAllQuery = true;

            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            //Check if court exists
            if (!isAllQuery)
            {
                var existCourt = await _unitOfWork.CourtRepository.FindAsync(query.CourtId);

                if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(query.CourtId);
            }

            //Get all available sub courts
            var avSubCourts = new List<SubCourt>();

            if (!isAllQuery) avSubCourts = (await _unitOfWork.SubCourtRepository.GetAvailableSubCourtsByCourtId(query.CourtId)).ToList();
            else avSubCourts = (await _unitOfWork.SubCourtRepository.GetSubCourtsOfOwner(accountId!)).ToList();

            var bookings = await _unitOfWork.BookingRepository.GetBookingsOfSubCourts(avSubCourts.Select(sc => sc.Id).ToList());

            return PaginatedResponse<CourtBookingHistoryReadDTO>.FromEnumerableWithMapping(
                bookings, query, _mapper);
        }

        public async Task<PaginatedResponse<CourtBookingHistoryReadDTO>> GetBookingsOfASubCourtAsync(GetBookingsOfSubCourtPaginatedQuery query)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();


            var existSubCourt = await _unitOfWork.SubCourtRepository.FindAsync(query.SubCourtId);

            if (existSubCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(query.SubCourtId);


            var bookings = await _unitOfWork.BookingRepository.GetBookingsOfASubCourtAsync(existSubCourt.Id);

            return PaginatedResponse<CourtBookingHistoryReadDTO>.FromEnumerableWithMapping(
                bookings, query, _mapper);

        }

        public async Task<PaginatedResponse<IncomingBookingReadDTO>> GetIncomingBookingsOfCustomer(IncomingBookingQuery query)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Get Incoming Bookings
            var incomingBookings = await _unitOfWork.BookingRepository.GetIncomingBookingsOfUser(accountId!);

            var result = PaginatedResponse<IncomingBookingReadDTO>.FromEnumerableWithMapping(
                incomingBookings, query, _mapper);

            foreach (var b in result)
            {
                if (DateTime.Now.CompareTo(b.PlayDate.Add(b.StartTime)) >= 0
                 && DateTime.Now.CompareTo(b.PlayDate.Add(b.EndTime)) <= 0)
                    b.Status = "started";
                else b.Status = "incoming";
            }

            return result;
        }

        public async Task<PaginatedResponse<FinishedBookingReadDTO>> GetFinishedBookingsOfCustomer(FinishedBookingQuery query)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Get Incoming Bookings
            var incomingBookings = await _unitOfWork.BookingRepository.GetFinishedBookingsOfUser(accountId!);

            return PaginatedResponse<FinishedBookingReadDTO>.FromEnumerableWithMapping(
                incomingBookings, query, _mapper);
        }

        public async Task<bool> CheckUserCanReportOrComment(string userId, string courtId)
        {
            var accountId = _userContextService.AccountID.ToString();
            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();
            var isValid = await _unitOfWork.BookingRepository.GetBookingOfUserByCourtId(userId, courtId);
            if (isValid == null) return false;
            return true;

        }
    }
}
