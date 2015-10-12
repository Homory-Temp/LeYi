using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class InTarget : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        obj_search.DataSource = db.Value.QueryStorageObject("", StorageId).ToList();
        if (!IsPostBack)
        {
            var targetId = TargetId;
            var target = db.Value.StorageTarget.Single(o => o.Id == targetId);
            target_id.Value = targetId.ToString();
            target_number.Text = target.Number;
            target_receipt.Text = target.ReceiptNumber;
            target_content.InnerHtml = target.Content;
            target_source.Text = target.OrderSource;
            target_target.Text = target.UsageTarget;
            target_toPay.Text = target.ToPay.Money();
            target_paid.Text = target.Paid.Money();
            target_day.Text = target.TimeNode.TimeNode();
            target_keeper.Text = target.KeepUserId.HasValue ? db.Value.UserName(target.KeepUserId.Value) : "无";
            target_brokerage.Text = target.BrokerageUserId.HasValue ? db.Value.UserName(target.BrokerageUserId.Value) : "无";
        }
    }

    protected void ins_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        ins.Source(db.Value.StorageTargetGetOne(TargetId).StorageIn.ToList());
    }

    protected void obj_search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        obj_search.DataSource = db.Value.QueryStorageObject(e.FilterString, StorageId);
        obj_view.Rebind();
    }

    protected Guid TargetId
    {
        get { return "TargetId".Query().GlobalId(); }
    }


    protected void new_obj_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}".Formatted(StorageId));
    }

    protected void obj_view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        obj_view.Source(db.Value.QueryStorageObject(obj_search.Text.Null() ? null : obj_search.Text, StorageId));
        TestIn();
    }

    protected void obj_set_Click(object sender, EventArgs e)
    {
        obj_id.Value = (sender as RadButton).Value;
        obj_view.Rebind();
        TestIn();
    }

    protected void obj_search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        obj_id.Value = string.Empty;
        obj_view.Rebind();
    }

    protected void TestIn()
    {
        if (obj_id.Value.Null())
            @in.Visible = false;
        else
            @in.Visible = true;
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageIn/InDoing?StorageId={0}&ObjectId={1}&TargetId={2}".Formatted(StorageId, obj_id.Value, TargetId));
    }
}
