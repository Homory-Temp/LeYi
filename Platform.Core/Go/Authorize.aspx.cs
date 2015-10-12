using Homory.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoAuthorize : HomoryCorePageWithGrid
	{
		private const string Right = "Authorize";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadInit();
                LogOp(OperationType.查询);
            }
		}

		private void LoadInit()
		{
            BindCombo();
            InitCombo();
            BindTree();
			InitTree();
		}

        private void BindCombo()
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            else
            {
                var c = CurrentCampus;
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.Id == c.Id)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            combo.DataBind();
        }

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
        }

        protected string FormatTreeNode(dynamic department)
        {
            return department.State == State.启用 ? "ui green circle icon" : "ui red circle icon";
        }

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTree();
            InitTree();
            grid.Rebind();
            view.Rebind();
            viewX.Rebind();
        }

        //protected string CountChildren(Homory.Model.Department department)
        //{
        //    var count = department.DepartmentChildren.Count(o => o.Level == department.Level + 1 && o.State < State.删除 && o.Type == DepartmentType.部门);
        //    return count == 0 ? string.Empty : string.Format(" [{0}]", count);
        //}

        private void BindTree()
		{
            if (combo.SelectedIndex < 0)
            {
                tree.DataSource = null;
            }
            else
            {
                var c = Guid.Parse(combo.SelectedItem.Value);
                var source =
                HomoryContext.Value.Department.Where(
                    o => ((o.Type == DepartmentType.学校 || o.Type == DepartmentType.部门) && o.TopId == c && o.State < State.审核));
                tree.DataSource =
                    source.OrderBy(o => o.State)
                        .ThenBy(o => o.Ordinal)
                        .ThenBy(o => o.Name).ToList();
            }
			tree.DataBind();
		}

		private void InitTree()
		{
            RadTreeNode node = null;
            if (tree.Nodes.Count > 0 && tree.Nodes[0].Nodes.Count > 0)
                node = tree.Nodes[0].Nodes[0];
            else if (tree.Nodes.Count > 0)
                node = tree.Nodes[0];
            if (node == null) return;
            node.Expanded = true;
            node.ExpandParentNodes();
            node.Selected = true;
        }

        protected string ForceTreeName(Department department)
		{
			var count = department.DepartmentUser.Count(
				o =>
					(o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.部门兼职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State == State.启用);
			return count > 0
				? string.Format("{0} [{1}]", department.DisplayName, count
					)
				: department.DisplayName;
		}

        protected void peek_Search(object sender, SearchBoxEventArgs e)
        {
            view.Rebind();
        }

        protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
		{
            tree.CollapseAllNodes();
            var node = e.Node;
            if (node.Level == 0 && node.Nodes.Count > 0)
                node = node.Nodes[0];
            node.Selected = true;
            node.ExpandParentNodes();
            node.Expanded = true;
            grid.Rebind();
            view.Rebind();
            viewX.Rebind();
        }

        protected void view_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			var departmentId = tree.SelectedNode == null || tree.SelectedNode.Level == 0
						? (Guid?)null
						: Guid.Parse(tree.SelectedNode.Value);
			var list = departmentId.HasValue
				? HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId.Value && o.State < State.审核 && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.部门兼职教师 || o.Type == DepartmentUserType.借调后部门主职教师)).ToList()
				: null;
			view.Visible = departmentId.HasValue;
            var stext = peek.Text.Trim();
            view.DataSource = departmentId.HasValue
				? list.OrderBy(o => o.Type).ThenBy(o => o.PriorOrdinal).ThenBy(o => o.RealName).ToList().Where(o => o.Account.Contains(stext) || o.RealName.Contains(stext) || o.Phone.Contains(stext) || o.PinYin.Contains(stext) || o.IDCard.Contains(stext)).ToList()
                : null;
        }

		protected void view_OnItemDrop(object sender, RadListViewItemDragDropEventArgs e)
		{
			try
			{
				var target = e.DestinationHtmlElement;
				var roleId = Guid.Parse(target);
				var userId = Guid.Parse(e.DraggedItem.GetDataKeyValue("Id").ToString());
				var ur = new UserRole
				{
					UserId = userId,
					RoleId = roleId,
					State = State.启用
				};
				HomoryContext.Value.UserRole.AddOrUpdate(ur);
				HomoryContext.Value.SaveChanges();
                LogOp(OperationType.编辑);
                grid.Rebind();
				Notify(panel, "操作成功", "success");
			}
// ReSharper disable EmptyGeneralCatchClause
			catch
// ReSharper restore EmptyGeneralCatchClause
			{
			}
		}

		protected void viewX_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			viewX.DataSource = HomoryContext.Value.Role.Where(o => o.State < State.审核).OrderBy(o => o.Ordinal).ToList();
        }

		protected void viewX_OnItemDrop(object sender, RadListViewItemDragDropEventArgs e)
		{
			var target = e.DestinationHtmlElement;
			var roleId = Guid.Parse(e.DraggedItem.GetDataKeyValue("Id").ToString());
			var userId = Guid.Parse(target);
			var ur = new UserRole
			{
				UserId = userId,
				RoleId = roleId,
				State = State.启用
			};
			HomoryContext.Value.UserRole.AddOrUpdate(ur);
			HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
            grid.Rebind();
			Notify(panel, "操作成功", "success");
		}

		protected void grid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			var departmentId = tree.SelectedNode == null || tree.SelectedNode.Level == 0
					? (Guid?)null
					: Guid.Parse(tree.SelectedNode.Value);
			var list = departmentId.HasValue
				? HomoryContext.Value.ViewRole.Where(o => o.DepartmentId == departmentId && o.State == State.启用).OrderBy(o => o.UserName).ToList()
				: null;
			grid.Visible = departmentId.HasValue;
			grid.DataSource = departmentId.HasValue ? list : null;
        }

		protected void grid_OnDeleteCommand(object sender, GridCommandEventArgs e)
		{
			try
			{
				var item = (e.Item as GridEditableItem);
// ReSharper disable PossibleNullReferenceException
				var roleId = Guid.Parse(item.GetDataKeyValue("RoleId").ToString());
// ReSharper restore PossibleNullReferenceException
				var userId = Guid.Parse(item.GetDataKeyValue("UserId").ToString());
				var ur = new UserRole
				{
					UserId = userId,
					RoleId = roleId,
					State = State.删除
				};
				HomoryContext.Value.UserRole.AddOrUpdate(ur);
				HomoryContext.Value.SaveChanges();
                LogOp(OperationType.删除);
                grid.Rebind();
				Notify(panel, "操作成功", "success");
			}
// ReSharper disable EmptyGeneralCatchClause
			catch
// ReSharper restore EmptyGeneralCatchClause
			{
			}
		}

		protected override string PageRight
		{
			get { return Right; }
		}
	}
}
