namespace Gym.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string CoachId { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; } //days
        public double Price { get; set; }
        public ApplicationUser Coach { get; set; }
        public virtual ICollection<SubscriptionUser>? SubscriptionUsers { get; set; }

    }
}
