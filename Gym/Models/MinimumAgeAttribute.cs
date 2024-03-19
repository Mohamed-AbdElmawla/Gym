using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinimumAgeAttribute : ValidationAttribute
    {
        public int _MinAge { get; set; }
        public MinimumAgeAttribute(int MinAge)
        {
            _MinAge = MinAge;
        }
        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(_MinAge) < DateTime.Now;
            }
            return false;

        }
    }
}
