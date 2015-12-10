using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Go
{
    public partial class GoViewStudioX : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				leader.DataSource = CurrentGroup.GroupUser.Where(o => o.Type == GroupUserType.创建者 && o.State < State.审核).Select(o => o.User).ToList();
                leader.DataBind();

                members.DataSource = CurrentGroup.GroupUser.Where(o => o.Type != GroupUserType.创建者 && o.State < State.审核).Select(o => o.User).ToList();
                members.DataBind();

				var list = new List<Catalog>();
                var cid = Guid.Parse(Request.QueryString["CatalogId"]);
				list.AddRange(HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_名师 && o.ParentId == CurrentGroup.Id && o.Id == cid && o.State < State.审核).ToList());
                foreach (var catalog in HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_名师 && o.ParentId == CurrentGroup.Id && o.Id == cid && o.State < State.审核).ToList())
				{
                    list.AddRange(HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_名师 && o.ParentId == catalog.Id && o.State < State.审核).ToList());
				}
				catalogs.DataSource = list.Where(o => o.ResourceCatalog.Count(p => p.State == State.启用) > 0).ToList().OrderBy(o => o.Ordinal).ToList();
                catalogs.DataBind();

                introduction.InnerText = CurrentGroup.Introduction;
			}
		}

		private Group _group;

		protected Group CurrentGroup
		{
			get
			{
				if (_group == null)
				{
					var id = Guid.Parse(Request.QueryString["Id"]);
					_group = HomoryContext.Value.Group.Single(o => o.Id == id);
				}
				return _group;
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}

        protected void catalogs_OnItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var catalog = e.Item.DataItem as Catalog;
			var item = e.Item.DataItem;
			dynamic row = e.Item.DataItem;
            var id = row.Id;
			var control = e.Item.FindControl("resources") as Repeater;
			control.DataSource =
				HomoryContext.Value.Resource.ToList().Where(
					o => o.ResourceCatalog.Count(p => p.CatalogId == id && p.State < State.审核) > 0 && o.State == State.启用).OrderByDescending(o => o.Time)
					.ToList();
			control.DataBind();
		}
	}
}
