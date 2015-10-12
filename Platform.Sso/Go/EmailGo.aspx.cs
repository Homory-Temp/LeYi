using Homory.Model;
using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

public partial class Go_EmailGo : HomoryPage
{
	private const string SignKey = "a8ff59a1fa19608ba0830b79";

    protected void Page_Load(object sender, EventArgs e)
    {
        var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
        this.Title = doc.Root.Element("Sso").Value;
        User u;
		if (IsSsoOnline(out u))
		{
			var id = u.Account;
			Response.Redirect(string.Format("EmailGo".FromWebConfig(), Server.UrlEncode(u.Account), u.Account.Equals("admin", StringComparison.InvariantCultureIgnoreCase) ? "xsfx3450" : "EmailPassword".FromWebConfig()), false);
			return;
		}
		else
		{
			SignOn();
			return;
		}
    }

	protected void SignOn()
	{
		var path = Request.Url.AbsoluteUri;
		if (path.IndexOf('?') > 0)
			path = path.Substring(0, path.IndexOf('?'));
		var query = Request.QueryString.ToString();
		var url = string.Format("{0}?{1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
			string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
		Response.Redirect(url, false);
	}

	protected bool IsSsoOnline(out User user)
	{
		try
		{
			var onlineStringId = string.Empty;
			if (Session[HomoryConstant.SessionOnlineId] != null)
				onlineStringId = Session[HomoryConstant.SessionOnlineId].ToString();
			else if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
			{
				var httpCookie = Request.Cookies[HomoryConstant.CookieOnlineId];
				if (httpCookie != null)
				{
					var value = httpCookie.Value;
					HttpContext.Current.Session[HomoryConstant.SessionOnlineId] = Guid.Parse(value);
					onlineStringId = value;
				}
			}
			if (string.IsNullOrEmpty(onlineStringId))
			{
				user = null;
				return false;
			}
			var onlineId = Guid.Parse(onlineStringId);
			var online = HomoryContext.Value.UserOnline.SingleOrDefault(o => o.Id == onlineId);
			if (online == null)
			{
				Session.Remove(HomoryConstant.SessionOnlineId);
				if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
				{
					var cookie = Request.Cookies[HomoryConstant.CookieOnlineId];
					if (cookie != null)
					{
						cookie.Expires = DateTime.Now.AddSeconds(-1);
						Response.SetCookie(cookie);
					}
				}
				user = null;
				return false;
			}
			else
			{
				var cookie = new HttpCookie(HomoryConstant.CookieOnlineId, online.Id.ToString().ToUpper());
				var expire = int.Parse(HomoryContext.Value.ApplicationPolicy.Single(o => o.Name == "UserCookieExpire" && o.ApplicationId == Guid.Empty).Value);
				cookie.Expires = DateTime.Now.AddMinutes(expire);
				HttpContext.Current.Response.SetCookie(cookie);
				online.TimeStamp = DateTime.Now;
				HomoryContext.Value.SaveChanges();
			}
			user = online.User;
			return true;
		}
		catch
		{
			user = null;
			return false;
		}
	}
}