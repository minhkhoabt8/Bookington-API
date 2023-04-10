using Bookington.Core.Entities;
using Bookington.Infrastructure.DTOs.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IOrderRepository :
        IGetAllAsync<Order>,
        IAddAsync<Order>,
        IUpdate<Order>,
        IFindAsync<Order>
    {
        bool IsOrderYours(string accountId, string orderId);
        bool IsOrderFromYourCourts(string accountId, string orderId);
        Order GetOrderDetailsById(string orderId);
    }
}
