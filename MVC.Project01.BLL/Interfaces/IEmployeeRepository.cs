using MVC.Project01.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Project01.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
         Task<IEnumerable<Employee>> GetByNameAsync(string name);

        //IEnumerable<Employee> GetAll();

        //Employee Get(int Id);

        //int Add(Employee entity);
        //int Update(Employee entity);
        //int Delete(Employee entity);



    }
}
