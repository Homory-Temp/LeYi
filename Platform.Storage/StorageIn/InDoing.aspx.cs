using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class InDoing : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        keeper_source.DataSource = sourceX;
        if (!IsPostBack)
        {
            in_age.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.年龄段).OrderBy(o => o.Name).ToList());
            in_place.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.存放地).OrderBy(o => o.Name).ToList());
            var targetId = TargetId;
            var target = db.Value.StorageTarget.Single(o => o.Id == targetId);
            target_id.Value = targetId.ToString();
            target_number.Text = target.Number;
            target_receipt.Text = target.ReceiptNumber;
            target_content.InnerHtml = target.Content;
            target_source.Text = target.OrderSource;
            target_target.Text = target.UsageTarget;
            target_toPay.Text = target.ToPay.Money();
            target_paid.Text = target.Paid.Money();
            target_day.Text = target.TimeNode.TimeNode();
            target_keeper.Text = target.KeepUserId.HasValue ? db.Value.UserName(target.KeepUserId.Value) : "无";
            target_brokerage.Text = target.BrokerageUserId.HasValue ? db.Value.UserName(target.BrokerageUserId.Value) : "无";
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
            //fixedArea.Visible = obj.Fixed;
            object_fixed_serial.Text = obj.FixedSerial;
            if (obj.StorageIn.Count > 0)
            {
                var tmp = obj.StorageIn.OrderByDescending(o => o.Time).First();
                in_age.Text = tmp.Age;
                in_age.SelectedIndex = in_age.FindItemIndexByText(tmp.Age);
                in_place.Text = tmp.Place;
                in_place.SelectedIndex = in_age.FindItemIndexByText(tmp.Place);
                var u = tmp.ResponsibleUserId;
                responsible.Text = db.Value.UserName(u);
                responsibleId.Value = u.ToString();
                keeper_del.Visible = true;
            }
        }
    }

    protected Guid TargetId
    {
        get { return "TargetId".Query().GlobalId(); }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected void ins_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        ins.Source(db.Value.StorageTargetGetOne(TargetId).StorageIn.ToList());
    }

    protected void responsible_Load(object sender, EventArgs e)
    {
        responsible.Attributes["onclick"] = "responsible_selecting();";
    }

    protected void in_place_DataSourceSelect(object sender, SearchBoxDataSourceSelectEventArgs e)
    {
        in_place.DataSource = db.Value.StorageIn.Where(o => o.StorageObject.Id == StorageId).Select(o => o.Place).Distinct().ToList().Where(o => o.ToLower().Contains(e.FilterString.ToLower()));
    }

    protected bool InRecord()
    {
        if (in_amount.MissingValue("请输入数量")) { in_amount.Text = string.Empty; return false; }
        if (in_perPrice.MissingValue("请输入单价")) { in_perPrice.Text = string.Empty; return false; }
        //if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; return false; }
        db.Value.SetIn(ObjectId, TargetId, in_age.Text, in_place.Text, responsibleId.Value.Null() ? (Guid?)null : responsibleId.Value.GlobalId(), CurrentUser, in_amount.Value(0.00M), decimal.Multiply(in_amount.Value(0.00M), in_perPrice.Value(0.00M)), in_additionalPrice.Value(0.00M), in_note.Text);
        db.Value.StorageSave();
        return true;
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!InRecord()) return;
        Response.Redirect("~/StorageIn/InDone?StorageId={0}&ObjectId={1}&TargetId={2}".Formatted(StorageId, ObjectId, TargetId));
    }

    protected void in_done_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        db.Value.StorageTargetGetOne(TargetId).In = true;
        if (!InRecord()) return;
        Response.Redirect("~/StorageIn/InDone?StorageId={0}&ObjectId={1}&TargetId={2}".Formatted(StorageId, ObjectId, TargetId));
    }

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
}
