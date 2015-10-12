using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class ReturnDoneM : SingleStoragePage
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
        }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected Guid ReturnId
    {
        get { return "ReturnId".Query().GlobalId(); }
    }

    protected Guid UserId
    {
        get { return "UserId".Query().GlobalId(); }
    }

    protected void steps_ActiveStepChanged(object sender, EventArgs e)
    {
        steps.ActiveStepIndex = 2;
    }

    protected void in_back_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}".Formatted(StorageId));
    }

    protected void in_other_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}&UserId={1}".Formatted(StorageId, "UserId".Query()));
    }

    protected void borrow_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        borrow.Source(db.Value.StorageReturn.Where(o => o.Id == ReturnId).ToList());
    }
}
