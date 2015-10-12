using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class QueryPersonal : StoragePageMobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_sourceX.DataSource = sourceX;
        if (!IsPostBack)
        {
            responsibleIdX.Value = CurrentUser.ToString();
            name.InnerHtml = "{0}&nbsp;个人借用记录".Formatted(db.Value.UserName(CurrentUser));
        }
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

    protected int CountBack(Guid id)
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
}
