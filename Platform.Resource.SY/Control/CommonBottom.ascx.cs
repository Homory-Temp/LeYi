using Homory.Model;
using System;

namespace Control
{
	public partial class ControlCommonBottom : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
