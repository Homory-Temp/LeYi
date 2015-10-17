using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_Target : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
        }
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        var start = (new DateTime(time.Year, time.Month, 1).AddDays(-1)).ToTimeNode();
        var end = (new DateTime(time.Year, time.Month, 1).AddMonths(1)).ToTimeNode();
        switch (combo.SelectedValue)
        {
            case "0":
                {
                    view.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end && o.In == false).OrderByDescending(o => o.TimeNode).ToList();
                    break;
                }
            case "1":
                {
                    view.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end && o.In == true).OrderByDescending(o => o.TimeNode).ToList();
                    break;
                }
            case "2":
                {
                    view.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end).OrderByDescending(o => o.TimeNode).ToList();
                    break;
                }
        }
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {

    }

    protected void in_ServerClick(object sender, EventArgs e)
    {

    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {

    }

    protected void done_ServerClick(object sender, EventArgs e)
    {

    }
}
