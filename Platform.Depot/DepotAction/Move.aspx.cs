using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Move : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
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
            var id = Depot.Id;
            target.DataSource = DataContext.DepotOrder.Where(o => o.State < State.停用 && o.DepotId == id && o.Done == false).OrderByDescending(o => o.OrderTime).ToList();
            target.DataBind();
            depots.DataSource = DataContext.Depot.Where(o => o.State < State.停用).ToList().Where(o => !o.Featured(DepotType.固定资产库)).ToList();
            depots.DataBind();
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
    }

    protected void period_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        ReloadOrders();
        view_target.Rebind();
    }

    protected void source_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadOrders();
        view_target.Rebind();
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadOrders();
        view_target.Rebind();
    }

    protected void people_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ReloadOrders();
        view_target.Rebind();
    }

    protected void target_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        view_target.Rebind();
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

    protected void depots_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }

    protected void do_move_ServerClick(object sender, EventArgs e)
    {

    }
}
