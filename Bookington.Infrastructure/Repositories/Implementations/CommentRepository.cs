﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookington.Core.Entities;
using Bookington.Core.Data;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class CommentRepository : GenericRepository<Comment, BookingtonDbContext>, ICommentRepository
    {
        public CommentRepository(BookingtonDbContext context) : base(context)
        {
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
    }
}
