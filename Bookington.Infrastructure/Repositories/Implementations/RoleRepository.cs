using Bookington.Core.Data;
using Bookington.Core.Entities;
using Bookington.Infrastructure.Repositories.Interfaces;

namespace Bookington.Infrastructure.Repositories.Implementations
{
    public class RoleRepository : GenericRepository<Role, BookingtonDbContext>, IRoleRepository
    {
        public RoleRepository(BookingtonDbContext context) : base(context)
        {
        }
    }
}
