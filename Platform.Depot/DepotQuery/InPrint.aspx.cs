﻿using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class DepotQuery_InPrint : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var orderId = "OrderId".Query().GlobalId();
            var ord = DataContext.DepotInRecord.Single(o => o.Id == orderId);
            var oo = DataContext.DepotOrder.Single(o => o.Id == orderId);
            var campusId = Depot.CampusId;
            campus.InnerText = DataContext.Department.Single(o => o.Id == campusId).Name;
            time.InnerText = ord.RecordTime.ToDay();
            orderNo.InnerText = oo.Name;
            order.Value = ord.购置来源;
            keep.Value = ord.保管人;
            brokerage.Value = ord.经手人;
        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var orderId = "OrderId".Query().GlobalId();
        var ord = DataContext.DepotInRecord.Single(o => o.Id == orderId);
        var source = DataContext.DepotInXRecord.Where(o => o.OrderId == orderId).OrderByDescending(o => o.Time).ToList();
        view_record.DataSource = source;
        var s = source.Sum(o => o.Total);
        var oo = DataContext.DepotOrder.Single(o => o.Id == orderId);
        oo.Paid = s;
        DataContext.SaveChanges();
        total.Value = source.Sum(o => o.Amount).ToAmount(Depot.Featured(DepotType.小数数量库)) + "@@@" + s.ToMoney();
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../DepotQuery/In?DepotId={0}".Formatted(Depot.Id));
    }
}
