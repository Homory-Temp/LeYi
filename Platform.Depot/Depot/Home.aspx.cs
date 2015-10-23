using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Depot_Home : DepotPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            creating.Visible = RightCreate;
        }
    }

    protected bool CanVisit(Guid depotId)
    {
        return DataContext.DepotMember.Count(o => o.Id == DepotUser.Id && o.DepotId == depotId) > 0;
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/HomeAdd");
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view.DataSource = DataContext.Depot.Where(o => o.State < State.停用).OrderBy(o => o.Ordinal).ToList();
    }
}
