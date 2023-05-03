using Bookington.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookington.Infrastructure.DTOs.Notification
{
    public static class NotificationFactory
    {
        public static string SuccessBooking(string orderId) 
            => $"Congratulations! Your bookings with order Id ${orderId} has been confirmed. We look forward to seeing you soon!";
        public static string Reminder(string date, string location)
            => $"Don't forget, your court booking is coming up soon! {date} at {location}.";
        public static string RecieveNewVoucher(decimal amount, string voucherCode)
            => $"Congratulations! You have received a new voucher worth {amount:C}! Use code {voucherCode} to redeem your discount on your next purchase.";
        public static string AccountCreditNotification(double amount)
            => $"Your account has been credited with {amount:C} for court bookings. Start booking now!";
        public static string CancelledOrderNotification(string orderId, string datetime)
            => $"Important update: Your court booking with Order: {orderId} has been changed/cancelled on {datetime}. Please check your order history for more information.";

    }
}
