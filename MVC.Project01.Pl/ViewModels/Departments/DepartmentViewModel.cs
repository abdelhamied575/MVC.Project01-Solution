using MVC.Project01.DAL.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.Project01.Pl.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }

        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }




    }
}
