using Homory.Model;
using System;
using System.Linq;

namespace Go
{
	public partial class GoStudio : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				studioList.DataSource = HomoryContext.Value.Group.Where(o => o.Type == GroupType.名师团队 && o.State == State.启用).ToList();
				studioList.DataBind();

				var resources =
					HomoryContext.Value.Resource.Where(
						o =>
							o.ResourceCatalog.Count(p => p.Catalog.Type == CatalogType.团队_名师 && p.State == State.启用) > 0 && o.State == State.启用).OrderByDescending(o => o.Comment).Take(5)
						.ToList();
				studioHonor.DataSource = resources;
				studioHonor.DataBind();

				latest.DataSource = HomoryContext.Value.Resource.Where(
					o =>
                        o.ResourceCatalog.Count(p => p.Catalog.Type == CatalogType.团队_名师 && p.State == State.启用) > 0 && o.State == State.启用)
					.OrderByDescending(o => o.Time)
					.Take(10)
					.ToList();
				latest.DataBind();

				popular.DataSource = HomoryContext.Value.Resource.Where(
					o =>
                        o.ResourceCatalog.Count(p => p.Catalog.Type == CatalogType.团队_名师 && p.State == State.启用) > 0 && o.State == State.启用)
					.OrderByDescending(o => o.View)
					.Take(10)
					.ToList();
				popular.DataBind();

				best.DataSource = HomoryContext.Value.Resource.Where(
					o =>
                        o.ResourceCatalog.Count(p => p.Catalog.Type == CatalogType.团队_名师 && p.State == State.启用) > 0 && o.State == State.启用)
					.OrderByDescending(o => o.Grade)
					.Take(10)
					.ToList();
				best.DataBind();
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
