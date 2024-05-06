using Gym.Models;
using System.ComponentModel.DataAnnotations;

namespace Gym.View_Models
{
    public class CreatingTrainingPlaneViewModel
    {
        public CreatingTrainingPlaneViewModel()
        {
            this.Sets = new List<Set>();
        }
        [Required(ErrorMessage = "Plan Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Plan Name must be between 3 and 100 characters")]
        public string Name { get; set; }
        public List<Set> Sets { get; set; }
    }
}
