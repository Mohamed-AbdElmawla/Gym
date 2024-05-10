using Gym.Models;

namespace Gym.View_Models
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string MessageBody { get; set; }
        public DateTime MessageSentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
