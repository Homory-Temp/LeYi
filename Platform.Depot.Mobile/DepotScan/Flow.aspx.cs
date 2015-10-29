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
            if (!"Code".Query().None())
            {
                scan.Text = "Code".Query().Trim();
                scanFlow_ServerClick(null, null);
            }
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
            view.DataSource = DataContext.DepotFlow.Where(o => o.ObjectId == obj.Id).ToList();
            view.DataBind();
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
            view.DataSource = DataContext.DepotFlowX.Where(o => o.ObjectId == obj.Id && o.ObjectOrdinal == inx.Ordinal).ToList();
            view.DataBind();
        }
        Reset();
    }
}
