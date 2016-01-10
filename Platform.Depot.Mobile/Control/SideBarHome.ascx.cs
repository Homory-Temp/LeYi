using System;

public partial class Control_SideBarHome : DepotControlSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            u.InnerText = DepotUser.Name;
        }
    }

    protected void qb_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();
        var link = "~/Default.aspx";
        Response.Redirect(link);
    }

    protected void storeName_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/DepotHome.aspx?DepotId={0}".Formatted(Depot.Id));
    }
}
