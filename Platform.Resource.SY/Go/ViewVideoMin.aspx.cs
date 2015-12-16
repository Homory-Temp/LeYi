using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Go_ViewVideoMin : HomoryResourcePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var SourceId = Guid.Parse(Server.UrlDecode(Request.QueryString["Id"].ToString()));
        var url = HomoryContext.Value.ResourceAttachment.SingleOrDefault(o=>o.Id == SourceId).Source;
        player.Video = url;
    }


    protected override bool ShouldOnline
    {
        get { return false; }
    }
}