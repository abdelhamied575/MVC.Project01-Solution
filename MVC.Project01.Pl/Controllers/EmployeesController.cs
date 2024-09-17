using Microsoft.AspNetCore.Mvc;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.DAL.Models;

namespace MVC.Project01.Pl.Controllers
{
    public class EmployeesController : Controller
    {


        private readonly IEmployeeRepository _employeeRepository; // Null

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var employee = _employeeRepository.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {

            if (ModelState.IsValid)
            {

                var Count = _employeeRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
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

            return View(ViewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department=_departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

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

                    var Count = _employeeRepository.Update(model);
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
