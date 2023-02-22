using Bookington.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.Repositories.Interfaces
{
    public interface IOtpRepository : IAddAsync<AccountOtp>,
        IUpdate<AccountOtp>,
        IDelete<AccountOtp>
    {
        Task<AccountOtp?> VerifyAccountAsync(string phoneNumber, string otp);

        Task<AccountOtp> FindAccountOtpByPhone(string phoneNumber);
    }
}
