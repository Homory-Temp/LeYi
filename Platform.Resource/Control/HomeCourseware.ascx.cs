using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using Homory.Model;

namespace Control
{
	public partial class ControlHomeCourseware : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindCourseware();
			}
		}

		private Guid[] Courses = new[] { Guid.Parse("97DC99E4-CBF5-4F8B-83C8-36C4A1A37F92"), Guid.Parse("88B89ED4-09C2-4AF2-AAB2-56398A13DD93"), Guid.Parse("20ECC1AC-EF8B-4A7C-BEF1-CEC008AEBC78"), Guid.Parse("B888FFDA-0B34-444E-A33F-F4BA090F13DB"), Guid.Parse("0738784F-0578-403E-9F09-D016BF158A22"), Guid.Parse("3A204C8D-46C9-46EA-B289-BD31AA39F26E"), Guid.Parse("F0B82122-4E2F-3522-22D7-9E5A7FFA8B13") };

		public void BindCourseware()
		{
			var repeaters = new Repeater[]
			{A0, A1, A2, A3, A4, A5, A6, B0, B1, B2, B3, B4, B5, B6, C0, C1, C2, C3, C4, C5, C6};
			foreach (var repeater in repeaters)
			{
				Func<Resource, bool> funcWhere;
				Func<Resource, dynamic> funcOrder;
				var index = int.Parse(repeater.ID.Substring(1, 1));
                var order = repeater.ID.Substring(0, 1);
                switch (order)
                {
                    case "A":
                        funcOrder = o => o.Time;
                        break;
                    case "B":
                        funcOrder = o => o.View;
                        break;
                    default:
                        funcOrder = o => o.Credit;
                        break;
                }
                if (index < 6)
				{
                    funcWhere =
                        o => o.Type == ResourceType.课件 && o.State < State.审核 && o.CourseId != null && o.CourseId == Courses[index];
                    if (HomeCampus == null)
                    {
                        repeater.DataSource = HomoryContext.Value.Resource.Where(predicate: funcWhere).OrderByDescending(funcOrder).Take(10).ToList();
                    }
                    else
                    {
                        var predicate = SR();
                        repeater.DataSource = HomoryContext.Value.Resource.Where(predicate: funcWhere).Where(predicate).OrderByDescending(funcOrder).Take(10).ToList();
                    }
                    repeater.DataBind();
                }
                else
				{
                    funcWhere = o => o.Type == ResourceType.课件 && o.State < State.审核 && o.CourseId != null;
                    var courseP = Courses[index];
                    var coursesP = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.课程 && o.ParentId == courseP).ToList();
                    if (HomeCampus == null)
                    {
                        var source = coursesP.Join(HomoryContext.Value.Resource.Where(funcWhere), c => c.Id, r => r.CourseId, (c, r) => r).ToList();
                        repeater.DataSource = source;
                    }
                    else
                    {
                        var predicate = SR();
                        var source = coursesP.Join(HomoryContext.Value.Resource.Where(funcWhere).Where(predicate), c => c.Id, r => r.CourseId, (c, r) => r).ToList();
                        repeater.DataSource = source;
                    }
                    repeater.DataBind();
                }
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
