using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EntityFramework.Extensions;
using Homory.Model;
using System.Xml.Linq;

namespace Go
{
	public partial class GoRegister : HomoryPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
                this.Title = doc.Root.Element("Sso").Value;
                LoadInit();
			}
		}

		private void LoadInit()
		{
			Session[HomoryConstant.SessionRegisterId] = HomoryContext.Value.GetId();
		}

		protected void upload_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
		{
			var file = string.Format("~/Common/头像/用户/{0}.png", Session[HomoryConstant.SessionRegisterId].ToString().ToUpper());
			var path = Server.MapPath(file);
			e.File.SaveAs(path, true);
			viewer.ImageUrl = string.Format("{0}?{1}", file, Guid.NewGuid());
		}

		public dynamic UserRegister(Entities db, Guid id, string email, string password1, string password2, string name,
			string realName, string iconUrl, string description)
		{
			dynamic output = new ExpandoObject();
			output.Ok = false;
			output.Data = new ExpandoObject();
			try
			{
				var open =
					db.ApplicationPolicy.Where(o => o.Name == "UserRegistration" && o.ApplicationId == Guid.Empty)
						.FutureFirstOrDefault();
				var mRegex =
					db.ApplicationPolicy.Where(o => o.Name == "UserEmailRegex" && o.ApplicationId == Guid.Empty).FutureFirstOrDefault();
				var length =
					db.ApplicationPolicy.Where(o => o.Name == "UserPasswordLength" && o.ApplicationId == Guid.Empty)
						.FutureFirstOrDefault();
				var count = db.User.Where(o => o.Account == email && o.State < State.审核).FutureCount();
				if (!bool.Parse(open.Value.Value))
				{
					output.Data.Message = "用户注册功能暂未开放";
					output.Data.Parameter = string.Empty;
					return output;
				}
				if (string.IsNullOrWhiteSpace(email) || !new Regex(mRegex.Value.Value).IsMatch(email))
				{
					output.Data.Message = "请输入.com/.cn结尾的电子邮箱";
					output.Data.Parameter = "email";
					return output;
				}
				if (string.IsNullOrWhiteSpace(password1) || string.IsNullOrWhiteSpace(password2) ||
					!password1.Equals(password2, StringComparison.Ordinal) || password1.Length < int.Parse(length.Value.Value))
				{
					output.Data.Message = string.Format("请输入不少于{0}位的密码，并确保两次输入的密码一致", length);
					output.Data.Parameter = "password";
					return output;
				}
				string key, salt;
				var password = HomoryCryptor.Encrypt(password1, out key, out salt);
				if (count.Value == 0)
				{
					var user = new User
					{
						Id = id,
						Account = email,
						RealName = string.IsNullOrWhiteSpace(realName) ? (string.IsNullOrWhiteSpace(name) ? email : name) : realName,
						DisplayName = string.IsNullOrWhiteSpace(name) ? email : name,
						Icon = iconUrl,
						Stamp = Guid.NewGuid(),
						Password = password,
						PasswordEx = null,
						CryptoKey = key,
						CryptoSalt = salt,
						Type = UserType.注册,
						State = State.默认,
						Ordinal = 0,
						Description = string.IsNullOrWhiteSpace(description) ? null : description
					};
					db.User.Add(user);
					db.SaveChanges();
					output.Data.Entity = user;
					output.Ok = true;
					return output;
				}
				output.Data.Message = "电子邮件已被注册";
				output.Data.Parameter = string.Empty;
				return output;
			}
			catch (Exception exception)
			{
				output.Data.Message = exception.Message;
				return output;
			}
		}

		protected void buttonRegister_OnClick(object sender, EventArgs e)
		{
			var file = string.Format("~/Common/头像/用户/{0}.png", Session[HomoryConstant.SessionRegisterId].ToString().ToUpper());
			if (!File.Exists(Server.MapPath((file))))
			{
				file = "~/Common/默认/用户.png";
			}
			var output = UserRegister(HomoryContext.Value, (Guid)Session[HomoryConstant.SessionRegisterId], userEmail.Value, userPassword.Value,
				userPasswordRepeat.Value, userName.Value, userRealName.Value, file, userDescription.Value);
			if (output.Ok)
			{
				Response.Redirect(Application["Sso"] + "Go/ToVerify", false);
				return;
			}
			string control = null;
			switch ((string)output.Data.Parameter)
			{
				case "email":
					control = userEmail.ClientID;
					break;
				case "password":
					control = userPassword.ClientID;
					break;
			}
			string script = string.Format("notify({0}, '{1}', 'error');",
				control == null ? "null" : "'#" + control + "'", output.Data.Message);
			areaAction.ResponseScripts.Add(script);
			Session.Remove(HomoryConstant.SessionRegisterId);
		}

		protected void buttonBack_OnClick(object sender, EventArgs e)
		{
			Session.Remove(HomoryConstant.SessionRegisterId);
			Response.Redirect(Application["Sso"] + "Go/SignOn", false);
		}
	}
}
