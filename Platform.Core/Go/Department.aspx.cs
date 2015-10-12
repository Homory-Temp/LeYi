using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoDepartment : HomoryCorePageWithGrid
	{
		private const string Right = "Department";

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

        private void BindTree()
		{
            if (combo.SelectedIndex < 0)
            {
                tree.DataSource = null;
            }
            else
            {
                var c = Guid.Parse(combo.SelectedItem.Value);
                tree.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.Id == c) || (o.Type == DepartmentType.部门 && o.State < State.删除)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
			tree.DataBind();
		}

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
        }

        private void InitTree()
		{
            if (tree.Nodes.Count <= 0) return;
            tree.Nodes[0].Expanded = true;
            tree.Nodes[0].Selected = true;
        }

		protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			var parentId = tree.SelectedNode == null ? (Guid?)(null) : Guid.Parse(tree.SelectedNode.Value);
			grid.DataSource = parentId.HasValue
				? HomoryContext.Value.Department.Where(o => o.State < State.删除 && o.ParentId == parentId.Value && o.Type == DepartmentType.部门)
					.OrderBy(o => o.State)
					.ThenBy(o => o.Ordinal)
                    .ThenBy(o => o.Name)
					.ToList()
				: null;
			grid.Visible = parentId.HasValue;
            LogOp(OperationType.新增);
        }

		protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
		{
			var parentId = Guid.Parse(tree.SelectedNode.Value);
			var level = tree.SelectedNode.Level + 1;
            var rootNode = tree.SelectedNode;
            while (rootNode.Level > 0)
            {
                rootNode = rootNode.ParentNode;
            }
            var rootId = Guid.Parse(rootNode.Value);
			foreach (var command in e.Commands)
			{
				var values = command.NewValues;
				if (NotSet(values, "Name"))
					continue;
				var name = values["Name"].ToString();
				var ordinal = Get(values, "Ordinal", 99);
				var state = Get(values, "State", State.启用);
				switch (command.Type)
				{
					case GridBatchEditingCommandType.Insert:
						{
                            var d = new Homory.Model.Department
                            {
                                Id = HomoryContext.Value.GetId(),
                                TopId = rootId,
                                Name = name,
                                DisplayName = name,
                                ParentId = parentId,
                                Level = level,
                                Hidden = false,
                                Ordinal = ordinal,
                                State = state,
                                Type = DepartmentType.部门,
                                Code = string.Empty,
                                BuildType = BuildType.无,
                                ClassType = ClassType.无
                            };
                            HomoryContext.Value.Department.Add(d);
                            try { DepartmentHelper.InsertDepartment(parentId.ToString().ToUpper(), d.Id.ToString().ToUpper(), name, ordinal); } catch { }
                            HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
						}
					case GridBatchEditingCommandType.Update:
						{
							var id = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.Department.Where(o => o.Id == id).Update(o => new Homory.Model.Department
							{
								Name = name,
								DisplayName = name,
								Ordinal = ordinal,
								State = state,
							});
                            try { DepartmentHelper.UpdateDepartment(name, ordinal, state, id.ToString().ToUpper()); } catch { }
							HomoryContext.Value.SaveChanges();
                            LogOp(state);
                        }
						break;
				}
			}
			RebindBatch();
			Notify(panel, "操作成功", "success");
		}

		protected void tree_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
		{
			if (CurrentRights.Contains(HomoryCoreConstant.RightMoveDepartment))
			{
				if (e.SourceDragNode == null || e.DestDragNode == null || e.SourceDragNode.Level == 0 ||
					e.SourceDragNode.ParentNode.Value == e.DestDragNode.Value) return;
				var id = Guid.Parse(e.SourceDragNode.Value);
				var parentId = Guid.Parse(e.DestDragNode.Value);
                var rootNode = e.DestDragNode;
                while (rootNode.Level > 0)
                {
                    rootNode = rootNode.ParentNode;
                }
                var rootId = Guid.Parse(rootNode.Value);
                var level = e.DestDragNode.Level + 1;
                var c6obj = HomoryContext.Value.Department.First(o => o.Id == id);
                HomoryContext.Value.Department.Where(o => o.Id == id).Update(o => new Homory.Model.Department
				{
					ParentId = parentId,
                    TopId = rootId,
					Level = level
				});
                HomoryContext.Value.SaveChanges();
                LogOp(OperationType.编辑);
                HomoryContext = new Lazy<Entities>();
				RebindMove(e.SourceDragNode.ParentNode.Value, e.SourceDragNode.Value, e.DestDragNode.Value);
                try { DepartmentHelper.UpdateDepartment(c6obj.Name, c6obj.Ordinal, c6obj.State, c6obj.Id.ToString().ToUpper(), parentId.ToString().ToUpper()); } catch { }
                Notify(panel, "操作成功", "success");
			}
			else
			{
				Notify(panel, "无权限调动部门", "warn");
			}
		}

		protected void RebindExpanded()
		{
			var expanded = tree.GetAllNodes().Where(o => o.Expanded).Select(o => o.Value).ToList();
			BindTree();
			foreach (
				var toExpand in
					expanded.Select(value => tree.GetAllNodes().SingleOrDefault(o => o.Value == value))
						.Where(toExpand => toExpand != null))
			{
				toExpand.Expanded = true;
			}
		}

		protected void RebindBatch()
		{
			var selected = tree.SelectedNode.Value;
			RebindExpanded();
			tree.GetAllNodes().Single(o => o.Value == selected).Selected = true;
		}

		protected void RebindMove(string sourceParent, string source, string target)
		{
            BindTree();
            tree.GetAllNodes().Single(o => o.Value == source).Expanded = true;
            tree.GetAllNodes().Single(o => o.Value == source).ExpandParentNodes();
            tree.GetAllNodes().Single(o => o.Value == source).Selected = true;
            //RebindExpanded();
            grid.Rebind();
		}

		protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
		{
            tree.CollapseAllNodes();
            e.Node.Selected = true;
            e.Node.ExpandParentNodes();
            e.Node.Expanded = true;
			grid.Rebind();
		}

		protected string CountChildren(Homory.Model.Department department)
		{
			var count = department.DepartmentChildren.Count(o => o.Level == department.Level + 1 && o.State < State.删除 && o.Type == DepartmentType.部门);
			return count == 0 ? string.Empty : string.Format(" [{0}]", count);
		}

		protected override string PageRight
		{
			get { return Right; }
		}

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTree();
            InitTree();
            grid.Rebind();
        }
    }
}
