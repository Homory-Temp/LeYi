using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_CheckResult : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var batchId = "BatchId".Query().GlobalId();
        view.DataSource = DataContext.DepotCheckX.Where(o => o.State  == 1 && o.BatchId == batchId).OrderByDescending(o => o.Time).ToList();
    }

    protected void del_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var time = DateTime.Parse((sender as HtmlInputButton).Attributes["matchx"]);
        var item = DataContext.DepotCheckX.Where(o => o.BatchId == bid).ToList().SingleOrDefault(o =>  o.Time.ToString("yyyy-MM-dd HH:mm:ss") == time.ToString("yyyy-MM-dd HH:mm:ss"));
        if (item != null)
        {
            item.State = 2;
        }
        DataContext.SaveChanges();
        view.Rebind();
    }

    protected void view_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        Response.Redirect("~/DepotScan/CheckListView?DepotId={0}&BatchId={1}".Formatted(Depot.Id, bid));
    }

    protected void start_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        Response.Redirect("~/DepotScan/CheckDo?DepotId={0}&BatchId={1}".Formatted(Depot.Id, bid));
    }
}
