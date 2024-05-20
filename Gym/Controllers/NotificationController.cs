using Gym.Data;
using Gym.Migrations;
using Gym.Models;
using Gym.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Gym.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public NotificationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var TempModel = await _context.Messages.Where(x => x.ReceiverId == userId).Include(x=>x.Sender).ToListAsync();
            List<NotificationViewModel> model = new();
            foreach (var Noti in TempModel)
            {
                NotificationViewModel temp = new NotificationViewModel
                {
                    Id = Noti.Id,
                    MessageBody = Noti.MessageBody,
                    SentDate = Noti.SentDate,
                    IsRead = Noti.IsRead,
                    SenderName = Noti.Sender.FirstName
                };
                model.Add(temp);

            }
            return View(model);
        }
        [HttpPost]
        public IActionResult MarkAsReaded(int id)
        {

            var notifaction = _context.Messages.SingleOrDefault(n => n.Id == id);
            if (notifaction == null)
            {
                return NotFound();
            }

            notifaction.IsRead = true;
            _context.SaveChanges();

            return RedirectToAction("Index", "Notification");

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            var notifaction = _context.Messages.SingleOrDefault(n => n.Id == id);
            if (notifaction == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(notifaction);
            _context.SaveChanges();

            return RedirectToAction("Index", "Notification");

        }
    }
}
