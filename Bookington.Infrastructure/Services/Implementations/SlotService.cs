using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.Role;
using Bookington.Infrastructure.DTOs.Slot;
using Bookington.Infrastructure.DTOs.SubCourtSlot;
using Bookington.Infrastructure.Enums;
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
    public class SlotService : ISlotService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public SlotService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }        

        public async Task GenerateDefaultSlots(int slotDuration)
        {
            // Check if default slots have already been generated
            var slots = await _unitOfWork.SlotRepository.GetAllAsync();

            if (!slots.IsNullOrEmpty()) throw new InvalidActionException("Default slots have already been generated!");            

            // DaysOfWeek is a list of string that represents 7 days in a week starting from Monday -> Sunday
            List<string> daysOfWeek = new List<string>();

            foreach (var day in DayOfWeek.GetValues(typeof(DayOfWeek)))
            {
                daysOfWeek.Add(day.ToString()!);
            }

            var sunday = daysOfWeek.ElementAt(0);
            daysOfWeek.RemoveAt(0);
            daysOfWeek.Add(sunday);            

            var defaultSlots = new List<Slot>();

            var startOfTheDay = TimeOnly.MinValue;            

            for (int i = 0; i < 7; i++)
            {
                do
                {
                    var slot = new Slot();

                    slot.StartTime = startOfTheDay.ToTimeSpan();
                    startOfTheDay = startOfTheDay.AddMinutes(slotDuration);
                    slot.EndTime = startOfTheDay.ToTimeSpan();
                    slot.DaysInSchedule = daysOfWeek[i];

                    if (TimeOnly.FromTimeSpan(slot.StartTime).ToString().Contains("PM") 
                     && TimeOnly.FromTimeSpan(slot.EndTime).ToString().Contains("AM"))
                    {
                        startOfTheDay = TimeOnly.MinValue;

                        // Not enough duration for a slot so break the loop here
                        if (TimeOnly.FromTimeSpan(slot.EndTime) != TimeOnly.MinValue) break;
                        else slot.EndTime = TimeOnly.MaxValue.ToTimeSpan();
                    }

                    defaultSlots.Add(slot);
                } while (startOfTheDay.CompareTo(TimeOnly.MinValue) != 0);
            }

            foreach (var slot in defaultSlots) await _unitOfWork.SlotRepository.AddAsync(slot);

            await _unitOfWork.CommitAsync();
        }        

        public async Task<IEnumerable<SlotReadDTO>> GetAllDefaultSlotsAsync()
        {
            var slots = await _unitOfWork.SlotRepository.GetAllDefaultSlotsAsync();

            return _mapper.Map<IEnumerable<SlotReadDTO>>(slots);
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

        public async Task<string> GenerateDefaultSlotsForSubCourt(DefaultSubCourtSlotWriteDTO dto)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Price of a slot can't be lower than or equal 0
            if (dto.Price <= 0) throw new UniqueConstraintException("Price of a slot must be greater than 0!");

            // Check if sub court exists
            var currSubCourt = await _unitOfWork.SubCourtRepository.FindAsync(dto.SubCourtId);

            if (currSubCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(dto.SubCourtId);

            // Check if the current user owns the sub court
            var subCourtOwner = await _unitOfWork.SubCourtRepository.GetCourtOwnerBySubCourtId(dto.SubCourtId);

            if (subCourtOwner.Id != accountId) throw new InvalidActionException("You are not the owner of this sub court to do this action!");

            // Check if there's already slots created proceed to delete them first then add the new slots in
            if (await _unitOfWork.SubCourtSlotRepository.SubCourtHasSlot(dto.SubCourtId))
            {
                await _unitOfWork.SubCourtSlotRepository.FlushSlotsOfSubCourt(dto.SubCourtId);
                await _unitOfWork.CommitAsync();
            }

            // Create new slots for sub court
            var defaultSlotsOfSystem = await _unitOfWork.SlotRepository.GetAllDefaultSlotsAsync();

            var defaultSlotsForSubCourt = new List<SubCourtSlot>();

            var currCourt = await _unitOfWork.CourtRepository.FindAsync(currSubCourt.ParentCourtId);

            foreach (var defSlot in defaultSlotsOfSystem)
            {
                if (defSlot.StartTime.CompareTo(currCourt!.OpenAt) >= 0
                &&  defSlot.EndTime.CompareTo(currCourt!.CloseAt) <= 0)               
                {
                    var scSlot = new SubCourtSlot()
                    {
                        RefSubCourt = dto.SubCourtId,
                        RefSlot = defSlot.Id,
                        Price = dto.Price,
                        IsActive = true
                    };

                    defaultSlotsForSubCourt.Add(scSlot);
                }
            }            

            foreach (var sl in defaultSlotsForSubCourt)
            {
                await _unitOfWork.SubCourtSlotRepository.AddAsync(sl);
            }

            // Commit to database
            await _unitOfWork.CommitAsync();

            return "Slots have been generated successfully!";
        }

        public async Task<IEnumerable<SubCourtSlotScheduleReadDTO>> GetScheduleOfASubCourt(string subCourtId)
        {
            // Check if account is valid
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            // Check if the current user owns the sub court
            var subCourtOwner = await _unitOfWork.SubCourtRepository.GetCourtOwnerBySubCourtId(subCourtId);

            if (subCourtOwner.Id != accountId) throw new InvalidActionException("You are not the owner of this sub court to do this action!");

            // Get sub court's schedule
            return _mapper.Map<IEnumerable<SubCourtSlotScheduleReadDTO>>(await _unitOfWork.SubCourtSlotRepository.GetScheduleOfASubCourt(subCourtId));            
        }


        public async Task UpdateSlot(string subCourtId, SlotUpdateDTO slot)
        {
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var subCourtOwner = await _unitOfWork.SubCourtRepository.GetCourtOwnerBySubCourtId(subCourtId);

            if (subCourtOwner.Id != accountId) throw new InvalidActionException("You are not the owner of this sub court to do this action!");

            var subCourt = await _unitOfWork.SubCourtRepository.FindAsync(subCourtId);

            if (subCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(subCourtId);

            var existSlot = await _unitOfWork.SubCourtSlotRepository.FindAsync(slot.Id);

            if (existSlot == null) throw new EntityWithIDNotFoundException<SubCourtSlot>(slot.Id);

            _mapper.Map(slot, existSlot);


            await _unitOfWork.CommitAsync();

        }
    }
}
