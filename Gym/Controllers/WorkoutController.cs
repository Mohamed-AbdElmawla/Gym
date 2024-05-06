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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Gym.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public const string SessionKeyPlan = "_TrainingPlan";
        [BindProperty]
        public static CreatingTrainingPlaneViewModel input { get; set; }
        public WorkoutController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.Remove(SessionKeyPlan);
            string id = _userManager.GetUserId(User);
            var trainingPlans = await _context.trainingPlans.Where(x => x.UserId == id).ToListAsync();
            return View(trainingPlans);
        }
        [HttpGet]
        public IActionResult Create()
        {
            input = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (input == default)
            {
                input = new CreatingTrainingPlaneViewModel();

                HttpContext.Session.SetObject(SessionKeyPlan, input);
            }
            return View(input);
        }
        [HttpPost]
        public IActionResult MangeRequests(int? op, int? setIndex, int? feildSetIndex)
        {
            if (op == null)
            {
                TempData["ErrorMessage"] = "Unexpected Error,please try again";
                return RedirectToAction("Create");
            }
            HttpContext.Session.SetObject(SessionKeyPlan, input);
            if (op == 0)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("SaveThePlan");
                }
            }else if(op == 1)
            {
                if(setIndex != null)
                {
                    return RedirectToAction("DeleteExercise", "Exercise", new { setIndex = setIndex });
                }
                
            }else if(op== 2)
            {
                if (setIndex != null&& feildSetIndex != null)
                {
                    return RedirectToAction("DeleteSet", "Exercise", new { setIndex = setIndex, feildSetIndex = feildSetIndex });
                }

            }else if (op == 3)
            {
                if (setIndex != null)
                {
                    return RedirectToAction("AddSet", "Exercise", new { setIndex = setIndex });
                }
            }else if (op == 4)
            {

                return RedirectToAction("AddExercise", "Exercise");
            }
                TempData["ErrorMessage"] = "Unexpected Error,please try again";
                return RedirectToAction("Create");
        }
        [HttpPost]
        public async Task<IActionResult> SaveThePlan()
        {
            input = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);
            if (input != default)
            {
                try
                {
                    string id = _userManager.GetUserId(User);
                    TrainingPlan trainingPlan = new TrainingPlan
                    {
                        UserId = id,
                        Name = input.Name,
                        Date = DateTime.Now
                    };
                    foreach (var set in input.Sets)
                    {
                        set.TrainingId = trainingPlan.Id;
                    }
                    await _context.trainingPlans.AddAsync(trainingPlan);
                    await _context.sets.AddRangeAsync(input.Sets);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "The plan saved successfully.";
                    HttpContext.Session.Remove(SessionKeyPlan);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = "Failed to save the training plan. Please try again.";
                    RedirectToAction("Create");
                }
            }
            TempData["ErrorMessage"] = "No training plan found. Please create a new plan.";
            return RedirectToAction("Create");
        }
    }
}
