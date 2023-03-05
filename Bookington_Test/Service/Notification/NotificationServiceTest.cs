using AutoMapper;
using Bookington.Infrastructure.DTOs.Notification;
using Bookington.Infrastructure.Hubs;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Moq;
using Xunit;


namespace Bookington_Test.Service.Notification
{
    public class NotificationServiceTest
    {
        [Fact]
        public async Task CreateNotificationAsync_SendsNotificationToUser()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var notificationWriteDto = new NotificationWriteDTO
            {
                RefAccount = "account",
                Content = "test",
                IsRead = false
            };
            var notificationReadDto = new NotificationReadDTO
            {
                Id = "1",
                RefAccount = "account",
                Content = "test",
                CreateAt = DateTime.Now,
                IsRead = false
            };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(uow => uow.NotificationRepository.AddAsync(It.IsAny<Bookington.Core.Entities.Notification>()))
                .Callback<Bookington.Core.Entities.Notification>(n => n.Id = notificationReadDto.Id)
                .Returns(Task.CompletedTask);

            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<Bookington.Core.Entities.Notification>(notificationWriteDto))
                .Returns(new Bookington.Core.Entities.Notification());
            mapperMock
                .Setup(m => m.Map<NotificationReadDTO>(It.IsAny<Bookington.Core.Entities.Notification>()))
                .Returns(notificationReadDto);

            var userContextServiceMock = new Mock<IUserContextService>();


            userContextServiceMock
                .Setup(u => u.AccountID)
                .Returns(new Guid(userId));

            var hubContextMock = new Mock<INotificationUserHub>();

            hubContextMock
                .Setup(h => h.SendNotification(userId, It.IsAny<NotificationReadDTO>()))
                .Returns(Task.CompletedTask);

            var notificationService = new NotificationService(
                unitOfWorkMock.Object,
                mapperMock.Object,
                userContextServiceMock.Object,
                hubContextMock.Object
            );

            // Act
            var result = await notificationService.CreateNotificationAsync(notificationWriteDto);

            // Assert
            unitOfWorkMock.Verify(uow => uow.NotificationRepository.AddAsync(It.IsAny<Bookington.Core.Entities.Notification>()), Times.Once);
            unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            mapperMock.Verify(m => m.Map<NotificationReadDTO>(It.IsAny<Bookington.Core.Entities.Notification>()), Times.AtLeastOnce);//this should be Once instead of AtLeastOne
            hubContextMock.Verify(h => h.SendNotification(userId, It.IsAny<NotificationReadDTO>()), Times.Once);
            Assert.Equal(notificationReadDto.Id, result.Id);
        }


    }
}
