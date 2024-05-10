using Microsoft.AspNetCore.Mvc;
using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Gym.View_Models;
using System.Linq;
using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
namespace Gym.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public const string SessionKeyPlan = "_TrainingPlan";
        public WorkoutController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.Remove(SessionKeyPlan);
            string id = _userManager.GetUserId(User);
            var trainingPlans = await _context.TrainingPlans.Where(x => x.UserId == id).ToListAsync();
            return View(trainingPlans);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                temp = new CreatingTrainingPlaneViewModel();

                HttpContext.Session.SetObject(SessionKeyPlan, temp);
            }
            return View(temp);
        }
        [HttpPost]
        public async Task<IActionResult> SaveThePlan()
        {
            var temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                temp = new CreatingTrainingPlaneViewModel();

                HttpContext.Session.SetObject(SessionKeyPlan, temp);
            }
            if (temp.Name == null)
            {
                TempData["ErrorMessage"] = "You should enter the name of the plan";

                return RedirectToAction("Create");
            }
            try
            {
                string id = _userManager.GetUserId(User);
                TrainingPlan trainingPlan = new TrainingPlan
                {
                    UserId = id,
                    Name = temp.Name,
                    Date = DateTime.Now
                };
                await _context.TrainingPlans.AddAsync(trainingPlan);
                await _context.SaveChangesAsync();
                foreach (var set in temp.Sets)
                {
                    set.TrainingId = trainingPlan.Id;
                }
                await _context.Sets.AddRangeAsync(temp.Sets);
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove(SessionKeyPlan);
            }catch (Exception e)
            {
                await Console.Out.WriteLineAsync("will thats fuccked shit");
            }
            return RedirectToAction("Index");
        }
    }
}
