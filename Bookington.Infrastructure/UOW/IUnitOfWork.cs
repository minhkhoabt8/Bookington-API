using Bookington.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.UOW
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }

        public ICourtRepository CourtRepository { get; }
        public IOtpRepository OtpRepository { get; }

        public IRoleRepository RoleRepository { get; }

        public IBookingRepository BookingRepository { get; }

        public ISubCourtRepository SubCourtRepository { get; }

        public ISlotRepository SlotRepository { get; }

        public IVoucherRepository VoucherRepository { get; }

        public ICommentRepository CommentRepository { get; }
        Task<int> CommitAsync();
    }
}
