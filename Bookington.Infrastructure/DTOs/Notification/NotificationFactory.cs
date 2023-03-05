using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Notification
{
    public static class NotificationFactory
    {
        public static string SuccessBooking(DateTime date, string location) 
            => $"Congratulations! Your booking for {date.ToString("yyyy/MM/dd")} at {location} has been confirmed. We look forward to seeing you soon!";
        public static string Reminder(string date, string location)
            => $"Don't forget, your court booking is coming up soon! {date} at {location}.";
        public static string RecieveNewVoucher(decimal amount, string voucherCode)
            => $"Congratulations! You have received a new voucher worth {amount:C}! Use code {voucherCode} to redeem your discount on your next purchase.";
        public static string CancelledNotification(DateTime date, string location)
            => $"Important update: Your court booking on {date.ToString("yyyy/MM/dd")} at {location} has been changed/cancelled. Please check your account for more information.";
        public static string AccountCreditNotification(decimal amount)
            => $"Your account has been credited with {amount:C} for court bookings. Start booking now!";
    }
}
