using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SubscriptionController(ApplicationDbContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles ="Coach")]
        public async Task<IActionResult> ShowAll()
        {
            string id = _userManager.GetUserId(User);
            var subscriptions = await _context.Subscriptions.Where(e => e.CoachId == id).ToListAsync();
            return View(subscriptions);
        }
        [Authorize(Roles ="Coach")]
        [HttpGet]
        public IActionResult Create(string coachId)
        {

            return View();
        }
        [Authorize(Roles = "Coach")]
        [HttpPost]
        public IActionResult PoCreate()
        {
            return View();
        }

    }
}
