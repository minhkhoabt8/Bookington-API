﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Core.Data;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Bookington.Infrastructure.DTOs.Comment;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class CommentRepository : GenericRepository<Comment, BookingtonDbContext>, ICommentRepository
    {
        public CommentRepository(BookingtonDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsOfCourtAsync(CommentQuery query, bool trackChanges = false)
        {
            IQueryable<Comment> comments = _context.Comments.Include(c=>c.CommentWriter).Where(a => a.RefCourtNavigation.Id == query.CourtId && a.IsDeleted == false && a.IsActive == true);

            return await Task.FromResult(comments);
        }

        public async Task<double> GetAverageRatingOfCommentsOfACourtAsync(string courtId)
        {
            var comments = await _context.Comments
                .Where(c => c.RefCourt == courtId && !c.IsDeleted)
                .ToListAsync();

            if (comments.Any())
            {
                return comments.Average(c => c.Rating);
            }

            return 0;
        }

        public async Task<int> GetReviewsNumberOfCourt(string courtId)
        {
            var count = await _context.Comments
                .Where(c => c.RefCourt == courtId && !c.IsDeleted && c.Rating != 0)
                .CountAsync();
            return count;
        }
    }
}
