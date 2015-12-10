using Homory.Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace Control
{
    public partial class ControlCommonAssistant : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				InitializeHomoryControl();
			}
		}

		protected void InitializeHomoryControl()
		{
            if (CatalogName == "推荐")
            {
                Func<Resource, bool> funcWhere =
                    o =>
                        o.Type == ResourceType && o.State < State.审核 &&
                        o.CourseId != null;
                L.DataSource =
                    HomoryContext.Value.Resource.Where(predicate: funcWhere).OrderByDescending(o => o.View).Take(2).ToList();
                L.DataBind();
                S.DataSource =
                    HomoryContext.Value.Resource.Where(predicate: funcWhere).OrderByDescending(o => o.View)
                    //.Skip(2)
                        .Take(10).ToList();
                S.DataBind();
                return;
            }

			var c =
				HomoryContext.Value.Catalog.SingleOrDefault(
					o => o.Type == CatalogType.课程 && o.State < State.审核 && o.Name == CatalogName);
            if (c != null && c.State == State.内置)
            {
                if (c.Name == "综合")
                {
                    Func<Resource, bool> funcWhere = o => o.Type == ResourceType.课件 && o.State < State.审核 && o.CourseId != null && o.GradeId != null && o.AssistantType == 1;
                    var courseP = Guid.Parse("F0B82122-4E2F-3522-22D7-9E5A7FFA8B13");
                    var coursesP = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.课程 && o.ParentId == courseP).ToList();
                    var source = coursesP.Join(HomoryContext.Value.Resource.Where(funcWhere), x => x.Id, r => r.CourseId, (x, r) => r).ToList();
                    L.DataSource = source.OrderByDescending(o => o.Time).Take(2).ToList();
                    L.DataBind();
                    S.DataSource =source.OrderByDescending(o => o.Time)
                        //.Skip(2)
                            .Take(10).ToList();
                    S.DataBind();
                }
                else
                {
                    Func<Resource, bool> funcWhere =
                        o =>
                            o.Type == ResourceType && o.State < State.审核 &&
                            o.CourseId == c.Id && o.GradeId != null && o.AssistantType == 1;
                    L.DataSource =
                        HomoryContext.Value.Resource.Where(predicate: funcWhere).OrderByDescending(o => o.Time).Take(2).ToList();
                    L.DataBind();
                    S.DataSource =
                        HomoryContext.Value.Resource.Where(predicate: funcWhere).OrderByDescending(o => o.Time)
                        //.Skip(2)
                            .Take(10).ToList();
                    S.DataBind();
                }
            }
		}

		public string CatalogName { get; set; }

		public ResourceType ResourceType { get; set; }

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
