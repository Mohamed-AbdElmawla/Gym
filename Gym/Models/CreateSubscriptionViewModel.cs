using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class CreateSubscriptionViewModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int Days { get; set; }
        [Required]
        public int Months { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
