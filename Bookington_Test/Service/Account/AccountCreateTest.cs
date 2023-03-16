using Bookington.Core.Exceptions;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Core.Enums;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.UOW;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Bookington_Test.Service.Account
{
    public class AccountCreateTest
    {
        [Fact]
        public async Task CreateAsync_WithDuplicatedPhoneNumber_ThrowsUniqueConstraintException()
        {
            string phone = "0834102453";

            var existAccount = CreateTestAccount(phone);
            
            var mockUOW = new Mock<IUnitOfWork>();
            var accountService = new AccountService(null!, mockUOW.Object, null!,null!,null! ,null!);
            mockUOW.Setup(uow => uow.AccountRepository.FindAccountByPhoneNumberAsync(existAccount.Phone)).ReturnsAsync(existAccount);
            await Assert.ThrowsAsync<UniqueConstraintException<Bookington.Core.Entities.Account>>(() =>
               accountService.CreateAsync(new AccountWriteDTO { Phone = existAccount.Phone }));
        }


        private Bookington.Core.Entities.Account CreateTestAccount(string phone)
        {
            return new Bookington.Core.Entities.Account
            {
                Phone = phone,
                FullName = "Test@@",
                RoleId = AccountRole.customer.ToString(),
                CreateAt = DateTime.Now,
                Password = "Test@@"
            };
        }

        [Fact]
        public void EncryptPassword_ReturnsNonEmptyString()
        {
            string password = "password123";
            string encryptedPassword = AccountWriteDTO.EncryptPassword(password);
            Assert.NotNull(encryptedPassword);
        }

        [Fact]
        public void Password_Set_CallsEncryptPassword()
        {
            string password = "password123";
            AccountWriteDTO account = new AccountWriteDTO();
            account.Password = password;
            Assert.NotEqual(password, account.Password);
        }

    }
}
