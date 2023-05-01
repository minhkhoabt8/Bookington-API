using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ICommentService _commentService;

        public CourtService(IMapper mapper, IUnitOfWork unitOfWork, IUserContextService userContextService, IUploadFileService uploadFileService, ICommentService commentService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userContextService = userContextService;
            _uploadFileService = uploadFileService;
            _commentService = commentService;
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

            var owner = await _unitOfWork.AccountRepository.FindAsync(ownerId);

            var courtReads =  PaginatedResponse<CourtReadDTO>.FromEnumerableWithMapping(
                courts, query, _mapper);

            var listImages = new List<string>();

            foreach (var court in courtReads) 
            {
                court.Phone = owner.Phone;

                court.NumberOfSubCourt = await _unitOfWork.SubCourtRepository.GetNumberOfSubCourts(court.Id);

                court.RatingStar = await _commentService.GetAverageRatingOfCommentsOfACourtAsync(court.Id);

                court.NumOfReview = await _commentService.GetReviewsNumberOfCourt(court.Id);

                court.MoneyPerHour = await _unitOfWork.SlotRepository.GetTheLowestSlotPriceOfACourt(court.Id);

                //Add  image to court
                var images = await _unitOfWork.CourtImageRepository.GetImagesOfCourtByIdAsync(court.Id);

                foreach (var item in images)
                {
                    listImages.Add(item.RefImage);
                }

                court.CourtPictures = await _uploadFileService.GetImageFilesAsync(listImages, false);
            }

            return courtReads;
            
        }

        public async Task<CourtReadDTO> GetByIdAsync(string id)
        {
            var existCourt = await _unitOfWork.CourtRepository.FindAsync(id, include: "District");

            //if (existCourt?.OwnerId != _userContextService.AccountID.ToString()) throw new ForbiddenException();

            if (existCourt == null) throw new EntityWithIDNotFoundException<Court>(existCourt!.Id);

            if(existCourt.IsDeleted) throw new EntityWithIDNotFoundException<Court>(existCourt!.Id);

            //if (!existCourt.IsActive) throw new Exception($"Court with ID: {existCourt.Id} is banned from system"); 

            var result = _mapper.Map<CourtReadDTO>(existCourt);

            result.NumberOfSubCourt = await _unitOfWork.SubCourtRepository.GetNumberOfSubCourts(existCourt.Id);

            var owner = await  _unitOfWork.AccountRepository.FindAsync(existCourt.OwnerId);

            result.Phone = owner.Phone;

            result.RatingStar = await _commentService.GetAverageRatingOfCommentsOfACourtAsync(existCourt.Id);

            result.NumOfReview = await _commentService.GetReviewsNumberOfCourt(existCourt.Id);
            
            result.MoneyPerHour = await _unitOfWork.SlotRepository.GetTheLowestSlotPriceOfACourt(existCourt.Id);

            //Add  image to court
            var images = await _unitOfWork.CourtImageRepository.GetImagesOfCourtByIdAsync(existCourt.Id);

            var listImages = new List<string>();

            foreach(var item in images)
            {
                listImages.Add(item.RefImage);
            }

            result.CourtPictures = await _uploadFileService.GetImageFilesAsync(listImages, false);

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

            var listImages = new List<string>();

            foreach (var court in result.ToList())
            {
                
                // Remove any courts found after query with no sub courts
                court.NumberOfSubCourt = await _unitOfWork.SubCourtRepository.GetNumberOfSubCourts(court.Id);

                if (court.NumberOfSubCourt == 0)
                    result.Remove(court);

                court.RatingStar = await _commentService.GetAverageRatingOfCommentsOfACourtAsync(court.Id);

                court.NumOfReview = await _commentService.GetReviewsNumberOfCourt(court.Id);

                // Get the lowest slot's price of each court
                court.MoneyPerHour = await _unitOfWork.SlotRepository.GetTheLowestSlotPriceOfACourt(court.Id);

                if (court.MoneyPerHour == (double) 0)
                    result.Remove(court);

                //Add  image to court
                var images = await _unitOfWork.CourtImageRepository.GetImagesOfCourtByIdAsync(court.Id);

                foreach (var item in images)
                {
                    listImages.Add(item.RefImage);
                }

                court.CourtPictures = await _uploadFileService.GetImageFilesAsync(listImages, false);

            }

            return result;
        }

    }
}
