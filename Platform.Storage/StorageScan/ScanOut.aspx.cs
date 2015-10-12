using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class ScanOut : SingleStoragePage
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
            out_type.SourceBind(Enum.GetNames(typeof(OutType)).Select(o => new { Id = Enum.Parse(typeof(OutType), o), Name = o }).ToList());
            ScanOutList.Clear();
            ScanOutListSingle.Clear();
        }
        code.Focus();
    }

    protected Dictionary<Guid, int> ScanOutList
    {
        get
        {
            if (Session["ScanUseW"] == null)
                Session["ScanUseW"] = new Dictionary<Guid, int>();
            return Session["ScanUseW"] as Dictionary<Guid, int>;
        }
    }

    protected Dictionary<Guid, int> ScanOutListSingle
    {
        get
        {
            if (Session["ScanUseD"] == null)
                Session["ScanUseD"] = new Dictionary<Guid, int>();
            return Session["ScanUseD"] as Dictionary<Guid, int>;
        }
    }

    protected int WAmount(Guid id)
    {
        return ScanOutList[id];
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var c = code.Text;
        code.Text = string.Empty;
        if (c.Length != 12)
            return;
        string type;
        Guid id;
        db.Value.FromQR(c, out type, out id);
        switch (type)
        {
            case "W":
                {
                    if (ScanOutList.ContainsKey(id))
                        ScanOutList[id] += 1;
                    else
                        ScanOutList.Add(id, 1);
                    viewW.Rebind();
                    break;
                }
            case "D":
                {
                    if (ScanOutListSingle.ContainsKey(id))
                        ScanOutListSingle[id] += 1;
                    else
                        ScanOutListSingle.Add(id, 1);
                    viewD.Rebind();
                    break;
                }
        }
    }

    protected void viewW_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = ScanOutList.Keys.ToList().Join(db.Value.StorageObject, o => o, o => o.Id, (x, y) => y).ToList();
        viewW.Source(source);
    }

    protected void viewD_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = ScanOutListSingle.Keys.ToList().Join(db.Value.StorageInSingle, o => o, o => o.Id, (x, y) => y).ToList();
        viewD.Source(source);
    }

    protected void responsible_Load(object sender, EventArgs e)
    {
        responsible.Attributes["onclick"] = "responsible_selecting();";
    }

    protected bool InRecord()
    {
        if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; return false; }
        apx.ResponseScripts.Add("scanDo();");
        return true;
    }

    protected void out_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!InRecord()) return;
    }

    protected void ap_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {
        foreach (var item in viewD.Items)
        {
            var id = item.GetDataKeyValue("Id").ToString().GlobalId();
            var @single = db.Value.StorageInSingle.SingleOrDefault(o=>o.Id==id);
            var amountCtrl = (item.FindControl("amount") as RadNumericTextBox);
            var amount = amountCtrl.Value.HasValue ? (decimal)amountCtrl.Value.Value : 0;
            if (amount == 0)
                continue;
            var list =new List<int>();
            list.Add(@single.InOrdinal);
            var note = (item.FindControl("note") as RadTextBox).Text;
            db.Value.SetOutSpecific(@single.ObjectId, responsibleId.Value.Null() ? (Guid?)null : responsibleId.Value.GlobalId(), CurrentUser, (OutType)Enum.Parse(typeof(OutType), out_type.SelectedValue), out_reason.Text, list, note ?? string.Empty);
            db.Value.StorageSave();
        }
        foreach (var item in viewW.Items)
        {
            var id = item.GetDataKeyValue("Id").ToString().GlobalId();
            var obj = db.Value.StorageObject.SingleOrDefault(o => o.Id == id);
            var amountCtrl = (item.FindControl("amount") as RadNumericTextBox);
            var amount = amountCtrl.Value.HasValue ? (decimal)amountCtrl.Value.Value : 0;
            if (amount == 0)
                continue;
            var note = (item.FindControl("note") as RadTextBox).Text;
            if (obj.Single)
            {
                db.Value.SetOutRandom(obj.Id, responsibleId.Value.Null() ? (Guid?)null : responsibleId.Value.GlobalId(), CurrentUser, (OutType)Enum.Parse(typeof(OutType), out_type.SelectedValue), out_reason.Text, (int)amount, note ?? string.Empty);
            }
            else
            {
                db.Value.SetOutM(obj.Id, responsibleId.Value.Null() ? (Guid?)null : responsibleId.Value.GlobalId(), CurrentUser, (OutType)Enum.Parse(typeof(OutType), out_type.SelectedValue), out_reason.Text, amount, note ?? string.Empty);
            }
            db.Value.StorageSave();
        }
        ScanOutList.Clear();
        ScanOutListSingle.Clear();
        Response.Redirect("~/StorageQuery/QueryOut?StorageId={0}".Formatted(StorageId));
    }
}
