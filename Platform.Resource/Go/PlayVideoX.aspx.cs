using Homory.Model;
using System;

public partial class Go_PlayVideoX : HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		var name = Server.UrlDecode(Request.QueryString[0]);
		player.Video = name;
    }
}