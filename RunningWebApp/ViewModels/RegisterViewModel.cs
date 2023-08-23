using System.ComponentModel.DataAnnotations;

namespace RunningWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")] // Validation annotations
        [Required(ErrorMessage = "Email address required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password required")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
