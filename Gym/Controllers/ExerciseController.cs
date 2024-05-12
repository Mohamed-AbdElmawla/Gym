using Gym.Data;
using Gym.Models;
using Gym.View_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    [Authorize]
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
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                temp = new CreatingTrainingPlaneViewModel();
            }
            var exercises = await _context.Exercises.Where(x => exerciseIds.Contains(x.Id)).ToListAsync();
            foreach (var exercise in exercises)
            {
                SetViewModel tempSet = new SetViewModel();
                tempSet.ExerciseId = exercise.Id;
                tempSet.Exercise = exercise;
                temp.Sets.Add(tempSet);
            }
            HttpContext.Session.SetObject(SessionKeyPlan,temp);
            return Json(new { success = true });
        }
        public IActionResult DeleteExercise(int exerciseIndex)
        {
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                temp = new CreatingTrainingPlaneViewModel();
            }
            if (exerciseIndex >= 0 && exerciseIndex < temp.Sets.Count)
            {
                temp.Sets.RemoveAt(exerciseIndex);
            }
            HttpContext.Session.SetObject(SessionKeyPlan, temp);
            return RedirectToAction("Create", "WorkOut");
        }
        public IActionResult DeleteSet(int exerciseIndex, int setFeildIndex)
        {
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                temp = new CreatingTrainingPlaneViewModel();
            }
            if (exerciseIndex >= 0 && exerciseIndex < temp.Sets.Count && setFeildIndex >= 0 && setFeildIndex < temp.Sets[exerciseIndex].Field.Count)
            {
                temp.Sets[exerciseIndex].Field.RemoveAt(setFeildIndex);
            }
            HttpContext.Session.SetObject(SessionKeyPlan, temp);
            return RedirectToAction("Create", "WorkOut");
        }
        public  IActionResult AddSet(int exerciseIndex)
        {
            CreatingTrainingPlaneViewModel temp = HttpContext.Session.GetObject<CreatingTrainingPlaneViewModel>(SessionKeyPlan);

            if (temp == default)
            {
                temp = new CreatingTrainingPlaneViewModel();
            }
            if (exerciseIndex >= 0 && exerciseIndex < temp.Sets.Count )
            {
                SetAttributeViewModel set = new SetAttributeViewModel();
                temp.Sets[exerciseIndex].Field.Add(set);
            }
            HttpContext.Session.SetObject(SessionKeyPlan, temp);
            return RedirectToAction("Create", "WorkOut");
        }
    }
}
