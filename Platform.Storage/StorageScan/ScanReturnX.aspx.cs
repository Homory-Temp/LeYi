using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ScanReturnX : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_sourceX.DataSource = sourceX;
        if (!IsPostBack)
        {
            ScanOutList.Clear();
            ScanOutListSingle.Clear();
        }
        code.Focus();
    }

    protected void keeper_sourceX_DataSourceSelect(object sender, SearchBoxDataSourceSelectEventArgs e)
    {
        var source = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_sourceX.DataSource = source.Where(o => o.Name.ToLower().Contains(e.FilterString.ToLower()) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
    }

    protected void keeper_sourceX_Search(object sender, SearchBoxEventArgs e)
    {
        responsibleX.Text = e.Text;
        responsibleIdX.Value = e.Value;
        viewX.Rebind();
        apxx.RaisePostBackEvent("Do");
    }

    protected List<Guid> ScanOutList
    {
        get
        {
            if (Session["ScanReturnW"] == null)
                Session["ScanReturnW"] = new List<Guid>();
            return Session["ScanReturnW"] as List<Guid>;
        }
    }

    protected List<Guid> ScanOutListSingle
    {
        get
        {
            if (Session["ScanReturnD"] == null)
                Session["ScanReturnD"] = new List<Guid>();
            return Session["ScanReturnD"] as List<Guid>;
        }
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
                    ScanOutList.Add(id);
                    viewW.Rebind();
                    break;
                }
            case "D":
                {
                    ScanOutListSingle.Add(id);
                    viewD.Rebind();
                    break;
                }
        }
    }

    protected void viewW_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = ScanOutList.Join(db.Value.StorageObject, o => o, o => o.Id, (x, y) => y).ToList();
        viewW.Source(source);
    }

    protected void viewD_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = ScanOutListSingle.Join(db.Value.StorageInSingle, o => o, o => o.Id, (x, y) => y).ToList();
        viewD.Source(source);
    }

    protected void viewX_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        Guid? id = responsibleIdX.Value.Null() ? (Guid?)null : responsibleIdX.Value.GlobalId();
        if (id.HasValue)
        {
            var uid = id.Value;
            var source = db.Value.查询_借用单.Where(o => o.是否归还 == false && o.借用人标识 == uid).OrderByDescending(o => o.时间).ToList();
            viewX.DataSource = source;
        }
        else
            viewX.Source(null);
    }

    protected string GetAutoId(查询_借用单 sheet)
    {
        return db.Value.ToQR("W", db.Value.StorageObject.Single(o => o.Id == sheet.物品标识).AutoId);
    }

    protected string GetAutoId(StorageLendSingle sheet)
    {
        return db.Value.ToQR("D", sheet.StorageIn.StorageInSingle.Single(o => o.InOrdinal == sheet.Ordinal).AutoId);
    }

    protected void responsibleX_Load(object sender, EventArgs e)
    {
        responsibleX.Attributes["onclick"] = "responsible_selectingX();";
    }

    protected void apxx_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        viewX.Rebind();
    }

    protected void viewX_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        try
        {
            var s = e.Item.FindControl("s") as Repeater;
            var id = (e.Item as RadListViewDataItem).GetDataKeyValue("借用标识").ToString().GlobalId();
            var x = db.Value.StorageLend.Single(o => o.Id == id);
            if (x.StorageObject.Single)
            {
                var source = x.StorageLendSingle.Where(o => o.Returned == false).ToList();
                s.DataSource = source;
                s.DataBind();
            }
            else
            {
                s.DataSource = null;
                s.DataBind();
            }
        }
        catch
        {
        }
    }

    protected decimal CountBack(Guid id)
    {
        Guid? uid = responsibleIdX.Value.Null() ? (Guid?)null : responsibleIdX.Value.GlobalId();
        if (uid.HasValue)
        {
            var guid = uid.Value;
            return db.Value.查询_借用单.Where(o => o.是否归还 == false && o.借用人标识 == guid && o.物品标识 == id).Sum(o => o.待归还数).Value;
        }
        else
            return 0;
    }

    protected bool InRecord()
    {
        if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; return false; }
        apx.ResponseScripts.Add("scanDoX();");
        return true;
    }

    protected void out_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!InRecord()) return;
    }

    protected void ap_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (e.Argument == "Refresh")
        {
            viewX.Rebind();
            return;
        }
        Guid? uid = responsibleIdX.Value.Null() ? (Guid?)null : responsibleIdX.Value.GlobalId();
        if (!uid.HasValue) { return; }
        var guid = uid.Value;
        foreach (var item in viewD.Items)
        {
            var id = item.GetDataKeyValue("Id").ToString().GlobalId();
            var @single = db.Value.StorageInSingle.SingleOrDefault(o => o.Id == id);
            var list = new List<int>();
            list.Add(@single.InOrdinal);
            var note = (item.FindControl("note") as RadTextBox).Text;
            db.Value.SetReturnSpecific(@single.ObjectId, guid, CurrentUser, list, note ?? string.Empty);
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
                db.Value.SetReturnRandom(obj.Id, guid, CurrentUser, (int)amount, note ?? string.Empty);
            }
            else
            {
                db.Value.SetReturnM(obj.Id, guid, CurrentUser, amount, note ?? string.Empty);
            }
            db.Value.StorageSave();
        }
        ScanOutList.Clear();
        ScanOutListSingle.Clear();
        Response.Redirect("~/StorageQuery/QueryReturn?StorageId={0}".Formatted(StorageId));
    }
}
