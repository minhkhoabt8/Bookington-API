using Bookington.Infrastructure.UOW;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.BackgroundServices
{
    public class AutoGenerateSlotJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;

        public AutoGenerateSlotJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();

        }

    }
}
