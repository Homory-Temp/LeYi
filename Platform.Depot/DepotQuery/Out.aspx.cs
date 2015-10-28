using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_Out  : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            peopleX.Items.Clear();
            peopleX.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "报废申请人", Value = "0", Selected = true });
            peopleX.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            peopleX.DataBind();
        }
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        var start = new DateTime(time.Year, time.Month, 1).AddMilliseconds(-1);
        var end = new DateTime(time.Year, time.Month, 1).AddMonths(1);
        var list = DataContext.DepotOutRecord.Where(o => o.DepotId == Depot.Id && o.Time > start && o.Time < end).OrderByDescending(o => o.Time).ToList();
        if (peopleX.SelectedIndex > 0)
        {
            var ou = peopleX.SelectedItem.Value.GlobalId();
            list = list.Where(o => o.UserId == ou).ToList();
        }
        if (outType.SelectedIndex > 0)
        {
            var x = int.Parse(outType.SelectedItem.Value);
            list = list.Where(o => o.State == x).ToList();
        }
        view.DataSource = list;
        pager.Visible = list.Count > pager.PageSize;
    }
}
