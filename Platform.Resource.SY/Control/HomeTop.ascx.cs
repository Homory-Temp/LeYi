using Homory.Model;
using System;
using System.Linq;

namespace Control
{
    public partial class ControlHomeTop : HomoryResourceControl
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
            home_top_sign_on_go.Visible = !IsOnline;
            home_top_user_label.Visible = home_top_sign_off_go.Visible = IsOnline;
            home_top_user_label.InnerText = IsOnline ? string.Format("你好，{0}", CurrentUser.DisplayName) : string.Empty;
            home_top_campus.InnerText = GetCampusFromSession();
        }

        protected string GetCampusFromSession()
        {
            if (string.IsNullOrEmpty(Request.QueryString["Campus"]))
            {
                Session["______Campus"] = Guid.Empty;
            }
            else
            {
                Session["______Campus"] = Guid.Parse(Request.QueryString["Campus"]);
            }

            var id = (Guid)Session["______Campus"];
            if (id == Guid.Empty)
            {
                return "HomeAllCampus".FromWebConfig();
            }
            else
            {
                return HomoryContext.Value.Department.First(o => o.Id == id).Name;
            }
        }

        protected void home_top_sign_on_go_OnServerClick(object sender, EventArgs e)
        {
            Session["RESOURCE"] = "RESOURCE";
            SignOn();
        }

        protected void home_top_sign_off_go_OnServerClick(object sender, EventArgs e)
        {
            Session.Clear();
            Session["RESOURCE"] = "RESOURCE";
            SignOff();
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }
    }
}
