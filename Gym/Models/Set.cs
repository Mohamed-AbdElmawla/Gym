using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models
{
    public class SetAttribute
    {
        public int Id { get; set; }
        [ForeignKey("Set")]
        public int SetId { get; set; }
        public int SetCount { get; set; }
        public int Weight { get; set; }
    }
    public class Set
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TrainingPlan")]
        public int TrainingId { get; set; }
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public virtual ICollection<SetAttribute> Field { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
        public virtual Exercise Exercise { get; set; }


    }
}
