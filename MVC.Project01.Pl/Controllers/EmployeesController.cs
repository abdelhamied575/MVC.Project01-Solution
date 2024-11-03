using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.Helpers;
using MVC.Project01.Pl.ViewModels.Employees;
using System.Reflection.Metadata;

namespace MVC.Project01.Pl.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {


        private readonly IEmployeeRepository _employeeRepository; // Null
        private readonly IDepartmentRepository _departmentRepository; // Null
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository,IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string InputSearch)
        {
            var employee = Enumerable.Empty<Employee>();
            //IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(InputSearch))
            {
                 employee =await _employeeRepository.GetAllAsync();

            }
            else
            {
                employee=await _employeeRepository.GetByNameAsync(InputSearch);
            }
            var Result=_mapper.Map<IEnumerable<EmployeeViewModel>>(employee);


            return View(Result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var department=await _departmentRepository.GetAllAsync();

            ViewData["department"] = department;

            return View();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    


                    //// Casting EmployeeViewModel(ViewModel) To Employee(Model)
                    //// Mapping
                    //// 1. Manual Mapping
                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    Email = model.Email,
                    //    PhoneNumber = model.PhoneNumber
                    //};


                    // 2. Auto Mapping

                    

                    var employee=_mapper.Map<Employee>(model);

                    var Count =await _employeeRepository.AddAsync(employee);
                    if (Count > 0)
                    {
                        TempData["Message"] = "Employee Created!";
                    }
                    else
                    {
                        TempData["Message"] = "Employee Not Created!";

                    }
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    
                }


            }
            return View(model);

        }





        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var employee =await  _employeeRepository.GetAsync(id.Value);

            if (employee is null) return NotFound();

            //// Mapping : Employee ---> EmployeeViewModel

            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = employee.Id,
            //    Name = employee.Name,
            //    Address = employee.Address,
            //    Salary = employee.Salary,
            //    Age = employee.Age,
            //    HiringDate = employee.HiringDate,
            //    IsActive = employee.IsActive,
            //    WorkFor = employee.WorkFor,
            //    WorkForId = employee.WorkForId,
            //    Email = employee.Email,
            //    PhoneNumber = employee.PhoneNumber
            //};

            // Auto Mapping 
            var employeeViewModel=  _mapper.Map<EmployeeViewModel>(employee);

            return View(ViewName, employeeViewModel);
        }






        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department=_departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

            var department =await _departmentRepository.GetAllAsync();

            ViewData["department"] = department;



            return await Details(id, "Edit");


        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel model, [FromRoute] int? id)
        {
            try
            {
                

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {

                    if(model.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "Images");
                    }
                    model.ImageName  = DocumentSettings.UploadFile(model.Image,"Images");

                    var employee=_mapper.Map<Employee>(model);

                    var Count =await _employeeRepository.UpdateAsync(employee);
                    if (Count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {
                // 1. Log Exception
                // 2. Friendly Message

                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);

        }






        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

            return await Details(id, "Delete");

        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, Employee model)
        {

            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {

                    var Count =await _employeeRepository.DeleteAsync(model);
                    if (Count > 0)
                    {
                        DocumentSettings.DeleteFile(model.ImageName, "Images");
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception Ex)
            {

                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);

        }













    }
}
