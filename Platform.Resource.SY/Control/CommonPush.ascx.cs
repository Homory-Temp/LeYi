using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace Control
{
    public partial class ControlCommonPush : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var list = new List<dynamic>();
				var groups =
					CurrentUser.GroupUser.Where(o => o.State < State.审核)
						.Select(o => o.Group)
						.Where(o => o.Type == GroupType.名师团队)
						.ToList();
				list.AddRange(groups.Select(o => new {o.Id, ParentId = (Guid?)null, o.Name}));
				foreach (var group in groups)
				{
					list.AddRange(
						HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.团队_名师 && o.ParentId != null && o.ParentId == group.Id).OrderBy(o => o.Ordinal)
                            .ToList()
							.Select(o => new {o.Id, o.ParentId, o.Name}));
				}
				groupTree.DataSource = list;
				groupTree.DataBind();
				groupTree.ExpandAllNodes();
			}
		}

		protected override bool ShouldOnline
		{
			get { return true; }
		}

		protected void groupTree_OnNodeClick(object sender, RadTreeNodeEventArgs e)
		{
			e.Node.Selected = true;
			pop.Visible = e.Node.Nodes.Count == 0;
			pop.Attributes["onclick"] = string.Format("popupPush('../Popup/PublishPush?{0}');",e.Node.Value);
			result.DataSource =
				HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用)
					.ToList()
					.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == CatalogId && p.State == State.启用) > 0)
					.ToList();
			result.DataBind();
		}

		protected Guid CatalogId
		{
			get
			{
				if (groupTree.SelectedNode == null)
				{
					return Guid.Empty;
				}
				return Guid.Parse(groupTree.SelectedNode.Value);
			}
		}

		public void ReBind()
		{
			result.DataSource =
				HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用)
					.ToList()
					.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == CatalogId && p.State == State.启用) > 0)
					.ToList();
			result.DataBind();
		}

		protected void btnDo_OnServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["itemid"]);
			var rc = new ResourceCatalog
			{
				ResourceId = id,
				CatalogId = CatalogId,
				State = State.删除
			};
			HomoryContext.Value.ResourceCatalog.AddOrUpdate(rc);
			HomoryContext.Value.SaveChanges();
			result.DataSource =
				HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用)
					.ToList()
					.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == CatalogId && p.State == State.启用) > 0)
					.ToList();
			result.DataBind();
		}
	}
}
