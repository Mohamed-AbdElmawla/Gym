using Gym.Models;
using Gym.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessageController(IMessageService messageService, UserManager<ApplicationUser> userManager)
        {
            _messageService = messageService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string receiverEmail, string content, IFormFile attachment)
        {
            var sender = await _userManager.GetUserAsync(User);
            var receiver = await _userManager.FindByEmailAsync(receiverEmail);
            if (receiver == null)
            {
                ModelState.AddModelError("", "Receiver not found");
                return View("Index", await _messageService.GetUserMessagesAsync(sender.Id));
            }

            if (string.IsNullOrEmpty(content))
            {
                ModelState.AddModelError("Content", "Message content is required");
                return View("Index", await _messageService.GetUserMessagesAsync(sender.Id));
            }

            string attachmentPath = null;
            if (attachment != null)
            {
                // Save attachment logic here
            }

            await _messageService.SendMessageAsync(sender.Id, receiver.Id, content, attachmentPath);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reply(int messageId, string content, IFormFile attachment)
        {
            var sender = await _userManager.GetUserAsync(User);

            if (string.IsNullOrEmpty(content))
            {
                ModelState.AddModelError("Content", "Reply content is required");
                return View("Index", await _messageService.GetUserMessagesAsync(sender.Id));
            }

            string attachmentPath = null;
            if (attachment != null)
            {
                // Save attachment logic here
            }

            await _messageService.ReplyToMessageAsync(messageId, sender.Id, content, attachmentPath);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var messages = await _messageService.GetUserMessagesAsync(user.Id);
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> BlockUser(string blockedEmail)
        {
            var blocker = await _userManager.GetUserAsync(User);
            var blocked = await _userManager.FindByEmailAsync(blockedEmail);
            if (blocked == null)
            {
                ModelState.AddModelError("", "User to block not found");
                return View("Index", await _messageService.GetUserMessagesAsync(blocker.Id));
            }

            await _messageService.BlockUserAsync(blocker.Id, blocked.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int messageId)
        {
            await _messageService.MarkAsReadAsync(messageId);
            return RedirectToAction("Index");
        }
    }

}
