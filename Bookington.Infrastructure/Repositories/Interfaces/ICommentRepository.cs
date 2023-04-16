using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Comment;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface ICommentRepository :
        IGetAllAsync<Comment>,
        IAddAsync<Comment>,
        IUpdate<Comment>,
        IFindAsync<Comment>,
        IDelete<Comment>
    {
        //get the average rating of all comments
        Task<double> GetAverageRatingOfCommentsOfACourtAsync(string courtId);
        Task<int> GetReviewsNumberOfCourt(string courtId);
        Task<IEnumerable<Comment>> GetAllCommentsOfCourtAsync(CommentQuery query, bool trackChanges = false);
    }
}
