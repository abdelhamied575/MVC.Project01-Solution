using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVC.Project01.Pl.ViewModels.Roles;

namespace MVC.Project01.Pl.Mapping
{
    public class RoleProfile:Profile
    {

        public RoleProfile()
        {
            CreateMap<IdentityRole,RoleViewModel>().ReverseMap();
        }


    }
}
