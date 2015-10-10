using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Popup
{
	public partial class CampusSelector : HomoryResourcePage
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                view.DataSource = HomoryContext.Value.Department.Where(o => o.State < State.审核 && o.ParentId == null).OrderBy(o => o.Ordinal).ToList();
                view.DataBind();
                var campus = (Guid)Session["______Campus"];
                if (campus == Guid.Empty)
                {
                    all.Checked = true;
                }
                else
                {
                    var list = view.Items.Select(o => (RadButton)o.FindControl("one")).ToList();
                    foreach (var btn in list)
                    {
                        btn.Checked = btn.Value == campus.ToString();
                    }

                    all.Checked = false;
                }
            }
        }

        protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
