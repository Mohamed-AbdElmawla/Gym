using Gym.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace Gym.View_Models
{
    public class CreatingTrainingPlaneViewModel
    {
        public CreatingTrainingPlaneViewModel()
        {
            this.Sets = new List<SetViewModel>();
        }
        [Required,StringLength(50)]
        public string Name { get; set; }
        public IList<SetViewModel> Sets { get; set; }
    }
    public class SetViewModel
    {
        public SetViewModel()
        {
            this.Field = new List<SetAttributeViewModel>();
        }
        public int ExerciseId { get; set; }
        public IList<SetAttributeViewModel> Field { get; set; }
        public Exercise? Exercise { get; set; }

    }
    public class SetAttributeViewModel
    {
        public int Reps { get; set; } = 0;
        public double Weight { get; set; } = 0;
    }
}
