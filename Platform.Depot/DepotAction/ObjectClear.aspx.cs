using System;
using System.Linq;

public partial class DepotAction_ObjectClear : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "ObjectId".Query().GlobalId();
            store.InnerText = DataContext.DepotObject.Single(o => o.Id == id).Name;
        }
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        if (!name.Text.Equals(store.InnerText.Trim(), StringComparison.InvariantCultureIgnoreCase))
        {
            NotifyError(ap, "请输入完整的物资名称");
            return;
        }
        var id = "ObjectId".Query().GlobalId();
        var obj = DataContext.DepotObject.SingleOrDefault(o => o.Id == id);
        if (obj == null)
        {
            Response.Redirect("~/DepotAction/Object?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, "CatalogId".Query()));
            return;
        }
        foreach (var @in in obj.DepotIn)
        {
            @in.DepotOrder.ToPay -= @in.Amount * @in.Price;
            @in.DepotOrder.Paid -= @in.Amount * @in.Price;
        }
        DataContext.SaveChanges();
        DataContext.DepotStatistics.RemoveRange(obj.DepotStatistics.ToList());
        DataContext.SaveChanges();
        DataContext.DepotFlowX.RemoveRange(obj.DepotFlowX.ToList());
        DataContext.SaveChanges();
        DataContext.DepotFlow.RemoveRange(obj.DepotFlow.ToList());
        DataContext.SaveChanges();
        var codes = obj.DepotInX.Select(o => o.Code).ToList();
        codes.Add(obj.Code);
        var p = codes.Join(DataContext.DepotPlace, o => o, o => o.Code, (a, b) => b).ToList();
        DataContext.DepotPlace.RemoveRange(p);
        DataContext.SaveChanges();
        DataContext.DepotRedo.RemoveRange(obj.DepotRedo.ToList());
        DataContext.SaveChanges();
        var rs = DataContext.DepotUseX.Select(o => o.Id).ToList();
        var r = rs.Join(DataContext.DepotReturn, o => o, o => o.UseXId, (a, b) => b).ToList();
        DataContext.DepotReturn.RemoveRange(r);
        DataContext.SaveChanges();
        foreach (var t in obj.DepotUseX)
        {
            t.DepotUse.Money -= t.Money;
        }
        DataContext.SaveChanges();
        DataContext.DepotUseX.RemoveRange(obj.DepotUseX);
        DataContext.SaveChanges();
        DataContext.DepotInX.RemoveRange(obj.DepotInX);
        DataContext.SaveChanges();
        DataContext.DepotIn.RemoveRange(obj.DepotIn);
        DataContext.SaveChanges();
        var oc = DataContext.DepotObjectCatalog.Where(o => o.ObjectId == obj.Id).ToList();
        DataContext.DepotObjectCatalog.RemoveRange(oc);
        DataContext.SaveChanges();
        DataContext.DepotObject.Remove(obj);
        DataContext.SaveChanges();
        Response.Redirect("~/DepotAction/Object?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, "CatalogId".Query()));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        var oid = "ObjectId".Query().GlobalId();
        Response.Redirect("~/DepotAction/Object?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, "CatalogId".Query()));
    }
}
