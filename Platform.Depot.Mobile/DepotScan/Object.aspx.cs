using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_Object : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Reset();
        }
    }

    protected void Reset()
    {
        scan.Text = "";
        scan.Focus();
    }

    protected void scanAdd_ServerClick(object sender, EventArgs e)
    {
        var code = scan.Text.Trim();
        var inx = DataContext.DepotInX.FirstOrDefault(o => o.Code == code);
        if (inx == null)
        {
            Reset();
            return;
        }
        var obj = inx.DepotObject;
        Response.Redirect("~/DepotQuery/Object?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, obj.Id));
    }
}
