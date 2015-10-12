using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using Telerik.Web.UI;

namespace Extended
{
    public partial class ExtendedStudioCatalog : HomoryCorePageWithGrid
	{
		private const string Right = "Studio";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
// ReSharper disable Html.PathError
				Response.Redirect("~/Go/Studio", false);
// ReSharper restore Html.PathError
				return;
			}
			if (!IsPostBack)
			{
				LoadInit();
			}
		}

		private Group _group;

		protected Group CurrentGroup
		{
			get
			{
				if (_group == null)
				{
					var id = Guid.Parse(Request.QueryString[0]);
					_group = HomoryContext.Value.Group.SingleOrDefault(o => o.Id == id);
				}
				return _group;
			}
		}

		private void LoadInit()
		{
			loading.InitialDelayTime = int.Parse("Busy".FromWebConfig());
			BindTree();
			InitTree();
		}

		private void BindTree()
		{
			tree.DataSource = HomoryContext.Value.Catalog.Where(o => o.TopId == CurrentGroup.Id && o.State == State.启用).OrderBy(o => o.Ordinal).ToList();
			tree.DataBind();
		}

		private void InitTree()
		{
			tree.ExpandAllNodes();
			if (tree.Nodes.Count <= 0) return;
			tree.Nodes[0].Expanded = true;
			tree.Nodes[0].Selected = true;
		}

		protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			var parentId = tree.SelectedNode == null ? (Guid?)(null) : Guid.Parse(tree.SelectedNode.Value);
			grid.DataSource = parentId.HasValue
				? HomoryContext.Value.Catalog.Where(o => o.State == State.启用 && o.ParentId == parentId.Value && o.Type == CatalogType.团队_名师)
					.OrderBy(o => o.State)
					.ThenBy(o => o.Ordinal)
					.ToList()
				: null;
		}

		protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
		{
			var parentId = Guid.Parse(tree.SelectedNode.Value);
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
						HomoryContext.Value.Catalog.Add(new Catalog
						{
							Id = HomoryContext.Value.GetId(),
							Name = name,
							ParentId = parentId,
                            TopId = CurrentGroup.Id,
							Ordinal = ordinal,
							State = state,
							Type = CatalogType.团队_名师
						});
						HomoryContext.Value.SaveChanges();
						break;
					}
					case GridBatchEditingCommandType.Update:
					{
						var id = Get(values, "Id", Guid.Empty);
						HomoryContext.Value.Catalog.Where(o => o.Id == id).Update(o => new Catalog
						{
							Name = name,
							Ordinal = ordinal,
							State = state,
						});
						HomoryContext.Value.SaveChanges();
					}
						break;
				}
			}
			RebindBatch();
			Notify(panel, "操作成功", "success");
		}

		protected string FormatTreeNode(dynamic catalog)
		{
			return catalog.State < State.审核 ? "ui green circle icon" : "ui red circle icon";
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

		protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
		{
			e.Node.Selected = true;
			e.Node.Expanded = true;
			grid.Rebind();
		}

		protected string CountChildren(Catalog catalog)
		{
			var count = catalog.CatalogChildren.Count(o => o.State == State.启用 && o.Type == CatalogType.团队_名师);
			return count == 0 ? string.Empty : string.Format(" [{0}]", count);
		}

		protected override string PageRight
		{
			get { return Right; }
		}
	}
}
