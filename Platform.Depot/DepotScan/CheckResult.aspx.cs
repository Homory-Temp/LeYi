using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_CheckResult : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Reset();
            if (!"Code".Query().None())
            {
                scan.Text = "Code".Query().Trim();
                scanFlow_ServerClick(null, null);
            }
        }
    }

    protected void Reset()
    {
        scan.Text = "";
        scan.Focus();
    }

    protected void scanFlow_ServerClick(object sender, EventArgs e)
    {
        var code = scan.Text.Trim();
        var x = h.Value.None() ? new List<InMemoryCheck>() : h.Value.FromJson<List<InMemoryCheck>>();
        x.SingleOrDefault(o => o.Code == code).In = true;
        h.Value = x.ToJson();
        var id = "BatchId".Query().GlobalId();
        var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
        foreach (var item in items)
        {
            var obj = item.CodeJson.FromJson<List<InMemoryCheck>>();
            if (obj.Count(o => o.Code == code) > 0)
            {
                obj.First(o => o.Code == code).In = true;
            }
            item.CodeJson = obj.ToJson();
            break;
        }
        DataContext.SaveChanges();
        view.Rebind();
        Reset();
    }

    protected List<InMemoryCheck> Codes
    {
        get
        {
            return h.Value.None() ? new List<InMemoryCheck>() : h.Value.FromJson<List<InMemoryCheck>>();
        }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var id = "BatchId".Query().GlobalId();
        var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
        name.InnerText = items[0].Name;
        var checks = new List<InMemoryCheck>();
        foreach (var item in items)
        {
            checks.AddRange(item.CodeJson.FromJson<List<InMemoryCheck>>());
        }
        view.DataSource = checks;
        h.Value = checks.ToJson();
    }
}
