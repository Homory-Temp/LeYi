using Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_Statistics : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            ps.SelectedDate = DateTime.Today;
            pe.SelectedDate = DateTime.Today;
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
        var sv = ps.SelectedDate.HasValue ? ps.SelectedDate.Value : DateTime.Today;
        var ev = pe.SelectedDate.HasValue ? pe.SelectedDate.Value : DateTime.Today;
        if (sv > ev)
        {
            var _t = sv;
            sv = ev;
            ev = _t;
        }
        var _s = new DateTime(sv.Year, sv.Month, 1).AddMilliseconds(-1);
        var _e = new DateTime(ev.Year, ev.Month, 1).AddMonths(1);
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(DataContext.DepotST.Where(o => o.Time >= _s && o.Time <= _e), o => o, o => o.CatalogId, (a, b) => b).ToList();
        var list = new List<InMemoryST>();
        foreach (var g in source.GroupBy(o => o.ObjectId))
        {
            var obj = new InMemoryST();
            var v = g.OrderBy(o => o.Time).ToList();
            obj.CatalogPath = v.First().CatalogPath;
            obj.Name = v.First().Name;
            obj.Single = v.First().Single;
            obj.Consumable = v.First().Consumable;
            obj.Fixed = v.First().Fixed;
            obj.S = g.First().StartAmount;
            obj.SM = g.First().StartMoney;
            obj.I = g.Sum(o => o.InAmount);
            obj.IM = g.Sum(o => o.InMoney);
            obj.U = Math.Abs(g.Sum(o => o.LendAmount + o.ConsumeAmount));
            obj.UM = Math.Abs(g.Sum(o => o.LendMoney + o.ConsumeMoney));
            obj.R = g.Sum(o => o.RedoAmount);
            obj.RM = g.Sum(o => o.RedoMoney);
            obj.O = Math.Abs(g.Sum(o => o.OutAmount));
            obj.OM = Math.Abs(g.Sum(o => o.OutMoney));
            obj.E = g.Last().EndAmount;
            obj.EM = g.Last().EndMoney;
            list.Add(obj);
        }
        if (!name.Text.Trim().None())
        {
            list = list.Where(o => o.Name.ToLower().Contains(name.Text.Trim().ToLower())).ToList();
        }
        ___total.Value = list.Sum(o => o.S).ToAmount() + "@" + list.Sum(o => o.SM).ToMoney() + "@" + list.Sum(o => o.I).ToAmount() + "@" + list.Sum(o => o.IM).ToMoney() + "@" + list.Sum(o => o.U).ToAmount() + "@" + list.Sum(o => o.UM).ToMoney() + "@" + list.Sum(o => o.O).ToAmount() + "@" + list.Sum(o => o.OM).ToMoney() + "@" + list.Sum(o => o.E).ToAmount() + "@" + list.Sum(o => o.EM).ToMoney();
        view.DataSource = list.OrderBy(o => o.CatalogPath).ThenBy(o => o.Name).ToList();
    }
}
