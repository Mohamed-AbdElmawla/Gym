namespace Gym.View_Models
{
    public class ShowTwoUsersMessages
    {
        public string? MessageBody { get; set; }
        public string? AttachmentPath { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public bool IsCurUser { get; set; }
    }
}
