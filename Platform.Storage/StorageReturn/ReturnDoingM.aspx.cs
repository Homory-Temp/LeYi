using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class OutDoingM : SingleStoragePage
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
            var m = db.Value.StorageLend.Where(o => o.BorrowUserId == UserId && o.Returned == false && o.ObjectId == ObjectId);
            try
            {
                in_amount.MaxValue = (double)(m.Sum(o => o.Amount) - m.Sum(o => o.ReturnedAmount));
            }
            catch
            {
                in_amount.MaxValue = 0;
            }
        }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected Guid UserId
    {
        get { return "UserId".Query().GlobalId(); }
    }

    protected void steps_ActiveStepChanged(object sender, EventArgs e)
    {
        steps.ActiveStepIndex = 1;
    }

    protected bool InRecord(out Guid cid)
    {
        if (in_amount.MissingValue("请输入数量")) { in_amount.Text = string.Empty; cid = Guid.Empty; return false; }
        if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; cid = Guid.Empty; return false; }
        cid = db.Value.SetReturnM(ObjectId, UserId, CurrentUser, in_amount.Value(0.00M), in_note.Text);
        if (cid == Guid.Empty) { in_amount.Text = string.Empty; var objectId = ObjectId; var obj = db.Value.StorageObject.Single(o => o.Id == objectId); in_amount.MaxValue = (double)obj.InAmount; cid = Guid.Empty; return false; }
        db.Value.StorageSave();
        return true;
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Guid cid;
        if (!InRecord(out cid)) return;
        var objectId = ObjectId;
        var obj = db.Value.StorageObject.Single(o => o.Id == objectId);
        Response.Redirect("~/StorageReturn/ReturnDone{3}?StorageId={0}&ObjectId={1}&ReturnId={2}&UserId={4}".Formatted(StorageId, ObjectId, cid, obj.Single ? "S" : "M", UserId));
    }

    protected void borrow_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        borrow.Source(db.Value.StorageLend.Where(o => o.BorrowUserId == UserId && o.Returned == false && o.ObjectId == ObjectId).ToList());
    }
}
