using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class ConsumeDone : SingleStoragePage
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
            var consumeId = ConsumeId;
            var consume = db.Value.StorageConsume.Single(o => o.Id == consumeId);
            consumer.Text = db.Value.UserName(consume.ConsumeUserId.HasValue ? consume.ConsumeUserId.Value : CurrentUser).ToString();
            consume_totalAmount.Text = consume.Amount.ToString();
            consume_totalMoney.Text = consume.TotalMoney.Money();
            day.Text = consume.TimeNode.TimeNode();
            note.Text = consume.Note;
        }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected Guid ConsumeId
    {
        get { return "ConsumeId".Query().GlobalId(); }
    }

    protected void in_obj_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageConsume/ConsumeDoing?StorageId={0}&ObjectId={1}".Formatted(StorageId, ObjectId));
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
