using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.BLL.Repositories;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.ViewModels.Departments;
using System.Collections.Generic;

namespace MVC.Project01.Pl.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentRepository; // Null
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _departmentRepository=departmentRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var departments= _departmentRepository.GetAll();

            var Result=_mapper.Map<IEnumerable<DepartmentViewModel>>(departments);

            return View(Result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel model)
        {

            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);

                var Count = _departmentRepository.Add(department);
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

            var model=_mapper.Map<DepartmentViewModel>(department);


            return View(ViewName, model);
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
        public IActionResult Edit(DepartmentViewModel model,[FromRoute]int ?id)
        {
            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var department=_mapper.Map<Department>(model);

                    var Count = _departmentRepository.Update(department);
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
        public IActionResult Delete([FromRoute] int? id,DepartmentViewModel model)
        {

            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var department= _mapper.Map<Department>(model);
                    var Count = _departmentRepository.Delete(department);
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
