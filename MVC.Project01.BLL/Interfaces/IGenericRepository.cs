using MVC.Project01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Project01.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(int Id);

        Task <int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);



    }
}
