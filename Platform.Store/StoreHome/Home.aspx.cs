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
        if (!IsPostBack)
        {
            scan.Visible = CurrentStore.State != Models.StoreState.食品;
        }
    }

    public class HomeItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    protected void view_action_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var list = new List<HomeItem>();
        list.Add(new HomeItem { Name = "购置登记", Url = "../StoreAction/Target?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "物资入库", Url = "../StoreAction/In?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "物资出库", Url = "../StoreAction/Use?StoreId={0}".Formatted(StoreId) });
        if (CurrentStore.State != Models.StoreState.食品)
        {
            list.Add(new HomeItem { Name = "物资归还", Url = "../StoreAction/Return?StoreId={0}".Formatted(StoreId) });
        }
        list.Add(new HomeItem { Name = "物资管理", Url = "../StoreAction/Object?StoreId={0}".Formatted(StoreId) });
        if (CurrentStore.State == Models.StoreState.固产)
        {
            list.Add(new HomeItem { Name = "资产导入", Url = "../StoreAction/Import?StoreId={0}".Formatted(StoreId) });
            list.Add(new HomeItem { Name = "资产分库", Url = "../StoreAction/Move?StoreId={0}".Formatted(StoreId) });
        }
        view_action.DataSource = list;
    }

    protected void view_query_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var list = new List<HomeItem>();
        list.Add(new HomeItem { Name = "购置单查询", Url = "../StoreQuery/Target?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "入库查询", Url = "../StoreQuery/In?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "出库单查询", Url = "../StoreQuery/Use?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "出库查询", Url = "../StoreQuery/Used?StoreId={0}".Formatted(StoreId) });
        if (CurrentStore.State != Models.StoreState.食品)
        {
            list.Add(new HomeItem { Name = "归还查询", Url = "../StoreQuery/Return?StoreId={0}".Formatted(StoreId) });
        }
        else
        {
            list.Add(new HomeItem { Name = "月库存查询", Url = "../StoreQuery/StatisticsMonthly?StoreId={0}".Formatted(StoreId) });
        }
        list.Add(new HomeItem { Name = "汇总统计", Url = "../StoreQuery/Statistics?StoreId={0}".Formatted(StoreId) });
        if (CurrentStore.State == Models.StoreState.固产)
        {
            list.Add(new HomeItem { Name = "导入查询", Url = "../StoreQuery/Import?StoreId={0}".Formatted(StoreId) });
            list.Add(new HomeItem { Name = "分库查询", Url = "../StoreQuery/Move?StoreId={0}".Formatted(StoreId) });
        }
        view_query.DataSource = list;
    }

    protected void view_scan_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var list = new List<HomeItem>();
        list.Add(new HomeItem { Name = "条码打印", Url = "../StoreScan/Code?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "扫码出库", Url = "../StoreScan/Use?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "扫码归还", Url = "../StoreScan/Return?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "流通查询", Url = "../StoreScan/Flow?StoreId={0}".Formatted(StoreId) });
        view_scan.DataSource = list;
    }

    protected void view_setting_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var list = new List<HomeItem>();
        list.Add(new HomeItem { Name = "物资类别", Url = "../StoreSetting/Catalog?StoreId={0}".Formatted(StoreId) });
        list.Add(new HomeItem { Name = "基础数据", Url = "../StoreSetting/Dictionary?StoreId={0}".Formatted(StoreId) });
        if (RightAdvanced)
        {
            list.Add(new HomeItem { Name = "权限设置", Url = "../StoreSetting/Permission?StoreId={0}".Formatted(StoreId) });
        }
        view_setting.DataSource = list;
    }
}
