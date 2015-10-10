using System;
using System.Linq;
using Homory.Model;
using System.Xml.Linq;

namespace Go
{
    public partial class GoCampusHome : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["Campus"]))
                {
                    Response.Redirect("../Go/Home", true);
                    return;
                }
                Session["______Campus"] = Guid.Parse(Request.QueryString["Campus"]);
                BindTag();
            }
        }

        private Department homeCampus;

        protected Department HomeCampus
        {
            get
            {
                if (homeCampus == null)
                {
                    Guid id = Guid.Parse(Request.QueryString["Campus"]);
                    homeCampus = HomoryContext.Value.Department.First(o => o.Id == id);
                }
                return homeCampus;
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void BindTag()
        {
            var s = HomoryContext.Value.ResourceTag.Where(o => o.State < State.审核 && o.Resource.User.DepartmentUser.Count(p => p.TopDepartmentId == HomeCampus.Id && p.State < State.审核 && (p.Type == DepartmentUserType.借调后部门主职教师 || p.Type == DepartmentUserType.部门主职教师)) > 0).Select(o => o.Tag).Distinct().ToList();
            tags.DataSource = s.OrderBy(o => Guid.NewGuid()).Take(4);
            tags.DataBind();
        }

        protected void reTag_ServerClick(object sender, EventArgs e)
        {
            BindTag();
        }
    }
}
