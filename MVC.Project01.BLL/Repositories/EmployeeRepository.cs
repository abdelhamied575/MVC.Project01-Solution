using Microsoft.EntityFrameworkCore;
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
    public class EmployeeRepository :GenericRepository<Employee> , IEmployeeRepository
    {


        public EmployeeRepository(AppDbContext context):base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await _context.Employees
                           .Where(E=>E.Name.ToLower()
                           .Contains(name.ToLower()))
                           .Include(E => E.WorkFor)
                           .ToListAsync();

        }



        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}


        //public Employee Get(int Id)
        //{
        //   return _context.Employees.Find(Id);
        //}


        //public int Add(Employee entity)
        //{
        //    _context.Add(entity);
        //    return _context.SaveChanges();
        //}

        //public int Update(Employee entity)
        //{
        //    _context.Update(entity);
        //    return _context.SaveChanges();
        //}


        //public int Delete(Employee entity)
        //{
        //    _context.Remove(entity);
        //    return _context.SaveChanges();
        //}



    }
}
