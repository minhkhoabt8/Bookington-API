using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using System;
using System.Data;
using System.Globalization;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class CourtService : ICourtService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;

        public CourtService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
        }

        public async Task<CourtReadDTO> CreateAsync(CourtWriteDTO dto)
        {
            
            var court = _mapper.Map<Court>(dto);

            await _unitOfWork.CourtRepository.AddAsync(court);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CourtReadDTO>(court);
        }

        public async Task DeleteAsync(string id)
        {
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existCourt?.OwnerId != _userContextService.AccountID.ToString()) throw new ForbiddenException();

            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            existCourt.IsDeleted = true;

            _unitOfWork.CourtRepository.Update(existCourt);

            await _unitOfWork.CommitAsync();
        }

        public async Task<PaginatedResponse<CourtReadDTO>> GetAllCourtsByOwnerIdAsync(CourtOfOwnerQuery query)
        {

            var ownerId = _userContextService.AccountID.ToString();

            if (ownerId == null) throw new ForbiddenException();

            var courts = await _unitOfWork.CourtRepository.GetAllCourtsByOwnerIdAsync(ownerId);            

            return PaginatedResponse<CourtReadDTO>.FromEnumerableWithMapping(
                courts, query, _mapper);
        }

        public async Task<CourtReadDTO> GetByIdAsync(string id)
        {
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id, include: "District");

            if (existCourt?.OwnerId != _userContextService.AccountID.ToString()) throw new ForbiddenException();

            else if (existCourt == null || existCourt.IsDeleted) throw new EntityWithIDNotFoundException<Court>(existCourt!.Id);

            var result = _mapper.Map<CourtReadDTO>(existCourt);

            result.NumberOfSubCourt = await _unitOfWork.SubCourtRepository.GetNumberOfSubCourts(existCourt.Id);

            // TODO: Needs rating of courts
            
            result.MoneyPerHour = await _unitOfWork.SlotRepository.GetTheLowestSlotPriceOfACourt(existCourt.Id);

            return result;
        }

        public async Task<CourtReadDTO> UpdateAsync(string id, CourtWriteDTO dto)
        {

            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existCourt?.OwnerId != _userContextService.AccountID.ToString()) throw new ForbiddenException();


            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            _mapper.Map(dto, existCourt);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CourtReadDTO>(existCourt);

        }

        public async Task<PaginatedResponse<CourtQueryResponse>> QueryCourtsAsync(CourtItemQuery query)
        {
            var courts = await _unitOfWork.CourtRepository.QueryAsync(query);

            var result = PaginatedResponse<CourtQueryResponse>.FromEnumerableWithMapping(
                courts, query, _mapper);
            
            foreach (var court in result.ToList())
            {
                // Remove any courts found after query with no sub courts
                court.NumberOfSubCourt = await _unitOfWork.SubCourtRepository.GetNumberOfSubCourts(court.Id);

                if (court.NumberOfSubCourt == 0)
                    result.Remove(court);

                // TODO: Needs rating of courts

                // Get the lowest slot's price of each court
                court.MoneyPerHour = await _unitOfWork.SlotRepository.GetTheLowestSlotPriceOfACourt(court.Id);

                if (court.MoneyPerHour == (double) 0)
                    result.Remove(court);
            }

            return result;
        }

    }
}
