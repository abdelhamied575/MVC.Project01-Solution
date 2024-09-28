using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.Helpers;
using MVC.Project01.Pl.ViewModels.Auth;
using NuGet.Common;

namespace MVC.Project01.Pl.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager ,
			SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

		#region SignUp

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{

			if (ModelState.IsValid)
			{
				try
				{

					var user = await _userManager.FindByNameAsync(model.UserName);

					if (user is null)
					{
						// Mapping :SignUpViewModel ---> ApplicationUser

						user = await _userManager.FindByEmailAsync(model.Email);

						if (user is null)
						{
							user = new ApplicationUser()
							{
								UserName = model.UserName,
								Email = model.Email,
								FristName = model.FristName,
								LastName = model.LastName,
								IsAgree = model.IsAgree,
							};

							var result = await _userManager.CreateAsync(user, model.Password);

							if (result.Succeeded)
							{
								return RedirectToAction("SignIn");
							}

							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}




					}





					ModelState.AddModelError(string.Empty, "User Is Aready Exits");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);


				}



			}


			return View(model);
		}

		#endregion


		#region SignIn
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{

			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						// Check Password
						var Flag = await _userManager.CheckPasswordAsync(user, model.Password);

						if (Flag)
						{
							// SignIn
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

							if (result.Succeeded)
							{
								//return RedirectToAction(nameof(HomeController.Index));
								return RedirectToAction("Index", "Home");
							}



						}



					}

					ModelState.AddModelError(string.Empty, "Invalid Login !!");

				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);

				}
			}


			return View(model);
		}

		#endregion


		#region SignOut

		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();

			return RedirectToAction(nameof(SignIn));


		}






		#endregion


		#region Forget-Password

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}
		public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel model)
		{

			if (ModelState.IsValid)
			{
				var user=await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{

					// Create Token
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);
					// Create Reset Password URL
					var url =  Url.Action("ResetPassword", "Account", new {email=model.Email,token=token},Request.Scheme);

					// Create Email
					var email = new DAL.Models.Email()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = $"Click in Link to Reset your Password \n{url}"
					};
					// Send Email

					EmailSettings.SendEmail(email);

					return RedirectToAction(nameof(CheckYourInbox));

				}

				ModelState.AddModelError(string.Empty, "Invalid Operation");

			}


			return View(model);
		}


		[HttpGet]
		public IActionResult CheckYourInbox()
		{
			return View();
		}






		#endregion


		#region Reset Password


		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{

			if (ModelState.IsValid)
			{
				try
				{
					var email = TempData["email"] as string;
					var token = TempData["token"] as string;

					var user = await _userManager.FindByEmailAsync(email);

					if (user is not null)
					{
						var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

						if (result.Succeeded)
						{
							return RedirectToAction(nameof(SignIn));
						}


					}
				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);

				}

			}

			ModelState.AddModelError(string.Empty, "Invalid Operation");





			return View(model);
		}




        #endregion


        #region AccessDenied

		public IActionResult AccessDenied()
		{
			return View();
		}


        #endregion



    }
}
