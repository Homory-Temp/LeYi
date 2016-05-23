using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public class SsoPage : System.Web.UI.Page
{
    protected Lazy<Entities> db = new Lazy<Entities>(() => new Entities());

    protected override void OnLoad(EventArgs e)
    {
        try
        {
            if (Session["HousingId"] == null)
            {
                if (string.IsNullOrEmpty(Request.QueryString["OnlineId"]))
                {
                    var url = string.Format("{0}?SsoRedirect={1}", WebConfigurationManager.AppSettings["SsoUrl"], Server.UrlEncode(Request.Url.OriginalString));
                    Response.Redirect(url, false);
                    return;
                }
                var onlineId = Guid.Parse(Request.QueryString["OnlineId"]);
                var obj = db.Value.Housing_Member.FirstOrDefault(m => m.OnlineId == onlineId);
                if (obj == null)
                {
                    Response.Redirect("https://www.baidu.com/");
                    return;
                }
                Session["HousingId"] = obj.DepartmentId;
                Session["MemberId"] = obj.UserId;
                Session["MemberName"] = db.Value.User.SingleOrDefault(o => o.Id == obj.UserId).RealName;
            }
        }
        catch
        {
            Response.Redirect("https://www.baidu.com/");
            return;
        }
        base.OnLoad(e);
    }
}

public static class Extensions
{
    public static string V(this object value)
    {
        return value == null ? "" : value.ToString();
    }
}
