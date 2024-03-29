﻿using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IBookingRepository :        
        IGetAllAsync<Booking>,
        IAddAsync<Booking>,
        IUpdate<Booking>,
        IFindAsync<Booking>,
        IDelete<Booking>
    {               
        Task<IEnumerable<Booking>> GetBookingsOfSubCourts(List<string> subCourtIds, bool trackChanges = false);
        Task<IEnumerable<Booking>> GetBookingsOfASubCourtAsync(string subCourtIds, bool trackChanges = false);
        Task<IEnumerable<Booking>> GetBookingsOfOrder(string orderId, bool trackChanges = false);
        Task<IEnumerable<Booking>> GetIncomingBookingsOfUser(string userId);
        Task<IEnumerable<Booking>> GetFinishedBookingsOfUser(string userId);
        Task<Booking?> GetBookingOfUserByCourtId(string userId, string courtId);
        Task<IEnumerable<Booking>> GetAllBookingOfOrderAsync(string orderId);
    }
}
