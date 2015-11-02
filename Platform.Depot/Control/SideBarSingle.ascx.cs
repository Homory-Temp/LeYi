using System;
using System.Linq;

public partial class Control_SideBarSingle : DepotControlSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            u.InnerText = DepotUser.Name;
            storeName.Value = Depot.Name;
        }
    }

    protected void qb_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();
        var link = "{0}Go/Board".Formatted(Application["Sso"]);
        Response.Redirect(link);
    }

    protected bool HasWarn()
    {
        return DataContext.DepotObject.Count(o => ((o.Amount < o.Low && o.Low > 0) || (o.Amount > o.High && o.High > 0)) && o.State < Models.State.停用) > 0;
    }

    public string Crumb
    {
        get
        {
            return crumb.InnerText;
        }
        set
        {
            crumb.InnerText = value;
        }
    }

    public bool NoCrumb
    {
        get
        {
            return !crumb.Visible;
        }
        set
        {
            crumb.Visible = !value;
        }
    }

    protected void storeName_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/DepotHome?DepotId={0}".Formatted(Depot.Id));
    }
}
