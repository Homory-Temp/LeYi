using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class DepotQuery_UsePrint : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "UseId".Query().GlobalId();
            var use = DataContext.DepotUse.Single(o => o.Id == id);
            campus.InnerText = DataContext.Department.Single(o => o.Id == Depot.CampusId).Name;
            total.Value = use.Money.ToMoney();
            people.Value = DataContext.DepotUser.Single(o => o.Id == use.UserId).Name;
            time.InnerText = use.Time.ToDay();
        }
    }

    public class UseRecord
    {
        public string Name { get; set; }
        public string Catalog { get; set; }
        public string Unit { get; set; }
        public UseType Type { get; set; }
        public decimal Amount { get; set; }
        public decimal PerPrice { get; set; }
        public decimal Money { get; set; }
        public string Specification { get; set; }
        public string Note { get; set; }
        public Guid ObjectId { get; set; }
        public Guid InId { get; set; }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var id = "UseId".Query().GlobalId();
        var use = DataContext.DepotUse.Single(o => o.Id == id);
        var list = new List<UseRecord>();
        var isVirtual = Depot.Featured(DepotType.固定资产库);
        foreach (var us in DataContext.DepotUseX.Where(o => o.UseId == use.Id).ToList())
        {
            if (list.Count(o => o.ObjectId == us.ObjectId && o.Type == us.Type && o.InId == us.InXId) == 0)
            {
                var objId = us.ObjectId;
                var obj = DataContext.DepotObject.Single(o => o.Id == objId);
                var catalog = DataContext.DepotObjectCatalog.Single(o => o.ObjectId == objId && o.IsLeaf == true && o.IsVirtual == isVirtual);
                list.Add(new UseRecord { ObjectId = us.ObjectId, Name = obj.Name, Catalog = DataContext.ToCatalog(catalog.CatalogId, catalog.Level).Single(), Type = us.Type, Unit = obj.Unit, Specification = obj.Specification, Amount = us.Amount, Money = us.Money, Note = us.Note, InId = us.InXId, PerPrice = decimal.Divide(us.Money, us.Amount) });
            }
            else
            {
                var x = list.First(o => o.ObjectId == us.ObjectId && o.Type == us.Type && o.InId == us.InXId);
                x.Amount += us.Amount;
                x.Money += us.Money;
            }
        }
        view_record.DataSource = list.OrderBy(o => o.Name).ThenBy(o => o.Type);
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../DepotQuery/Use?DepotId={0}".Formatted(Depot.Id));
    }
}
