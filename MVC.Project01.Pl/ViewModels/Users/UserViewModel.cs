namespace MVC.Project01.Pl.ViewModels.Users
{
	public class UserViewModel
	{

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IEnumerable <string> Roles { get; set; }



    }
}
