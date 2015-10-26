using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_InEdit : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var inId = "InId".Query().GlobalId();
            var @in = DataContext.DepotIn.Single(o => o.Id == inId);
            day.SelectedDate = @in.Time;
            amount.Value = (double)@in.Amount;
            perPrce.Value = (double)@in.PriceSet;
            money.Value = (double)@in.Total;
            place.Text = @in.Place;
            note.Text = @in.Note;
            amount.NumberFormat.DecimalDigits = Depot.Featured(DepotType.小数数量库) ? 2 : 0;
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        var inId = "InId".Query().GlobalId();
        var @in = DataContext.DepotIn.Single(o => o.Id == inId);
        DataContext.DepotActInEdit(@in, day.SelectedDate.HasValue ? day.SelectedDate.Value : DateTime.Today, amount.PeekValue(0M), perPrce.PeekValue(0M), money.PeekValue(0M), place.Text.Trim(), note.Text.Trim(), DepotUser.Id);
        Response.Redirect("../DepotQuery/InX?DepotId={0}".Formatted(Depot.Id));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../DepotQuery/InX?DepotId={0}".Formatted(Depot.Id));
    }
}
