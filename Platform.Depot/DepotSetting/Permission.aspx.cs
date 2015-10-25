using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotSetting_Permission : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = DataContext.DepotRoleLoad(Depot.Id).ToList();
            tree.DataBind();
            InitializeArea();
        }
        search.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList();
    }

    protected void InitializeArea()
    {
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var role = DataContext.DepotRoleLoad(Depot.Id).Single(o => o.Id == id);
            ordinal.Value = role.Ordinal;
            name.Text = role.Name;
            if (role.State == 0)
            {
                r1.Checked = r2.Checked = r3.Checked = true;
                r1.ReadOnly = r2.ReadOnly = r3.ReadOnly = true;
                delete.Visible = false;
                sp.Visible = true;
            }
            else
            {
                r1.Checked = role.Rights.Contains(r1.Value.GetFirstChar());
                r2.Checked = role.Rights.Contains(r2.Value.GetFirstChar());
                r3.Checked = role.Rights.Contains(r3.Value.GetFirstChar());
                r1.ReadOnly = r2.ReadOnly = r3.ReadOnly = false;
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
            r1.Checked = r2.Checked = r3.Checked = false;
            r1.ReadOnly = r2.ReadOnly = r3.ReadOnly = false;
            sp.Visible = false;
            delete.Visible = false;
            search.Visible = false;
        }
        view.Rebind();
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
            var role = DataContext.DepotRoleLoad(Depot.Id).Single(o => o.Id == id);
            var userCount = DataContext.DepotUserRole.Count(o => o.DepotRoleId == id);
            return role.State == State.内置 && userCount == 1;
        }
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

    protected void add_ServerClick(object sender, EventArgs e)
    {
        var content = name.Text.Trim();
        var value = CurrentNode;
        if (content.None())
        {
            NotifyError(ap, "请输入要添加的角色名称");
            return;
        }
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var role = DataContext.DepotRoleLoad(Depot.Id).Single(o => o.Id == id);
            role.Ordinal = ordinal.PeekValue(100);
            role.Name = content;
            if (role.State == State.启用)
            {
                role.Rights = "{0}{1}{2}".Formatted(r1.PeekValue(true).GetFirstChar(), r2.PeekValue(true).GetFirstChar(), r3.PeekValue(true).GetFirstChar());
            }
            DataContext.SaveChanges();
            tree.DataSource = DataContext.DepotRoleLoad(Depot.Id).ToList();
            tree.DataBind();
            var node = tree.GetAllNodes().ToList().First(o => o.Value == id.ToString());
            node.Selected = true;
        }
        else
        {
            var role = new DepotRole
            {
                Id = DataContext.GlobalId(),
                DepotId = Depot.Id,
                Name = content,
                Rights = "{0}{1}{2}".Formatted(r1.PeekValue(true).GetFirstChar(), r2.PeekValue(true).GetFirstChar(), r3.PeekValue(true).GetFirstChar()),
                Ordinal = ordinal.PeekValue(100),
                State = State.启用
            };
            DataContext.DepotRole.Add(role);
            DataContext.SaveChanges();
            tree.DataSource = DataContext.DepotRoleLoad(Depot.Id).ToList();
            tree.DataBind();
            var node = tree.GetAllNodes().ToList().First(o => o.Value == role.Id.ToString());
            node.Selected = true;
            tree0.Nodes[0].Selected = false;
            InitializeArea();
        }
        NotifyOK(ap, "角色保存成功");
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var source = DataContext.DepotMember.Where(o => o.RoleId == id).ToList();
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
            var role = DataContext.DepotRoleLoad(Depot.Id).Single(o => o.Id == id);
            var userId = Guid.Parse((sender as HtmlInputButton).Attributes["match"]);
            var userRole = DataContext.DepotUserRole.Single(o => o.DepotRoleId == id && o.UserId == userId);
            DataContext.DepotUserRole.Remove(userRole);
            DataContext.SaveChanges();
            view.Rebind();
        }
        NotifyOK(ap, "用户成功退出角色");
    }

    protected void delete_ServerClick(object sender, EventArgs e)
    {
        if (CurrentNode.HasValue)
        {
            var id = CurrentNode.Value;
            var role = DataContext.DepotRoleLoad(Depot.Id).Single(o => o.Id == id);
            if (role.State == State.启用)
            {
                role.State = State.停用;
                DataContext.SaveChanges();
            }
        }
        tree0.Nodes[0].Selected = true;
        tree.DataSource = DataContext.DepotRoleLoad(Depot.Id).ToList();
        tree.DataBind();
        InitializeArea();
        NotifyOK(ap, "角色删除成功");
    }

    protected void search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        search.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList().Where(o => o.Name.Contains(e.FilterString) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
        search.DataBind();
    }

    protected void search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        if (CurrentNode.HasValue && !e.Value.None())
        {
            var id = CurrentNode.Value;
            var userRole = new DepotUserRole
            {
                UserId = e.Value.GlobalId(),
                DepotRoleId = id
            };
            DataContext.DepotUserRole.Add(userRole);
            DataContext.SaveChanges();
            view.Rebind();
            search.Text = string.Empty;
            NotifyOK(ap, "用户成功加入角色");
        }
    }
}
