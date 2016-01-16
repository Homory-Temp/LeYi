using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Batch : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SideBarSingle.Crumb = "物资管理 - {0}".Formatted(Depot.Featured(DepotType.固定资产库) ? "资产分库" : "批量转移");
        if (!IsPostBack)
        {
            //tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null, !Depot.Featured(DepotType.固定资产库)).Count().EmptyWhenZero());
            //tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            //tree.DataBind();
            if (Depot.Featured(DepotType.固定资产库))
            {
                tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList().Where(o => o.Code != "*Homory:Null*").ToList();
            }
            else
            {
                tree.DataSource = DataContext.DepotCatalogTreeNoFixLoad(Depot.Id).ToList().Where(o => o.Code != "*Homory:Null*").ToList();
            }
            tree.DataBind();
            catalog.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            catalog.DataBind();
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
        //if (tree0.SelectedNode != null)
        //    tree0.SelectedNode.Selected = false;
        tree.GetAllNodes().Where(o => o.ParentNode == e.Node.ParentNode).ToList().ForEach(o => o.Expanded = false);
        e.Node.Expanded = true;
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var node = CurrentNode;
        if (node.HasValue)
        {
            var source = DataContext.DepotObjectLoad(Depot.Id, node.Value.GlobalId(), !Depot.Featured(DepotType.固定资产库));
            if (!toSearch.Text.None())
            {
                source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
            }
            view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
        }
        else
        {
            view.DataSource = null;
        }
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void all_ServerClick(object sender, EventArgs e)
    {
        var cbs = view.Items.Select(o => o.FindControl("check") as CheckBox).ToList();
        if (cbs.All(o => o.Checked))
        {
            cbs.ForEach(o => o.Checked = false);
        }
        else
        {
            cbs.ForEach(o => o.Checked = true);
        }
    }

    public static void DepotObjectEdit(DepotEntities db, Guid id, List<Guid> catalogIds)
    {
        var obj = db.DepotObject.Single(o => o.Id == id);
        var catalogs = db.DepotObjectCatalog.Where(o => o.ObjectId == id && o.IsVirtual == false).ToList();
        for (var i = 0; i < catalogs.Count(); i++)
        {
            db.DepotObjectCatalog.Remove(catalogs.ElementAt(i));
        }
        for (var i = 0; i < catalogIds.Count; i++)
        {
            db.DepotObjectCatalog.Add(new DepotObjectCatalog { ObjectId = id, CatalogId = catalogIds[i], IsVirtual = false, Level = i, IsLeaf = i == catalogIds.Count - 1 });
        }
    }

    protected void move_ServerClick(object sender, EventArgs e)
    {
        var cbs = view.Items.Select(o => o.FindControl("check") as CheckBox).ToList();
        if (!catalog.SelectedValue.None() && cbs.Any(o => o.Checked == true))
        {
            var catalogId = catalog.SelectedValue.GlobalId();
            var list = cbs.Where(o => o.Checked == true).Select(o => o.Attributes["OBJ"].GlobalId()).ToList();
            var ids = new List<Guid>();
            var node = catalog.EmbeddedTree.SelectedNode;
            ids.Add(node.Value.GlobalId());
            while (node.ParentNode != null)
            {
                node = node.ParentNode;
                ids.Insert(0, node.Value.GlobalId());
            }
            foreach (var item in list)
            {
                var obj = DataContext.DepotObject.Single(o => o.Id == item);
                DepotObjectEdit(DataContext, obj.Id, ids);
            }
            DataContext.SaveChanges();
        }
        //tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null).Count().EmptyWhenZero());
        tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
        tree.DataBind();
        view.Rebind();
    }
}
