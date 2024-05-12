using Gym.Models;

namespace Gym.View_Models
{
    public class TrainingPlanViewModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<Set> Sets { get; set; }
    }
}
