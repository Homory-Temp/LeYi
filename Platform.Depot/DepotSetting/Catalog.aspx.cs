using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotSetting_Catalog : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            tree.DataSource = DataContext.DepotCatalogLoad(Depot.Id).ToList();
            tree.DataBind();
            if (!"Initial".Query().None())
            {
                tree0.Nodes[0].Selected = false;
                var value = "Initial".Query();
                var node = tree.GetAllNodes().ToList().First(o => o.Value == value);
                node.Selected = true;
                node.Expanded = true;
                node.ExpandParentNodes();
                node.ExpandChildNodes();
                view.Rebind();
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

    protected Guid? TopNode
    {
        get
        {
            if (CurrentNode.HasValue)
            {
                var node = tree.SelectedNode;
                while (node.ParentNode != null)
                {
                    node = node.ParentNode;
                }
                return node.Value.GlobalId();
            }
            else
            {
                return null;
            }
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
        var parent = CurrentNode;
        var source = DataContext.DepotCatalogLoad(Depot.Id).Where(o => o.ParentId == parent).ToList();
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        var content = name.Text.Trim();
        var value = CurrentNode;
        if (content.None())
        {
            NotifyError(ap, "请输入要添加的类别名称");
            return;
        }
        if (CurrentNode.HasValue)
        {
            var parent = CurrentNode.Value;
            if (DataContext.DepotCatalogLoad(Depot.Id).Count(o => o.Name == content && o.ParentId == parent) == 0)
            {
                var pinYin = DataContext.ToPinYin(content).Single();
                var catalog = new DepotCatalog
                {
                    Id = DataContext.GlobalId(),
                    ParentId = parent,
                    TopId = TopNode.Value.GlobalId(),
                    DepotId = Depot.Id,
                    Name = content,
                    PinYin = pinYin,
                    Ordinal = ordinal.PeekValue(100),
                    State = State.启用,
                    Code = string.Empty
                };
                DataContext.DepotCatalog.Add(catalog);
                DataContext.SaveChanges();
                catalog.Code = DataContext.ToQR(CodeType.Catalog, catalog.AutoId);
                DataContext.SaveChanges();
            }
        }
        else
        {
            if (DataContext.DepotCatalogLoad(Depot.Id).Count(o => o.Name == content && o.ParentId == null) == 0)
            {
                var pinYin = DataContext.ToPinYin(content).Single();
                var id = DataContext.GlobalId();
                var catalog = new DepotCatalog
                {
                    Id = id,
                    ParentId = null,
                    TopId = id,
                    DepotId = Depot.Id,
                    Name = content,
                    PinYin = pinYin,
                    Ordinal = ordinal.PeekValue(100),
                    State = State.启用,
                    Code = string.Empty
                };
                DataContext.DepotCatalog.Add(catalog);
                DataContext.SaveChanges();
                catalog.Code = DataContext.ToQR(CodeType.Catalog, catalog.AutoId);
                DataContext.SaveChanges();
            }
        }
        ordinal.Text = string.Empty;
        ordinal.Value = null;
        name.Text = string.Empty;
        tree.DataSource = DataContext.DepotCatalogLoad(Depot.Id).ToList();
        tree.DataBind();
        if (value.HasValue)
        {
            var node = tree.GetAllNodes().ToList().First(o => o.Value == value.Value.ToString());
            node.Selected = true;
            node.Expanded = true;
            node.ExpandParentNodes();
            node.ExpandChildNodes();
        }
        else
        {
            tree0.Nodes[0].Selected = true;
        }
        view.Rebind();
        NotifyOK(ap, "物资类别添加成功");
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        Response.Redirect("../DepotSetting/CatalogEdit?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, id));
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        var value = CurrentNode;
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var childrenCount = DataContext.DepotCatalog.Count(o => o.ParentId == id && o.State < State.停用);
        if (childrenCount > 0)
        {
            NotifyError(ap, "请先删除该类别下的子类别");
            return;
        }
        var objectCount = DataContext.DepotObjectCatalog.Where(o => o.CatalogId == id).Join(DataContext.DepotObject.Where(o => o.State < State.停用), o => o.ObjectId, o => o.Id, (c, o) => o.Id).Count();
        if (objectCount > 0)
        {
            NotifyError(ap, "请先删除该类别下的物资");
            return;
        }
        var catalog = DataContext.DepotCatalog.Single(o => o.Id == id);
        catalog.State = State.停用;
        DataContext.SaveChanges();
        tree.DataSource = DataContext.DepotCatalogLoad(Depot.Id).ToList();
        tree.DataBind();
        if (value.HasValue)
        {
            var node = tree.GetAllNodes().ToList().First(o => o.Value == value.Value.ToString());
            node.Selected = true;
            node.Expanded = true;
            node.ExpandParentNodes();
            node.ExpandChildNodes();
        }
        else
        {
            tree0.Nodes[0].Selected = true;
        }
        view.Rebind();
        NotifyOK(ap, "类别删除成功");
    }
}
