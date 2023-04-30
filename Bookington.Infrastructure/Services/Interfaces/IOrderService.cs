using Bookington.Infrastructure.DTOs.CheckOut;
using Bookington.Infrastructure.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IOrderService
    {        
        Task<IEnumerable<OrderReadDTO>> GetAllAsync();
        Task<OrderReadDTO> GetByIdAsync(string id);
        Task<CheckOutResponse> CheckOutAsync(CheckOutWriteDTO dto);
        Task<OrderReadDTO> CancelOrderAsync(string orderId);
    }
}
