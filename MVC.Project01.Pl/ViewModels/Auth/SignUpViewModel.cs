using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Project01.Pl.ViewModels.Auth
{
	public class SignUpViewModel
	{

        [Required(ErrorMessage ="User Name Is Required !!")]
        public string UserName { get; set; }


		[Required(ErrorMessage = "Frist Name Is Required !!")]
		public string FristName { get; set; }


		[Required(ErrorMessage = "Last Name Is Required !!")]
		public string LastName { get; set; }


        [EmailAddress(ErrorMessage ="Invalid Email")]
		[Required(ErrorMessage = "Email Is Required !!")]
		public string Email { get; set; }


		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password Is Required !!")]
		[MinLength(5,ErrorMessage ="Password Min Length Is 5")]
		public string Password { get; set; }



		[DataType(DataType.Password)]
		[MinLength(5,ErrorMessage ="Password Min Length Is 5")]
		[Required(ErrorMessage = "ConfirmPassword Is Required !!")]
		[Compare(nameof(Password),ErrorMessage ="Confirmed Password Does Not match Password")]
		public string ConfirmPassword { get; set; }



		[Required(ErrorMessage = " Required !!")]
		public bool IsAgree { get; set; }

	}
}
