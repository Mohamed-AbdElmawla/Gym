using Gym.Data;
using Gym.Migrations;
using Gym.Models;
using Gym.View_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public interface IMessageService
    {
        Task SendMessageAsync(string senderId, string receiverId, string content, string attachmentPath);
        Task ReplyToMessageAsync(int messageId, string senderId, string content, string attachmentPath);
        Task BlockUserAsync(string blockerId, string blockedId);
        Task<List<Message>> GetUserMessagesAsync(string userId);
        Task<List<UserMessageViewModel>> GetAllUserMessagesAsync(string userId);
        Task<List<UserMessageViewModel>> GetSingleUserMessagesAsync(string curUserId, string userId);
        Task<List<ShowTwoUsersMessages>> GetTwoUserMessagesAsync(string curUserId, string userId);
        Task MarkAsReadAsync(int messageId);
    }
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;
        public MessageService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, INotificationService notificationService)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task SendMessageAsync(string senderId, string receiverId, string content, string attachmentPath)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageBody = content,
                AttachmentPath = attachmentPath,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            await _notificationService.SendMessageNotification(receiverId, senderId);
        }

        public async Task ReplyToMessageAsync(int messageId, string senderId, string content, string attachmentPath)
        {
            var originalMessage = await _context.Messages.FindAsync(messageId);
            if (originalMessage == null) throw new Exception("Original message not found");

            await SendMessageAsync(senderId, originalMessage.SenderId, content, attachmentPath);
        }

        public async Task BlockUserAsync(string blockerId, string blockedId)
        {
            var userBlock = new UserBlock
            {
                BlockerId = blockerId,
                BlockedId = blockedId
            };

            _context.UserBlocks.Add(userBlock);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Message>> GetUserMessagesAsync(string userId)
        {
            return await _context.Messages.Where(m => m.ReceiverId == userId || m.SenderId == userId).OrderBy(x=>x.SentDate).ToListAsync();
        }
        public async Task<List<UserMessageViewModel>> GetAllUserMessagesAsync(string userId)
        {
            var messages= await _context.Messages.Where(m => m.ReceiverId == userId || m.SenderId == userId).OrderBy(x => x.SentDate).Include(x => x.Sender).Include(x => x.Receiver).ToListAsync();
            var groupedMessages = messages
             .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
             .ToDictionary(
                 g => g.Key,
                 g => g.OrderBy(m => m.SentDate).ToList()
             );
            List<UserMessageViewModel> retMessages = new();
            foreach(var item in groupedMessages)
            {
                int Unreaded =  item.Value.Count(x=>x.IsRead == false && userId == x.ReceiverId);
                var user = await _userManager.FindByIdAsync(item.Key);
                retMessages.Add(new UserMessageViewModel
                {
                    Email = user.Email,
                    UnrededMessages = Unreaded,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ImageUrl = user.ProfilePicturePath
                });
            }
            return retMessages;
        }
        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null) throw new Exception("Message not found");

            message.IsRead = true;
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserMessageViewModel>> GetSingleUserMessagesAsync(string curUserId,string userId)
        {
            
            var messages = await _context.Messages.Where(m => (m.ReceiverId == userId && m.SenderId == curUserId)||(m.ReceiverId == curUserId && m.SenderId == userId))
                .OrderBy(x => x.SentDate).Include(x => x.Sender).Include(x => x.Receiver).ToListAsync();
            var groupedMessages = messages
             .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
             .ToDictionary(
                 g => g.Key,
                 g => g.OrderBy(m => m.SentDate).ToList()
             );
            List<UserMessageViewModel> retMessages = new();
            foreach (var item in groupedMessages)
            {
                int Unreaded = item.Value.Count(x => x.IsRead == false && curUserId == x.ReceiverId);
                var user = await _userManager.FindByIdAsync(item.Key);
                retMessages.Add(new UserMessageViewModel
                {
                    Email = user.Email,
                    UnrededMessages = Unreaded,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ImageUrl = user.ProfilePicturePath
                });
            }
            if(retMessages.Count == 0) {
                var user = await _userManager.FindByIdAsync(userId);
                retMessages.Add(new UserMessageViewModel
                {
                    Email = user.Email,
                    UnrededMessages = 0,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ImageUrl = user.ProfilePicturePath
                });
            }
            return retMessages;
        }

        public async Task<List<ShowTwoUsersMessages>> GetTwoUserMessagesAsync(string curUserId, string userId)
        {

            var messages = await _context.Messages.Where(m => (m.ReceiverId == userId && m.SenderId == curUserId) || (m.ReceiverId == curUserId && m.SenderId == userId))
            .OrderBy(x => x.SentDate).Include(x => x.Sender).ToListAsync();
            List<ShowTwoUsersMessages> retMessages = new();
            foreach (var message in messages)
            {
                if(message.Receiver.Id == curUserId)
                {
                    await MarkAsReadAsync(message.Id);
                }
                retMessages.Add(new ShowTwoUsersMessages
                {
                    AttachmentPath = message.AttachmentPath,
                    MessageBody = message.MessageBody,
                    IsRead = message.IsRead,
                    SentDate = message.SentDate,
                    IsCurUser = curUserId == message.Sender.Id
                });
            }
            return retMessages;
        }
    }
}
