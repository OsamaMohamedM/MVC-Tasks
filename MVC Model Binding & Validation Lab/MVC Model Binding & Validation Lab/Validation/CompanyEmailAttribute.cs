using System.ComponentModel.DataAnnotations;

namespace MVC_Model_Binding___Validation_Lab.Validation
{
    public class CompanyEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string email)
            {
                if (email.EndsWith("@company.com", StringComparison.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(ErrorMessage ?? "it must be end with @company.com");
            }
            return new ValidationResult("formatting is invalid");
        }
    }
}