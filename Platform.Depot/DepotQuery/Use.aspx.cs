using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_Use : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            periodx.SelectedDate = DateTime.Today.AddMonths(-1);
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            peopleX.Items.Clear();
            peopleX.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "借领人", Value = "0", Selected = true });
            peopleX.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            peopleX.DataBind();
            age.Items.Clear();
            age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            age.DataBind();
            age.Visible = Depot.Featured(DepotType.幼儿园);
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
        var list = DataContext.DepotUse.Where(o => o.DepotId == Depot.Id && o.Time > start && o.Time < end).OrderByDescending(o => o.Time).ToList();
        if (age.SelectedIndex > 0)
            list = list.Where(o => o.Age == age.SelectedItem.Text).ToList();
        if (people.SelectedIndex > 0)
        {
            var op = people.SelectedItem.Value.GlobalId();
            list = list.Where(o => o.OperatorId == op).ToList();
        }
        if (peopleX.SelectedIndex > 0)
        {
            var ou = peopleX.SelectedItem.Value.GlobalId();
            list = list.Where(o => o.UserId == ou).ToList();
        }
        view.DataSource = list;
        ___total.Value = list.Sum(o => o.Money).ToMoney();
        //pager.Visible = list.Count > pager.PageSize;
    }

    protected string UserName(Guid id)
    {
        return DataContext.DepotUserLoad(Depot.CampusId).Single(o => o.Id == id).Name;
    }

    protected void print_ServerClick(object sender, EventArgs e)
    {
        var url = "../DepotQuery/UsePrint?DepotId={0}&UseId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"].GlobalId());
        ap.ResponseScripts.Add("window.open('{0}', '_blank');".Formatted(url));
    }
}
