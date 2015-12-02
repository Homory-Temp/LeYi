using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class DepotQuery_InPrint : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "报废流程";
        }
    }

    //protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    //{
    //    var orderId = "OrderId".Query().GlobalId();
    //    var ord = DataContext.DepotOrder.Single(o => o.Id == orderId).MainID;
    //    view.DataSource = DataContext.C__DepotOrderContent.Where(o => o.MainID == ord);
    //}

    protected void viewx_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var id = "MainID".Query();
        viewx.DataSource = DataContext.C__DepotOrderFlow.Where(o => o.MainID == id);
    }
}
