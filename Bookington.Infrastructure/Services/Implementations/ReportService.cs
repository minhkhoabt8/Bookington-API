using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Comment;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.DTOs.ReportType;
using Bookington.Infrastructure.DTOs.SubCourt;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReportReadDTO> CreateAsync(ReportWriteDTO dto)
        {
            var newReport = _mapper.Map<Report>(dto);

            // Check if the reporter is valid
            var existReporter = await _unitOfWork.AccountRepository.FindAsync(dto.ReporterId);

            if (existReporter == null) throw new EntityWithIDNotFoundException<Account>(dto.ReporterId);

            // Check if report type is valid
            var existReportType = await _unitOfWork.ReportTypeRepository.FindAsync(dto.TypeId);

            if (existReportType == null) throw new EntityWithIDNotFoundException<ReportType>(dto.TypeId);

            await _unitOfWork.ReportRepository.AddAsync(newReport);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReportReadDTO>(newReport);
        }

        public async Task DeleteAsync(string id)
        {
            var existReport = await _unitOfWork.ReportRepository.FindAsync(id);

            if (existReport == null) throw new EntityWithIDNotFoundException<Report>(id);

            _unitOfWork.ReportRepository.Delete(existReport);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ReportReadDTO>> GetAllAsync()
        {
            var reports = await _unitOfWork.ReportRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ReportReadDTO>>(reports);
        }

        public async Task<ReportReadDTO> GetByIdAsync(string id)
        {
            var existReport = await _unitOfWork.ReportRepository.FindAsync(id);

            if (existReport == null) throw new EntityWithIDNotFoundException<Report>(id);

            return _mapper.Map<ReportReadDTO>(existReport);
        }

        public async Task<ReportReadDTO> UpdateAsync(string id, ReportWriteDTO dto)
        {
            var existReport = await _unitOfWork.ReportRepository.FindAsync(id);

            if (existReport == null) throw new EntityWithIDNotFoundException<Report>(id);

            existReport = _mapper.Map<Report>(dto);

            _unitOfWork.ReportRepository.Update(existReport);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReportReadDTO>(existReport);
        }

        public async Task<ReportTypeReadDTO> CreateTypeAsync(ReportTypeWriteDTO dto)
        {
            var newReportType = _mapper.Map<ReportType>(dto);

            await _unitOfWork.ReportTypeRepository.AddAsync(newReportType);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReportTypeReadDTO>(newReportType);
        }

        public async Task DeleteTypeAsync(int id)
        {
            var existReportType = await _unitOfWork.ReportTypeRepository.FindAsync(id);

            if (existReportType == null) throw new EntityWithIDNotFoundException<ReportType>(id);

            _unitOfWork.ReportTypeRepository.Delete(existReportType);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ReportTypeReadDTO>> GetAllTypesAsync()
        {
            var reportTypes = await _unitOfWork.ReportTypeRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ReportTypeReadDTO>>(reportTypes);
        }

        public async Task<ReportTypeReadDTO> GetTypeByIdAsync(int id)
        {
            var existReportType = await _unitOfWork.ReportTypeRepository.FindAsync(id);

            if (existReportType == null) throw new EntityWithIDNotFoundException<ReportType>(id);

            return _mapper.Map<ReportTypeReadDTO>(existReportType);
        }

        public async Task<ReportTypeReadDTO> UpdateTypeAsync(int id, ReportTypeWriteDTO dto)
        {
            var existReportType = await _unitOfWork.ReportTypeRepository.FindAsync(id);

            if (existReportType == null) throw new EntityWithIDNotFoundException<ReportType>(id);

            _mapper.Map(dto, existReportType);

            existReportType = _mapper.Map<ReportType>(existReportType);

            _unitOfWork.ReportTypeRepository.Update(existReportType);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ReportTypeReadDTO>(existReportType);
        }
    }
}
