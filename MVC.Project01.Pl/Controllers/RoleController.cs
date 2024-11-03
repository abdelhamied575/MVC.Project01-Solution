using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.ViewModels.Roles;
using MVC.Project01.Pl.ViewModels.UserRole;
using MVC.Project01.Pl.ViewModels.Users;

namespace MVC.Project01.Pl.Controllers
{
    [Authorize(Roles="Admin")]
    
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        // Get - GetAll - Add - Update - Delete
        // Index - Create -Details - Edit - Delete

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }




        [HttpGet]
        public async Task<IActionResult> Index(string InputSearch)
        {
            var roles = Enumerable.Empty<RoleViewModel>();

            if (string.IsNullOrEmpty(InputSearch))
            {
                roles = await _roleManager.Roles.Select(R => new RoleViewModel
                {
                    Id = R.Id,
                    RoleName = R.Name

                }).ToListAsync();

            }
            else
            {

                roles = await _roleManager.Roles.Where(R => R.Name
                                                        .ToLower()
                                                        .Contains(InputSearch.ToLower()))
                                                        .Select(R => new RoleViewModel
                                                        {
                                                            Id = R.Id,
                                                            RoleName = R.Name 
                                                        }).ToListAsync(); ;

            }


            return View(roles);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var role = new IdentityRole()
                {
                    Name = model.RoleName
                };


                var result= await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }

            }



            return View(model);


        }




        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var RoleFromDb = await _roleManager.FindByIdAsync(id);

            if (RoleFromDb is null) return NotFound(); // 404

            var role = new RoleViewModel()
            {
                Id = RoleFromDb.Id,
                RoleName = RoleFromDb.Name
            };


            return View(ViewName, role);
        }





        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string? id)
        {



            if (id != model.Id) return BadRequest();

            if (ModelState.IsValid)
            {


                var RoleFromDb = await _roleManager.FindByIdAsync(id);

                if (RoleFromDb is null)
                    return NotFound();  

                RoleFromDb.Id = model.Id;
                RoleFromDb.Name = model.RoleName;
     

                await _roleManager.UpdateAsync(RoleFromDb);

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
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {

            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var RoleFromDb = await _roleManager.FindByIdAsync(id);

                if (RoleFromDb is null)
                    return NotFound();

                await _roleManager.DeleteAsync(RoleFromDb);
                return RedirectToAction(nameof(Index));
                

            }



            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();

            ViewData["RoleId"] = roleId;

            var UsersInRole= new List<UserInRoleViewModel>();

            var users = await _userManager.Users.ToListAsync();



            foreach (var user in users)
            {
                var userInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                UsersInRole.Add(userInRole);

            }

            return View(UsersInRole);


        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach(var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if(appUser is not null)
                    {

                        if (user.IsSelected && ! await _userManager.IsInRoleAsync(appUser,role.Name) )
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);

                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
    
                        }

                    }

                    
                }


                return RedirectToAction(nameof(Edit), new {id=roleId});

            }


            return View(users);


        }


    }
}
