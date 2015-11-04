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

    protected string CountTotal(DepotObject obj)
    {
        var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
        var noOut = query.Count() > 0 ? query.Sum(o => o.Amount - o.ReturnedAmount) : 0;
        return (obj.Amount + noOut).ToAmount(Depot.Featured(DepotType.小数数量库));
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
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
        }
        view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
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
        Response.Redirect("~/DepotAction/In?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        var objId = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var isVirtual = Depot.Featured(DepotType.固定资产库);
        var catalogId = DataContext.DepotObjectCatalog.Single(o => o.ObjectId == objId && o.IsLeaf == true && o.IsVirtual == isVirtual).CatalogId;
        Response.Redirect("~/DepotAction/ObjectEdit?DepotId={0}&ObjectId={1}&CatalogId={2}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"], catalogId));
    }

    protected void delete_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectRemove?DepotId={0}&ObjectId={1}&CatalogId={2}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"], CurrentNode.HasValue ? CurrentNode.Value : Guid.Empty));
    }

    protected void use_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Use?DepotId={0}&ObjectId={1}&UseType=2".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void usex_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Use?DepotId={0}&ObjectId={1}&UseType=1".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void out_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Out?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }
}
