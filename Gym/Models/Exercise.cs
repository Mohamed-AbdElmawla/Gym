using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models
{
    public class Exercise
    {
        [Key]
        public int Id { get; set; }
        /*
        [ForeignKey("Category")]
        public int MuscleId { get; set; }
        [ForeignKey("Category")]
        public int EquipmentId { get; set; }
        */
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        /*
        public string Status { get; set; }
        public Category Muscle { get; set; }
        public Category Equipment { get; set; }
        */

    }
}
