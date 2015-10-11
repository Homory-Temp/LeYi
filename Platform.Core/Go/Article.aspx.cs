using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;
using System.Collections.Generic;

namespace Go
{
    public partial class GoArticle : HomoryCorePageWithGrid
    {
        private const string Right = "Article";

        private static Guid ArticleTopId = Guid.Parse("023caf84-4f7b-4777-abeb-66137b4e71fd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInit();
            }
        }

        private void LoadInit()
        {
            BindTree();
            InitTree();
        }

        private void BindTree()
        {
            tree.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.文章 && o.State < State.删除).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
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
                ? HomoryContext.Value.Catalog.Where(o => o.State < State.删除 && o.State > State.内置 && o.ParentId == parentId.Value && o.Type == CatalogType.文章)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.Ordinal)
                    .ToList()
                : null;
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            Guid? parentId = Guid.Parse(tree.SelectedNode.Value);
            if (parentId == Guid.Empty)
            {
                parentId = null;
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
                                Ordinal = ordinal,
                                State = state,
                                Type = CatalogType.文章
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
                var count = HomoryContext.Value.Catalog.Count(o => o.State < State.删除 && o.Type == CatalogType.文章 && (o.ParentId == null || (o.ParentId != null && o.ParentId == ArticleTopId)));
                return count == 0 ? string.Empty : string.Format(" [{0}]", count);
            }
            else
            {
                var count = catalog.CatalogChildren.Count(o => o.State < State.删除 && o.Type == CatalogType.文章);
                return count == 0 ? string.Empty : string.Format(" [{0}]", count);
            }
        }

        protected override string PageRight
        {
            get { return Right; }
        }
    }
}
