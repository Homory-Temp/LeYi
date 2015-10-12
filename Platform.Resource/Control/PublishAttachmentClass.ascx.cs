using Homory.Model;
using System;

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
			get { return false; }
		}
	}
}
