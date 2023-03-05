using Bookington.Core.Entities;
using Bookington.Infrastructure.BackgroundServices;
using Bookington.Infrastructure.UOW;
using Moq;
using Xunit;

namespace Bookington_Test.BackgroundJob
{
    public class NotificationCleanupJobTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly NotificationCleanupJob _job;

        public NotificationCleanupJobTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _job = new NotificationCleanupJob(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Should_DeleteOldNotifications()
        {
            // Arrange
            var cutoffDate = DateTime.Now.AddDays(-7);
            var notificationsToDelete = new List<Notification>
            {
                new Notification { CreateAt = cutoffDate.AddDays(-1) },
                new Notification { CreateAt = cutoffDate.AddDays(-2) },
                new Notification { CreateAt = cutoffDate.AddDays(-3) },
            };
            _mockUnitOfWork.Setup(uow => uow.NotificationRepository.GetAllOverDateNotification())
                .ReturnsAsync(notificationsToDelete);

            // Act
            await _job.Execute(null);

            // Assert
            _mockUnitOfWork.Verify(uow => uow.NotificationRepository.Delete(It.IsAny<Notification>()), Times.Exactly(3));
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(), Times.Exactly(3));
        }
    }
}
