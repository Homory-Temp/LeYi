using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services.Description;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Words.Lists;
using Homory.Model;

namespace Go
{
	public partial class GoRooms : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var s = HomoryContext.Value.ResourceRoom.Where(o => o.State < State.审核).OrderBy(o => o.Ordinal).ToList();
				toH.Style.Add("height", string.Format("{0}px", (s.Count % 2 == 0 ? s.Count / 2 : s.Count / 2 + 1) * 296 + 48));
				roomList.DataSource = s;
				roomList.DataBind();
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
