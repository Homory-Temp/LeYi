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

        }
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
        var checks = new List<InMemoryCheck>();
        foreach (var item in items)
        {
            checks.AddRange(item.CodeJson.FromJson<List<InMemoryCheck>>());
        }
        name.InnerText = "{0} 总数：{1} 已盘：{2} 未盘：{3}".Formatted(items[0].Name, checks.Count, checks.Count(o => o.In == true), checks.Count(o => o.In == false));
        view.DataSource = checks;
        h.Value = checks.ToJson();
    }
}
