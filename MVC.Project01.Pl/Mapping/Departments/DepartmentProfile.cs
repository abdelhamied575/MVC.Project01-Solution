using AutoMapper;
using MVC.Project01.DAL.Models;
using MVC.Project01.Pl.ViewModels.Departments;

namespace MVC.Project01.Pl.Mapping.Departments
{
    public class DepartmentProfile:Profile
    {

        public DepartmentProfile()
        {
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }


    }
}
