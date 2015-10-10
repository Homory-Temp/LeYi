using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Go_PlayVideoX : HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		var name = Server.UrlDecode(Request.QueryString[0]);
		player.Video = name;
    }
}