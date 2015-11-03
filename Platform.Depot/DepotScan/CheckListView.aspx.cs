using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_CheckListView : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Reset();
            if (!"Code".Query().None())
            {
                //scan.Text = "Code".Query().Trim();
                //scanFlow_ServerClick(null, null);
            }
        }
    }

    protected void Reset()
    {
        //scan.Text = "";
        //scan.Focus();
    }

    protected void scanFlow_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotScan/CheckDo?DepotId={0}&BatchId={1}".Formatted(Depot.Id, "BatchId".Query()));
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var id = "BatchId".Query().GlobalId();
        var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
        var checks = new List<InMemoryCheck>();
        foreach (var item in items)
        {
            checks.AddRange(item.CodeJson.FromJson<List<InMemoryCheck>>());
        }
        view.DataSource = checks;
    }
}
