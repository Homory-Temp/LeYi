using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class OutDoneM : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            fixedArea.Visible = obj.Fixed;
            object_fixed_serial.Text = obj.FixedSerial;
            var outId = OutId;
            var @out = db.Value.StorageOut.Single(o => o.Id == outId);
            consumer.Text = db.Value.UserName(@out.OutUserId.HasValue ? @out.OutUserId.Value : CurrentUser).ToString();
            consume_totalAmount.Text = @out.Amount.ToString();
            consume_totalMoney.Text = @out.TotalMoney.Money();
            day.Text = @out.TimeNode.TimeNode();
            note.Text = @out.Note;
            type.Text = @out.Type.ToString();
            reason.Text = @out.Reason;
        }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected Guid OutId
    {
        get { return "OutId".Query().GlobalId(); }
    }

    protected void in_obj_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageOut/OutDoingM?StorageId={0}&ObjectId={1}".Formatted(StorageId, ObjectId));
    }

    protected void in_back_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}".Formatted(StorageId));
    }

    protected void in_other_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}&UserId={1}".Formatted(StorageId, "UserId".Query()));
    }
}
