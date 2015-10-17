using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreSetting_Catalog : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            sp.Visible = CurrentStore.State == StoreState.食品;
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            if (!"Initial".Query().Null())
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

    protected void add_ServerClick(object sender, EventArgs e)
    {
        var content = name.Text.Trim();
        var value = CurrentNode;
        if (content.Null())
        {
            Notify(ap, "请输入要添加的类别名称", "error");
            return;
        }
        if (CurrentNode.HasValue)
        {
            var parent = CurrentNode.Value;
            if (db.Value.StoreCatalog.Count(o => o.StoreId == StoreId && o.State < 2 && o.Name == content && o.ParentId == parent) == 0)
            {
                var pinYin = db.Value.ToPinYin(content).Single();
                var catalog = new StoreCatalog
                {
                    Id = db.Value.GlobalId(),
                    ParentId = parent,
                    StoreId = StoreId,
                    Name = content,
                    PinYin = pinYin,
                    Ordinal = ordinal.PeekValue(),
                    State = 1,
                    Code = string.Empty
                };
                db.Value.StoreCatalog.Add(catalog);
                db.Value.SaveChanges();
            }
        }
        else
        {
            if (db.Value.StoreCatalog.Count(o => o.StoreId == StoreId && o.State < 2 && o.Name == content && o.ParentId == null) == 0)
            {
                var pinYin = db.Value.ToPinYin(content).Single();
                var catalog = new StoreCatalog
                {
                    Id = db.Value.GlobalId(),
                    ParentId = null,
                    StoreId = StoreId,
                    Name = content,
                    PinYin = pinYin,
                    Ordinal = ordinal.PeekValue(),
                    State = 1,
                    Code = string.Empty
                };
                db.Value.StoreCatalog.Add(catalog);
                if (CurrentStore.State == StoreState.食品)
                {
                    var dictionary = new StoreDictionary
                    {
                        StoreId = StoreId,
                        Type = DictionaryType.使用对象,
                        Name = content,
                        PinYin = catalog.PinYin
                    };
                    db.Value.StoreDictionary.Add(dictionary);
                    var dictionary2 = new StoreDictionary
                    {
                        StoreId = StoreId,
                        Type = DictionaryType.年龄段,
                        Name = content,
                        PinYin = catalog.PinYin
                    };
                    db.Value.StoreDictionary.Add(dictionary2);
                }
                db.Value.SaveChanges();
            }
        }
        ordinal.Text = string.Empty;
        ordinal.Value = null;
        name.Text = string.Empty;
        tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
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
        Notify(ap, "类别添加成功", "success");
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
        var source = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2 && o.ParentId == parent).OrderBy(o => o.Ordinal).ToList(); ;
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        var value = CurrentNode;
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var catalog = db.Value.StoreCatalog.Single(o => o.Id == id);
        if (catalog.ChildrenStoreCatalog.Count > 0)
        {
            Notify(ap, "请先删除该类别下的子类别", "error");
            return;
        }
        if (catalog.StoreObject.Count > 0)
        {
            Notify(ap, "请先删除该类别下的物资", "error");
            return;
        }
        if (CurrentStore.State == StoreState.食品 && catalog.ParentId == null)
        {
            var dictionary = db.Value.StoreDictionary.First(o => o.StoreId == StoreId && o.Type == DictionaryType.使用对象 && o.Name == catalog.Name);
            db.Value.StoreDictionary.Remove(dictionary);
            var dictionary2 = db.Value.StoreDictionary.First(o => o.StoreId == StoreId && o.Type == DictionaryType.年龄段 && o.Name == catalog.Name);
            db.Value.StoreDictionary.Remove(dictionary2);
        }
        db.Value.StoreCatalog.Remove(catalog);
        db.Value.SaveChanges();
        tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
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
        Notify(ap, "类别删除成功", "success");
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        Response.Redirect("../StoreSetting/CatalogEdit?StoreId={0}&CatalogId={1}".Formatted(StoreId, id));
    }
}
