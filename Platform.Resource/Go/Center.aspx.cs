﻿using System;
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

namespace Go
{
	public partial class GoCenter : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				InitializeHomoryPage();
			}
		}

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(!string.IsNullOrWhiteSpace(Request.QueryString["DoPublish"]))
                this.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString("N"), "popupPublish();", true);
            }
        }

		protected void InitializeHomoryPage()
		{
			var user = CurrentUser;

            PersonalActionPersonal.ActionUserId = user.Id;
            PersonalActionPersonal1.ActionUserId = user.Id;
            PersonalActionPersonal2.ActionUserId = user.Id;
            PersonalActionPersonal3.ActionUserId = user.Id;
        }

		protected override bool ShouldOnline
		{
			get { return true; }
		}
	}
}
