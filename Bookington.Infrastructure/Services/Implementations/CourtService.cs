﻿using AutoMapper;
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
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id, include: "District");

            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(existCourt.Id);



            return _mapper.Map<CourtReadDTO>(existCourt);
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

            return PaginatedResponse<CourtQueryResponse>.FromEnumerableWithMapping(
                courts, query, _mapper);
        }
    }
}
