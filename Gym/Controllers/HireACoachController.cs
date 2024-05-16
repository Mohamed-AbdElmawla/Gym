using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace Gym.Controllers
{
    public class HireACoachController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        
        public HireACoachController (ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var subscriptions = _context.Subscriptions.ToList();
            return View(subscriptions);
        }
    }
}
