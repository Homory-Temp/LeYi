using Homory.Model;
using System;
using System.Linq;

namespace Control
{
	public partial class ControlHomeStudio : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindStudio();
			}
		}

		public void BindStudio()
		{
			studio.DataSource = HomoryContext.Value.Group.Where(o => o.State < State.审核 && o.Type == GroupType.名师团队).Take(5).ToList();
			studio.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
