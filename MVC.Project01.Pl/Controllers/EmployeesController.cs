using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.ViewModels.Employees;

namespace MVC.Project01.Pl.Controllers
{
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

        public IActionResult Index(string InputSearch)
        {
            var employee = Enumerable.Empty<Employee>();
            //IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(InputSearch))
            {
                 employee = _employeeRepository.GetAll();

            }
            else
            {
                employee= _employeeRepository.GetByName(InputSearch);
            }
            var Result=_mapper.Map<IEnumerable<EmployeeViewModel>>(employee);


            return View(Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var department=_departmentRepository.GetAll();

            ViewData["department"] = department;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
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

                    var Count = _employeeRepository.Add(employee);
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
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();

            var employee = _employeeRepository.Get(id.Value);

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
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department=_departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

            var department = _departmentRepository.GetAll();

            ViewData["department"] = department;



            return Details(id, "Edit");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee model, [FromRoute] int? id)
        {
            try
            {
                

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var employee=_mapper.Map<Employee>(model);

                    var Count = _employeeRepository.Update(employee);
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
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest();

            //var department = _departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

            return Details(id, "Delete");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int? id, Employee model)
        {

            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {

                    var Count = _employeeRepository.Delete(model);
                    if (Count > 0)
                    {
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
