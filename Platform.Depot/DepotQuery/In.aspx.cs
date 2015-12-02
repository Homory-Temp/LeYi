using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_In : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            periodx.SelectedDate = DateTime.Today.AddMonths(-1);
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList();
            people.DataBind();
            source.Items.Clear();
            source.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "购置来源", Value = "0", Selected = true });
            source.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.购置来源).ToList();
            source.DataBind();
            usage.Items.Clear();
            usage.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "使用对象", Value = "", Selected = true });
            usage.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.使用对象).ToList();
            usage.DataBind();
        }
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var timex = periodx.SelectedDate.HasValue ? periodx.SelectedDate.Value : DateTime.Today;
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        if (timex > time)
        {
            var time_t = timex;
            timex = time;
            time = time_t;
        }
        var start = timex.AddMilliseconds(-1);
        var end = time.AddDays(1);
        var list = new List<DepotInRecord>();
        switch (combo.SelectedValue)
        {
            case "0":
                {
                    list = DataContext.DepotInRecord.Where(o => o.DepotId == Depot.Id && o.RecordTime > start && o.RecordTime < end && o.Done == false).OrderByDescending(o => o.RecordTime).ToList();
                    break;
                }
            case "1":
                {
                    list = DataContext.DepotInRecord.Where(o => o.DepotId == Depot.Id && o.RecordTime > start && o.RecordTime < end && o.Done == true).OrderByDescending(o => o.RecordTime).ToList();
                    break;
                }
            case "2":
                {
                    list = DataContext.DepotInRecord.Where(o => o.DepotId == Depot.Id && o.RecordTime > start && o.RecordTime < end).OrderByDescending(o => o.RecordTime).ToList();
                    break;
                }
        }
        if (source.SelectedIndex > 0)
            list = list.Where(o => o.购置来源 == source.SelectedItem.Text).ToList();
        if (usage.SelectedIndex > 0)
            list = list.Where(o => o.使用对象 == usage.SelectedItem.Text).ToList();
        if (people.SelectedIndex > 0)
            list = list.Where(o => o.操作人 == people.SelectedItem.Text).ToList();
        view.DataSource = list;
        //___total.Value = list.Sum(o => o.实付金额).ToMoney();
        pager.Visible = list.Count > pager.PageSize;
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/OrderEdit?DepotId={0}&OrderId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/In?DepotId={0}&OrderId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void done_ServerClick(object sender, EventArgs e)
    {
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var order = DataContext.DepotOrder.Single(o => o.Id == id);
        order.Done = true;
        DataContext.SaveChanges();
        Response.Redirect("~/DepotQuery/InPrint?DepotId={0}&OrderId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void redo_ServerClick(object sender, EventArgs e)
    {
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var order = DataContext.DepotOrder.Single(o => o.Id == id);
        order.Done = false;
        DataContext.SaveChanges();
        view.Rebind();
    }

    protected void print_ServerClick(object sender, EventArgs e)
    {
        var url = "../DepotQuery/InPrint?DepotId={0}&OrderId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"].GlobalId());
        ap.ResponseScripts.Add("window.open('{0}', '_blank');".Formatted(url));
    }

    protected void back_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotQuery/InX?DepotId={0}&OrderId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void fs_ServerClick(object sender, EventArgs e)
    {
        var url = "../DepotQuery/InOrder?DepotId={0}&OrderId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"].GlobalId());
        ap.ResponseScripts.Add("pop('{0}');".Formatted(url));
    }
}
