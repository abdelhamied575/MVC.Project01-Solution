using Microsoft.AspNetCore.Mvc;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.BLL.Repositories;
using MVC.Project01.DAL.Models;

namespace MVC.Project01.Pl.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentRepository; // Null

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository=departmentRepository;    
        }

        public IActionResult Index()
        {
            var departments= _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {

            if (ModelState.IsValid)
            {

                var Count = _departmentRepository.Add(model);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model); 

        }

        [HttpGet]
        public IActionResult Details(int? id,string ViewName= "Details")
        {
            if (id is null) return BadRequest();

            var department=_departmentRepository.Get(id.Value);

            if (department is null) return NotFound();

            return View(ViewName,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department=_departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

            return Details(id,"Edit");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department model,[FromRoute]int ?id)
        {
            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {

                    var Count = _departmentRepository.Update(model);
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
        public IActionResult Delete([FromRoute] int? id,Department model)
        {

            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {

                    var Count = _departmentRepository.Delete(model);
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
