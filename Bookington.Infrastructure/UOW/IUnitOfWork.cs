using Bookington.Infrastructure.Repositories.Interfaces;
using Microsoft.Identity.Client;
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

        public IProvinceRepository ProvinceRepository { get; }

        public IDistrictRepository DistrictRepository { get; }

        public ICourtReportRepository CourtReportRepository { get; }

        public IUserReportRepository UserReportRepository { get; }

        public IUserBalanceRepository UserBalanceRepository { get; }

        public ITransactionHistoryRepository TransactionHistoryRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public INotificationRepository NotificationRepository { get; }

        public ILoginTokenRepository LoginTokenRepository { get; }

        public ICourtReportResponseRepository CourtReportResponseRepository { get; }

        public IBanRepository BanRepository { get; }

        public IUserReportResponseRepository UserReportResponseRepository { get; }
        Task<int> CommitAsync();
    }
}
