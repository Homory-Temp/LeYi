using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_UsePrint : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "UseId".Query().GlobalId();
            var use = db.Value.StoreUse.Single(o => o.Id == id);
            campus.InnerText = db.Value.Department.Single(o => o.Id == CurrentCampus).Name;
            total.Value = use.Money.ToMoney();
            people.Value = db.Value.GetUserName(use.UserId);
            time.InnerText = use.TimeNode.FromTimeNode();
            ___target.InnerText = use.UsageTarget;
        }
    }

    public class UseRecord
    {
        public string Name { get; set; }
        public string Catalog { get; set; }
        public string Unit { get; set; }
        public string Type { get; set; }
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
        var use = db.Value.StoreUse.Single(o => o.Id == id);
        var list = new List<UseRecord>();
        foreach (var us in db.Value.Store_UseSingle.Where(o=>o.UseId == use.Id && o.Amount > 0).ToList())
        {
            if (list.Count(o => o.ObjectId == us.ObjectId && o.Type == us.TypeName && o.InId == us.InId) == 0)
            {
                list.Add(new UseRecord { ObjectId = us.ObjectId, Name = us.Name, Catalog = us.CatalogName, Type = us.TypeName, Unit = us.Unit, Specification = us.Specification, Amount = us.Amount, Money = us.Money, Note = us.Note, InId = us.InId, PerPrice = decimal.Divide(us.Money, us.Amount) });
            }
            else
            {
                var x = list.First(o => o.ObjectId == us.ObjectId && o.Type == us.TypeName && o.InId == us.InId);
                x.Amount += us.Amount;
                x.Money += us.Money;
            }
        }
        view_record.DataSource = list;
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../StoreQuery/Use?StoreId={0}".Formatted(StoreId));
    }
}
