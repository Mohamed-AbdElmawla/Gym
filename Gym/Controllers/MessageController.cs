using Gym.Models;
using Gym.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gym.View_Models;
using Microsoft.CodeAnalysis.Emit;
namespace Gym.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MessageController(IMessageService messageService, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _messageService = messageService;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
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
            if (attachment != null && attachment.Length > 0)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                var filePath = Path.Combine(uploads, attachment.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await attachment.CopyToAsync(fileStream);
                }
                attachmentPath = "/uploads/" + attachment.FileName;
            }
            await _messageService.SendMessageAsync(sender.Id, receiver.Id, content, attachmentPath);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var messages = await _messageService.GetAllUserMessagesAsync(user.Id);
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
        [HttpGet]
        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return NotFound();
            }
            var user = await _userManager.FindByEmailAsync(search);
            var curUser = await _userManager.GetUserAsync(User);
            List<UserMessageViewModel> retMessages = new List<UserMessageViewModel>();
            if (user != null && !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                retMessages = await _messageService.GetSingleUserMessagesAsync(curUser.Id, user.Id);
            }
            return View("Index", retMessages);
        }
        public async Task<IActionResult> ShowProfile(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user != null && !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("ShowProfile", "Trainee", new { userId = user.Id });
            }
            return NotFound();
        }
        public async Task<IActionResult> ShowMessages(string userEmail)
        {
            if(userEmail == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByEmailAsync(userEmail);
            var curUser = await _userManager.GetUserAsync(User);
            List<ShowTwoUsersMessages> retMessages = new List<ShowTwoUsersMessages>();
            if (user != null && !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                retMessages = await _messageService.GetTwoUserMessagesAsync(curUser.Id, user.Id);
            }
            ViewBag.CurrentUserId = curUser.Id;
            ViewBag.CurrentUserEmail = curUser.Email;
            ViewBag.CurrentUserFirstName = curUser.FirstName;
            ViewBag.CurrentUserLastName = curUser.LastName;
            ViewBag.CurrentUserImageUrl = curUser.ProfilePicturePath;

            ViewBag.OtherUserId = user?.Id;
            ViewBag.OtherUserEmail = user.Email;
            ViewBag.OtherUserFirstName = user.FirstName;
            ViewBag.OtherUserLastName = user.LastName;
            ViewBag.OtherUserImageUrl = user.ProfilePicturePath;

            return View(retMessages);
        }
    }

}
