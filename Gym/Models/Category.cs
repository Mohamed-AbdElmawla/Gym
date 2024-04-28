namespace Gym.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMuscle { get; set; } = true;//Muscle or Equipment
    }
}
