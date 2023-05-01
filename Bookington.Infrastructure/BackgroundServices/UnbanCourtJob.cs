using Bookington.Infrastructure.Services.Interfaces;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.BackgroundServices
{
    public class UnbanCourtJob : IJob
    {
        private readonly IBanServices _banServices;

        public UnbanCourtJob(IBanServices banServices)
        {
            _banServices = banServices;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _banServices.UnBanCourtCronJobAsync();
        }
    }
}
