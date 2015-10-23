using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_TargetPrint : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var targetId = "TargetId".Query().GlobalId();
            var target = db.Value.StoreTarget.Single(o => o.Id == targetId);
            campus.InnerText = db.Value.Department.Single(o => o.Id == CurrentCampus).Name;
            time.InnerText = target.TimeNode.FromTimeNode();
            order.Value = target.OrderSource;
            total.Value = target.Paid.ToMoney();
            keep.Value = target.KeepUserId.HasValue ? db.Value.GetUserName(target.KeepUserId.Value) : "";
            brokerage.Value = target.BrokerageUserId.HasValue ? db.Value.GetUserName(target.BrokerageUserId.Value) : "";
        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var targetId = "TargetId".Query().GlobalId();
        view_record.DataSource = db.Value.Store_RecordIn.Where(o => o.TargetId == targetId).OrderByDescending(o => o.入库日期).ToList();
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../StoreQuery/Target?StoreId={0}".Formatted(StoreId));
    }
}
