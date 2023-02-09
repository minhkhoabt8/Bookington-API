using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.Enums;


namespace Bookington.Infrastructure.DTOs.Account
{
    public class AccountQuery : PaginatedQuery
    {
        public string? SearchField { get; set; } = "";
        public RoleEnum? Role { get; set; } = RoleEnum.Customer;
        public bool? isActive { get; set; } = false;
    }
}
