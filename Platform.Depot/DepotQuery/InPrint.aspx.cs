using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_InPrint : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var orderId = "OrderId".Query().GlobalId();
            var ord = DataContext.DepotInRecord.Single(o => o.Id == orderId);
            var campusId = Depot.CampusId;
            campus.InnerText = DataContext.Department.Single(o => o.Id == campusId).Name;
            time.InnerText = ord.RecordTime.ToDay();
            order.Value = ord.购置来源;
            total.Value = ord.应付金额.ToMoney();
            keep.Value = ord.保管人;
            brokerage.Value = ord.经手人;
        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var orderId = "OrderId".Query().GlobalId();
        view_record.DataSource = DataContext.DepotInXRecord.Where(o => o.OrderId == orderId).OrderByDescending(o => o.Time).ToList();
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../DepotQuery/In?DepotId={0}".Formatted(Depot.Id));
    }
}
