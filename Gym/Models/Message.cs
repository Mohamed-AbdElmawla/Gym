using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class Message
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
        public DateTime MessageSentAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public ApplicationUser Receiver { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
