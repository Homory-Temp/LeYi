using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class ReturnDoingS : SingleStoragePage
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
        if (act_r.Checked && in_amount.MissingValue("请输入数量")) { in_amount.Text = string.Empty; cid = Guid.Empty; return false; }
        var list = view.Items.Select(o => o.FindControl("c") as RadButton).ToList().Where(o => o.Checked == true).Select(o => int.Parse(o.Text)).ToList();
        if (act_s.Checked && list.Count == 0) { cid = Guid.Empty; return false; }
        if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; cid = Guid.Empty; return false; }
        if (act_r.Checked)
            cid = db.Value.SetReturnRandom(ObjectId, UserId, CurrentUser, in_amount.Value(0), in_note.Text);
        else
            cid = db.Value.SetReturnSpecific(ObjectId, UserId, CurrentUser, list, in_note.Text);
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
        var source = db.Value.StorageLend.Where(o => o.BorrowUserId == UserId && o.Returned == false && o.ObjectId == ObjectId).Select(o => o.StorageLendSingle.Where(p => p.Returned == false)).ToList();
        var ids = new List<int>();
        foreach (var x in source)
        {
            ids.AddRange(x.Select(o => o.Ordinal).ToList());
        }
        view.Source(ids);
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

    protected void borrow_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        borrow.Source(db.Value.StorageLend.Where(o => o.BorrowUserId == UserId && o.Returned == false && o.ObjectId == ObjectId).ToList());
    }
}
