using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Core.Enums;


namespace Bookington.Infrastructure.DTOs.Account
{
    public class AccountQuery : PaginatedQuery
    {
        public string? SearchField { get; set; } = "";
        public AccountRole? Role { get; set; } = AccountRole.customer;
        public bool? isActive { get; set; } = false;
    }
}
