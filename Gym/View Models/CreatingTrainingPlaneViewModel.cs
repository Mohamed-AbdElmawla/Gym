using Gym.Models;
using System.ComponentModel.DataAnnotations;

namespace Gym.View_Models
{
    public class CreatingTrainingPlaneViewModel
    {
        public CreatingTrainingPlaneViewModel()
        {
            this.Sets = new List<Set>();
        }
        [Required,StringLength(50)]
        public string Name { get; set; }
        public IList<Set> Sets { get; set; }
    }
}
