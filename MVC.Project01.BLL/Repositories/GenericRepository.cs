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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.WorkFor).ToListAsync();
            }

            return await _context.Set<T>().ToListAsync();

        }

        public async Task<T> GetAsync(int Id)
        {
           

            return await _context.Set<T>().FindAsync(Id);
        }


        public async Task<int> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync();
        }

       

    }
}
