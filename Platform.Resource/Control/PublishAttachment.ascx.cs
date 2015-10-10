using System;
using Homory.Model;

namespace Control
{
	public partial class ControlPublishAttachment : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
			}
		}

		protected override bool ShouldOnline
		{
			get { return true; }
		}
	}
}
