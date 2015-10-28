using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Move : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SideBarSingle.Crumb = "物资管理 - {0}".Formatted(Depot.Featured(DepotType.固定资产库) ? "资产分库" : "批量调拨");
        if (!IsPostBack)
        {
            tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null).Count().EmptyWhenZero());
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            if (!"CatalogId".Query().None())
            {
                var cid = "CatalogId".Query();
                if (tree.GetAllNodes().Count(o => o.Value == cid) == 1)
                {
                    var node = tree.GetAllNodes().Single(o => o.Value == cid);
                    node.Selected = true;
                    node.ExpandParentNodes();
                    node.ExpandChildNodes();
                    tree0.Nodes[0].Selected = false;
                }
            }
        }
    }

    protected Guid? CurrentNode
    {
        get
        {
            return tree.SelectedNode == null ? (Guid?)null : tree.SelectedValue.GlobalId();
        }
    }

    protected void tree0_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree.SelectedNode != null)
            tree.SelectedNode.Selected = false;
        view.Rebind();
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree0.SelectedNode != null)
            tree0.SelectedNode.Selected = false;
        tree.GetAllNodes().Where(o => o.ParentNode == e.Node.ParentNode).ToList().ForEach(o => o.Expanded = false);
        e.Node.Expanded = true;
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var node = CurrentNode;
        var source = DataContext.DepotObjectLoad(Depot.Id, node.HasValue ? node.Value.GlobalId() : (Guid?)null);
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
        }
        view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }
}
