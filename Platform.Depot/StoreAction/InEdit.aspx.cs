using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_InEdit : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var inId = "InId".Query().GlobalId();
            var @in = db.Value.StoreIn.Single(o => o.Id == inId);
            day.SelectedDate = @in.Time;
            amount.Value = (double)@in.OriginalAmount;
            perPrce.Value = (double)@in.SourcePerPrice;
            money.Value = (double)@in.OriginalMoney;
            place.Text = @in.Place;
            note.Text = @in.Note;
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        var inId = "InId".Query().GlobalId();
        var @in = db.Value.StoreIn.Single(o => o.Id == inId);
        if (@in.StoreConsumeSingle.Count == 0 && @in.StoreLendSingle.Count == 0 && @in.StoreOutSingle.Count == 0)
        {
            db.Value.ActionInEditExt(@in, day.SelectedDate.HasValue ? day.SelectedDate.Value : DateTime.Today, amount.PeekValue(0M), perPrce.PeekValue(0M), money.PeekValue(0M), place.Text.Trim(), note.Text.Trim(), CurrentUser);
        }
        Response.Redirect("../StoreQuery/In?StoreId={0}".Formatted(StoreId));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../StoreQuery/In?StoreId={0}".Formatted(StoreId));
    }
}
