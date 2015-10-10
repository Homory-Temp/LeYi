using System;
using System.Linq;
using Homory.Model;
using System.Xml.Linq;

namespace Go
{
    public partial class GoHome : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["______Campus"] = Guid.Empty;
                BindTag();
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void BindTag()
        {
            var s = HomoryContext.Value.ResourceTag.Where(o => o.State < State.审核).Select(o => o.Tag).Distinct().ToList();
            tags.DataSource = s.OrderBy(o => Guid.NewGuid()).Take(4);
            tags.DataBind();
        }

        protected void reTag_ServerClick(object sender, EventArgs e)
        {
            BindTag();
        }
    }
}
