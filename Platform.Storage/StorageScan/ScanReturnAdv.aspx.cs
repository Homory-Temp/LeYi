using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ScanReturnAdv : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_sourceX.DataSource = sourceX;
        keeper_sourceX.Focus();
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
        viewW.Rebind();
        viewD.Rebind();
    }

    protected void viewW_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        Guid? id = responsibleIdX.Value.Null() ? (Guid?)null : responsibleIdX.Value.GlobalId();
        if (id.HasValue)
        {
            var uid = id.Value;
            var source = db.Value.查询_借用单.Where(o => o.是否归还 == false && o.借用人标识 == uid && o.仓库标识 == StorageId).OrderByDescending(o => o.时间).ToList().Join(db.Value.StorageObject.Where(o => o.Single == false), o => o.物品标识, o => o.Id, (x, y) => x).ToList();
            viewW.DataSource = source;
        }
        else
            viewW.Source(null);
    }

    protected void viewD_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        Guid? id = responsibleIdX.Value.Null() ? (Guid?)null : responsibleIdX.Value.GlobalId();
        if (id.HasValue)
        {
            var uid = id.Value;
            var source = db.Value.查询_借还流.Where(o => o.人员标识 == uid && o.仓库标识 == StorageId).OrderByDescending(o => o.时间).ToList();
            viewD.DataSource = source;
        }
        else
            viewD.Source(null);
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
        Guid? uid = responsibleIdX.Value.Null() ? (Guid?)null : responsibleIdX.Value.GlobalId();
        if (!uid.HasValue) { return false; }
        var guid = uid.Value;
        foreach (var item in viewD.Items)
        {
            var id = item.GetDataKeyValue("单借标识").ToString().GlobalId();
            var amountCtrl = (item.FindControl("amount") as RadNumericTextBox);
            var amount = amountCtrl.Value.HasValue ? (decimal)amountCtrl.Value.Value : 0;
            if (amount == 0)
                continue;
            var sheet = db.Value.查询_借还流.Single(o => o.单借标识 == id);
            var @single = db.Value.StorageInSingle.SingleOrDefault(o => o.Id == sheet.单入标识);
            var list = new List<int>();
            list.Add(@single.InOrdinal);
            var note = (item.FindControl("note") as RadTextBox).Text;
            db.Value.SetReturnSpecific(@single.ObjectId, guid, CurrentUser, list, note ?? string.Empty);
            db.Value.StorageSave();
            var amountCtrlX = (item.FindControl("amountX") as RadNumericTextBox);
            var amountX = amountCtrlX.Value.HasValue ? (decimal)amountCtrlX.Value.Value : 0;
            if (amountX == 1)
            {
                db.Value.SetOutSpecific(@single.ObjectId, guid, CurrentUser, OutType.人为损耗, "人为损耗：归还报废", list, db.Value.UserName(guid));
                db.Value.StorageSave();
            }
        }
        foreach (var item in viewW.Items)
        {
            var amountCtrl = (item.FindControl("amount") as RadNumericTextBox);
            var amount = amountCtrl.Value.HasValue ? (decimal)amountCtrl.Value.Value : 0;
            if (amount == 0)
                continue;
            var amountCtrlX = (item.FindControl("amountX") as RadNumericTextBox);
            var amountX = amountCtrlX.Value.HasValue ? (decimal)amountCtrlX.Value.Value : 0;
            var id = item.GetDataKeyValue("借用标识").ToString().GlobalId();
            var sheet = db.Value.查询_借用单.Single(o => o.借用标识 == id);
            var obj = db.Value.StorageObject.SingleOrDefault(o => o.Id == sheet.物品标识);
            var note = (item.FindControl("note") as RadTextBox).Text;
            db.Value.SetReturnM(obj.Id, guid, CurrentUser, amount, note ?? string.Empty);
            db.Value.StorageSave();
            if (amountX > 0)
            {
                db.Value.SetOutM(obj.Id, guid, CurrentUser, OutType.人为损耗, "人为损耗：归还报废", amountX, db.Value.UserName(guid));
                db.Value.StorageSave();
            }
        }
        Response.Redirect("~/StorageQuery/QueryReturn?StorageId={0}".Formatted(StorageId));
        return true;
    }

    protected void out_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!InRecord()) return;
    }

    protected void viewW_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        var item = e.Item as RadListViewDataItem;
        var a = item.FindControl("amount") as RadNumericTextBox;
        var ax = item.FindControl("amountX") as RadNumericTextBox;
        a.Attributes["ob"] = ax.ClientID;
    }

    protected void viewD_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        var item = e.Item as RadListViewDataItem;
        var a = item.FindControl("amount") as RadNumericTextBox;
        var ax = item.FindControl("amountX") as RadNumericTextBox;
        a.Attributes["ob"] = ax.ClientID;
    }

    protected void han_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageScan/ScanReturn?StorageId={0}".Formatted(StorageId));
    }
}
