using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gym.Data;
using Microsoft.EntityFrameworkCore;
using Gym.Models;
using Microsoft.AspNetCore.Identity;
using Gym.Services;
using Gym.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace Gym.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            INotificationService notificationService, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _notificationService = notificationService;
            _hubContext = hubContext;
        }
        public async Task<IActionResult> Index(Models.Status? status, DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10)
        {
            IQueryable<CoachEnrollment> enrollmentsQuery = _context.CoachEnrollments;

            if (status != null)
            {
                enrollmentsQuery = enrollmentsQuery.Where(enroll => enroll.Status == status);
            }

            if (startDate != null)
            {
                enrollmentsQuery = enrollmentsQuery.Where(enroll => enroll.Date >= startDate);
            }

            if (endDate != null)
            {
                enrollmentsQuery = enrollmentsQuery.Where(enroll => enroll.Date <= endDate);
            }

            // Calculate total count for pagination
            int totalCount = await enrollmentsQuery.CountAsync();

            // Apply paging
            List<CoachEnrollment> enrollments = await enrollmentsQuery
                .OrderByDescending(enroll => enroll.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Pass necessary data to the view
            ViewBag.Status = status;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.Page = page;
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = pageSize;

            return View(enrollments);
        }
        [HttpGet]
        public async Task<IActionResult> ShowEnrollmentDetails(int enrollmentId)
        {
            var enrollment = await _context.CoachEnrollments.FindAsync(enrollmentId);

            if (enrollment == null)
            {
                return NotFound();
            }
            if (enrollment.User == null)
            {
                enrollment.User = await _userManager.FindByIdAsync(enrollment.UserId);
            }

            return View(enrollment);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEnrollmentStatus(int enrollmentId, string status, string feedback)
        {
            var enrollment =await _context.CoachEnrollments.FindAsync(enrollmentId);
            if (enrollment == null)
            {
                return NotFound();
            }
            string senderId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(senderId);
            if (enrollment.Status == Gym.Models.Status.Pending)
            {
                if (status == "Accepted")
                {
                    await _notificationService.SendEnrollmentAccepted(enrollment.UserId, senderId);
                    enrollment.Status = Gym.Models.Status.Accepted;
                }
                else if (status == "Rejected")
                {
                    await _notificationService.SendEnrollmentRejected(enrollment.UserId, senderId, feedback);
                    enrollment.Status = Gym.Models.Status.Rejected;
                    enrollment.Feedback = feedback;
                }
               await _context.SaveChangesAsync();
            }
            await _hubContext.Clients.User(enrollment.UserId).SendAsync("ReceiveNotification", $"You Have a new Notification from {user.FirstName}");
            return RedirectToAction("Index");
        }
    }
}
