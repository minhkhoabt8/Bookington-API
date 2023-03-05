using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Quartz;

namespace Bookington.Infrastructure.BackgroundServices
{
    public class NotificationCleanupJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;


        public NotificationCleanupJob(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var cutoffDate = DateTime.Now.AddDays(-7); // Get the cutoff date for deleting old notifications
            var notificationsToDelete = await _unitOfWork.NotificationRepository.GetAllOverDateNotification();
            foreach(var notification in notificationsToDelete)
            {
                _unitOfWork.NotificationRepository.Delete(notification);
                await _unitOfWork.CommitAsync();
            }
        }
    }
}
