using Homory.Model;
using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Go
{
	public partial class GoSignApi : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
            this.Title = doc.Root.Element("Sso").Value;
            var id = string.Empty;
            if (Request.QueryString.AllKeys.Contains("OnlineId"))
            {
                id = Request.QueryString["OnlineId"];
            }
			if (Session[HomoryConstant.SessionOnlineId] != null)
				id = Session[HomoryConstant.SessionOnlineId].ToString();
			else if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
			{
				var httpCookie = Request.Cookies[HomoryConstant.CookieOnlineId];
				if (httpCookie != null)
				{
					var value = httpCookie.Value;
					HttpContext.Current.Session[HomoryConstant.SessionOnlineId] = Guid.Parse(value);
					id = value;
				}
			}


			var path = Server.UrlDecode(Request.QueryString["SsoRedirect"]);
			var query = Request.QueryString.ToString();
			string url;
            if (query.IndexOf("OnlineId=") >= 0)
            {
                url = path;
            }
            else
            {
				if (string.IsNullOrEmpty(id))
				{
					url = path;
				}
				else
				{
					if (query.IndexOf('&') <= 0)
					{
						url = string.Format("{0}?OnlineId={1}", path, id);
					}
					else
					{
						var index = query.IndexOf('&');
						url = string.Format("{0}?OnlineId={1}{2}", path, id, query.Substring(index));
					}
				}
            }
			Response.Redirect(url, false);
		}
	}
}
