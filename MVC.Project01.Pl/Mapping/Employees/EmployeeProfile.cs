using AutoMapper;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.ViewModels.Employees;

namespace MVC.Project01.Pl.Mapping.Employees
{
    public class EmployeeProfile:Profile
    {

        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
            


        }

    }
}
