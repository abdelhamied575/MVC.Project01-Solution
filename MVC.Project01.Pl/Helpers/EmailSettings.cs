using MVC.Project01.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC.Project01.Pl.Helpers
{
	public static class EmailSettings
	{

		public static void SendEmail(Email email)
		{
			// Mail Server : gmail.com

			// SMTP

			var client = new SmtpClient("smtp.gmail.com", 587);

			client.EnableSsl = true; // For Encrypt If I Use HTTPS Protocol

			client.Credentials = new NetworkCredential("abdelhamiedbelal470@gmail.com", "askuttcxjqkuqotl");

			client.Send("abdelhamiedbelal470@gmail.com",email.To,email.Subject,email.Body);

		}


	}
}
