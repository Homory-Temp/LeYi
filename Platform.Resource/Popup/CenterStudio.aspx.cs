using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Popup
{
	public partial class PopupCenterStudio : HomoryResourcePage
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
			get { return false; }
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
