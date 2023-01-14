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

        public async Task<SubCourtReadDTO> UpdateAsync(int id, SubCourtWriteDTO dto)
        {
            var existSubCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existSubCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            var updatedSubCourt = _mapper.Map<SubCourt>(dto);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<SubCourtReadDTO>(updatedSubCourt);
        }

        public async Task DeleteAsync(int id)
        {
            var existSubCourt = await _unitOfWork.SubCourtRepository.FindAsync(id);

            if (existSubCourt == null) throw new EntityWithIDNotFoundException<SubCourt>(id);
            // not actually "deleted" just set some flags
            existSubCourt.IsActive = false;
            existSubCourt.IsDeleted = true;

            _unitOfWork.SubCourtRepository.Update(existSubCourt);

            await _unitOfWork.CommitAsync();            
        }
    }
}
