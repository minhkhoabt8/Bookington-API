using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Moq;
using NUnit.Framework;
using Xunit;
using Assert = Xunit.Assert;

namespace Bookington_Test.Service.Account
{
    public class AccountUpdateTest
    {

        [Fact]
        public async Task UpdateAsync_WithNonExistentAccount_ThrowsEntityWithIDNotFoundException()
        {
            // Arrange
            var id = "non-existent-id";
            var dto = new AccountUpdateDTO();

            var userContextServiceMock = new Mock<IUserContextService>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            unitOfWorkMock
                .Setup(uow => uow.AccountRepository.FindAsync(id))
                .ReturnsAsync((Bookington.Core.Entities.Account)null);

            var service = new AccountService(
                mapperMock.Object,
                unitOfWorkMock.Object,
                null!,
                null!,
                null!,
                userContextServiceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<EntityWithIDNotFoundException<Bookington.Core.Entities.Account>>(
                () => service.UpdateAsync(id, dto));
        }


        [Fact]
        public async Task UpdateAsync_WithUnauthorizedAccount_ThrowsForbiddenException()
        {
            // Arrange
            var id = "1";
            var dto = new AccountUpdateDTO();

            var userContextServiceMock = new Mock<IUserContextService>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var existingAccount = new Bookington.Core.Entities.Account { Id = "existing-id-but-different" };

            unitOfWorkMock
                .Setup(uow => uow.AccountRepository.FindAsync(id))
                .ReturnsAsync(existingAccount);

            userContextServiceMock
                .Setup(ucs => ucs.AccountID)
                .Returns(Guid.NewGuid());

            var service = new AccountService(
                 mapperMock.Object,
                 unitOfWorkMock.Object,
                 null!,
                 null!,
                 null!,
                 userContextServiceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ForbiddenException>(
                () => service.UpdateAsync(id, dto));
        }

        [Fact]
        public async Task UpdateAsync_WithAuthorizedAccount_UpdatesAccountAndReturnsReadDTO()
        {
            // Arrange
            var id = "fe9e33fb-cbe5-423a-a6cd-b3bbe27f47e3";
            var dto = new AccountUpdateDTO
            {
                FullName = "KhoaLe",
                DateOfBirth="24/06/2001"
            };

            var userContextServiceMock = new Mock<IUserContextService>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();

            var existingAccount = new Bookington.Core.Entities.Account { Id = id };
            var readDto = new AccountReadDTO();

            unitOfWorkMock
                .Setup(uow => uow.AccountRepository.FindAsync(id))
                .ReturnsAsync(existingAccount);

            userContextServiceMock
                .Setup(ucs => ucs.AccountID)
                .Returns(Guid.Parse(id));

            mapperMock
                .Setup(m => m.Map(dto, existingAccount))
                .Callback((AccountUpdateDTO updateDto, Bookington.Core.Entities.Account account) => {
                // Perform updates to the existing account based on the updateDto
                
                });

            mapperMock
                .Setup(m => m.Map<AccountReadDTO>(existingAccount))
                .Returns(readDto);

            var service = new AccountService(
                 mapperMock.Object,
                 unitOfWorkMock.Object,
                 null!,
                 null!,
                 null!,
                 userContextServiceMock.Object);

            // Act
            var result = await service.UpdateAsync(id, dto);

            // Assert
            unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once());
            mapperMock.Verify(m => m.Map(dto, existingAccount), Times.Once());
            mapperMock.Verify(m => m.Map<AccountReadDTO>(existingAccount), Times.Once());
            Assert.Equal(readDto, result);
        }

    }
}
