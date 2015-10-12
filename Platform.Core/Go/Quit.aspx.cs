using System;

namespace Go
{
    public partial class GoQuit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Session.Abandon();
			Session.Clear();
			Response.Write("OK");
		}
	}
}
