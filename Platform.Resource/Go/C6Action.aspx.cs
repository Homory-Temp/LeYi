using System;
using Homory.Model;

namespace Go
{
    public partial class GoC6Action : HomoryResourcePage
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
