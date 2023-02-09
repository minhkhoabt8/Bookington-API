 using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.District;
using Bookington.Infrastructure.DTOs.Province;
using Bookington.Infrastructure.Repositories.Interfaces;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class DistrictServices : IDistrictService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DistrictServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DistrictReadDTO> CreateAsync(DistrictWriteDTO dto)
        {
            var existDistrict = await _unitOfWork.DistrictRepository.FindAsync(dto.Id);

            if (existDistrict != null) throw new UniqueConstraintException("District", "Id", existDistrict.Id);

            var district = _mapper.Map<District>(dto);

            await _unitOfWork.DistrictRepository.AddAsync(district);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<DistrictReadDTO>(district);
        }

        public async Task<IEnumerable<DistrictReadDTO>> GetAllAsync()
        {
            var district = await _unitOfWork.DistrictRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DistrictReadDTO>>(district);
        }

        public async Task SyncDistrict()
        {
            HttpClient client = new HttpClient();

            var json = await client.GetStringAsync("https://provinces.open-api.vn/api/d/");

            List<DistrictSyncDTO> oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DistrictSyncDTO>>(json);

            foreach (var item in oMycustomclassname)
            {
                var district = new DistrictWriteDTO
                {
                    Id = item.code.ToString(),
                    DistrictName = item.name,
                    ProvinceId = item.province_code.ToString()
                };
                await CreateAsync(district);
            }
        }

        public async Task<DistrictReadDTO> UpdateAsync(string id, DistrictWriteDTO dto)
        {
            var district = await _unitOfWork.DistrictRepository.FindAsync(id);

            if (district == null) throw new EntityWithIDNotFoundException<District>(id);

            _mapper.Map(dto, district);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<DistrictReadDTO>(district);
        }
    }
}
