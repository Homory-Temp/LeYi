using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class InDoing : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
            var objectId = ObjectId;
            var obj = db.Value.StorageObject.Single(o => o.Id == objectId);
            object_id.Value = obj.ToString();
            object_name.Text = obj.Name;
            object_catalog.Text = obj.GeneratePath();
            object_unit.Text = obj.Unit;
            object_specification.Text = obj.Specification;
            object_fixed.Visible = obj.Fixed;
            object_consumable.Visible = obj.Consumable;
            object_inAmount.Text = obj.InAmount.ToString();
            object_low.Visible = obj.Low > 0 && obj.InAmount < obj.Low;
            object_high.Visible = obj.High > 0 && obj.InAmount > obj.High;
           // fixedArea.Visible = obj.Fixed;
            object_fixed_serial.Text = obj.FixedSerial;
            if (target.In)
            {
                in_tar.Visible = false;
                in_end.Visible = false;
                confirm.Visible = false;
            }
        }
    }

    protected Guid TargetId
    {
        get { return "TargetId".Query().GlobalId(); }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected void ins_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        ins.Source(db.Value.StorageTargetGetOne(TargetId).StorageIn.ToList());
    }

    protected void in_obj_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageIn/InObject?StorageId={0}&ObjectId={1}".Formatted(StorageId, ObjectId));
    }

    protected void in_tar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageIn/InTarget?StorageId={0}&TargetId={1}".Formatted(StorageId, TargetId));
    }

    protected void in_end_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!confirm.Checked) { confirm.ForeColor = Color.Red; return; }
        var t = db.Value.StorageTargetGetOne(TargetId);
        t.In = true;
        db.Value.StorageSave();
        if (print.Checked)
            Response.Redirect("~/StorageTarget/TargetIn?StorageId={0}&TargetId={1}".Formatted(StorageId, t.Id));
        else
            Response.Redirect("~/StorageObject/Object?StorageId={0}".Formatted(StorageId));
    }

    protected void in_back_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageHome/Home?StorageId={0}".Formatted(StorageId));
    }

    protected void confirm_CheckedChanged(object sender, EventArgs e)
    {
        print.Visible = confirm.Checked;
    }
}
