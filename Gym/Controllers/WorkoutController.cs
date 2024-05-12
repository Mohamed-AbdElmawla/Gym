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
        public WorkoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
        public async Task<IActionResult> Details(int trainingPlanId)
        {
            TrainingPlanViewModel model = new TrainingPlanViewModel();
            TrainingPlan temp = await _context.TrainingPlans.FirstOrDefaultAsync(t => t.Id == trainingPlanId);
            model.Name = temp.Name;
            model.Date = temp.Date;
            model.Sets = await _context.Sets.Where(x=>x.TrainingId == trainingPlanId).Include(x=>x.Field).Include(x=>x.Exercise).ToListAsync();
            return View(model);
        }
        public async Task<IActionResult> Delete(int trainingPlanId)
        {
            var trainingPlan = _context.TrainingPlans.Find(trainingPlanId);
            if (trainingPlan == null)
            {
                TempData["ErrorMessage"] = "The plan doesn't exits"; ;
                return RedirectToAction("Index");
            }

            try
            {
                _context.TrainingPlans.Remove(trainingPlan);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error deleting the training plan";
                return RedirectToAction("Index");
            }
            TempData["SuccessMessage"] = "Operation succeeded!";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                temp = new CreatingTrainingPlaneViewModel();

                HttpContext.Session.SetObject(SessionKeyPlan, temp);
            }
            foreach (var set in temp.Sets)
            {
                set.Exercise = await _context.Exercises.FirstOrDefaultAsync(e => e.Id == set.ExerciseId);
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

            if (input.operation == null)
            {
                TempData["ErrorMessage"] = "Unexpected error (operation is null),please try again";
                return RedirectToAction("Create");
            }
            if (input.operation == 0)
            {
                if (input.exerciseIndex != null)
                {
                    return RedirectToAction("DeleteExercise", "Exercise", new { exerciseIndex = input.exerciseIndex });
                }
            }
            else if (input.operation == 1)
            {
                if (input.exerciseIndex != null)
                {
                    return RedirectToAction("AddSet", "Exercise", new { exerciseIndex = input.exerciseIndex });
                }
            }
            else if (input.operation == 2)
            {
                if (input.exerciseIndex != null && input.setFeildIndex != null)
                {
                    return RedirectToAction("DeleteSet", "Exercise", new { exerciseIndex = input.exerciseIndex, setFeildIndex = input.setFeildIndex });
                }
            }
            else if (input.operation == 3)
            {
                return RedirectToAction("AddExercise", "Exercise");
            }
            else if (input.operation == 4)
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Name is required";
                    return RedirectToAction("Create");
                }

               return RedirectToAction("SaveThePlan");

            }
            TempData["ErrorMessage"] = "Unexpected error, please try again";
            return RedirectToAction("Create");
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
                };
                await _context.TrainingPlans.AddAsync(trainingPlan);
                await _context.SaveChangesAsync();
                foreach (var set in temp.Sets)
                {
                    Set new_set = new Set
                    {
                        TrainingId = trainingPlan.Id,
                        ExerciseId = set.ExerciseId,
                    };
                    await _context.Sets.AddAsync(new_set);
                    await _context.SaveChangesAsync();
                    foreach (var setAttribute in set.Field)
                    {
                        SetAttribute set_attribute = new SetAttribute
                        {
                            SetId = new_set.Id,
                            Reps = setAttribute.Reps,
                            Weight = setAttribute.Weight

                        };
                        await _context.SetAttributes.AddAsync(set_attribute);
                    }
                }
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove(SessionKeyPlan);
                TempData["SuccessMessage"] = "The training plan is Saved";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("Error in saving training paln");
            }
            TempData["ErrorMessage"] = "Unexpected error in saving the training plan";
            return RedirectToAction("Create");
        }
    }
}
