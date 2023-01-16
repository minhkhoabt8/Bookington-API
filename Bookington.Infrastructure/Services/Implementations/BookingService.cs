using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Booking;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.IdentityModel.Tokens;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //Will have to check status of voucher code and the referenced slot
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
        }

        // Create disregards validation
        public async Task<BookingReadDTO> DebugCreateAsync(DebugBookingWriteDTO dto)
        {
            var newBooking = _mapper.Map<Booking>(dto);

            await _unitOfWork.BookingRepository.AddAsync(newBooking);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<BookingReadDTO>(newBooking);
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

            //NOT WORKING
            /*var bookings = new List<Booking>();
            
            foreach (var sc in avSubCourts)
            {
                bookings.AddRange(await _unitOfWork.BookingRepository.GetBookingsOfSubCourt(sc.Id));
            }

            //Order by the most recent booking            
            bookings = bookings.OrderByDescending(b => b.BookAt).ToList();*/            

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

        public async Task<bool> IsCustomerAvailableForCommenting(string userId)
        {
            return await _unitOfWork.BookingRepository.IsCustomerAvailableForCommenting(userId);
        }
    }
}
