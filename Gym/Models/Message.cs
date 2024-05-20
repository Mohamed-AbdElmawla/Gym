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
        [StringLength(5000, ErrorMessage = "The message content cannot exceed 5000 characters.")]
        public string? MessageBody { get; set; }
        public string? AttachmentPath { get; set; }
        public DateTime SentDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public ApplicationUser Receiver { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
