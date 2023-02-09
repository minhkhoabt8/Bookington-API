using Bookington.Infrastructure.DTOs.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Account
{
    public class AccountQuery : PaginatedQuery
    {
        public string? SearchField { get; set; }

    }
}
