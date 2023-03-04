using Bookington.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Bookington_Api.Authorizers
{
    /// <summary>
    /// Authorization for user roles
    /// </summary>
    public class RoleAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// Concatenate those roles in enum to a string
        /// </summary>
        public RoleAuthorize(params AccountRole[] allowedRoles)
        {
            var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(AccountRole), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
