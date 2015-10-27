using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_Flow : DepotPageSingle
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

    protected void scanFlow_ServerClick(object sender, EventArgs e)
    {
        var code = scan.Text.Trim();
        if (code.StartsWith("O"))
        {
            var inx = DataContext.DepotInX.FirstOrDefault(o => o.Code == code);
            if (inx == null)
            {
                Reset();
                return;
            }
            var obj = inx.DepotObject;
            grid.DataSource = DataContext.DepotFlow.Where(o => o.ObjectId == obj.Id).ToList();
            grid.DataBind();
        }
        else if (code.StartsWith("S"))
        {
            var inx = DataContext.DepotInX.FirstOrDefault(o => o.Code == code);
            if (inx == null)
            {
                Reset();
                return;
            }
            var obj = inx.DepotObject;
            grid.DataSource = DataContext.DepotFlowX.Where(o => o.ObjectId == obj.Id && o.ObjectOrdinal == inx.Ordinal).ToList();
            grid.DataBind();
        }
        Reset();
    }
}
