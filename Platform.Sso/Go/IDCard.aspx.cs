using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;

public partial class Go_IDCard : HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var idc = Request.QueryString.Count == 0 ? "" : Request.QueryString[0];
        Response.Write(HomoryContext.Value.Teacher.Count(o=>o.IDCard == idc) == 1 ? "OK" : "NO");
    }
}
