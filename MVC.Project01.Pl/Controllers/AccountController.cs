using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.ViewModels.Auth;

namespace MVC.Project01.Pl.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {

            if(ModelState.IsValid)
            {

                var user =await _userManager.FindByNameAsync(model.UserName);

                if(user is not  null)
                {
					// Mapping :SignUpViewModel ---> ApplicationUser

					user = await _userManager.FindByEmailAsync(model.Email);

                    if(user is not null)
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


            return View(model);
        }
    }
}
