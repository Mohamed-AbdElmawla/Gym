using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace Gym.Models
{
    public class PhotoValidationAttribute : ValidationAttribute
    {
        public int _maxSize { get; set; }
        public string[] _extensions { get; set; }
        public PhotoValidationAttribute(int maxSize, string[] extension)
        {
            _maxSize = maxSize;
            _extensions = extension;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null) return ValidationResult.Success;
            var file = value as IFormFile;
            if(file.Length > _maxSize)
            {
                return new ValidationResult($"Maximum allowed Photo size is {_maxSize} bytes");
            }
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension))
            {
                return new ValidationResult("Only JPG, JPEG and PNG files are allowed");
            }
            return ValidationResult.Success;
        }
    }
}
