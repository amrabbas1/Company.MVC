using Company.G03.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.G03.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(EmailModel email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("amrabbas2003@gmail.com", "buamqmzesffazhht");
			client.Send("amrabbas2003@gmail.com",email.To,email.Subject,email.Body);

		}
	}
}
