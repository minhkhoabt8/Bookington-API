using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public ReportService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }

        public async Task<CourtReportReadDTO> CreateCourtReportAsync(CourtReportWriteDTO dto)
        {
            var accountId = _userContextService.AccountID.ToString();            

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var newReport = _mapper.Map<CourtReport>(dto);

            newReport.ReporterId = (accountId ?? "");                     

            if (_unitOfWork.AccountRepository.FindAsync(newReport.ReporterId) == null) throw new EntityWithIDNotFoundException<Account>(newReport.ReporterId);            

            await _unitOfWork.CourtReportRepository.AddAsync(newReport);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CourtReportReadDTO>(newReport);
        }

        public async Task<IEnumerable<CourtReportReadDTO>> GetAllCourtReportsAsync()
        {
            var courtReports = await _unitOfWork.CourtReportRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CourtReportReadDTO>>(courtReports);
        }

        public async Task<CourtReportReadDTO> GetCourtReportByIdAsync(string id)
        {
            var existCourtReport = await _unitOfWork.CourtReportRepository.FindAsync(id);

            if (existCourtReport == null) throw new EntityWithIDNotFoundException<CourtReport>(id);

            return _mapper.Map<CourtReportReadDTO>(existCourtReport);
        }

        public async Task<CourtReportReadDTO> UpdateCourtReportAsync(string id, CourtReportWriteDTO dto)
        {
            var existCourtReport = await _unitOfWork.CourtReportRepository.FindAsync(id);

            if (existCourtReport == null) throw new EntityWithIDNotFoundException<CourtReport>(id);

            var updatedCourtReport = _mapper.Map<CourtReport>(dto);

            _unitOfWork.CourtReportRepository.Update(updatedCourtReport);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CourtReportReadDTO>(updatedCourtReport);
        }

        public async Task DeleteCourtReportAsync(string id)
        {
            var existCourtReport = await _unitOfWork.CourtReportRepository.FindAsync(id);

            if (existCourtReport == null) throw new EntityWithIDNotFoundException<CourtReport>(id);                        

            _unitOfWork.CourtReportRepository.Delete(existCourtReport);

            await _unitOfWork.CommitAsync();
        }
    }
}
