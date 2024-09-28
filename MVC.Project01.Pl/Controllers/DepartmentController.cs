using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.BLL.Repositories;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.ViewModels.Departments;
using System.Collections.Generic;

namespace MVC.Project01.Pl.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository _departmentRepository; // Null
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _departmentRepository=departmentRepository;
            _mapper = mapper;
        }





        public async Task<IActionResult> Index()
        {
            var departments=await _departmentRepository.GetAllAsync();

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
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {

            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);

                var Count =await _departmentRepository.AddAsync(department);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model); 

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id,string ViewName= "Details")
        {
            if (id is null) return BadRequest();

            var department=await _departmentRepository.GetAsync(id.Value);

            if (department is null) return NotFound();

            var model=_mapper.Map<DepartmentViewModel>(department);


            return View(ViewName, model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id is null) return BadRequest();

            //var department=_departmentRepository.Get(id.Value);

            //if (department is null) return NotFound();

            //return View(department);

            return await Details(id,"Edit");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel model,[FromRoute]int ?id)
        {
            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var department= _mapper.Map<Department>(model);

                    var Count = await _departmentRepository.UpdateAsync(department);
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
        public async Task<IActionResult> Delete([FromRoute] int? id,DepartmentViewModel model)
        {

            try
            {

                if (id != model.Id) return BadRequest();

                if (ModelState.IsValid)
                {
                    var department= _mapper.Map<Department>(model);
                    var Count =await _departmentRepository.DeleteAsync (department);
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
