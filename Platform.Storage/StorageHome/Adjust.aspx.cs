using Models;
using System;
using System.Linq;

public partial class Home : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "InId".Query().GlobalId();
            var @in = db.Value.StorageIn.Single(o => o.Id == id);
            o_num.Text = @in.Amount.ToString();
            o_price.Text = @in.PerPrice.Money();
            o_off.Text = @in.AdditionalFee.Money();
            x_num.Value = (double)@in.Amount;
            x_price.Value = (double)@in.PerPrice;
            x_off.Value = (double)@in.AdditionalFee;
            if (@in.StorageObject.Single)
            {
                x_num.MinValue = (double)@in.Amount;
            }
            else
            {
                x_num.MinValue = 1;
            }
            x_price.MinValue = 0.00;
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var id = "InId".Query().GlobalId();
        var @in = db.Value.StorageIn.Single(o => o.Id == id);
        var o_num = @in.Amount;
        decimal o_per = @in.PerPrice;
        decimal o_fee = @in.AdditionalFee;
        var n_num = (decimal)x_num.Value.Value;
        decimal n_per = (decimal)x_price.Value.Value;
        decimal n_fee = (decimal)x_off.Value.Value;
        var so = @in.StorageObject;
        #region 修改价格
        if (o_per != n_per || o_fee != n_fee)
        {
            var total = so.TotalMoney;
            so.InMoney += decimal.Divide(@in.InAmount, @in.Amount) * ((n_per * o_num) + n_fee) - @in.InMoney;
            so.LendMoney += decimal.Divide(@in.LendAmount, @in.Amount) * ((n_per * o_num) + n_fee) - @in.LendMoney;
            so.OutMoney+= decimal.Divide(@in.OutAmount, @in.Amount) * ((n_per * o_num) + n_fee) - @in.OutMoney;
            so.TotalMoney = so.InMoney + so.LendMoney + so.OutMoney;
            total = so.TotalMoney - total;
            db.Value.SetFlow(StorageId, so, FlowType.调整, DateTime.Now, int.Parse(DateTime.Today.ToString("yyyyMMdd")), total, "价格调整", decimal.Divide(@in.InAmount, @in.Amount) * ((n_per * o_num) + n_fee) - @in.InMoney, decimal.Divide(@in.LendAmount, @in.Amount) * ((n_per * o_num) + n_fee) - @in.LendMoney, decimal.Divide(@in.OutAmount, @in.Amount) * ((n_per * o_num) + n_fee) - @in.OutMoney);
            @in.PerPrice = n_per;
            @in.AdditionalFee = n_fee;
            @in.TotalPrice = n_per * o_num;
            @in.TotalMoney = (n_per * o_num) + n_fee;
            @in.InMoney = decimal.Divide(@in.InAmount, @in.Amount) * ((n_per * o_num) + n_fee);
            @in.LendMoney = decimal.Divide(@in.LendAmount, @in.Amount) * ((n_per * o_num) + n_fee);
            @in.OutMoney = decimal.Divide(@in.OutAmount, @in.Amount) * ((n_per * o_num) + n_fee);
            if (so.Consumable)
            {
                foreach (var cons in @in.StorageConsumeSingle.Select(o => o.StorageConsume).Distinct())
                {
                    cons.PerPrice = n_per;
                    cons.TotalPrice = n_per * cons.Amount;
                    cons.AdditionalFee = decimal.Divide(cons.Amount, @in.Amount) * n_fee;
                    cons.TotalMoney = n_per * cons.Amount + cons.AdditionalFee;
                }
            }
            else
            {
                foreach (var lend in @in.StorageLendSingle.Select(o => o.StorageLend).Distinct())
                {
                    lend.PerPrice = n_per;
                    lend.TotalPrice = n_per * lend.Amount;
                    lend.AdditionalFee = decimal.Divide(lend.Amount, @in.Amount) * n_fee;
                    lend.TotalMoney = n_per * lend.Amount + lend.AdditionalFee;
                    foreach (var back in lend.StorageReturnSingle.Select(o => o.StorageReturn).Distinct())
                    {
                        back.PerPrice = n_per;
                        back.TotalPrice = n_per * back.Amount;
                        back.AdditionalFee = decimal.Divide(back.Amount, @in.Amount) * n_fee;
                        back.TotalMoney = n_per * back.Amount + back.AdditionalFee;
                    }
                }
            }
            foreach (var obso in @in.StorageOutSingle.Select(o => o.StorageOut).Distinct())
            {
                obso.PerPrice = n_per;
                obso.TotalPrice = n_per * obso.Amount;
                obso.AdditionalFee = decimal.Divide(obso.Amount, @in.Amount) * n_fee;
                obso.TotalMoney = n_per * obso.Amount + obso.AdditionalFee;
            }
        }
        #endregion
        #region 修改数量
        var price = decimal.Divide(1, @in.InAmount) * @in.AdditionalFee + @in.PerPrice;
        if (n_num > o_num)
        {
            var adj_num = n_num - o_num;
            so.InAmount += adj_num;
            so.InMoney += adj_num * price;
            so.TotalAmount += adj_num;
            so.TotalMoney += adj_num * price;
            db.Value.SetFlow(StorageId, so, FlowType.调整, DateTime.Now, int.Parse(DateTime.Today.ToString("yyyyMMdd")), adj_num * price, "数量调整", adj_num);
            @in.Amount += adj_num;
            @in.AdditionalFee += adj_num * decimal.Divide(1, @in.InAmount) * @in.AdditionalFee;
            @in.InAmount += adj_num;
            @in.InMoney += adj_num * price;
            @in.TotalAmount += adj_num;
            @in.TotalMoney+= adj_num * price;
            @in.TotalPrice += adj_num * @in.PerPrice;
            var nodes = @in.StorageInSingle.ToList();
            var max = nodes.Max(o => o.InOrdinal);
            for (var i = 0; i < adj_num; i++)
            {
                max++;
                db.Value.StorageInSingle.Add(new StorageInSingle
                {
                    Id = db.Value.GlobalId(),
                    ObjectId = so.Id,
                    InId = @in.Id,
                    InOrdinal = max,
                    In = true,
                    Lend = false,
                    Out = false,
                    Place = nodes.First().Place
                });
            }
        }
        else if (n_num < o_num && !so.Single)
        {
            var adj_num = n_num - o_num;
            so.InAmount += adj_num;
            so.InMoney += adj_num * price;
            so.TotalAmount += adj_num;
            so.TotalMoney += adj_num * price;
            db.Value.SetFlow(StorageId, so, FlowType.调整, DateTime.Now, int.Parse(DateTime.Today.ToString("yyyyMMdd")), adj_num * price, "数量调整", adj_num);
            @in.Amount += adj_num;
            @in.AdditionalFee += adj_num * decimal.Divide(1, @in.InAmount) * @in.AdditionalFee;
            @in.InAmount += adj_num;
            @in.InMoney += adj_num * price;
            @in.TotalAmount += adj_num;
            @in.TotalMoney += adj_num * price;
            @in.TotalPrice += adj_num * @in.PerPrice;
        }
        #endregion
        db.Value.SaveChanges();
        Response.Redirect("~/StorageQuery/QueryIn?StorageId={0}".Formatted(StorageId));
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageQuery/QueryIn?StorageId={0}".Formatted(StorageId));
    }
}
