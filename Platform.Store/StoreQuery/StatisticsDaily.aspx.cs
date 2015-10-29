using Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_StatisticsDaily : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            periodx.SelectedDate = DateTime.Today;
            period.SelectedDate = DateTime.Today;
            view.Rebind();
        }
    }

    protected void all_ServerClick(object sender, EventArgs e)
    {
        if (_all.Value == "1")
        {
            tree.UncheckAllNodes();
            _all.Value = "0";
            all.Value = "全部选定";
        }
        else
        {
            tree.CheckAllNodes();
            _all.Value = "1";
            all.Value = "清除选定";
        }
        view.Rebind();
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var timex = periodx.SelectedDate.HasValue ? periodx.SelectedDate.Value : DateTime.Today;
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        if (timex > time)
        {
            var time_t = timex;
            timex = time;
            time = time_t;
        }
        var start = timex.AddMilliseconds(-1).ToTimeNode();
        var end = time.AddDays(1).ToTimeNode();
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(db.Value.StoreObject, o => o, o => o.CatalogId, (a, b) => b.Id).Join(db.Value.StoreFlow.Where(o => o.TimeNode > start && o.TimeNode < end), o => o, o => o.ObjectId, (a, b) => b).ToList();
        var list = new List<InMemoryST>();
        foreach (var g in source.GroupBy(o => o.ObjectId))
        {
            var obj = new InMemoryST();
            var v = g.OrderBy(o => o.Time).ToList();
            var x = v.First().StoreObject;
            obj.CatalogPath = db.Value.GetCatalogPath(x.CatalogId).First();
            obj.Name = x.Name;
            obj.Single = x.Single;
            obj.Consumable = x.Consumable;
            obj.Fixed = x.Fixed;
            obj.S = 0;
            obj.SM = 0;
            obj.I = g.Where(o => o.Type == FlowType.入库 || o.Type == FlowType.入库修改).Sum(o => o.Amount);
            obj.IM = g.Where(o => o.Type == FlowType.入库 || o.Type == FlowType.入库修改).Sum(o => o.Money);
            obj.U = Math.Abs(g.Where(o => o.Type == FlowType.借用出库 || o.Type == FlowType.领用出库 || o.Type == FlowType.出库修改).Sum(o => o.Amount));
            obj.UM = Math.Abs(g.Where(o => o.Type == FlowType.借用出库 || o.Type == FlowType.领用出库 || o.Type == FlowType.出库修改).Sum(o => o.Money));
            obj.R = 0;
            obj.RM = 0;
            obj.O = 0;
            obj.OM = 0;
            obj.E = 0;
            obj.EM = 0;
            list.Add(obj);
        }
        if (!name.Text.Trim().Null())
        {
            list = list.Where(o => o.Name == name.Text.Trim()).ToList();
        }
        ___total.Value = list.Sum(o => o.I).ToMoney() + "@" + list.Sum(o => o.IM).ToMoney() + "@" + list.Sum(o => o.U).ToMoney() + "@" + list.Sum(o => o.UM).ToMoney();
        view.DataSource = list.OrderBy(o => o.CatalogPath).ThenBy(o => o.Name).ToList();
    }
}
