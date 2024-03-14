using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var file = value as IFormFile;
        if (file != null && file.Length > _maxFileSize)
        {
            return new ValidationResult($"The file size should not exceed {_maxFileSize} bytes.");
        }

        return ValidationResult.Success;
    }
}
