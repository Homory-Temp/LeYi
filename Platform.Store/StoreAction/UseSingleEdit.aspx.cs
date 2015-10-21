using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_UseSingleEdit : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var lcId = "LCId".Query().GlobalId();
            var lc = db.Value.Store_LC.Single(o => o.Id == lcId);
            day.Text = lc.TimeNode.FromTimeNode();
            people.Text = lc.User;
            name.Text = lc.Name;
            amount.MaxValue = (double)lc.Amount;
            amount.Value = (double)lc.Amount;
            amount.MinValue = 0;
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Adjust();
        Response.Redirect("../StoreQuery/Use?StoreId={0}".Formatted(StoreId));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../StoreQuery/Used?StoreId={0}".Formatted(StoreId));
    }

    protected void Adjust()
    {
        var lcId = "LCId".Query().GlobalId();
        var lc = db.Value.Store_LC.Single(o => o.Id == lcId);
        var obj = db.Value.StoreObject.Single(o => o.Id == lc.ObjectId);
        if (lc.Type == "领用" && obj.Consumable)
        {
            var cs = db.Value.StoreConsumeSingle.Single(o => o.Id == lc.Id);
            var @in = cs.StoreIn;
            var perPrice = @in.PerPrice;
            var fee = @in.Fee / @in.OriginalAmount;
            var plusAmount = cs.Amount - amount.PeekValue(lc.Amount);
            var plusFee = fee * plusAmount;
            var plusMoney = perPrice * plusAmount + plusFee;
            cs.Amount -= plusAmount;
            cs.Money -= plusMoney;
            var c = cs.StoreConsume;
            c.Amount -= plusAmount;
            c.Money -= plusMoney;
            @in.Amount += plusAmount;
            @in.Money += plusMoney;
            obj.Amount += plusAmount;
            obj.Money += plusMoney;
            var u = db.Value.StoreUseSingle.Single(o => o.SingleConsumeId == cs.Id);
            u.Amount -= plusAmount;
            u.Money -= plusMoney;
            var us = u.StoreUse;
            us.Money -= plusMoney;
            var flow = new StoreFlow
            {
                Id = db.Value.GlobalId(),
                ObjectId = obj.Id,
                UserId = CurrentUser,
                Type = FlowType.出库修改,
                TypeName = FlowType.出库修改.ToString(),
                TimeNode = lc.TimeNode,
                Time = lc.TimeNode.ToTime(),
                Amount = plusAmount,
                Money = plusMoney,
                Note = "出库修改"
            };
            db.Value.StoreFlow.Add(flow);
            var time = c.Time;
            var year = time.Year;
            var month = time.Month;
            var stamp = new DateTime(year, month, 1).ToTimeNode();
            var last = db.Value.StoreStatistics.Where(o => o.ObjectId == obj.Id && o.TimeNode < stamp).OrderByDescending(o => o.TimeNode).FirstOrDefault();
            var current = db.Value.StoreStatistics.Single(o => o.ObjectId == obj.Id && o.Year == year && o.Month == month);
            current.ConsumeAmount -= plusAmount;
            current.ConsumeMoney -= plusMoney;
            current.EndAmount += plusAmount;
            current.EndMoney += plusMoney;
            foreach (var currentX in db.Value.StoreStatistics.Where(o => o.ObjectId == obj.Id && o.TimeNode > stamp))
            {
                currentX.StartAmount += plusAmount;
                currentX.StartMoney += plusMoney;
                currentX.EndAmount += plusAmount;
                currentX.EndMoney += plusMoney;
            }
            db.Value.SaveChanges();
        }
    }
}
