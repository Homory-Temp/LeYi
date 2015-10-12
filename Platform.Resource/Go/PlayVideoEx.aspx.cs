using Homory.Model;
using System;
using System.Linq;

public partial class PlayVideoEx : HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var id = Guid.Parse(Request.QueryString["Id"]);
        player.Video = HomoryContext.Value.Resource.Single(o => o.Id == id).Preview;
    }
}
