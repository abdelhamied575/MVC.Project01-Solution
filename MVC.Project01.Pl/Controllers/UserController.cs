using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.Helpers;
using MVC.Project01.Pl.ViewModels.Employees;
using MVC.Project01.Pl.ViewModels.Users;

namespace MVC.Project01.Pl.Controllers
{
    [Authorize(Roles = "Admin")]

    public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		// Get - GetAll - Add - Update - Delete
		// Index - Details - Edit - Delete

		public UserController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}




		[HttpGet]
		public async Task<IActionResult> Index(string InputSearch)
		{
			var users = Enumerable.Empty<UserViewModel>();

			if (string.IsNullOrEmpty(InputSearch))
			{
				users= await _userManager.Users.Select(U => new UserViewModel
				{
					Id = U.Id,
					FirstName = U.FristName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync() ;

			}
			else
			{

				users  = await _userManager.Users.Where(U=>U.Email
													    .ToLower()
													    .Contains(InputSearch.ToLower()))
													    .Select(U => new UserViewModel
													    {
														    Id = U.Id,
														    FirstName = U.FristName,
														    LastName = U.LastName,
														    Email = U.Email,
														    Roles = _userManager.GetRolesAsync(U).Result
													    }).ToListAsync(); ;	

			}


			return View(users);
		}




        [HttpGet]
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var userFromDb = await _userManager.FindByIdAsync(id);

            if (userFromDb is null) return NotFound(); // 404

			var user = new UserViewModel()
			{
				Id = userFromDb.Id,
				FirstName = userFromDb.FristName,
				LastName = userFromDb.LastName,
				Email = userFromDb.Email,
				Roles=  _userManager.GetRolesAsync(userFromDb).Result
			};
            

            return View(ViewName,user);
        }




          



        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string? id)
        {
            


                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {


                    var UserFromDb = await _userManager.FindByIdAsync(id);

                    if (UserFromDb is null)
                        return NotFound();

                    UserFromDb.FristName = model.FirstName;
                    UserFromDb.LastName = model.LastName;
                    UserFromDb.Email = model.Email;

                    await _userManager.UpdateAsync(UserFromDb);

                    return RedirectToAction(nameof(Index));
                    

                   
                }

            

            

            return View(model);

        }






        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

            return await Details(id, "Delete");

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {

            if(id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var UserFromDb=await _userManager.FindByIdAsync(id);

                if(UserFromDb is null)
                    return NotFound();

                var result= await _userManager.DeleteAsync(UserFromDb);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }



            return View(model);

        }











    }
}
