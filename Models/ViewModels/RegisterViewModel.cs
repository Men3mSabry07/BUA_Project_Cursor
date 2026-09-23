using System.ComponentModel.DataAnnotations;

namespace BUA_project.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string AccountType { get; set; }

        // Driver-specific fields
        public string? LicenseNumber { get; set; }

        public string? QualificationStatus { get; set; }

        public DateTime? QualificationValidUntil { get; set; }
    }
}