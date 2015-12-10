using Homory.Model;
using System;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoCenterStudio : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				InitializeHomoryPage();
			}
		}

		protected void InitializeHomoryPage()
		{
			var user = CurrentUser;
        }

		protected override bool ShouldOnline
		{
			get { return true; }
		}

		protected void refreshFavourite_OnServerClick(object sender, EventArgs e)
		{
		}

		protected void pushPanel_OnAjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			CommonPush.ReBind();
		}
	}
}
