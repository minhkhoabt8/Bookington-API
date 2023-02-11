using Bookington.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Security.Claims;

namespace Bookington.Infrastructure.Services.Implementations
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid? AccountID =>
           Guid.TryParse(
               _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.NameIdentifier)
                   ?.Value, out var id)
               ? id
               : null;   

        public string? FullName =>
            _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.Name)?.Value ?? null;

        public string? Phone => _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(cl => cl.Type == ClaimTypes.MobilePhone)?.Value ?? null;               
    }
}
