using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StoreHome_Home : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

        }
    }

    public class HomeItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Class { get; set; }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var list = new List<HomeItem>();
        list.Add(new HomeItem { Name = "购置登记", Class = "btn-info", Url = "../StoreAction/Target?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "物资入库", Class = "btn-info", Url = "../StoreAction/In?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "物资出库", Class = "btn-info", Url = "../StoreAction/Use?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "物资归还", Class = "btn-info", Url = "../StoreAction/Return?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "物资管理", Class = "btn-info", Url = "../StoreAction/Object?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "购置查询", Class = "btn-warning", Url = "../StoreQuery/Target?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "入库查询", Class = "btn-warning", Url = "../StoreQuery/In?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "出库查询", Class = "btn-warning", Url = "../StoreQuery/Use?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "归还查询", Class = "btn-warning", Url = "../StoreQuery/Return?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "库存查询", Class = "btn-warning", Url = "../StoreStatistics/Object?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "物资类别", Class = "btn-danger", Url = "../StoreSetting/Catalog?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "基础数据", Class = "btn-danger", Url = "../StoreSetting/Dictionary?StoreId={0}".Formatted(StoreId) });
        list.ForEach(o => { if (o.Class == "btn-danger") o.Class = "btn-temp"; });
        list.ForEach(o => { if (o.Class == "btn-warning") o.Class = "btn-danger"; });
        list.ForEach(o => { if (o.Class == "btn-temp") o.Class = "btn-warning"; });
        view.DataSource = list;
    }
}
