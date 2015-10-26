using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_In : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            inTime.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            source.Items.Clear();
            source.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "购置来源", Value = "", Selected = true });
            source.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.购置来源).ToList();
            source.DataBind();
            usage.Items.Clear();
            usage.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "使用对象", Value = "", Selected = true });
            usage.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.使用对象).ToList();
            usage.DataBind();
            if (!"OrderId".Query().None())
            {
                var value = "OrderId".Query();
                var id = value.GlobalId();
                var t = DataContext.DepotOrder.Single(o => o.Id == id);
                period.SelectedDate = t.OrderTime;
                source.SelectedIndex = source.FindItemIndexByText(t.OrderSource);
                usage.SelectedIndex = usage.FindItemIndexByText(t.UsageTarget);
                people.SelectedIndex = people.FindItemIndexByValue(t.OperatorId.ToString());
                ReloadOrders();
                target.Items.FindItemByValue(value).Selected = true;
                view_target.Rebind();
                counter.Value = "1";
                x1.Visible = x2.Visible = x3.Visible = x4.Visible = true;
            }
            else
            {
                var id = Depot.Id;
                target.DataSource = DataContext.DepotOrder.Where(o => o.State < State.停用 && o.DepotId == id && o.Done == false).OrderByDescending(o => o.OrderTime).ToList();
                target.DataBind();
                x1.Visible = x2.Visible = x3.Visible = x4.Visible = false;
            }
        }
    }

    protected void ReloadOrders()
    {
        target.SelectedIndex = -1;
        target.Text = string.Empty;
        counter.Value = "0";
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        var start = new DateTime(time.Year, time.Month, 1).AddMilliseconds(-1);
        var end = new DateTime(time.Year, time.Month, 1).AddMonths(1);
        var list = DataContext.DepotOrder.Where(o => o.DepotId == Depot.Id && o.OrderTime > start && o.OrderTime < end && o.Done == false).OrderByDescending(o => o.OrderTime).ToList();
        if (source.SelectedIndex > 0)
            list = list.Where(o => o.OrderSource == source.SelectedItem.Text).ToList();
        if (usage.SelectedIndex > 0)
            list = list.Where(o => o.UsageTarget == usage.SelectedItem.Text).ToList();
        if (people.SelectedIndex > 0)
            list = list.Where(o => o.OperatorId == people.SelectedItem.Value.GlobalId()).ToList();
        target.DataSource = list;
        target.DataBind();
        x1.Visible = x2.Visible = x3.Visible = x4.Visible = false;
    }

    protected void period_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        ReloadOrders();
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void source_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadOrders();
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadOrders();
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void people_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadOrders();
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
            view_target.DataSource = DataContext.DepotInRecord.Where(o => o.Id == id).ToList();
            view_target.Visible = true;
        }
    }

    protected void plus_ServerClick(object sender, EventArgs e)
    {
        counter.Value = ((int.Parse(counter.Value)) + 1).ToString();
        var toRem = view_obj.Items.Select(o => (o.FindControl("ObjectIn") as Control_ObjectIn)).Select(o => o.PeekValue()).ToList();
        x.Value = toRem.ToJson();
        view_obj.Rebind();
    }

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view_obj.DataSource = new int[(int.Parse(counter.Value))];
    }

    protected void do_in_ServerClick(object sender, EventArgs e)
    {
        //DoIn(false);
        //Response.Redirect("~/StoreQuery/In?StoreId={0}".Formatted(StoreId));
    }

    protected void DoIn(bool finish)
    {
        //for (var i = 0; i < view_obj.Items.Count; i++)
        //{
        //    var c = view_obj.Items[i].FindControl("ObjectInBody") as Control_ObjectInBody;
        //    var @in = c.PeekValue();
        //    var targetId = @in.TargetId;
        //    var t = db.Value.StoreTarget.Single(o => o.Id == targetId);
        //    decimal amount = @in.Amount.HasValue ? @in.Amount.Value : 0M;
        //    decimal fee = @in.Fee.HasValue ? @in.Fee.Value : 0M;
        //    decimal sourcePerPrice = @in.SourcePerPrice.HasValue ? @in.SourcePerPrice.Value : 0M;
        //    decimal money = @in.Money.HasValue ? @in.Money.Value : 0M;
        //    if (@in.ObjectId.HasValue && amount > 0M && money > 0M)
        //    {
        //        db.Value.ActionInExt(targetId, @in.ObjectId.Value, @in.Age, @in.Place, "", null, @in.Note, @in.TimeNode.ToTime(), CurrentUser, "", amount, money - fee, sourcePerPrice, fee, money);
        //    }
        //}
        //if (finish)
        //{
        //    var tid = target.SelectedValue.GlobalId();
        //    var t = db.Value.StoreTarget.Single(o => o.Id == tid);
        //    t.In = true;
        //    if (total.Value.HasValue)
        //    {
        //        t.Paid = (decimal)total.Value.Value;
        //        t.AdjustedMoney = (decimal)total.Value.Value;
        //    }
        //    db.Value.SaveChanges();
        //}
    }

    protected void view_obj_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        var c = e.Item.FindControl("ObjectIn") as Control_ObjectIn;
        var list = x.Value.None() ? new List<InMemoryIn>() : x.Value.FromJson<List<InMemoryIn>>();
        if (list.Count < c.ItemIndex + 1)
        {
            c.LoadDefaults(new InMemoryIn { OrderId = target.SelectedValue.GlobalId(), Time = inTime.SelectedDate.HasValue ? inTime.SelectedDate.Value.Date : DateTime.Today });
        }
        else
        {
            c.LoadDefaults(list[c.ItemIndex]);
        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        //var targetId = target.SelectedValue == null ? (Guid?)null : target.SelectedValue.GlobalId();
        //var adjust = 0M;
        //if (targetId.HasValue)
        //{
        //    var id = targetId.Value;
        //    var source = db.Value.Store_RecordIn.Where(o => o.TargetId == id).OrderByDescending(o => o.入库日期).ToList();
        //    view_record.DataSource = source;
        //    adjust = source.Sum(o => o.合计);
        //    ap.ResponseScripts.Add("set_adj({0});".Formatted(adjust));
        //}
        //else
        //{
        //    view_record.DataSource = null;
        //}
    }

    protected void done_in_ServerClick(object sender, EventArgs e)
    {
        //DoIn(true);
        //Response.Redirect("~/StoreQuery/TargetPrint?StoreId={0}&TargetId={1}".Formatted(StoreId, target.SelectedValue));
    }
}
