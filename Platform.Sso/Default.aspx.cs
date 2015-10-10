using Homory.Model;
using System;

public partial class Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Response.Redirect(Application["Sso"] + "Go/SignOn", false);
	}
}
