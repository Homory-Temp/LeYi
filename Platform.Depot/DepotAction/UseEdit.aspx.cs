using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_UseEdit : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var usexId = "UseXId".Query().GlobalId();
            var @ux = DataContext.DepotUseX.Single(o => o.Id == usexId);
            var usexRecord = DataContext.DepotUseXRecord.Single(o => o.Id == usexId);
            info.Text = string.Format("{0}于{1}领用，数量为{2}。".Formatted(usexRecord.UserName, usexRecord.Time.ToString("yyyy-MM-dd"), usexRecord.Amount.ToAmount()));
            amount.Value = (double)@ux.Amount;
            amount.MaxValue = (double)@ux.Amount;
            amount.NumberFormat.DecimalDigits = Depot.Featured(DepotType.小数数量库) ? 2 : 0;
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (!amount.Value.HasValue)
        {
            Response.Redirect("../DepotQuery/UseX?DepotId={0}".Formatted(Depot.Id));
            return;
        }
        var usexId = "UseXId".Query().GlobalId();
        var @ux = DataContext.DepotUseX.Single(ox => ox.Id == usexId);
        var @ix = @ux.DepotInX;
        var @u = @ux.DepotUse;
        var camount = (decimal)(amount.Value.Value);
        var mamount = @ux.Amount - camount;
        @ux.Amount -= mamount;
        @ux.Money -= mamount * @ix.PriceSet;
        @ix.AvailableAmount += mamount;
        var @i = @ix.DepotIn;
        var @o = @ix.DepotObject;
        @i.AvailableAmount += mamount;
        @o.Amount += mamount;
        @o.Money+=mamount * @ix.PriceSet;
        @u.Money -= mamount * @ix.PriceSet;
        DataContext.DepotFlow.Add(new DepotFlow
        {
            Amount = mamount,
            Id = DataContext.GlobalId(),
            Money = mamount * @ix.PriceSet,
            Note = "领用修改",
            ObjectId = @o.Id,
            Time = DateTime.Now,
            Type = FlowType.领用修改,
            TypeName = "领用修改",
            UserId = DepotUser.Id
        });
        DataContext.SaveChanges();
        DataContext.DepotActStatistics(@o.Id, DateTime.Now, mamount, mamount * @ix.PriceSet, 0, 0, 0, 0, 0, 0, 0, 0);
        Response.Redirect("../DepotQuery/UseX?DepotId={0}".Formatted(Depot.Id));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../DepotQuery/UseX?DepotId={0}".Formatted(Depot.Id));
    }
}
