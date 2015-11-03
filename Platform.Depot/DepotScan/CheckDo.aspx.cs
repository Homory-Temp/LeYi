using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_CheckDo : DepotPageSingle
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
        var x = h.Value.None() ? new List<string>() : h.Value.FromJson<List<string>>();
        x.Add(code);
        h.Value = x.ToJson();
        view.Rebind();
        Reset();
    }

    protected List<string> Codes
    {
        get
        {
            return h.Value.None() ? new List<string>() : h.Value.FromJson<List<string>>();
        }
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

    protected void save_ServerClick(object sender, EventArgs e)
    {
        var id = "BatchId".Query().GlobalId();
        var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
        var checks = new List<InMemoryCheck>();
        foreach (var item in items)
        {
            checks.AddRange(item.CodeJson.FromJson<List<InMemoryCheck>>());
        }
        var r = "";
        var codes = Codes;
        foreach (var ck in checks)
        {
            if (codes.Contains(ck.Code))
            {
                r += "1";
            }
            else
            {
                r += "0";
            }
        }
        var dcx = new DepotCheckX
        {
            BatchId = "BatchId".Query().GlobalId(),
            Time = DateTime.Now,
            CodeJson = r,
            State = 1
        };
        DataContext.DepotCheckX.Add(dcx);
        DataContext.SaveChanges();
        Response.Redirect("~/Depot/DepotHome?DepotId={0}".Formatted(Depot.Id));
    }
}
