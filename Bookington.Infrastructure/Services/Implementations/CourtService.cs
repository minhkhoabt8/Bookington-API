using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.AspNetCore.Http;
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
        private readonly IUploadFileService _uploadFileService;

        public CourtService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService, IUploadFileService uploadFileService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _uploadFileService = uploadFileService;
        }

        public async Task<CourtReadDTO> CreateAsync(CourtWriteDTO dto, IEnumerable<IFormFile> courtImages)
        {
            
            //add court
            var court = _mapper.Map<Court>(dto);

            await _unitOfWork.CourtRepository.AddAsync(court);

            //add court image

            foreach(var item in courtImages) 
            {
                //Call Upload File Async
                var filename = await _uploadFileService.UploadFileAsyncReturnFileName(item, false);
                var courtImage = new CourtImage
                {
                    CourtId = court.Id,
                    RefImage = filename
                };
                await _unitOfWork.CourtImageRepository.AddAsync(courtImage);
            }

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

            //if (existCourt?.OwnerId != _userContextService.AccountID.ToString()) throw new ForbiddenException();

            if (existCourt == null || existCourt.IsDeleted) throw new EntityWithIDNotFoundException<Court>(existCourt!.Id);

            var result = _mapper.Map<CourtReadDTO>(existCourt);

            result.NumberOfSubCourt = await _unitOfWork.SubCourtRepository.GetNumberOfSubCourts(existCourt.Id);

            // TODO: Needs rating of courts
            
            result.MoneyPerHour = await _unitOfWork.SlotRepository.GetTheLowestSlotPriceOfACourt(existCourt.Id);

            return result;
        }

        public async Task<CourtReadDTO> UpdateAsync(string id, CourtWriteDTO dto, IEnumerable<IFormFile> courtImages)
        {

            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id);

            if (existCourt?.OwnerId != _userContextService.AccountID.ToString()) throw new ForbiddenException();


            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(id);

            //get all picture of a court
            var images = await _unitOfWork.CourtImageRepository.GetImagesOfCourtByIdAsync(existCourt.Id);

            //delete old court imgs in system
            foreach (var img in images)
            {
                //delete in local
                await _uploadFileService.DeleteFileAsync(img.RefImage, false);
                //delete in db
                _unitOfWork.CourtImageRepository.Delete(img);
            }

            //add new court imgs to system
            foreach (var image in courtImages)
            {
                //Call Upload File Async
                var filename = await _uploadFileService.UploadFileAsyncReturnFileName(image, false);
                var courtImage = new CourtImage
                {

                    CourtId = existCourt.Id,
                    RefImage = filename
                };
                await _unitOfWork.CourtImageRepository.AddAsync(courtImage);
            }
            
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
