using Gym.Data;
using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SubscriptionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles = "Coach")]
        public async Task<IActionResult> ShowAll()
        {
            string id = _userManager.GetUserId(User);
            var subscriptions = await _context.Subscriptions.Where(e => e.CoachId == id).ToListAsync();
            return View(subscriptions);
        }
        [Authorize(Roles = "Coach")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriptionViewModel input)
        {
            try
            {
                Subscription newSubscription = new Subscription();
                string user = _userManager.GetUserId(User);
                newSubscription.CoachId = user;
                int durationInDays = (input.Months * 30) + input.Days;
                if (durationInDays<=0)
                {
                    TempData["ErrorMessage"] = "You should enter a valid duration of you subscription plan";
                    return RedirectToAction("Create");
                }
                newSubscription.Description = input.Description;
                newSubscription.Duration = durationInDays;
                newSubscription.Price = input.Price;
                await _context.Subscriptions.AddAsync(newSubscription);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Your Subscription Saved Successfully";
                return RedirectToAction("ShowAll");
            }
            catch (Exception ex) 
            {
                await Console.Out.WriteLineAsync("Error in saving subscription");
            }
            TempData["ErrorMessage"] = "Unexpected error on saving your subscription";
            return RedirectToAction("Create");
        }
    }
}
