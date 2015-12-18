using Homory.Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;

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

        private Guid[] Courses = new[]
        {
            Guid.Parse("3465E587-8C59-48C8-84BE-08D301D67881"),
            Guid.Parse("BABEE587-8CE5-448E-AAD9-08D301D67F27"),
            Guid.Parse("9FB8E587-8C51-43C2-B4F0-08D301D68575"),
            Guid.Parse("D68FE587-8C38-456E-BE38-08D301D68CBF"),
            Guid.Parse("9C36E587-8CC9-4032-B95C-08D301D6937D"),
            Guid.Parse("40D6E587-8CD5-4446-B439-08D301D69A02"),
            Guid.Parse("F0B82122-4E2F-3522-22D7-9E5A7FFA8B13") };

        public void BindCourseware()
        {
            var repeaters = new Repeater[]
            {A0, A1, A2, A3, A4, A5, A6, B0, B1, B2, B3, B4, B5, B6, C0, C1, C2, C3, C4, C5, C6};
            foreach (var repeater in repeaters)
            {
                Func<ResourceMap, bool> funcWhere;
                Func<ResourceMap, dynamic> funcOrder;
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
                funcWhere =
                    o => o.Type == ResourceType.课件 && o.State < State.审核 && o.CatalogId == Courses[index];
                repeater.DataSource = HomoryContext.Value.ResourceMap.Where(predicate: funcWhere).OrderByDescending(funcOrder).Take(10).ToList();
                repeater.DataBind();
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }
    }
}
