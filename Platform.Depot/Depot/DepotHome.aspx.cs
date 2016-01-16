using System;
using System.Linq;

public partial class Depot_DepotHome : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var q = DataContext.DepotOrder.Where(o => o.State < Models.State.停用 && o.DepotId == Depot.Id).Select(o => o.Id).ToList().Join(DataContext.DepotIn, o => o, o => o.OrderId, (a, b) => b).Join(DataContext.DepotObject, o => o.ObjectId, o => o.Id, (a, b) => a);
            C.InnerText = (q.Count() == 0 ? 0 : q.Sum(o => o.Amount)).ToAmount(Depot.Featured(Models.DepotType.小数数量库));
            M.InnerText = (q.Count() == 0 ? 0 : DataContext.DepotOrder.Where(o => o.DepotId == Depot.Id && o.State < Models.State.停用).Sum(o => o.Paid)).ToString().ToMoney();
        }
    }
}
