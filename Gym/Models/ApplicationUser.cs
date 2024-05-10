using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gym.Models
{
    public enum Gender
    {
        Male,
        Female

    }
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public DateTime DOB { get; set; }
        [Required]
        [StringLength(50)]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public Gender Gender { get; set; }
        [PersonalData]
        public string? ProfilePicturePath { get; set; }
    }
}
