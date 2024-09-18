using Microsoft.EntityFrameworkCore;
using MVC.Project01.BLL.Interfaces;
using MVC.Project01.DAL.Data.Contexts;
using MVC.Project01.DAL.Models;

namespace MVC.Project01.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _context.Employees.Include(E => E.WorkFor).ToList();
            }

            return _context.Set<T>().ToList();

        }

        public T Get(int Id)
        {
           

            return _context.Set<T>().Find(Id);
        }


        public int Add(T entity)
        {
            _context.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(T entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Remove(entity);
            return _context.SaveChanges();
        }

       

    }
}
