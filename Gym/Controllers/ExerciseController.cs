using Gym.Data;
using Gym.Models;
using Gym.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public const string SessionKeyPlan = "_TrainingPlan";
        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddExercise()
        {
            var exercises = await _context.Exercises.ToListAsync();
            return View(exercises);
        }
        public async Task<IActionResult> ExerciseDetails(int ExerciseId)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == ExerciseId);
            return View(exercise);
        }
        [HttpPost]
        public async Task<IActionResult> SaveExercise(List<int> exerciseIds)
        {
            if(HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan) == default)
            {
                await Console.Out.WriteLineAsync("not gooooo");
            }
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan)
                                                  ?? new CreatingTrainingPlaneViewModel();
            var exercises = await _context.Exercises.Where(x => exerciseIds.Contains(x.Id)).ToListAsync();
            foreach (var exercise in exercises)
            {
                temp.Sets.Add(new Set { ExerciseId = exercise.Id, Exercise = exercise, Field = new List<SetAttribute>() });
            }
            HttpContext.Session.SetObject(SessionKeyPlan,temp);
            return Json(new { success = true });
        }
        public IActionResult DeleteExercise(int setIndex)
        {
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan)
                                                  ?? new CreatingTrainingPlaneViewModel();
            if (setIndex >= 0 && setIndex < temp.Sets.Count)
            {
                temp.Sets.RemoveAt(setIndex);
            }
            HttpContext.Session.SetObject(SessionKeyPlan, temp);
            return RedirectToAction("Create", "WorkOut");
        }
        public IActionResult DeleteSet(int setIndex, int feildSetIndex)
        {
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan)
                                                  ?? new CreatingTrainingPlaneViewModel();
            if (setIndex >= 0 && setIndex < temp.Sets.Count && feildSetIndex >= 0 && feildSetIndex < temp.Sets[setIndex].Field.Count)
            {
                temp.Sets[setIndex].Field.RemoveAt(feildSetIndex);
            }
            HttpContext.Session.SetObject(SessionKeyPlan, temp);
            return RedirectToAction("Create", "WorkOut");
        }
        public  IActionResult AddSet(int setIndex)
        {
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan)
                                                  ?? new CreatingTrainingPlaneViewModel();
            if (setIndex >= 0 && setIndex < temp.Sets.Count )
            {
                SetAttribute set = new SetAttribute { 
                    SetId = temp.Sets[setIndex].Id, 
                    Reps =0,
                    Weight=0
                };
                temp.Sets[setIndex].Field.Add(set);
            }
            HttpContext.Session.SetObject(SessionKeyPlan, temp);
            return RedirectToAction("Create", "WorkOut");
        }
    }
}
