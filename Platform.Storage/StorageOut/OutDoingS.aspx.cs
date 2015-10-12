using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class OutDoingS : SingleStoragePage
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
            fixedArea.Visible = obj.Fixed;
            object_fixed_serial.Text = obj.FixedSerial;
            in_amount.MaxValue = (double)obj.InAmount;
            out_type.SourceBind(Enum.GetNames(typeof(OutType)).Select(o => new { Id = Enum.Parse(typeof(OutType), o), Name = o }).ToList());
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

    protected bool InRecord()
    {
        if (act_r.Checked && in_amount.MissingValue("请输入数量")) { in_amount.Text = string.Empty; return false; }
        var list = view.Items.Select(o => o.FindControl("c") as RadButton).ToList().Where(o => o.Checked == true).Select(o => int.Parse(o.Text)).ToList();
        if (act_s.Checked && list.Count == 0) { return false; }
        if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; return false; }
        bool cid;
        if (act_r.Checked)
            cid = db.Value.SetOutRandom(ObjectId, responsibleId.Value.Null() ? (Guid?)null : responsibleId.Value.GlobalId(), CurrentUser, (OutType)Enum.Parse(typeof(OutType), out_type.SelectedValue), out_reason.Text, in_amount.Value(0), in_note.Text);
        else
            cid = db.Value.SetOutSpecific(ObjectId, responsibleId.Value.Null() ? (Guid?)null : responsibleId.Value.GlobalId(), CurrentUser, (OutType)Enum.Parse(typeof(OutType), out_type.SelectedValue), out_reason.Text, list, in_note.Text);
        if (!cid) { in_amount.Text = string.Empty; var objectId = ObjectId; var obj = db.Value.StorageObject.Single(o => o.Id == objectId); in_amount.MaxValue = (double)obj.InAmount; return false; }
        db.Value.StorageSave();
        return true;
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!InRecord()) return;
        Response.Redirect("~/StorageObject/Object?StorageId={0}".Formatted(StorageId));
        //var objectId = ObjectId;
        //var obj = db.Value.StorageObject.Single(o => o.Id == objectId);
        //Response.Redirect("~/StorageOut/OutDone{3}?StorageId={0}&ObjectId={1}&OutId={2}".Formatted(StorageId, ObjectId, cid, obj.Single ? "S" : "M"));
    }

    protected void act_r_CheckedChanged(object sender, EventArgs e)
    {
        act_s.Checked = !act_r.Checked;
        r.Visible = act_r.Checked;
        s.Visible = act_s.Checked;
    }

    protected void act_s_CheckedChanged(object sender, EventArgs e)
    {
        act_r.Checked = !act_s.Checked;
        r.Visible = act_r.Checked;
        s.Visible = act_s.Checked;
    }

    protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        var objectId = ObjectId;
        var obj = db.Value.StorageObject.Single(o => o.Id == objectId);
        view.Source(obj.StorageInSingle.Where(o => o.In == true).Select(o => o.InOrdinal).OrderBy(o => o).ToList());
    }

    protected void s_ok_Click(object sender, EventArgs e)
    {
        var min = s_from.Value(0);
        var max = s_to.Value(0);
        if (min == 0 && max == 0)
            return;
        if (min == 0 || max == 0)
            min = max;
        int v;
        view.Items.Select(o => o.FindControl("c") as RadButton).ToList().ForEach(o =>
        {
            v = int.Parse(o.Text);
            if (v <= max && v >= min)
            {
                o.Checked = true;
            }
        });
    }

    protected void s_re_Click(object sender, EventArgs e)
    {
        var min = s_from.Value(0);
        var max = s_to.Value(0);
        if (min == 0 && max == 0)
            return;
        if (min == 0 || max == 0)
            min = max;
        int v;
        view.Items.Select(o => o.FindControl("c") as RadButton).ToList().ForEach(o =>
        {
            v = int.Parse(o.Text);
            if (v <= max && v >= min)
            {
                o.Checked = !o.Checked;
            }
        });
    }

    protected void s_cl_Click(object sender, EventArgs e)
    {
        var min = s_from.Value(0);
        var max = s_to.Value(0);
        if (min == 0 && max == 0)
            return;
        if (min == 0 || max == 0)
            min = max;
        int v;
        view.Items.Select(o => o.FindControl("c") as RadButton).ToList().ForEach(o =>
        {
            v = int.Parse(o.Text);
            if (v <= max && v >= min)
            {
                o.Checked = false;
            }
        });
    }
}
