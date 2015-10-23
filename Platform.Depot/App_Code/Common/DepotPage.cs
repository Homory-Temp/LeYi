using Models;
using System;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI;

public class DepotPage : System.Web.UI.Page, IDepot
{
    private Lazy<DepotEntities> db = new Lazy<DepotEntities>(() => new DepotEntities());

    protected override void OnLoad(EventArgs e)
    {
        var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
        Title = doc.Root.Element("Depot").Value;
        if (!string.IsNullOrWhiteSpace(Request.QueryString["OnlineId"]))
        {
            var id = Guid.Parse(Request.QueryString["OnlineId"]);
            if (db.Value.UserOnline.Count(o => o.Id == id) == 0)
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}Go/SignOff?SsoRedirect={1}{2}{3}", Application["Sso"], Server.UrlEncode(path), string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["DepotUser"] = null;
                Session["Depot"] = null;
                Response.Redirect(url, false);
            }
            else
            {
                var userId = db.Value.UserOnline.First(o => o.Id == id).UserId;
                Session["DepotUser"] = db.Value.DepotUser.Single(o => o.Id == userId);
                base.OnLoad(e);
            }
        }
        if (DepotOnline && Session["Depot"] != null)
        {
            base.OnLoad(e);
        }
        else
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["OnlineId"]))
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}Go/SignOn?SsoRedirect={1}{2}{3}", Application["Sso"], Server.UrlEncode(path), string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Depot"] = "Depot";
                Response.Redirect(url, false);
            }
        }
    }

    public DepotEntities DataContext
    {
        get
        {
            return db.Value;
        }
    }

    public bool DepotOnline
    {
        get
        {
            return Session["DepotUser"] != null;
        }
    }

    public DepotUser DepotUser
    {
        get
        {
            try
            {
                if (Session["DepotUser"].None())
                    throw new Exception();
                return (DepotUser)Session["DepotUser"];
            }
            catch
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}Go/SignOn?SsoRedirect={1}{2}{3}", Application["Sso"], Server.UrlEncode(path), string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Depot"] = "Depot";
                Response.Redirect(url, true);
                return null;
            }
        }
    }

    public bool RightCreate
    {
        get
        {
            var userId = DepotUser.Id;
            return db.Value.DepotCreator.Count(o => o.Id == userId) > 0;
        }
    }

    private void Notify(RadAjaxPanel panel, string message, string type)
    {
        panel.ResponseScripts.Add(string.Format("notify(null, '{0}', '{1}');", message, type));
    }

    public void NotifyOK(RadAjaxPanel panel, string message)
    {
        Notify(panel, message, "success");
    }

    public void NotifyError(RadAjaxPanel panel, string message)
    {
        Notify(panel, message, "error");
    }
}
