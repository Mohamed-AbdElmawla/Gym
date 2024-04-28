using Gym.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExerciseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddExercise(string? search)
        {
            var exercises = await _context.Exercises.Where(x =>search==null?true:x.Name.Contains(search)).ToListAsync();
            return View(exercises);
        }
    }
}
