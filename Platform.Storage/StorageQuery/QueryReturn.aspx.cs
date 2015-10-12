using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;

public partial class QueryReturn : SingleStoragePage
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
        tree.DataSource = db.Value.StorageCatalog.Where(o => o.State < State.删除 && o.StorageId == StorageId).OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
        tree.DataBind();
        tree.GetAllNodes().Where(o => o.Level < 2).ToList().ForEach(o => o.Expanded = true);
        tree.GetAllNodes().ToList().ForEach(o => o.Checked = true);
        day_start.SelectedDate = DateTime.Today.AddMonths(-1);
        day_end.SelectedDate = DateTime.Today;
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
        var n = name.Text.ToLower();
        var ds = int.Parse(day_start.SelectedDate.Value.AddDays(-1).ToString("yyyyMMdd"));
        var de = int.Parse(day_end.SelectedDate.Value.AddDays(1).ToString("yyyyMMdd"));
        var source = tree.CheckedNodes.Select(o => o.Value.GlobalId()).ToList().Join(db.Value.查询_归还单, o => o, o => o.分类标识, (x, y) => y).ToList().Where(o => (o.物品名称.ToLower().Contains(n) || o.拼音.ToLower().Contains(n)) && o.日期 > ds && o.日期 < de).OrderByDescending(o => o.时间).ToList();
        view.DataSource = source;
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected int CountIn(Guid objectId)
    {
        return db.Value.查询_归还单.Count(o => o.物品标识 == objectId);
    }

    protected void query_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        view.Rebind();
    }
}
