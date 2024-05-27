using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Gym.Services;

namespace Gym.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string SenderId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ReceiverId { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string? MessageBody { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public ApplicationUser Receiver { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
