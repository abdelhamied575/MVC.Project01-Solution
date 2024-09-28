using System.ComponentModel.DataAnnotations;

namespace MVC.Project01.Pl.ViewModels.Auth
{
	public class ForgetPasswordViewModel
	{

		[EmailAddress(ErrorMessage = "Invalid Email")]
		[Required(ErrorMessage = "Email Is Required !!")]
		public string Email { get; set; }


	}
}
