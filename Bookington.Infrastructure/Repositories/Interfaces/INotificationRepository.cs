using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface INotificationRepository :
        IAddAsync<Notification>,
        IDelete<Notification>,
        IFindAsync<Notification>,
        IQueryAsync<Notification, NotificationQuerry>
    {
        
    }
}
