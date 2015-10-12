using System;
using System.Web.UI;
using System.Linq;
using Models;

public partial class TargetEditPopup : SingleStoragePage
{
    protected void Page_Load(object ssender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_source.DataSource = sourceX;
        brokerage_source.DataSource = sourceX;
        if (!IsPostBack)
        {
            source.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.采购来源).OrderBy(o => o.Name).ToList());
            target.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.使用对象).OrderBy(o => o.Name).ToList());
            day.SelectedDate = DateTime.Today;
            var id = "Id".Query().GlobalId();
            var obj = db.Value.StorageTarget.FirstOrDefault(o => o.Id == id);
            number.Text = obj.Number;
            receipt.Text = obj.ReceiptNumber;
            content.Text = ReLined(obj.Content);
            source.Text = obj.OrderSource;
            source.SelectedIndex = source.FindItemIndexByText(obj.OrderSource);
            target.Text = obj.UsageTarget;
            target.SelectedIndex = target.FindItemIndexByText(obj.UsageTarget);
            toPay.Value = (double)obj.ToPay;
            paid.Value = (double)obj.Paid;
            if (obj.KeepUserId.HasValue)
            {
                keeperId.Value = obj.KeepUserId.ToString();
                keeper.Text = obj.KeepUser.RealName;
                keeper_del.Visible = true;
            }
            else
            {
                keeperId.Value = "";
                keeper.Text = "";
                keeper_del.Visible = false;
            }
            if (obj.BrokerageUserId.HasValue)
            {
                brokerageId.Value = obj.BrokerageUserId.ToString();
                brokerage.Text = obj.BrokerageUser.RealName;
                brokerage_del.Visible = true;
            }
            else
            {
                brokerageId.Value = "";
                brokerage.Text = "";
                brokerage_del.Visible = false;
            }
            string time = obj.TimeNode.ToString();
            day.SelectedDate = DateTime.Parse(time.Substring(0, 4) + "-" + time.Substring(4, 2) + "-" + time.Substring(6, 2));
        }
    }
    public string ReLined(string value)
    {
        return value.Replace("<br />", "\r\n");
    }

    protected void keeper_source_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        var source = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
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

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var id = "Id".Query().GlobalId();
        var obj = db.Value.StorageTarget.FirstOrDefault(o => o.Id == id);
        obj.Number = number.Text;
        obj.ReceiptNumber = receipt.Text;
        obj.OrderSource = source.Text;
        if (db.Value.StorageDictionaryGet(StorageId, DictionaryType.采购来源, source.Text) == null)
            db.Value.StorageDictionaryAdd(StorageId, source.Text, DictionaryType.采购来源);
        obj.UsageTarget = target.Text;
        if (db.Value.StorageDictionaryGet(StorageId, DictionaryType.使用对象, target.Text) == null)
            db.Value.StorageDictionaryAdd(StorageId, target.Text, DictionaryType.使用对象);
        obj.Content = content.Text.Lined();
        obj.ToPay = toPay.Value(0.00M);
        obj.Paid = paid.Value(0.00M);
        obj.BrokerageUserId = brokerageId.Value();
        obj.KeepUserId = keeperId.Value();
        obj.OperationUserId = CurrentUser;
        obj.In = false;
        obj.TimeNode = int.Parse(day.Value().ToString("yyyyMMdd"));
        db.Value.SaveChanges();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
