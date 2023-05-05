using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.UOW;
using Bookington.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class SubCourtService : ISubCourtService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SubCourtService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SubCourtReadDTO> CreateAsync(SubCourtWriteDTO dto)
        {
            var subCourt = _mapper.Map<SubCourt>(dto);

            await _unitOfWork.SubCourtRepository.AddAsync(subCourt);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<SubCourtReadDTO>(subCourt);
        }

        public async Task<IEnumerable<SubCourtReadDTO>> GetAllAsync()
        {
            var subCourts = await _unitOfWork.SubCourtRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<SubCourtReadDTO>>(subCourts);
        }

        public async Task<SubCourtReadDTO> GetByIdAsync(string id)
        {
            var existSubCourt = await _unitOfWork.SubCourtRepository.FindAsync(id);

            if (existSubCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(id);

            return _mapper.Map<SubCourtReadDTO>(existSubCourt);
        }

        public async Task<SubCourtReadDTO> UpdateAsync(string id, SubCourtWriteDTO dto)
        {
            var existSubCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existSubCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            var updatedSubCourt = _mapper.Map<SubCourt>(dto);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<SubCourtReadDTO>(updatedSubCourt);
        }

        public async Task DeleteAsync(string id)
        {
            var existSubCourt = await _unitOfWork.SubCourtRepository.FindAsync(id);

            if (existSubCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(id);
            // not actually "deleted" just set some flags
            existSubCourt.IsActive = false;
            existSubCourt.IsDeleted = true;

            _unitOfWork.SubCourtRepository.Update(existSubCourt);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<SubCourtReadDTO>> CreateSubCourtFromListAsync(List<SubCourtWriteDTO> dto)
        {
            var list = new List<SubCourt>();
            foreach (var subCourtWriteDTO in dto)
            {
                var subCourt = _mapper.Map<SubCourt>(subCourtWriteDTO);

                list.Add(subCourt);

                await _unitOfWork.SubCourtRepository.AddAsync(subCourt);

                await _unitOfWork.CommitAsync();
            }
            return _mapper.Map<IEnumerable<SubCourtReadDTO>>(list);
        }

        public async Task<PaginatedResponse<SubCourtReadDTO>> GetSubCourtsOfACourt(SubCourtQuery query)
        {
            var subCourts = await _unitOfWork.SubCourtRepository.QuerySubCourtOfCourt(query);

            return PaginatedResponse<SubCourtReadDTO>.FromEnumerableWithMapping(
                subCourts, query, _mapper);
        }

        public async Task<IEnumerable<SubCourtForBookingReadDTO>> GetSubCourtsForBooking(SubCourtQueryForBooking dto)
        {
            var currCourt = await _unitOfWork.CourtRepository.FindAsync(dto.CourtId);
            if (currCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(dto.CourtId);

            // Check if play date provided by user is valid or not
            // Play date must not be any day before today
            if (dto.PlayDate.ToDateTime(TimeOnly.MaxValue).CompareTo(DateTime.Now) <= 0) throw new Exception("Play Date " + dto.PlayDate.ToString() + " is not valid!");

            // Check start time and end time condition
            // End Time > Start Time (always)
            if (dto.EndTime.CompareTo(dto.StartTime) <= 0) throw new Exception("End time must be greater than start time!");

            var scs = await _unitOfWork.SubCourtRepository.GetSubCourtsForBooking(dto);
            
            var subCourts = _mapper.Map<IEnumerable<SubCourtForBookingReadDTO>>(scs);            
        
            return subCourts;            
        }
    }
}
