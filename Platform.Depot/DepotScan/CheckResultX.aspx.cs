using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_CheckResultX : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "BatchId".Query().GlobalId();
            var time = new DateTime(long.Parse("Time".Query()));
            var x = DataContext.DepotCheckX.Where(o => o.BatchId == id).ToList().Single(o => o.Time.ToString("yyyy-MM-dd HH:mm:ss") == time.ToString("yyyy-MM-dd HH:mm:ss"));
            h.Value = x.CodeJson;
            view.Rebind();
        }
    }

    protected string Codes
    {
        get
        {
            return h.Value;
        }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (h.Value.None())
        {
            view.DataSource = null;
            return;
        }
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
