using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreSetting_Permission : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            tree.DataSource = db.Value.StoreRole.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            InitializeArea();
        }
        search.DataSource = db.Value.User.Where(o => o.State < 2 && o.Type == 1).ToList();
    }

    protected Guid? CurrentNode
    {
        get
        {
            return tree.SelectedNode == null ? (Guid?)null : tree.SelectedValue.GlobalId();
        }
    }

    protected bool OnlyOne
    {
        get
        {
            var id = CurrentNode.Value;
            var role = db.Value.StoreRole.Single(o => o.Id == id);
            return role.State == 0 && role.User.Count == 1;
        }
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        var content = name.Text.Trim();
        var value = CurrentNode;
        if (content.Null())
        {
            Notify(ap, "请输入要添加的角色名称", "error");
            return;
        }
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var role = db.Value.StoreRole.Single(o => o.Id == id);
            role.Ordinal = ordinal.PeekValue();
            role.Name = content;
            if (role.State == 1)
            {
                role.Right = "{0}{1}{2}{3}{4}{5}".Formatted(r1.PeekValue(true), r2.PeekValue(true), r3.PeekValue(true), r4.PeekValue(true), r5.PeekValue(true), r6.PeekValue(true));
            }
            db.Value.SaveChanges();
            tree.DataSource = db.Value.StoreRole.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            var node = tree.GetAllNodes().ToList().First(o => o.Value == id.ToString());
            node.Selected = true;
        }
        else
        {
            var role = new StoreRole
            {
                Id = db.Value.GlobalId(),
                StoreId = StoreId,
                Name = content,
                Right = "{0}{1}{2}{3}{4}{5}".Formatted(r1.PeekValue(true), r2.PeekValue(true), r3.PeekValue(true), r4.PeekValue(true), r5.PeekValue(true), r6.PeekValue(true)),
                Ordinal = ordinal.PeekValue(),
                State = 1
            };
            db.Value.StoreRole.Add(role);
            db.Value.SaveChanges();
            tree.DataSource = db.Value.StoreRole.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            var node = tree.GetAllNodes().ToList().First(o => o.Value == role.Id.ToString());
            node.Selected = true;
            tree0.Nodes[0].Selected = false;
            InitializeArea();
        }
        Notify(ap, "角色保存成功", "success");
    }

    protected void tree0_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree.SelectedNode != null)
            tree.SelectedNode.Selected = false;
        InitializeArea();
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree0.SelectedNode != null)
            tree0.SelectedNode.Selected = false;
        tree.GetAllNodes().Where(o => o.ParentNode == e.Node.ParentNode).ToList().ForEach(o => o.Expanded = false);
        e.Node.Expanded = true;
        InitializeArea();
    }

    protected void InitializeArea()
    {
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var role = db.Value.StoreRole.Single(o => o.Id == id);
            ordinal.Value = role.Ordinal;
            name.Text = role.Name;
            if (role.State == 0)
            {
                r1.Checked = r2.Checked = r3.Checked = r4.Checked = r5.Checked = r6.Checked = true;
                r1.ReadOnly = r2.ReadOnly = r3.ReadOnly = r4.ReadOnly = r5.ReadOnly = r6.ReadOnly = true;
                delete.Visible = false;
                sp.Visible = true;
            }
            else
            {
                r1.Checked = role.Right.Contains("1");
                r2.Checked = role.Right.Contains("2");
                r3.Checked = role.Right.Contains("3");
                r4.Checked = role.Right.Contains("4");
                r5.Checked = role.Right.Contains("5");
                r6.Checked = role.Right.Contains("6");
                delete.Visible = true;
                sp.Visible = false;
            }
            search.Visible = true;
        }
        else
        {
            ordinal.Text = string.Empty;
            ordinal.Value = null;
            name.Text = string.Empty;
            r1.Checked = r2.Checked = r3.Checked = r4.Checked = r5.Checked = r6.Checked = false;
            r1.ReadOnly = r2.ReadOnly = r3.ReadOnly = r4.ReadOnly = r5.ReadOnly = r6.ReadOnly = false;
            sp.Visible = false;
            delete.Visible = false;
            search.Visible = false;
        }
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var source = db.Value.Store_Visitor.Where(o => o.RoleId == id).ToList();
            view.DataSource = source;
            pager.Visible = source.Count > pager.PageSize;
        }
        else
        {
            view.DataSource = null;
            pager.Visible = false;
        }
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var role = db.Value.StoreRole.Single(o => o.Id == id);
            var user = db.Value.GetUser((sender as HtmlInputButton).Attributes["match"]);
            role.User.Remove(user);
            db.Value.SaveChanges();
            view.Rebind();
        }
        Notify(ap, "用户成功退出角色", "success");
    }

    protected void delete_ServerClick(object sender, EventArgs e)
    {
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var role = db.Value.StoreRole.Single(o => o.Id == id);
            if (role.State == 1)
            {
                role.State = 2;
                db.Value.SaveChanges();
            }
        }
        tree0.Nodes[0].Selected = true;
        tree.DataSource = db.Value.StoreRole.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
        tree.DataBind();
        InitializeArea();
        Notify(ap, "角色删除成功", "success");
    }

    protected void search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        search.DataSource = db.Value.User.Where(o => o.State < 2 && o.Type == 1).ToList().Where(o => o.RealName.Contains(e.FilterString) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
        search.DataBind();
    }

    protected void search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        if (CurrentNode.HasValue && !e.Value.Null())
        {
            var id = CurrentNode.Value;
            var role = db.Value.StoreRole.Single(o => o.Id == id);
            role.User.Add(db.Value.GetUser(e.Value));
            db.Value.SaveChanges();
            view.Rebind();
            search.Text = string.Empty;
            Notify(ap, "用户成功加入角色", "success");
        }
    }
}
