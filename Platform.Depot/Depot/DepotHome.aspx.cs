using System;
using System.Linq;

public partial class Depot_DepotHome : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var qFix = DataContext.DepotOrder.Where(o => o.State < Models.State.停用 && o.DepotId == Depot.Id).Select(o => o.Id).ToList().Join(DataContext.DepotIn, o => o, o => o.OrderId, (a, b) => b).Join(DataContext.DepotObject.Where(o => o.Fixed == true), o => o.ObjectId, o => o.Id, (a, b) => a);
            var qNoFix = DataContext.DepotOrder.Where(o => o.State < Models.State.停用 && o.DepotId == Depot.Id).Select(o => o.Id).ToList().Join(DataContext.DepotIn, o => o, o => o.OrderId, (a, b) => b).Join(DataContext.DepotObject.Where(o => o.Fixed == false), o => o.ObjectId, o => o.Id, (a, b) => a);
            fixC.InnerText = qFix.Sum(o => o.Amount).ToAmount(Depot.Featured(Models.DepotType.小数数量库));
            fixM.InnerText = qFix.Sum(o => o.Amount * o.PriceSet).ToMoney();
            noFixC.InnerText = qNoFix.Sum(o => o.Amount).ToAmount(Depot.Featured(Models.DepotType.小数数量库));
            noFixM.InnerText = qNoFix.Sum(o => o.Amount * o.PriceSet).ToMoney();
        }
    }
}
