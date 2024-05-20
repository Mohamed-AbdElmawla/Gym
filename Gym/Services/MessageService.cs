using Gym.Data;
using Gym.Models;
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
        Task MarkAsReadAsync(int messageId);
    }
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            // Implement notification logic here
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
            return await _context.Messages.Where(m => m.ReceiverId == userId || m.SenderId == userId).Include(x=>x.Sender).Include(x=>x.Receiver).ToListAsync();
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null) throw new Exception("Message not found");

            message.IsRead = true;
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();
        }
    }
}
