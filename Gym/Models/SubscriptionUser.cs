using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class SubscriptionUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int SubscriptionId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public Subscription Subscription { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
    }
}
