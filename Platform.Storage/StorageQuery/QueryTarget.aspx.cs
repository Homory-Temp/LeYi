using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;

public partial class QueryTarget : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitQueryIn();
        }
    }

    protected void InitQueryIn()
    {
        day_start.SelectedDate = DateTime.Today.AddMonths(-1);
        day_end.SelectedDate = DateTime.Today;
        if (!"Number".Query(true).Null())
            number.Text = "Number".Query(true);
        view.Rebind();
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        e.Node.Selected = false;
        e.Node.Checked = !e.Node.Checked;
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var n = number.Text.ToLower();
        var u = people.Text.ToLower();
        var r = receipt.Text.ToLower();
        var ds = int.Parse(day_start.SelectedDate.Value.AddDays(-1).ToString("yyyyMMdd"));
        var de = int.Parse(day_end.SelectedDate.Value.AddDays(1).ToString("yyyyMMdd"));
        var source = db.Value.查询_购置单.Where(o => o.仓库 == StorageId).OrderByDescending(o => o.采购日期).ToList().Where(o => o.购置单号.ToLower().Contains(n) && o.发票编号.ToLower().Contains(r) && o.采购日期 > ds && o.采购日期 < de && (o.保管人.ToLower().Contains(u) || o.经手人.ToLower().Contains(u))).OrderByDescending(o => o.采购日期).ToList();
        view.DataSource = source;
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected void query_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        view.Rebind();
    }

    protected void view_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            (e.Item.FindControl("t") as RadToolTip).TargetControlID = e.Item.FindControl("c").ClientID;
        }
        catch
        {
        }
    }
}
