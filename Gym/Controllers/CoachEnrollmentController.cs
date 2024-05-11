using Gym.Data;
using Gym.Migrations;
using Gym.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace Gym.Controllers
{
    [Authorize(Roles = "Member")]
    public class CoachEnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CoachEnrollmentController(ApplicationDbContext context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            string id = _userManager.GetUserId(User);
            var userEnrollments = await _context.CoachEnrollments.Where(e => e.UserId == id).ToListAsync();
            return View(userEnrollments);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PhotoViewModel input)
        {
            if (ModelState.IsValid)
            {
                string id = _userManager.GetUserId(User);
                if (input.Photo != null && input.Photo.Length > 0)
                {
                    try
                    {
                        // Get the wwwroot path
                        string uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "enrollments");
                        Directory.CreateDirectory(uploadPath);
                        // Generate a unique filename for the uploaded file
                        string newfilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(input.Photo.FileName)}";
                        string fileName = Path.Combine(uploadPath, newfilename);

                        // Save the file to the server
                        using (var fileStream = new FileStream(fileName, FileMode.Create))
                        {
                            input.Photo.CopyTo(fileStream);
                        }
                        CoachEnrollment coachEnrollment = new CoachEnrollment
                        { 
                            UserId = id,
                            Status = Status.Pending,
                            NationalIdPicturePath = Path.Combine("\\enrollments", newfilename)
                        
                        };
                        await _context.CoachEnrollments.AddAsync(coachEnrollment);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Form submitted successfully!";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading file: " + ex.Message);
                    }
                }
            }
            TempData["ErrorMessage"] = "You Should Enter a valid National ID Picture";
            return RedirectToAction("Create");

        }
    }
}
