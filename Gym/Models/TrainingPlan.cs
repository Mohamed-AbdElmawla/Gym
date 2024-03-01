using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models
{
    public class TrainingPlan
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId {  get; set; }

        public virtual User User { get; set; }

    }
}
