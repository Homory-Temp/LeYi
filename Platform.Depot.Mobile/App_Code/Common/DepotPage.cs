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
        var doc = XDocument.Load(Server.MapPath("~/Common/配置/Title.xml"));
        Title = doc.Root.Element("Depot").Value;
        if (Session["DepotUserMobile"].None())
        {
            Response.Redirect("~/Default.aspx");
            return;
        }
        else
        {
            base.OnLoad(e);
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
            return Session["DepotUserMobile"] != null;
        }
    }

    public DepotUser DepotUser
    {
        get
        {
            try
            {
                if (Session["DepotUserMobile"].None())
                    throw new Exception();
                return (DepotUser)Session["DepotUserMobile"];
            }
            catch
            {
                Response.Redirect("~/Default.aspx");
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
