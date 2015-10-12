using Models;
using System;
using System.Drawing;
using System.Linq;

public partial class TargetAdd : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_source.DataSource = sourceX;
        brokerage_source.DataSource = sourceX;
        if (!IsPostBack)
        {
            source.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.采购来源).OrderBy(o => o.Name).ToList());
            target.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.使用对象).OrderBy(o => o.Name).ToList());
            day.SelectedDate = DateTime.Today;
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!TargetRecord()) return;
        Response.Redirect("~/StorageTarget/Target?StorageId={0}".Formatted(StorageId));
    }

    protected void goOn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!TargetRecord()) return;
        Response.Redirect("~/StorageTarget/TargetAdd?StorageId={0}".Formatted(StorageId));
    }

    protected bool TargetRecord()
    {
        if (number.MissingText("请填写购置单号")) return false;
        if (!confirm.Checked) { confirm.ForeColor = Color.Red; return false; }
        db.Value.StorageTargetAdd(StorageId, number.Text, receipt.Text, source.Text, target.Text, content.Text.Lined(), toPay.Value(0.00M), paid.Value(0.00M), keeperId.Value(), brokerageId.Value(), CurrentUser, DateTime.Now, day.Value());
        db.Value.StorageSave();
        return true;
    }

    protected bool TargetRecord(out Guid id)
    {
        id = Guid.Empty;
        if (number.MissingText("请填写购置单号")) return false;
        if (!confirm.Checked) { confirm.ForeColor = Color.Red; return false; }
        id = db.Value.StorageTargetAdd(StorageId, number.Text, receipt.Text, source.Text, target.Text, content.Text.Lined(), toPay.Value(0.00M), paid.Value(0.00M), keeperId.Value(), brokerageId.Value(), CurrentUser, DateTime.Now, day.Value());
        db.Value.StorageSave();
        return true;
    }

    protected void goOn_in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Guid gid;
        if (!TargetRecord(out gid)) return;
        Response.Redirect("~/StorageObject/Object?StorageId={0}&TargetId={1}".Formatted(StorageId, gid));
    }

    protected void keeper_source_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        var source = db.Value.ViewTeacher.Where(o=>(o.State == State.启用 || o.State == State.内置)&&(o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o=> new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_source.DataSource = source.Where(o => o.Name.ToLower().Contains(e.FilterString.ToLower()) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
    }

    protected void keeper_source_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        keeper.Text = e.Text;
        keeperId.Value = e.Value;
        keeper_del.Visible = true;
    }

    protected void brokerage_source_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        var source = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        brokerage_source.DataSource = source.Where(o => o.Name.ToLower().Contains(e.FilterString.ToLower()) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
    }

    protected void brokerage_source_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        brokerage.Text = e.Text;
        brokerageId.Value = e.Value;
        brokerage_del.Visible = true;
    }

    protected void keeper_del_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        keeper.Text = "";
        keeperId.Value = "";
        keeper_source.Text = "";
        keeper_del.Visible = false;
    }

    protected void brokerage_del_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        brokerage.Text = "";
        brokerageId.Value = "";
        brokerage_source.Text = "";
        brokerage_del.Visible = false;
    }
}
