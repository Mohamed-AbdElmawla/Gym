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
        [Required(ErrorMessage = "Reps is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Reps must be a positive integer")]
        public int Reps { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Weight must be a positive number")]
        public double Weight { get; set; }
    }
    public class Set
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TrainingPlan")]
        public int TrainingId { get; set; }
        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public virtual IList<SetAttribute> Field { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
        public virtual Exercise Exercise { get; set; }


    }
}
