using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Controllers
{
    //[Authorize(Roles ="Member")]
    public class TraineeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public TraineeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Profile()
        {
            if (_signInManager.IsSignedIn(User))
            {
                RedirectToAction("Index", "Home");
            }
            //if ()
            var user = await _userManager.GetUserAsync(User);
            //var temp = _userManager.GetRolesAsync(user);
            var role = await _userManager.GetRolesAsync(user);
            if (role == null)
            {
                return View();
            }
            return View(user);
        }
    }
}