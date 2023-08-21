using System.ComponentModel.DataAnnotations;

namespace RunningWebApp.ViewModels
{
	public class LoginViewModel
	{
		[Display(Name = "Email Address")] // Validation annotations
		[Required(ErrorMessage = "Email address required")]
		public string EmailAddress { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
