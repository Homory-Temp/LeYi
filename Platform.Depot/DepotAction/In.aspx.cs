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
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "", Value = "0", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            people.SelectedIndex = -1;
            source.Items.Clear();
            source.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "", Value = "", Selected = true });
            source.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.购置来源).ToList();
            source.DataBind();
            source.SelectedIndex = -1;
            usage.Items.Clear();
            usage.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "", Value = "", Selected = true });
            usage.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.使用对象).ToList();
            usage.DataBind();
            usage.SelectedIndex = -1;
            if (!"OrderId".Query().None() || !Session["DepotToInOrder"].None())
            {
                var value = "OrderId".Query().None() ? Session["DepotToInOrder"].ToString() : "OrderId".Query();
                var id = value.GlobalId();
                var t = DataContext.DepotOrder.Single(o => o.Id == id);
                period.SelectedDate = t.OrderTime;
                inTime.SelectedDate = t.OrderTime;
                source.SelectedIndex = source.FindItemIndexByText(t.OrderSource);
                usage.SelectedIndex = usage.FindItemIndexByText(t.UsageTarget);
                people.SelectedIndex = people.FindItemIndexByValue(t.OperatorId.ToString());
                ReloadOrders();
                target.Items.FindItemByValue(value).Selected = true;
                view_target.Rebind();
                counter.Value = "10";
                x1.Visible = x2.Visible = x3.Visible = x4.Visible = true;
            }
            else
            {
                var id = Depot.Id;
                target.DataSource = DataContext.DepotOrder.Where(o => o.State < State.停用 && o.DepotId == id && o.Done == false).OrderByDescending(o => o.OrderTime).ToList();
                target.DataBind();
                x1.Visible = x2.Visible = x3.Visible = x4.Visible = false;
            }
            if (!"ObjectId".Query().None())
            {
                var objId = "ObjectId".Query().GlobalId();
                var obj = DataContext.DepotObject.Single(o => o.Id == objId);
                var isVirtual = Depot.Featured(DepotType.固定资产库);
                var catalogId = DataContext.DepotObjectCatalog.Single(o => o.ObjectId == objId && o.IsLeaf == true && o.IsVirtual == isVirtual).CatalogId;
                counter.Value = "10";
                var list = new List<InMemoryIn>();
                list.Add(new InMemoryIn { Time = inTime.SelectedDate.HasValue ? inTime.SelectedDate.Value.Date : DateTime.Today, CatalogId = catalogId, ObjectId = objId });
                x.Value = list.ToJson();
                x1.Visible = x2.Visible = x3.Visible = x4.Visible = false;
                plus.Visible = false;
                back.Visible = true;
            }
            else
            {
                plus.Visible = true;
                back.Visible = false;
            }
            if (Session["DepotToIn"] != null)
            {
                var list = Session["DepotToIn"] as List<InMemoryIn>;
                target.SelectedValue = Session["DepotToInOrder"].ToString();
                counter.Value = list.Count.ToString();
                x.Value = list.ToJson();
                Session.Remove("DepotToIn");
                Session.Remove("DepotToInOrder");
            }
        }
    }

    protected void ReloadOrders()
    {
        target.SelectedIndex = -1;
        target.Text = string.Empty;
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
        counter.Value = target.SelectedIndex == -1 ? "0" : "10";
        x1.Visible = x2.Visible = x3.Visible = x4.Visible = target.SelectedIndex >= 0;
        view_target.Rebind();
        view_obj.Rebind();
    }

    protected void view_target_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (target.SelectedIndex == -1 || target.SelectedValue.None())
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
        if (Depot.Featured(DepotType.固定资产库) && !CheckDoIn())
        {
            NotifyError(ap, "请填写卡片编号");
            return;
        }
        DoIn(false);
        Response.Redirect("~/DepotQuery/InX?DepotId={0}".Formatted(Depot.Id));
    }

    protected bool CheckDoIn()
    {
        var list = new List<InMemoryIn>();
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectIn") as Control_ObjectIn;
            var @in = c.PeekValue();
            list.Add(@in);
        }
        return list.Count(o => o.Note.None() && o.ObjectId.HasValue && o.Amount > 0) == 0;
    }

    protected void DoIn(bool finish)
    {
        var list = new List<InMemoryIn>();
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectIn") as Control_ObjectIn;
            var @in = c.PeekValue();
            list.Add(@in);
        }
        DataContext.DepotActIn(Depot.Id, target.SelectedValue.GlobalId(), inTime.SelectedDate.HasValue ? inTime.SelectedDate.Value.Date : DateTime.Today, DepotUser.Id, list);
        if (finish)
        {
            var tid = target.SelectedValue.GlobalId();
            var t = DataContext.DepotOrder.Single(o => o.Id == tid);
            t.Done = true;
            if (total.Value.HasValue)
            {
                t.Paid = (decimal)total.Value.Value;
            }
            DataContext.SaveChanges();
        }
    }

    protected void view_obj_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        var c = e.Item.FindControl("ObjectIn") as Control_ObjectIn;
        var list = x.Value.None() ? new List<InMemoryIn>() : x.Value.FromJson<List<InMemoryIn>>();
        if (list.Count < c.ItemIndex + 1)
        {
            c.LoadDefaults(new InMemoryIn { Time = inTime.SelectedDate.HasValue ? inTime.SelectedDate.Value.Date : DateTime.Today });
        }
        else
        {
            c.LoadDefaults(list[c.ItemIndex]);
        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var orderId = target.SelectedValue.None() ? (Guid?)null : target.SelectedValue.GlobalId();
        var adjust = 0M;
        if (orderId.HasValue)
        {
            var id = orderId.Value;
            var isVirtual = Depot.Featured(DepotType.固定资产库);
            var source = DataContext.DepotInXRecord.Where(o => o.OrderId == id && o.IsVirtual == isVirtual).OrderByDescending(o => o.Time).ToList();
            view_record.DataSource = source;
            adjust = source.Sum(o => o.Total);
            ap.ResponseScripts.Add("set_adj({0});".Formatted(adjust));
        }
        else
        {
            view_record.DataSource = null;
        }
    }

    protected void done_in_ServerClick(object sender, EventArgs e)
    {
        if (Depot.Featured(DepotType.固定资产库) && !CheckDoIn())
        {
            NotifyError(ap, "请填写卡片编号");
            return;
        }
        DoIn(true);
        Response.Redirect("~/DepotQuery/InPrint?DepotId={0}&OrderId={1}".Formatted(Depot.Id, target.SelectedValue));
    }

    protected void back_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Object?DepotId={0}".Formatted(Depot.Id));
    }

    protected void addObj_ServerClick(object sender, EventArgs e)
    {
        var list = new List<InMemoryIn>();
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectIn") as Control_ObjectIn;
            var @in = c.PeekValue();
            list.Add(@in);
        }
        Session["DepotToIn"] = list;
        Session["DepotToInOrder"] = target.SelectedValue;
        var url = "../DepotAction/ObjectAddX?DepotId={0}".Formatted(Depot.Id);
        Response.Redirect(url);
    }
}
