using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Province;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;


namespace Bookington.Infrastructure.Services.Implementations
{
    public class ProvinceService : IProvinceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProvinceService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProvinceReadDTO>> GetAllAsync()
        {
            var province = await _unitOfWork.ProvinceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProvinceReadDTO>>(province);

        }

        public async Task<ProvinceReadDTO> UpdateAsync(string id, ProvinceWriteDTO dto)
        {
            var existProvince = await _unitOfWork.ProvinceRepository.FindAsync(id);

            if (existProvince == null) throw new EntityWithIDNotFoundException<Province>(id);

            _mapper.Map(dto, existProvince);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ProvinceReadDTO>(existProvince);
        }

        public async Task<ProvinceReadDTO> CreateAsync(ProvinceWriteDTO dto)
        {
            var existProvince = await _unitOfWork.ProvinceRepository.FindAsync(dto.Id);

            if (existProvince != null) throw new UniqueConstraintException("Province","Id", existProvince.Id);

            var province = _mapper.Map<Province>(dto);

            await _unitOfWork.ProvinceRepository.AddAsync(province);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ProvinceReadDTO>(province);
        }

        public async Task SyncProvince()
        {
            HttpClient client = new HttpClient();

            var json = await client.GetStringAsync("https://provinces.open-api.vn/api/p/");

            List<ProvinceSyncDTO> oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProvinceSyncDTO>>(json);
            
            foreach (var item in oMycustomclassname)
            {
                var province = new ProvinceWriteDTO
                {
                    Id = item.code.ToString(),
                    ProvinceName = item.name,
                };
                await CreateAsync(province);
            }
        }
    }  
}
