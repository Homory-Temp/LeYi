using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_Object : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree0.Nodes[0].Text = "总类别{0}".Formatted(db.Value.CountObjects(null, StoreId).Single().Value.EmptyWhenZero());
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            if (CurrentStore.State == StoreState.食品)
            {
                tree0.Nodes[0].Selected = false;
                tree0.Visible = false;
                if (tree.Nodes.Count > 0)
                {
                    tree.Nodes[0].Selected = true;
                    tree.Nodes[0].Expanded = true;
                }
            }
            if (CurrentStore.DefaultView == 1)
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

    protected Guid? CurrentNode
    {
        get
        {
            return tree.SelectedNode == null ? (Guid?)null : tree.SelectedValue.GlobalId();
        }
    }

    protected void manage_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreSetting/Catalog?StoreId={0}".Formatted(StoreId));
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreAction/ObjectAdd?StoreId={0}&CatalogId={1}".Formatted(StoreId, CurrentNode));
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
}
