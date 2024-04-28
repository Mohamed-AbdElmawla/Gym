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
            string id = _userManager.GetUserId(User);
            var trainingPlans = await _context.trainingPlans.Where(x => x.UserId == id).ToListAsync();
            return View(trainingPlans);
        }
        public IActionResult Create()
        {
            if (HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan) == default)
            {
                CreatingTrainingPlaneViewModel temp = new CreatingTrainingPlaneViewModel();
                HttpContext.Session.SetObject(SessionKeyPlan, temp);
            }
            return View(HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan));
        }
        
    }
}
