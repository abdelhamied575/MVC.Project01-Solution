using System.ComponentModel.DataAnnotations;

namespace MVC.Project01.Pl.ViewModels.Auth
{
	public class ResetPasswordViewModel
	{

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password Is Required !!")]
		[MinLength(5, ErrorMessage = "Password Min Length Is 5")]
		public string Password { get; set; }



		[DataType(DataType.Password)]
		[MinLength(5, ErrorMessage = "Password Min Length Is 5")]
		[Required(ErrorMessage = "ConfirmPassword Is Required !!")]
		[Compare(nameof(Password), ErrorMessage = "Confirmed Password Does Not match Password")]
		public string ConfirmPassword { get; set; }


	}
}
