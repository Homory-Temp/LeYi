using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_In : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId).Select(o => o.OperationUserId).ToList().Join(db.Value.User, o => o, o => o.Id, (o, u) => u).Distinct().ToList();
            people.DataBind();
            source.Items.Clear();
            source.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "采购来源", Value = "0", Selected = true });
            source.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId).Select(o => o.采购来源).Distinct().ToList();
            source.DataBind();
            usage.Items.Clear();
            usage.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "使用对象", Value = "", Selected = true });
            if (CurrentStore.State == StoreState.食品)
            {
                var s = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.ParentId == null && o.State < 2).OrderBy(o => o.Ordinal).ToList();
                usage.DataSource = s;
            }
            else
            {
                var s = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.使用对象).OrderBy(o => o.PinYin).ToList();
                usage.DataSource = s;
            }
            usage.DataBind();
            if (!"TargetId".Query().Null())
            {
                var value = "TargetId".Query();
                var id = value.GlobalId();
                var t = db.Value.Store_Target.Single(o => o.Id == id);
                period.SelectedDate = t.Time;
                source.SelectedIndex = source.FindItemIndexByText(t.采购来源);
                usage.SelectedIndex = usage.FindItemIndexByText(t.使用对象);
                people.SelectedIndex = people.FindItemIndexByText(t.操作人);
                ReloadTargets();
                target.Items.FindItemByValue(value).Selected = true;
                view_target.Rebind();
                counter.Value = "1";
                x1.Visible = x2.Visible = x3.Visible = x4.Visible = true;
            }
            else
            {
                target.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.In == false).OrderByDescending(o => o.TimeNode).ToList();
                target.DataBind();
                x1.Visible = x2.Visible = x3.Visible = x4.Visible = false;
            }
        }
    }

    protected void ReloadTargets()
    {
        target.SelectedIndex = -1;
        target.Text = string.Empty;
        counter.Value = "0";
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        var start = (new DateTime(time.Year, time.Month, 1).AddDays(-1)).ToTimeNode();
        var end = (new DateTime(time.Year, time.Month, 1).AddMonths(1)).ToTimeNode();
        List<Store_Target> list = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end && o.In == false).OrderByDescending(o => o.TimeNode).ToList();
        if (source.SelectedIndex > 0)
            list = list.Where(o => o.采购来源 == source.SelectedItem.Text).ToList();
        if (usage.SelectedIndex > 0)
            list = list.Where(o => o.使用对象 == usage.SelectedItem.Text).ToList();
        if (people.SelectedIndex > 0)
            list = list.Where(o => o.操作人 == people.SelectedItem.Text).ToList();
        target.DataSource = list;
        target.DataBind();
        x1.Visible = x2.Visible = x3.Visible = x4.Visible = false;
    }

    protected void period_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        ReloadTargets();
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void source_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadTargets();
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadTargets();
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void people_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadTargets();
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void target_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        counter.Value = target.SelectedIndex == -1 ? "0" : "1";
        x1.Visible = x2.Visible = x3.Visible = x4.Visible = target.SelectedIndex >= 0;
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void view_target_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (target.SelectedIndex == -1)
        {
            view_target.DataSource = null;
            view_target.Visible = false;
        }
        else
        {
            var id = target.SelectedValue.GlobalId();
            view_target.DataSource = db.Value.Store_Target.Where(o => o.Id == id).ToList();
            view_target.Visible = true;
        }
    }

    public class Rem
    {
        public Guid? CatalogId { get; set; }
        public Guid? ObjectId { get; set; }
        public decimal? Amount { get; set; }
    }

    protected void plus_ServerClick(object sender, EventArgs e)
    {
        counter.Value = ((int.Parse(counter.Value)) + 1).ToString();
        var toRem = view_obj.Items.Select(o => (o.FindControl("ObjectInBody") as Control_ObjectInBody)).Select(o => o.PeekValue()).ToList();
        x.Value = toRem.ToJson();
        view_obj.Rebind();
    }

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view_obj.DataSource = new int[(int.Parse(counter.Value))];
    }

    protected void do_in_ServerClick(object sender, EventArgs e)
    {
        DoIn(false);
        Response.Redirect("~/StoreQuery/In?StoreId={0}".Formatted(StoreId));
    }

    protected void DoIn(bool finish)
    {
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectInBody") as Control_ObjectInBody;
            var @in = c.PeekValue();
            var targetId = @in.TargetId;
            var t = db.Value.StoreTarget.Single(o => o.Id == targetId);
            decimal amount = @in.Amount.HasValue ? @in.Amount.Value : 0M;
            decimal fee = @in.Fee.HasValue ? @in.Fee.Value : 0M;
            decimal sourcePerPrice = @in.SourcePerPrice.HasValue ? @in.SourcePerPrice.Value : 0M;
            decimal money = @in.Money.HasValue ? @in.Money.Value : 0M;
            if (@in.ObjectId.HasValue && amount > 0M && money > 0M)
            {
                db.Value.ActionIn(targetId, @in.ObjectId.Value, t.OrderSource, @in.Place, "", null, @in.Note, @in.TimeNode.ToTime(), CurrentUser, "", amount, money - fee, sourcePerPrice, fee, money);
                db.Value.SaveChanges();
            }
        }
        if (finish)
        {
            var tid = target.SelectedValue.GlobalId();
            var t = db.Value.StoreTarget.Single(o => o.Id == tid);
            t.In = true;
            db.Value.SaveChanges();
        }
    }

    protected void view_obj_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        var c = e.Item.FindControl("ObjectInBody") as Control_ObjectInBody;
        var list = x.Value.Null() ? new List<CachedIn>() : x.Value.FromJson<List<CachedIn>>();
        if (list.Count < c.ItemIndex + 1)
        {
            c.LoadDefaults(new CachedIn { TargetId = target.SelectedValue.GlobalId() });
        }
        else
        {
            c.LoadDefaults(list[c.ItemIndex]);
        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var targetId = target.SelectedValue == null ? (Guid?)null : target.SelectedValue.GlobalId();
        if (targetId.HasValue)
        {
            var id = targetId.Value;
            view_record.DataSource = db.Value.Store_RecordIn.Where(o => o.TargetId == id).OrderByDescending(o => o.入库日期).ToList();
        }
        else
        {
            view_record.DataSource = null;
        }
    }

    protected void done_in_ServerClick(object sender, EventArgs e)
    {
        DoIn(true);
        Response.Redirect("~/StoreQuery/TargetPrint?StoreId={0}&TargetId={1}".Formatted(StoreId, target.SelectedValue));
    }
}
