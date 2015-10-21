using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_UseEdit : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var useId = "UseId".Query().GlobalId();
            var use = db.Value.StoreUse.Single(o => o.Id == useId);
            day.SelectedDate = use.Time;
            money.Value = (double)use.Money;
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        var useId = "UseId".Query().GlobalId();
        var use = db.Value.StoreUse.Single(o => o.Id == useId);
        use.Time = day.SelectedDate.HasValue ? day.SelectedDate.Value : DateTime.Today;
        use.TimeNode = use.Time.ToTimeNode();
        use.Money = money.PeekValue(use.Money);
        db.Value.SaveChanges();
        Response.Redirect("../StoreQuery/Use?StoreId={0}".Formatted(StoreId));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../StoreQuery/Use?StoreId={0}".Formatted(StoreId));
    }
}
