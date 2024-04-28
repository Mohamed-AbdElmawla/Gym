using Gym.Models;

namespace Gym.View_Models
{
    public class CreatingTrainingPlaneViewModel
    {
        public string Name { get; set; }
        public ICollection<Set> Sets { get; set; }
    }
}
