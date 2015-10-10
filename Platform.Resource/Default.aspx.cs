using Homory.Model;
using System;

public partial class Default : HomoryPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
        Response.Redirect(Application["Resource"] + "Go/Home", false);
	}
}
