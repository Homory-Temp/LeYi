using System;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;
using System.Text;
using EntityFramework.Extensions;
using Homory.Model;

namespace Homory.Utility
{
	public class Smtp
	{
		public static bool SendEmail(Entities db, string title, string alias, string content, string address, string sender)
		{
			try
			{
				var host =
					db.ApplicationPolicy.Where(o => o.Name == "SmtpHost" && o.ApplicationId == Guid.Empty).FutureFirstOrDefault();
				var port = db.ApplicationPolicy.Where(o => o.Name == "SmtpPort" && o.ApplicationId == Guid.Empty).FutureFirstOrDefault();
				var account = db.ApplicationPolicy.Where(o => o.Name == "SmtpAccount" && o.ApplicationId == Guid.Empty).FutureFirstOrDefault();
				var password = db.ApplicationPolicy.Where(o => o.Name == "SmtpPassword" && o.ApplicationId == Guid.Empty).FutureFirstOrDefault();
				var smtp = new SmtpClient
				{
					EnableSsl = false,
					Host = host.Value.Value,
					Port = int.Parse(port.Value.Value),
					Credentials =
						new NetworkCredential(account.Value.Value,
							password.Value.Value)
				};
				var mm = new MailMessage
				{
					From = new MailAddress(account.Value.Value, sender, Encoding.UTF8)
				};
				mm.To.Add(new MailAddress(address, alias, Encoding.UTF8));
				mm.Subject = title;
				mm.SubjectEncoding = Encoding.UTF8;
				mm.IsBodyHtml = true;
				mm.BodyEncoding = Encoding.UTF8;
				mm.Body = content;
				smtp.Send(mm);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
