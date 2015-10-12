using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Go
{
    public partial class GoSignOff : HomoryPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
                this.Title = doc.Root.Element("Sso").Value;
                UserSignOff(HomoryContext.Value);
				var query = Request.QueryString["SsoRedirect"];
				if (string.IsNullOrWhiteSpace(query))
				{
					Response.Redirect(Application["Sso"] + "Go/SignOn", false);
				}
				else
				{
					string url;
					if (query.IndexOf('&') <= 0)
						url = Server.UrlDecode(query);
					else
					{
						var index = query.IndexOf('&');
						var path = Server.UrlDecode(Request.QueryString["SsoRedirect"]);
                        url = string.Format("{0}?{1}", path, query.Substring(index + 1));
                    }
					Response.Redirect(string.IsNullOrWhiteSpace(url) ? Application["Sso"] + "Go/SignOn" : url, false);
				}
			}
		}

		public bool UserSignOff(Entities db)
		{
            try
			{
                var oid = string.Empty;
				if (Session[HomoryConstant.SessionOnlineId] != null)
				{
					oid = Session[HomoryConstant.SessionOnlineId].ToString();
				}
				else if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
				{
					var httpCookie = Request.Cookies[HomoryConstant.CookieOnlineId];
					if (httpCookie != null) oid = httpCookie.Value;
				}
				if (string.IsNullOrEmpty(oid))
				{
					return true;
				}
				var onlineGuid = Guid.Parse(oid);
				db.UserOnline.Where(o => o.Id == onlineGuid).Delete();
				db.SaveChanges();
				Session.Remove(HomoryConstant.SessionOnlineId);
				if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
				{
					var cookie = Request.Cookies[HomoryConstant.CookieOnlineId];
					if (cookie != null)
					{
						cookie.Expires = DateTime.Now.AddSeconds(-1);
						HttpContext.Current.Response.SetCookie(cookie);
					}
				}
                return true;
			}
            catch
            {
                return false;
            }
		}
	}
}
