using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_InRedo : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var inId = "InId".Query().GlobalId();
            var @in = DataContext.DepotIn.Single(o => o.Id == inId);
            amount.Text = @in.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
            price.Text = @in.PriceSet.ToMoney();
            backed.NumberFormat.DecimalDigits = Depot.Featured(DepotType.小数数量库) ? 2 : 0;
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        var inId = "InId".Query().GlobalId();
        var @in = DataContext.DepotIn.Single(o => o.Id == inId);
        DataContext.DepotActInRedo(Depot.Id, @in, backed.PeekValue(0M), DepotUser.Id);
        Response.Redirect("../DepotQuery/Redo?DepotId={0}".Formatted(Depot.Id));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../DepotQuery/InX?DepotId={0}".Formatted(Depot.Id));
    }
}
