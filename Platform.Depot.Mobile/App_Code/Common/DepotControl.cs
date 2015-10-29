using Models;
using System;
using System.Linq;
using Telerik.Web.UI;

public class DepotControl : System.Web.UI.UserControl, IDepot
{
    private Lazy<DepotEntities> db = new Lazy<DepotEntities>(() => new DepotEntities());

    protected override void OnLoad(EventArgs e)
    {
        if (Session["DepotUser"].None())
        {
            Response.Redirect("~/Default");
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
                Response.Redirect("~/Default");
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
