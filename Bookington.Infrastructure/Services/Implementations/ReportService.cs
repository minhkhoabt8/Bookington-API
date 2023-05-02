using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Report;
using Bookington.Infrastructure.DTOs.ReportResponse;
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
        private readonly IAccountService _accountService;
        private readonly ICourtService _courtService;
        private readonly IBanServices _banServices;

        public ReportService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService, IAccountService accountService, ICourtService courtService, IBanServices banServices)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _accountService = accountService;
            _courtService = courtService;
            _banServices = banServices;
        }

        // COURT REPORTS RELATED FUNCTIONS

        public async Task<CourtReportReadDTO> CreateCourtReportAsync(CourtReportWriteDTO dto)
        {
            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var newReport = _mapper.Map<CourtReport>(dto);

            newReport.ReporterId = (accountId ?? "");

            if (await _unitOfWork.AccountRepository.FindAsync(newReport.ReporterId) == null) throw new EntityWithIDNotFoundException<Account>(newReport.ReporterId);

            await _unitOfWork.CourtReportRepository.AddAsync(newReport);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CourtReportReadDTO>(newReport);
        }

        public async Task<IEnumerable<CourtReportReadDTO>> GetAllCourtReportsAsync()
        {
            var courtReports = await _unitOfWork.CourtReportRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<CourtReportReadDTO>>(courtReports);

            foreach (var item in result)
            {
                var court = await _courtService.GetByIdAsync(item.RefCourt);
                item.CourtName = court.Name;
                var reporter = await _accountService.GetByIdAsync(item.ReporterId);
                item.ReporterPhone = reporter.Phone;
                item.ReporterName = reporter.FullName;

                var bannedCourt = await _banServices.FindCourtBanByCourtIdAsync(item.RefCourt);

                if(bannedCourt != null)
                {
                    item.IsBan = true;
                }
            }

            return result;
        
        }

        public async Task<CourtReportReadDTO> GetCourtReportByIdAsync(string id)
        {
            var existCourtReport = await _unitOfWork.CourtReportRepository.FindAsync(id);

            if (existCourtReport == null) throw new EntityWithIDNotFoundException<CourtReport>(id);

            var result = _mapper.Map<CourtReportReadDTO>(existCourtReport);

            var court = await _courtService.GetByIdAsync(existCourtReport.RefCourt);

            result.CourtName = court.Name;

            var reporter = await _accountService.GetByIdAsync(existCourtReport.ReporterId);

            result.ReporterPhone = reporter.Phone;

            result.ReporterName = reporter.FullName;

            var bannedCourt = await _banServices.FindCourtBanByCourtIdAsync(existCourtReport.RefCourt);

            if (bannedCourt != null)
            {
                result.IsBan = true;
            }

            return result;
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

        public async Task<string> HandleCourtReportAsync(CourtReportResponseWriteDTO dto)
        {
            var existCourtReport = await _unitOfWork.CourtReportRepository.FindAsync(dto.CourtReportId);

            if (existCourtReport == null) throw new EntityWithIDNotFoundException<CourtReport>(dto.CourtReportId);
            
            if (existCourtReport.IsResponded) throw new InvalidActionException("This report has already been responded!");

            var courtBan = new Ban()
            {
                IsCourtBan = true,
                IsActive = true
            };

            var reportResponse = _mapper.Map<CourtReportResponse>(dto);

            // If the reported court is banned create a Ban record
            if (dto.IsBanned)
            {
                if (dto.Duration < 0) throw new InvalidActionException("Ban duration can not be lower than 0!");

                // Ban duration is in hours
                courtBan.Duration = dto.Duration;
                courtBan.Reason = dto.Content;
                courtBan.BanUntil = courtBan.CreateAt.AddHours(dto.Duration);
                courtBan.RefCourt = existCourtReport.RefCourt;

                await _unitOfWork.BanRepository.AddAsync(courtBan);

                // Deactivate court as well
                var existCourt = await _unitOfWork.CourtRepository.FindAsync(existCourtReport.RefCourt);

                if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(existCourtReport.RefCourt);

                existCourt.IsActive = false;

                _unitOfWork.CourtRepository.Update(existCourt);
            }

            // And record the response down as well
            await _unitOfWork.CourtReportResponseRepository.AddAsync(reportResponse);

            await _unitOfWork.CommitAsync();

            var result = "Response to Court Report ID (" + dto.CourtReportId + ") has been created successfully!";

            return result;
        }

        // USER REPORTS RELATED FUNCTIONS

        public async Task<UserReportReadDTO> CreateUserReportAsync(UserReportCreateDTO dto)
        {
            // JWT token check (TRUE to proceed)

            var accountId = _userContextService.AccountID.ToString();

            if (accountId.IsNullOrEmpty()) throw new ForbiddenException();

            var newReport = _mapper.Map<UserReport>(dto);

            newReport.ReporterId = (accountId ?? "");

            // Check if the reporter exists or not (TRUE to proceed)

            var reporter = await _unitOfWork.AccountRepository.FindAsync(newReport.ReporterId);

            if (reporter == null) throw new EntityWithIDNotFoundException<Account>(newReport.ReporterId);            

            // Proceed creating new report

            await _unitOfWork.UserReportRepository.AddAsync(newReport);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserReportReadDTO>(newReport);
        }

        public async Task<IEnumerable<UserReportReadDTO>> GetAllUserReportsAsync()
        {
            // Returning all reports

            var userReports = await _unitOfWork.UserReportRepository.GetAllAsync();

            var result =  _mapper.Map<IEnumerable<UserReportReadDTO>>(userReports);

            foreach(var userReport in result)
            {
                var account = await _accountService.GetByIdAsync(userReport.RefUser);
                userReport.RefUserName = account.FullName;
                var owner = await _accountService.GetByIdAsync(userReport.ReporterId);
                userReport.ReporterCourtName = owner.FullName;
                var bannedUser = await _banServices.FindUserBanByUserIdAsync(userReport.RefUser);
                if(bannedUser != null)
                {
                    userReport.IsBan = true;
                }
            }

            return result;
        }

        public async Task<UserReportReadDTO> GetUserReportByIdAsync(string id)
        {
            // Check if the report exists or not (TRUE to proceed)

            var existUserReport = await _unitOfWork.UserReportRepository.FindAsync(id, include: "Reporter");

            if (existUserReport == null) throw new EntityWithIDNotFoundException<UserReport>(id);

            // Proceed returning reports' info

            var result = _mapper.Map<UserReportReadDTO>(existUserReport);

            var account = await _accountService.GetByIdAsync(result.RefUser);

            result.RefUserName = account.FullName;

            var bannedUser = await _banServices.FindUserBanByUserIdAsync(result.RefUser);

            if (bannedUser != null)
            {
                result.IsBan = true;
            }

            return result;
        }

        public async Task<UserReportReadDTO> UpdateUserReportAsync(string id, UserReportUpdateDTO dto)
        {
            // Check if the report exists or not (TRUE to proceed)

            var existUserReport = await _unitOfWork.CourtReportRepository.FindAsync(id, include: "Reporter");
            
            if (existUserReport == null) throw new EntityWithIDNotFoundException<UserReport>(id);

            var updatedUserReport = _mapper.Map<UserReport>(dto);

            // Check if the reported user exists or not (TRUE to proceed)

            var reportedUser = await _unitOfWork.UserReportRepository.FindAsync(updatedUserReport.RefUser);

            if (reportedUser == null) throw new EntityWithIDNotFoundException<Account>(id);

            // Check reported user's role (Must be a "User")

            // Check if the reporter exists or not (TRUE to proceed)

            var reporter = await _unitOfWork.UserReportRepository.FindAsync(updatedUserReport.ReporterId);

            if (reporter == null) throw new EntityWithIDNotFoundException<Account>(id);

            // Check reported user's role (Must be a "Court Owner")

            // Proceed updating

            _unitOfWork.UserReportRepository.Update(updatedUserReport);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserReportReadDTO>(updatedUserReport);
        }

        public async Task DeleteUserReportAsync(string id)
        {
            // Check if the report exists or not (TRUE to proceed)

            var existUserReport = await _unitOfWork.UserReportRepository.FindAsync(id);

            if (existUserReport == null) throw new EntityWithIDNotFoundException<UserReport>(id);

            // Proceed deleting

            _unitOfWork.UserReportRepository.Delete(existUserReport);

            await _unitOfWork.CommitAsync();
        }

        public async Task<string> HandleUserReportAsync(UserReportResponseWriteDTO dto)
        {            
            var existAccountReport = await _unitOfWork.UserReportRepository.FindAsync(dto.UserReportId);

            if (existAccountReport == null) throw new EntityWithIDNotFoundException<UserReport>(dto.UserReportId);

            if (existAccountReport.IsResponded) throw new InvalidActionException("This Account Report ID (" + dto.UserReportId + ") has already been responded!");

            var userBan = new Ban()
            {
                IsAccountBan = true,
                IsActive = true
            };

            var reportResponse = _mapper.Map<UserReportResponse>(dto);

            // If the reported user is banned create a Ban record
            if (dto.IsBanned)
            {
                if (dto.Duration < 0) throw new InvalidActionException("Ban duration can not be lower than 0!");

                // Ban duration is in hours
                userBan.Duration = dto.Duration;
                userBan.Reason = dto.Content;
                userBan.BanUntil = userBan.CreateAt.AddHours(dto.Duration);
                userBan.RefCourt = existAccountReport.RefUser;

                await _unitOfWork.BanRepository.AddAsync(userBan);

                // Deactivate account as well
                var existAccount = await _unitOfWork.AccountRepository.FindAsync(existAccountReport.RefUser);

                if (existAccount == null) throw new EntityWithIDNotFoundException<Account>(existAccountReport.RefUser);

                existAccount.IsActive = false;

                _unitOfWork.AccountRepository.Update(existAccount);
            }

            // And record the response down as well
            await _unitOfWork.UserReportResponseRepository.AddAsync(reportResponse);

            await _unitOfWork.CommitAsync();

            var result = "Response to Account Report ID (" + dto.UserReportId + ") has been created successfully!";

            return result;            
        }
    }
}
