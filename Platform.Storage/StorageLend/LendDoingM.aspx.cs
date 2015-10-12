using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class LendDoingM : SingleStoragePage
{
    protected void keeper_del_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        responsible.Text = "";
        responsibleId.Value = "";
        keeper_source.Text = "";
        keeper_del.Visible = false;
    }

    protected void keeper_source_DataSourceSelect(object sender, SearchBoxDataSourceSelectEventArgs e)
    {
        var source = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_source.DataSource = source.Where(o => o.Name.ToLower().Contains(e.FilterString.ToLower()) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
    }

    protected void keeper_source_Search(object sender, SearchBoxEventArgs e)
    {
        responsible.Text = e.Text;
        responsibleId.Value = e.Value;
        keeper_del.Visible = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_source.DataSource = sourceX;
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
           // fixedArea.Visible = obj.Fixed;
            object_fixed_serial.Text = obj.FixedSerial;
            in_amount.MaxValue = (double)obj.InAmount;
            if (!"UserId".Query().Null())
            {
                responsible.Text = db.Value.UserName("UserId".Query().GlobalId());
                responsibleId.Value = "UserId".Query();
                keeper_source.Text = responsible.Text;
                keeper_del.Visible = true;
            }
        }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected void responsible_Load(object sender, EventArgs e)
    {
        responsible.Attributes["onclick"] = "responsible_selecting();";
    }

    protected bool InRecord(out Guid cid)
    {
        if (in_amount.MissingValue("请输入数量")) { in_amount.Text = string.Empty; cid = Guid.Empty; return false; }
        if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; cid = Guid.Empty; return false; }
        cid = db.Value.SetLendM(ObjectId, responsibleId.Value.Null() ? (Guid?)null : responsibleId.Value.GlobalId(), CurrentUser, in_amount.Value(0.00M), in_note.Text);
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
        Response.Redirect("~/StorageLend/LendDone{3}?StorageId={0}&ObjectId={1}&LendId={2}".Formatted(StorageId, ObjectId, cid, obj.Single ? "S" : "M"));
    }
}
