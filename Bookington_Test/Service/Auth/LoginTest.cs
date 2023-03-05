using Bookington.Core.Entities;
using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.Enums;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington.Infrastructure.UOW;
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
        public async Task LoginWithPhoneNumber_WhenPhoneNumberAndPasswordCorrect_ReturnsAccountLoginOutputDTO()
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
                IsActive = true
            };
            var role = new Role
            {
                Id = ((int)RoleEnum.customer).ToString(),
                RoleName = "customer"
            };
            _mockUnitOfWork.Setup(uow => uow.AccountRepository.LoginByPhoneAsync(inputDto))
                .ReturnsAsync(existingAccount);
            _mockUnitOfWork.Setup(uow => uow.RoleRepository.FindAsync(existingAccount.RoleId))
                .ReturnsAsync(role);
            _mockTokenService.Setup(ts => ts.GenerateTokenAsync(existingAccount))
                .ReturnsAsync("generated_token");

            // Act
            var result = await _accountService.LoginWithPhoneNumber(inputDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingAccount.Id, result.UserID);
            Assert.Equal(existingAccount.Phone, result.PhoneNumber);
            Assert.Equal(existingAccount.FullName, result.FullName);
            Assert.Equal(role.RoleName, result.Role);
            Assert.Equal("generated_token", result.SysToken);
            Assert.Equal(12000, result.SysTokenExpires);
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
