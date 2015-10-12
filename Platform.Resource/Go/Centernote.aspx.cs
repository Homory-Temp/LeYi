using Homory.Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoCenternote : HomoryResourcePage
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
			result.DataSource = HomoryContext.Value.MediaNote.Where(o => o.UserId == CurrentUser.Id).OrderByDescending(o =>o.Time).ToList();
			result.DataBind();
        }

		protected override bool ShouldOnline
		{
			get { return true; }
		}

		protected void refreshFavourite_OnServerClick(object sender, EventArgs e)
		{
		}

		protected void filterGo_OnServerClick(object sender, EventArgs e)
		{
			var timeS = ts.SelectedDate;
			var timeE = te.SelectedDate;
			if (!timeE.HasValue)
			{
				timeE = DateTime.Today;
			}
			if (!timeS.HasValue)
			{
				timeS = timeE.Value.AddMonths(-3);
			}
			if (timeS.Value > timeE.Value)
			{
				var timeT = timeS.Value;
				timeS = timeE;
				timeE = timeT;
			}
			timeE = new DateTime(timeE.Value.Year, timeE.Value.Month, timeE.Value.Day).AddDays(1);
			result.DataSource =
				HomoryContext.Value.MediaNote.Where(o => o.UserId == CurrentUser.Id)
					.ToList()
					.Where(o => o.Resource.Time > timeS.Value && o.Resource.Time < timeE.Value)
					.ToList();
			result.DataBind();
		}
	}
}
