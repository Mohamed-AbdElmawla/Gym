using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym.Models
{
    public class TrainingPlan
    {
        public int Id { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId {  get; set; }

        public virtual IdentityUser User { get; set; }

    }
}
