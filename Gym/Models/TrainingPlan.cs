using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models
{
    public class TrainingPlan
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId {  get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
