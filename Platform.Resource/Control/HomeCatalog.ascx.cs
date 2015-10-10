using System;
using System.Linq;
using Homory.Model;

namespace Control
{
	public partial class ControlHomeCatalog : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindCatalog();
			}
		}

		protected void BindCatalog()
		{
			homory_course.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程 && o.State < State.审核 && o.ParentId == null).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
			homory_course.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
