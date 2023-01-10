using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class CourtService : ICourtService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CourtService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CourtReadDTO> CreateAsync(CourtWriteDTO dto)
        {
            var court = _mapper.Map<Court>(dto);

            await _unitOfWork.CourtRepository.AddAsync(court);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CourtReadDTO>(court);
        }

        public async Task DeleteAsync(int id)
        {
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            _unitOfWork.CourtRepository.Delete(existCourt);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CourtReadDTO>> GetAllAsync()
        {
            var courts = await _unitOfWork.CourtRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CourtReadDTO>>(courts);
        }

        public async Task<CourtReadDTO> GetByIdAsync(string id)
        {
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            return _mapper.Map<CourtReadDTO>(existCourt);
        }

        public async Task<CourtReadDTO> UpdateAsync(int id, CourtWriteDTO dto)
        {
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            _mapper.Map(dto, existCourt);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CourtReadDTO>(existCourt);
        }
    }
}
