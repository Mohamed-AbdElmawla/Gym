using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gym.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public DateTime DOB { get; set; }
        [PersonalData]
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string? ProfilePicturePath { get; set; }
    }
}
