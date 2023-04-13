using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface IMomoPaymentService
    {
        Task<string> SendPaymentRequest(string endpoint, string postJsonString);
        Task<string> CreatePaymentRequestToMomo(decimal amount, string orderInfo, string extraData);
    }
}
