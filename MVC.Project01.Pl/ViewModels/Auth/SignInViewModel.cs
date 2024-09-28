using System.ComponentModel.DataAnnotations;

namespace MVC.Project01.Pl.ViewModels.Auth
{
	public class SignInViewModel
	{
		[EmailAddress(ErrorMessage = "Invalid Email")]
		[Required(ErrorMessage = "Email Is Required !!")]
		public string Email { get; set; }


		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password Is Required !!")]
		[MinLength(5, ErrorMessage = "Password Min Length Is 5")]
		public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
