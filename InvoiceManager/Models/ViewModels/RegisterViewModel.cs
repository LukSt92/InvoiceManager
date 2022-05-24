using System.ComponentModel.DataAnnotations;

namespace InvoiceManager.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź Hasło")]
        [Compare("Password", ErrorMessage = "Wpisane hasła się nie zgadzają.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Imię i nazwisko")]
        public string Name { get; set; }

        public Address Address { get; set; }
    }
}
