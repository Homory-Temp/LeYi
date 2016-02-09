using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Move : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SideBarSingle.Crumb = "物资管理 - {0}".Formatted(Depot.Featured(DepotType.固定资产库) ? "资产分库" : "批量转移");
        if (!IsPostBack)
        {
            tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null).Count().EmptyWhenZero());
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            target.DataSource = DataContext.Depot.Where(o => o.State < State.停用).ToList().Where(o => !o.Featured(DepotType.固定资产库)).OrderBy(o => o.Ordinal).ToList().Where(o => CanVisit(o.Id)).ToList();
            target.DataBind();
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
        var source = DataContext.DepotObjectXLoad(Depot.Id, node.HasValue ? node.Value.GlobalId() : (Guid?)null);
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
        }
        if (combo.SelectedIndex == 1)
        {
            source = source.Where(o => o.DepotId != null).ToList();
        }
        else if (combo.SelectedIndex == 2)
        {
            source = source.Where(o => o.DepotId == null).ToList();
        }
        view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void all_ServerClick(object sender, EventArgs e)
    {
        var cbs = view.Items.Select(o => o.FindControl("check") as CheckBox).ToList();
        if (cbs.Where(o => o.Enabled).All(o => o.Checked))
        {
            cbs.ForEach(o => o.Checked = false);
        }
        else
        {
            cbs.ForEach(o => o.Checked = true && o.Enabled);
        }
    }

    protected bool CanVisit(Guid depotId)
    {
        var userId = DepotUser.Id;
        return DataContext.DepotMember.Count(o => o.Id == userId && o.DepotId == depotId) > 0;
    }

    public static Guid DepotCatalogXAdd(DepotEntities db, Guid depotId, string name)
    {
        var pinYin = db.ToPinYin(name).Single();
        var newId = db.GlobalId();
        var catalog = new DepotCatalog
        {
            Id = newId,
            ParentId = null,
            TopId = newId,
            DepotId = depotId,
            Name = "未分类",
            PinYin = "WFL",
            Ordinal = int.MaxValue,
            State = State.启用,
            Code = "*Homory:Null*"
        };
        db.DepotCatalog.Add(catalog);
        db.SaveChanges();
        return newId;
    }

    protected void move_ServerClick(object sender, EventArgs e)
    {
        var cbs = view.Items.Select(o => o.FindControl("check") as CheckBox).ToList();
        if (!target.SelectedValue.None() && cbs.Any(o => o.Checked == true))
        {
            var depotId = target.SelectedValue.GlobalId();
            var depot = DataContext.Depot.Single(o => o.Id == depotId);
            var catalogObj = DataContext.DepotCatalog.SingleOrDefault(o => o.DepotId == depotId && o.State == State.启用 && o.ParentId == null && o.Code == "*Homory:Null*");
            Guid catalog;
            if (catalogObj == null)
            {
                catalog = DepotCatalogXAdd(DataContext, depotId, "未分类");
            }
            else
            {
                catalog = catalogObj.Id;
            }
            var list = cbs.Where(o => o.Checked == true).Select(o => o.Attributes["OBJ"].GlobalId()).ToList();
            foreach(var item in list)
            {
                var obj = DataContext.DepotObject.Single(o => o.Id == item);
                obj.DepotId = depotId;
                DataContext.DepotObjectCatalog.Add(new DepotObjectCatalog { ObjectId = item, CatalogId = catalog, IsVirtual = false, Level = 0, IsLeaf = true });
            }
            DataContext.SaveChanges();
        }
        tree.GetAllNodes().Where(o => o.Selected).ToList().ForEach(o => { o.Selected = false; });
        view.Rebind();
    }
}
