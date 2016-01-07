using System;

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
        var link = "~/Default.aspx";
        Response.Redirect(link);
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
        Response.Redirect("~/Depot/DepotHome.aspx?DepotId={0}".Formatted(Depot.Id));
    }
}
