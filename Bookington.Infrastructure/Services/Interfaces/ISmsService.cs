using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Services.Interfaces
{
    public interface ISmsService
    {
        Task sendSmsAsync(string phones, string otp);
    }
}
