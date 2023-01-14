using Bookington.Core.Data;
using Bookington.Infrastructure.Repositories.Interfaces;
using System;

namespace Bookington.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingtonDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<string, object> _singletonRepositories;

        public UnitOfWork(BookingtonDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _singletonRepositories = new Dictionary<string, object>();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        //Register Repository to Unit Of Work
        public IAccountRepository AccountRepository => GetSingletonRepository<IAccountRepository>();
        public ICourtRepository CourtRepository => GetSingletonRepository<ICourtRepository>();
        public IRoleRepository RoleRepository => GetSingletonRepository<IRoleRepository>();
        public IBookingRepository BookingRepository => GetSingletonRepository<IBookingRepository>();
        public ISubCourtRepository SubCourtRepository => GetSingletonRepository<ISubCourtRepository>();
        public ISlotRepository SlotRepository => GetSingletonRepository<ISlotRepository>();
        public IVoucherRepository VoucherRepository => GetSingletonRepository<IVoucherRepository>();
        public ICommentRepository CommentRepository => GetSingletonRepository<ICommentRepository>();
        private T GetSingletonRepository<T>()
        {
            if (!_singletonRepositories.ContainsKey(typeof(T).Name))
            {
                _singletonRepositories[typeof(T).Name] =
                    _serviceProvider.GetService(typeof(T)) ?? throw new InvalidOperationException();
            }

            return (T)_singletonRepositories[typeof(T).Name];
        }
    }
}
