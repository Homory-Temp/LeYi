using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;
using System.Net;

public partial class Go_Msg : HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var url = "http://www.4001185185.com/sdk/smssdk!mt.action?sdk=18687&code=lxjy6688&phones=" + Request.QueryString["phones"] + "&msg=" + Request.QueryString["msg"] + "&resulttype=txt&subcode=2897&rpt=0&homory="+DateTime.Now.ToString("yyyyMMddHHmmss");
        HttpWebRequest request = HttpWebRequest.CreateHttp(url);
        var response = request.GetResponse();
        Response.Write("OK");
    }
}
