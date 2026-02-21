using Microsoft.AspNetCore.Mvc;
using MVC_Model_Binding___Validation_Lab.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC_Model_Binding___Validation_Lab.Models
{
    public class EmployeeRegisterViewModel : IValidatableObject
    {
        [Required]
        [Remote(action: "VerifyUsername", controller: "Validation")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [CompanyEmail]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (JoinDate < DateTime.Today)
            {
                yield return new ValidationResult(
                    "invalid join date",
                    new[] { nameof(JoinDate) }
                );
            }
        }
    }
}