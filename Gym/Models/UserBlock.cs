namespace Gym.Models
{
    public class UserBlock
    {
        public int Id { get; set; }
        public string BlockerId { get; set; }
        public string BlockedId { get; set; }
        public virtual ApplicationUser Blocker { get; set; }
        public virtual ApplicationUser Blocked { get; set; }
    }
}
