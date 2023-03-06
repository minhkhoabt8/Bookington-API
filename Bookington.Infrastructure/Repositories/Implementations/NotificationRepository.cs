using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class NotificationRepository : GenericRepository<Notification, BookingtonDbContext>, INotificationRepository
    {
        public NotificationRepository(BookingtonDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> QueryAsync(NotificationQuerry query, bool trackChanges = false)
        {

            //get list notifications order by date create and not yet read of a user
            IQueryable<Notification> notifications = _context.Notifications
                .Where(n => n.RefAccount == query.UserId)
                .OrderByDescending(n => n.IsRead == false).ThenBy(n => n.CreateAt);

            return notifications;

        }

        public async Task<IEnumerable<Notification>> GetAllOverDateNotification()
        {
            return _context.Notifications.Where(n => n.CreateAt <= DateTime.Now.AddDays(7));

        }

    }
}
