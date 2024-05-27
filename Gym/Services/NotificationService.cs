using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Services
{
    public interface INotificationService
    {
        Task SendEnrollmentAccepted(string receiverId, string senderId);
        Task SendEnrollmentRejected(string receiverId, string senderId, string message);
        Task SendMessageNotification(string receiverId, string senderId);
        int CountUnReadNotifications(string userId);

    }

    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int CountUnReadNotifications(string userId)
        {
            var notificationCount = _context.Notifications.Count(n => n.ReceiverId == userId && !n.IsRead);
            return notificationCount;
        }

        public async Task SendEnrollmentAccepted(string receiverId, string senderId)
        {
            try
            {
                var messageBody = "Your Enrollment as a coach has been Accepted.\nNow you can add your subscription plan";
                var notification = new Notification { SenderId = senderId, ReceiverId = receiverId, MessageBody = messageBody, IsRead = false };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending enrollment accepted notification: {ex.Message}");
            }
        }
        public async Task SendMessageNotification(string receiverId, string senderId)
        {
            try
            {
                var sender = _userManager.FindByIdAsync(senderId).Result;
                var messageBody = $"{sender.FirstName} {sender.LastName} sent message";
                var notification = new Notification { SenderId = senderId, ReceiverId = receiverId, MessageBody = messageBody, IsRead = false };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending Message notification: {ex.Message}");
            }
        }

        public async Task SendEnrollmentRejected(string receiverId, string senderId, string message)
        {
            try
            {
                var messageBody = $"Your Enrollment as a coach has been Rejected.\nThe feedback:\n{message}.";
                var notification = new Notification { SenderId = senderId, ReceiverId = receiverId, MessageBody = messageBody, IsRead = false };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending enrollment rejected notification: {ex.Message}");
            }
        }
    }
}
