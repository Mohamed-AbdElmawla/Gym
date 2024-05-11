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
using Gym.Migrations;
namespace Gym.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        public class inputModel
        {
            public int? operation { get; set; }
            public CreatingTrainingPlaneViewModel exercises { get; set; }
            public int? exerciseIndex { get; set; }
            public int? setFeildIndex { get; set; }

        };
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public const string SessionKeyPlan = "_TrainingPlan";
        [BindProperty]
        public inputModel input { get; set; }
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
        public IActionResult ManageReqeusts()
        {
            if (input.exercises != null)
            {
                HttpContext.Session.SetObject(SessionKeyPlan, input.exercises);
            }
            
            if(input.operation==null)
            {
                TempData["ErrorMessage"] = "Unexpected error (operation is null),please try again";
                return RedirectToAction("Create");
            }
            if(input.operation == 0) {
                if(input.exerciseIndex == null)
                {
                    TempData["ErrorMessage"] = "Unexpected error (exerciseIndex is null),please try again";
                    return RedirectToAction("Create");
                }
                return RedirectToAction("DeleteExercise", "Exercise", new { exerciseIndex = input.exerciseIndex });
            }else if(input.operation == 1)
            {
                if (input.exerciseIndex == null)
                {
                    TempData["ErrorMessage"] = "Unexpected error (exerciseIndex is null),please try again";
                    return RedirectToAction("Create");
                }
                return RedirectToAction("AddSet", "Exercise", new { exerciseIndex = input.exerciseIndex });
            }else if(input.operation == 2)
            {
                if (input.exerciseIndex == null|| input.setFeildIndex == null)
                {
                    TempData["ErrorMessage"] = "Unexpected error (exerciseIndex is null or setFeildIndex is null),please try again";
                    return RedirectToAction("Create");
                }
                return RedirectToAction("DeleteSet", "Exercise", new { exerciseIndex = input.exerciseIndex, setFeildIndex = input.setFeildIndex });
            }else if(input.operation == 3)
            {
                return RedirectToAction("AddExercise", "Exercise");
            }else if(input.operation == 4)
            {
                if (ModelState.IsValid)
                {
                    RedirectToAction("SaveThePlan");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> SaveThePlan()
        {
            var temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                TempData["ErrorMessage"] = "There is no plan to save";
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
