using Homory.Model;
using System;

namespace Control
{
	public partial class ControlCommonTop : HomoryResourceControl
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
			common_top_sign_on_go.Visible = !IsOnline;
			common_top_user_label.Visible = common_top_sign_off_go.Visible = IsOnline;
			common_top_user_label.InnerText = IsOnline ? string.Format("你好，{0}", CurrentUser.DisplayName) : string.Empty;
		}

		protected void common_top_search_go_OnServerClick(object sender, EventArgs e)
		{
			Response.Redirect(
				string.Format("Search?Content={0}", Server.UrlEncode(common_top_search_content.Value.Trim())), false);
		}

		protected void common_top_sign_on_go_OnServerClick(object sender, EventArgs e)
		{
			Session["RESOURCE"] = "RESOURCE";
			SignOn();
		}

		protected void common_top_sign_off_go_OnServerClick(object sender, EventArgs e)
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
