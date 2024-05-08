using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models
{
    public enum Status
    {
        Accepted,
        Rejected,
        Pending
    }
    public class CoachEnrollment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string? Feedback { get; set; }
        public string NationalIdPicturePath { get; set; }
        public Status Status { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
