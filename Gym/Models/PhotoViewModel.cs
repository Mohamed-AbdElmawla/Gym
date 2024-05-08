using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public class PhotoViewModel
    {
        [Display(Name = "Photo")]
        [PhotoValidation(5 * 1024 * 1024, new string[] { ".jpg", ".png", ".jpeg" })]
        [DataType(DataType.Upload)]
        //[Required(ErrorMessage = "Photo is required.")]
        public IFormFile Photo { get; set; }
    }
}
