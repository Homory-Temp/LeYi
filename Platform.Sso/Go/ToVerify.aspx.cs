using Homory.Model;
using Homory.Utility;
using System;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace Go
{
	public partial class GoToVerify : HomoryPage
	{
		public const string ToVerifyHead = "{0}用户注册认证";
		public const string ToVerifyBody = "尊敬的用户\"{0}\"，您好：<br />&nbsp;&nbsp;&nbsp;&nbsp;您在{3}平台注册了账号，请确认是您本人进行的操作。如确认无误，请点击此链接<a href=\"{1}?{2}\">{1}?{2}</a>进行认证；链接若无法打开，请复制并粘贴至浏览器地址栏访问；如非本人操作，请删除此邮件。";

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
			if (Session[HomoryConstant.SessionRegisterId] == null)
			{
				Response.Redirect(Application["Sso"] + "Go/SignOn", false);
				return;
			}
			var output = UserGetInternal(HomoryContext.Value, (Guid)Session[HomoryConstant.SessionRegisterId]);
			if (output.Ok) return;
			Response.Redirect(Application["Sso"] + "Go/Register", false);
		}

		public dynamic UserGetInternal(Entities db, Guid id)
		{
			dynamic output = new ExpandoObject();
			output.Ok = false;
			output.Data = new ExpandoObject();
			try
			{
				var user = db.User.SingleOrDefault(o => o.Id == id && o.State < State.删除);
				if (user == null)
				{
					output.Data.Message = "用户不存在";
					return output;
				}
				output.Data.Entity = user;
				output.Ok = true;
				return output;
			}
			catch (Exception exception)
			{
				output.Data.Message = exception.Message;
				return output;
			}
		}

		protected void toSend_OnClick(object sender, EventArgs e)
		{
			var output = UserToVerify(HomoryContext.Value, (Guid)Session[HomoryConstant.SessionRegisterId]);
			if (!output.Ok)
			{
				var script = string.Format("notify({0}, '{1}', 'error');", "null", "验证邮件发送失败");
				areaAction.ResponseScripts.Add(script);
				return;
			}
			toSend.Visible = false;
			areaAction.ResponseScripts.Add("$('#tick').html('30'); $('#sent').css('display', 'block'); ticking();");
		}

		public dynamic UserToVerify(Entities db, Guid id)
		{
			dynamic output = new ExpandoObject();
			output.Ok = false;
			output.Data = new ExpandoObject();
			output.Data.Message = "验证邮件发送失败";
			try
			{
				var user = db.User.Single(o => o.Id == id);
				user.State = State.审核;
				db.SaveChanges();
				var sender =
					db.ApplicationPolicy.Single(o => o.Name == "SmtpSender" && o.ApplicationId == Guid.Empty).Value;
				var content = string.Format(ToVerifyBody, user.DisplayName,
                    Application["Sso"] + "Go/Verifying", user.Stamp, sender);
				output.Ok = Smtp.SendEmail(db, string.Format(ToVerifyHead, sender), user.DisplayName, content, user.Account, sender);
				return output;
			}
			catch
			{
				return output;
			}
		}

		protected void postRedo_OnClick(object sender, EventArgs e)
		{
			toSend.Visible = true;
			areaAction.ResponseScripts.Add("$('#sent').css('display', 'none');");
		}
	}
}
