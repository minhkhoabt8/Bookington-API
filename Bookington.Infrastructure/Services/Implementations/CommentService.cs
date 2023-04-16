using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Comment;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;


namespace Bookington.Infrastructure.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentReadDTO> CreateAsync(CommentWriteDTO dto)
        {
            var comment = _mapper.Map<Comment>(dto);

            await _unitOfWork.CommentRepository.AddAsync(comment);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CommentReadDTO>(comment);
        }

        public async Task DeleteAsync(string id)
        {
            var existComment = await _unitOfWork.CommentRepository.FindAsync(id);

            if (existComment == null) throw new EntityWithIDNotFoundException<Comment>(id);

            _unitOfWork.CommentRepository.Delete(existComment);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CommentReadDTO>> GetAllAsync()
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CommentReadDTO>>(comments);
        }

        public async Task<CommentReadDTO> GetByIdAsync(string id)
        {
            var existComment = await _unitOfWork.CommentRepository.FindAsync(id);

            if (existComment == null) throw new EntityWithIDNotFoundException<Comment>(id);

            return _mapper.Map<CommentReadDTO>(existComment);
        }

        public async Task<CommentReadDTO> UpdateAsync(string id, CommentWriteDTO dto)
        {
            var existComment = await _unitOfWork.CommentRepository.FindAsync(id);

            if (existComment == null) throw new EntityWithIDNotFoundException<Comment>(id);

            _mapper.Map(dto, existComment);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<CommentReadDTO>(existComment);
        }


        public async Task<double> GetAverageRatingOfCommentsOfACourtAsync(string courtId)
        {
            var result = await _unitOfWork.CommentRepository.GetAverageRatingOfCommentsOfACourtAsync(courtId);
            return result;
        }

        public async Task<int> GetReviewsNumberOfCourt(string courtId)
        {
            return await _unitOfWork.CommentRepository.GetReviewsNumberOfCourt(courtId);
        }

        public async Task<PaginatedResponse<CommentReadDTO>> GetAllCommentsOfCourt(CommentQuery query)
        {

            var comments = await _unitOfWork.CommentRepository.GetAllCommentsOfCourtAsync(query);

            return PaginatedResponse<CommentReadDTO>.FromEnumerableWithMapping(
                comments, query, _mapper);
        }

    }
}
