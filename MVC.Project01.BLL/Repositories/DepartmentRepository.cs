    using Microsoft.Identity.Client;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.DAL.Data.Contexts;
using MVC.Project01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Project01.BLL.Repositories
{
    // CLR
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        //private readonly AppDbContext _context; // NULL


        public DepartmentRepository(AppDbContext context) :base(context) // Ask The CLR To Create Object From AppDbContext
        {
            //_context = context;
        }


        //public IEnumerable<Department> GetAll()
        //{
        //   return _context.Departments.ToList();

        //}

        //public Department Get(int Id)
        //{
        //    //return _context.Departments.FirstOrDefault(D=>D.Id==Id);
        //    return _context.Departments.Find(Id);

        //}


        //public int Add(Department entity)
        //{
        //     _context.Departments.Add(entity);
        //    return _context.SaveChanges();

        //}


        //public int Update(Department entity)
        //{
        //    _context.Departments.Update(entity);
        //    return _context.SaveChanges();
        //}


        //public int Delete(Department entity)
        //{
        //    _context.Departments.Remove(entity);
        //    return _context.SaveChanges();
        //}


    }
}
