using Models;
using System;
using System.Linq;
using System.Xml.Linq;
using Telerik.Web.UI;

public class StorePage : System.Web.UI.Page
{
    protected Lazy<StoreEntity> db = new Lazy<StoreEntity>(() => new StoreEntity());

    protected void Notify(RadAjaxPanel panel, string message, string type)
    {
        panel.ResponseScripts.Add(string.Format("notify(null, '{0}', '{1}');", message, type));
    }

    protected bool IsOnline
    {
        get { return Session["Store__UserId"] != null; }
    }

    protected Guid CurrentCampus
    {
        get
        {
            if (CurrentUser == Guid.Empty)
                return Guid.Empty;
            var user = db.Value.User.Single(o => o.Id == CurrentUser);
            var department = user.DepartmentUser.FirstOrDefault(o => (o.Type == -1 || o.Type == -4) && o.State < 2);
            if (department == null) return Guid.Empty;
            return department.TopDepartmentId;
        }
    }

    protected Guid CurrentUser
    {
        get
        {
            try
            {
                var id = (Guid)Session["Store__UserId"];
                return id;
            }
            catch
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
                    string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Store__"] = "Store__";
                Response.Redirect(url, true);
                return Guid.Empty;
            }
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        #region 
        var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
        Title = doc.Root.Element("Store").Value;
        if (!string.IsNullOrWhiteSpace("OnlineId".Query()))
        {
            var id = "OnlineId".Query().GlobalId();
            if (db.Value.UserOnline.Count(o => o.Id == id) == 0)
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOff", Server.UrlEncode(path),
                    string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Store__UserId"] = null;
                Session["Store__"] = null;
                Response.Redirect(url, false);
            }
            else
            {
                Session["Store__UserId"] = db.Value.UserOnline.Single(o => o.Id == id).UserId;
                base.OnLoad(e);
            }
        }
        if (IsOnline && Session["Store__"] != null)
        {
            base.OnLoad(e);
        }
        else
        {
            if (string.IsNullOrWhiteSpace("OnlineId".Query()))
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
                    string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Store__"] = "Store__";
                Response.Redirect(url, false);
            }
        }
        #endregion
    }

    protected bool Right_Create
    {
        get
        {
            var query = db.Value.Database.SqlQuery<User>("SELECT * FROM Store_Creator");
            var user = query.SingleOrDefault(o => o.Id == CurrentUser);
            return user != null;
        }
    }
}
