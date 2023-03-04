using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Slot;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class SlotService : ISlotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SlotService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int id)
        {
            var existSlot = await _unitOfWork.SlotRepository.FindAsync(id);

            if (existSlot == null) throw new EntityWithIDNotFoundException<Slot>(id);

            _unitOfWork.SlotRepository.Delete(existSlot);

            await _unitOfWork.CommitAsync();
        }

        public async Task<SlotReadDTO> CreateAsync(SlotWriteDTO dto)
        {
            var newSlot = _mapper.Map<Slot>(dto);

            await _unitOfWork.SlotRepository.AddAsync(newSlot);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<SlotReadDTO>(newSlot);
        }

        public async Task<IEnumerable<SlotReadDTO>> GetAllAsync()
        {
            var slots = await _unitOfWork.SlotRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SlotReadDTO>>(slots);
        }

        public async Task<SlotReadDTO> GetByIdAsync(string id)
        {
            var existSlot = await _unitOfWork.SlotRepository.FindAsync(id);

            if (existSlot == null) throw new EntityWithIDNotFoundException<Slot>(id);

            return _mapper.Map<SlotReadDTO>(existSlot);
        }

        public async Task<SlotReadDTO> UpdateAsync(int id, SlotWriteDTO dto)
        {
            var existSlot = await _unitOfWork.SlotRepository.FindAsync(id);

            if (existSlot == null) throw new EntityWithIDNotFoundException<Slot>(id);

            _mapper.Map(dto, existSlot);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<SlotReadDTO>(existSlot);
        }

        public async Task<SlotsForBookingReadDTO> GetAvailableSlotsForBooking(SlotQueryForBooking dto)
        {
            // Check if current sub court exists
            var currSubCourt = await _unitOfWork.SubCourtRepository.FindAsync(dto.SubCourtId);

            if (currSubCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(dto.SubCourtId);                       

            // Check if current sub court is active
            if (currSubCourt.IsDeleted || !currSubCourt.IsActive) throw new Exception("Sub Court " + currSubCourt.Name + "is not active right now!");

            // Check if play date provided by user is valid or not
            // Play date must not be any day before today
            if (dto.PlayDate.ToDateTime(TimeOnly.MaxValue).CompareTo(DateTime.Now) <= 0) throw new Exception("Play Date " + dto.PlayDate.ToString() + " is not valid!");

            // Get Available Slots along with their statuses
            var slots = await _unitOfWork.SlotRepository.GetAvailableSlotsForBooking(dto);

            var slotsForBooking = _mapper.Map<IEnumerable<SlotForBookingReadDTO>>(slots);

            var result = new SlotsForBookingReadDTO
            {
                PlayDate = dto.PlayDate,
                Slots = slotsForBooking.ToList()
            };

            return result;
        }
    }
}
