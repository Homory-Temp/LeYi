using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Object : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null).Count().EmptyWhenZero());
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            if (Depot.DefaultObjectView == "Simple".GetFirstChar())
            {
                view_simple.Attributes["class"] = "btn btn-warning";
                view_photo.Attributes["class"] = "btn btn-info";
            }
            else
            {
                view_simple.Attributes["class"] = "btn btn-info";
                view_photo.Attributes["class"] = "btn btn-warning";
            }
        }
    }

    protected bool IsSimple
    {
        get
        {
            return view_photo.Attributes["class"] != "btn btn-warning";
        }
    }

    protected Guid? CurrentNode
    {
        get
        {
            return tree.SelectedNode == null ? (Guid?)null : tree.SelectedValue.GlobalId();
        }
    }

    protected void manage_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotSetting/Catalog?DepotId={0}".Formatted(Depot.Id));
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectAdd?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, CurrentNode));
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
        view.DataSource = source.OrderBy(o => o.Ordinal).ThenByDescending(o => o.Amount).ToList();
        pager.Visible = source.Count() > pager.PageSize;
    }

    protected void view_simple_ServerClick(object sender, EventArgs e)
    {
        if (view_simple.Attributes["class"] == "btn btn-info")
        {
            view_simple.Attributes["class"] = "btn btn-warning";
            view_photo.Attributes["class"] = "btn btn-info";
            view.Rebind();
        }
    }

    protected void view_photo_ServerClick(object sender, EventArgs e)
    {
        if (view_photo.Attributes["class"] == "btn btn-info")
        {
            view_simple.Attributes["class"] = "btn btn-info";
            view_photo.Attributes["class"] = "btn btn-warning";
            view.Rebind();
        }
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/In?DepotId={0}".Formatted(Depot.Id));
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectEdit?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void delete_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectRemove?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void use_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Use?DepotId={0}".Formatted(Depot.Id));
    }
}
