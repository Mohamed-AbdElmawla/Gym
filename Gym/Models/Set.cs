using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int TrainingId { get; set; }
        public int ExerciseId { get; set; }
        public int SetCount {  get; set; }
        public int Weight {  get; set; }
        public string Date { get; set; }

        public virtual TrainingPlan TrainingPlan { get; set; }
        public virtual Exercise Exercise { get; set; }


    }
}
