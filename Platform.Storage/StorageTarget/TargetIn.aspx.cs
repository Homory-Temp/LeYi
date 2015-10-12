using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class TargetIn : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var targetId = TargetId;
            var target = db.Value.StorageTarget.Single(o => o.Id == targetId);
            //target_id.Value = targetId.ToString();
            //target_number.Text = target.Number;
            //target_receipt.Text = target.ReceiptNumber;
            //target_content.InnerHtml = target.Content;
            //target_source.Text = target.OrderSource;
            target_target.Text = target.UsageTarget;
            //target_toPay.Text = target.ToPay.Money();
            //target_paid.Text = target.Paid.Money();
            target_day.Text = target.TimeNode.TimeNode();
            target_keeper.Text = target.KeepUserId.HasValue ? db.Value.UserName(target.KeepUserId.Value) : "无";
            target_brokerage.Text = target.BrokerageUserId.HasValue ? db.Value.UserName(target.BrokerageUserId.Value) : "无";
            c.Text = db.Value.Department.Single(o => o.Id == CurrentCampus).Name;
        }
    }

    protected Guid TargetId
    {
        get { return "TargetId".Query().GlobalId(); }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = db.Value.查询_入库单.Where(o => o.购置标识 == TargetId).OrderByDescending(o => o.日期).ToList();
        view.DataSource = source;
        if (source.Count == 0)
        {
            t.Text = "0.00";
        }
        else
        {
            decimal x = 0;
            foreach (var item in source)
            {
                x += Math.Round(item.合计, 2, MidpointRounding.AwayFromZero);
            }
            t.Text = x.Money();
        }
    }
}
