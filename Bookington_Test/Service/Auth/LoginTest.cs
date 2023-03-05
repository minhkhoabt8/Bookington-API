using AutoMapper;
using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Enums;
using Bookington.Infrastructure.Mapper;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Bookington_Test.Service.Auth
{
    public class LoginTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly AccountService _accountService;


        public LoginTest()
        {
            var configuration = new ConfigurationBuilder().Build();
            var smsServiceMock = new Mock<ISmsService>();
            var userBalanceServiceMock = new Mock<IUserBalanceService>();
            var userContextServiceMock = new Mock<IUserContextService>();

            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTokenService = new Mock<ITokenService>();
            _accountService = new AccountService(
                mapper: null,
                unitOfWork: _mockUnitOfWork.Object,
                tokenService: _mockTokenService.Object,
                smsService: null,
                userBalanceService: null,
                userContextService: null);
        }

        


        [Fact]
        public async Task LoginWithPhoneNumber_WhenPhoneNumberAndPasswordIncorrect_ThrowsEntityNotFoundException()
        {
            // Arrange
            var inputDto = new AccountLoginInputDTO
            {
                Phone = "1234567890",
                Password = "wrong_password"
            };
            _mockUnitOfWork.Setup(uow => uow.AccountRepository.LoginByPhoneAsync(inputDto))
                .ReturnsAsync((Bookington.Core.Entities.Account)null);

            // Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _accountService.LoginWithPhoneNumber(inputDto));
        }

        [Fact]
        public async Task LoginWithPhoneNumber_WhenAccountNotVerified_ThrowsInvalidActionException()
        {
            // Arrange
            var inputDto = new AccountLoginInputDTO
            {
                Phone = "1234567890",
                Password = "password"
            };
            var existingAccount = new Bookington.Core.Entities.Account
            {
                Id = Guid.NewGuid().ToString(),
                Phone = "1234567890",
                FullName = "John Doe",
                RoleId = ((int)RoleEnum.customer).ToString(),
                IsActive = false
            };
            _mockUnitOfWork.Setup(uow => uow.AccountRepository.LoginByPhoneAsync(inputDto))
                .ReturnsAsync(existingAccount);

            // Act and Assert
            await Assert.ThrowsAsync<InvalidActionException>(() => _accountService.LoginWithPhoneNumber(inputDto));
        }

    }
}
