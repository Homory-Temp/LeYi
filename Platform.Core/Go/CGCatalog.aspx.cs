using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoCGCatalog : HomoryCorePageWithGrid
    {
        private const string Right = "Article";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInit();
                grid.Rebind();
            }
        }

        private void LoadInit()
        {
            courseList.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程 && o.State < State.停用).OrderBy(o => o.Ordinal).ToList();
            courseList.DataBind();
            if (courseList.Items.Count > 0)
                courseList.SelectedIndex = 0;
            if (gradeList.Items.Count > 0)
                gradeList.SelectedIndex = 0;
            BindTree();
            InitTree();
        }

        private void BindTree()
        {
            var courseId = Guid.Parse(courseList.SelectedValue);
            var gradeId = Guid.Parse(gradeList.SelectedValue);
            var l1 = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程资源 && o.State < State.删除).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            var l2 = HomoryContext.Value.Catalog.Where(o => o.Id == gradeId).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            tree.DataSource = l1.Union(l2).ToList();
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
            try
            {
                var courseId = Guid.Parse(courseList.SelectedValue);
                var gradeId = Guid.Parse(gradeList.SelectedValue);
                var parentId = tree.SelectedNode == null ? (Guid?)(null) : Guid.Parse(tree.SelectedNode.Value);
                grid.DataSource = parentId.HasValue
                    ? HomoryContext.Value.Catalog.Where(o => o.State < State.删除 && o.ParentId == parentId && o.Type == CatalogType.课程资源 && o.TopId == courseId)
                        .OrderBy(o => o.State)
                        .ThenBy(o => o.Ordinal)
                        .ToList()
                    : null;
            }
            catch
            {
                grid.DataSource = null;
            }
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            var courseId = Guid.Parse(courseList.SelectedValue);
            var gradeId = Guid.Parse(gradeList.SelectedValue);
            Guid? parentId = Guid.Parse(tree.SelectedNode.Value);
            if (parentId == Guid.Empty)
            {
                return;
            }
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
                                TopId = courseId,
                                Ordinal = ordinal,
                                State = state,
                                Type = CatalogType.课程资源
                            });
                            HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
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
                            LogOp(state);
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
            if (catalog.Id == Guid.Empty)
            {
                var count = HomoryContext.Value.Catalog.Count(o => o.State < State.删除 && o.Type == CatalogType.课程资源 && (o.ParentId == null || (o.ParentId != null && o.ParentId == ArticleTopId)));
                return count == 0 ? string.Empty : string.Format(" [{0}]", count);
            }
            else
            {
                var count = catalog.CatalogChildren.Count(o => o.State < State.删除 && o.Type == CatalogType.课程资源);
                return count == 0 ? string.Empty : string.Format(" [{0}]", count);
            }
        }

        protected override string PageRight
        {
            get { return Right; }
        }

        protected void courseList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            gradeList.SelectedIndex = 0;
            BindTree();
            InitTree();
            grid.Rebind();
        }

        protected void gradeList_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTree();
            InitTree();
            grid.Rebind();
        }
    }
}
