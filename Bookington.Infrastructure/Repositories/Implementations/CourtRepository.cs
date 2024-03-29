﻿using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class CourtRepository : GenericRepository<Court, BookingtonDbContext>, ICourtRepository
    {
        public CourtRepository(BookingtonDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Court>> GetAllCourtsByOwnerIdAsync(string ownerId)
        {
            return Task.FromResult(_context.Courts.Where(c => c.OwnerId == ownerId && c.IsDeleted == false).Include(c => c.District).ThenInclude(c=>c.Province).AsEnumerable());
        }

        public Task<Court?> GetCourtFromTransactionId(string transactionId)
        {
            var booking = _context.Bookings.Include(b => b.RefSubCourtNavigation).Include(b => b.RefOrderNavigation)
                                            .FirstOrDefault(b => b.RefOrderNavigation.TransactionId == transactionId);

            if (booking == null) return null!;

            return Task.FromResult(_context.Courts.SingleOrDefault(c => c.Id == booking.RefSubCourtNavigation.ParentCourtId));
        }

        public async Task<IEnumerable<Court>> QueryAsync(CourtItemQuery query, bool trackChanges = false)
        {
            IQueryable<Court> courts = _context.Courts
                .Include(c => c.Owner)
                .Include(c => c.District)
                .Include(c=>c.SubCourts)
                .Include(c=>c.Comments)
                .Include(c=>c.District.Province)
                .Include(c=>c.CourtImages)
                .Where(c=>c.IsDeleted == false && c.IsActive == true);
            
            if (!trackChanges)
            {
                courts = courts.AsNoTracking();
            }
            if (!query.SearchText.IsNullOrEmpty())
            {
                courts = courts.Where(c => c.Name.Contains(query.SearchText));
            }
            if (!query.District.IsNullOrEmpty())
            {
                courts = courts.Where(c => c.District.DistrictName.Contains(query.District));
            }
            if (!query.Province.IsNullOrEmpty())
            {
                courts = courts.Where(c => c.District.Province.ProvinceName.Contains(query.Province));
            }
            if (!query.OpenAt.IsNullOrEmpty() && query.CloseAt.IsNullOrEmpty())
            {
                var openAt = TimeSpan.Parse(query.OpenAt);
                courts = courts.Where(c => c.OpenAt == openAt);
            }
            else if (!query.CloseAt.IsNullOrEmpty() && query.OpenAt.IsNullOrEmpty())
            {
                var closeAt = TimeSpan.Parse(query.CloseAt);
                courts = courts.Where(c => c.CloseAt == closeAt);
            }
            else if(!query.OpenAt.IsNullOrEmpty() && !query.CloseAt.IsNullOrEmpty())
            {

                var openAt = TimeSpan.Parse(query.OpenAt);
                var closeAt = TimeSpan.Parse(query.CloseAt);

                if(openAt <= closeAt)
                {
                    courts = courts.Where(c => c.OpenAt >= openAt && c.CloseAt <= closeAt);
                }
               
            }
            if(!query.PlayDate.IsNullOrEmpty() && !query.PlayTime.IsNullOrEmpty())
            {
                // Get all courts with active and non-deleted sub-courts
                var availableCourts = _context.Courts
                        .Include(c => c.Owner)
                        .Include(c => c.District)
                        .Include(c => c.SubCourts)
                            .ThenInclude(sc => sc.SubCourtSlots)
                            .ThenInclude(scs => scs.RefSlotNavigation)
                            .ThenInclude(r => r.Bookings)
                        .Include(c => c.Comments)
                        .Include(c => c.District.Province)
                        .Include(c => c.CourtImages)
                        .Where(c => c.IsActive && !c.IsDeleted && c.SubCourts.Any(sc => sc.IsActive && !sc.IsDeleted));

                TimeSpan startTime = TimeSpan.Parse(query.PlayTime);
                DateTime playDate = DateTime.Parse(query.PlayDate);
                // Filter out courts that have no available sub-courts with at least one unbooked slot
                courts = availableCourts
                        .Where(c => c.SubCourts.Any(sc => sc.IsActive && !sc.IsDeleted &&
                        sc.SubCourtSlots.Any(scs => scs.IsActive &&
                        scs.RefSlotNavigation.StartTime >= startTime && scs.RefSlotNavigation.Bookings.All(b => b.PlayDate != playDate))));
            }else if(!query.PlayDate.IsNullOrEmpty() && !query.PlayTime.IsNullOrEmpty() && !query.SearchText.IsNullOrEmpty())
            {
                // Get all courts with active and non-deleted sub-courts
                var availableCourts = _context.Courts
                        .Include(c => c.Owner)
                        .Include(c => c.District)
                        .Include(c => c.SubCourts)
                            .ThenInclude(sc => sc.SubCourtSlots)
                            .ThenInclude(scs => scs.RefSlotNavigation)
                            .ThenInclude(r => r.Bookings)
                        .Include(c => c.Comments)
                        .Include(c => c.District.Province)
                        .Include(c => c.CourtImages)
                        .Where(c => c.IsActive && !c.IsDeleted && c.Name.Contains(query.SearchText) && c.SubCourts.Any(sc => sc.IsActive && !sc.IsDeleted));

                TimeSpan startTime = TimeSpan.Parse(query.PlayTime);
                DateTime playDate = DateTime.Parse(query.PlayDate);
                // Filter out courts that have no available sub-courts with at least one unbooked slot
                courts = availableCourts
                        .Where(c => c.SubCourts.Any(sc => sc.IsActive && !sc.IsDeleted &&
                        sc.SubCourtSlots.Any(scs => scs.IsActive &&
                        scs.RefSlotNavigation.StartTime >= startTime && scs.RefSlotNavigation.Bookings.All(b => b.PlayDate != playDate))));
            }


            return courts;

        }

        public async Task<Court?> GetCourtFromSubCourtIdAsync(string subCourtId)
        {
            return await _context.Courts
                    .Include(c => c.SubCourts)
                    .FirstOrDefaultAsync(c => c.SubCourts.Any(sc => sc.Id == subCourtId));
        }

        public async Task<IEnumerable<Court?>> GetCourtsWithAvailableSlots(CourtQueryByDateAndTime query)
        {
            // Get all courts with active and non-deleted sub-courts
            var courts = await _context.Courts
             
                .Include(c => c.District)
                .Include(c => c.SubCourts)
                .ThenInclude(sc => sc.SubCourtSlots)
                .ThenInclude(scs => scs.RefSlotNavigation)
                .ThenInclude(r => r.Bookings)
                .Include(c => c.Comments)
                .Include(c => c.District.Province)
                .Include(c => c.CourtImages)
                .Where(c => c.IsActive && !c.IsDeleted && c.SubCourts.Any(sc => sc.IsActive && !sc.IsDeleted))
                .ToListAsync();

            TimeSpan startTime = TimeSpan.Parse(query.PlayTime);
            DateTime playDate = DateTime.Parse(query.PlayDate);
            // Filter out courts that have no available sub-courts with at least one unbooked slot
            var filteredCourts = courts
                .Where(c => c.SubCourts.Any(sc => sc.IsActive && !sc.IsDeleted &&
                    sc.SubCourtSlots.Any(scs => scs.IsActive &&
                    scs.RefSlotNavigation.StartTime >= startTime && scs.RefSlotNavigation.Bookings.All(b => b.PlayDate != playDate))));

            return filteredCourts;
        }
    }
}
