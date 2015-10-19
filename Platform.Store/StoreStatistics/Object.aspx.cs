using Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreStatistics_Object : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            ps.SelectedDate = DateTime.Today;
            pe.SelectedDate = DateTime.Today;
            grid.Rebind();
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
        grid.Rebind();
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        grid.Rebind();
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        grid.Rebind();
    }

    protected void grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var sv = ps.SelectedDate.Value;
        var ev = pe.SelectedDate.Value;
        var _s = new DateTime(sv.Year, sv.Month, 1).ToTimeNode();
        var _e = new DateTime(ev.Year, ev.Month, 1).ToTimeNode();
        if (_s > _e)
        {
            var _t = _s;
            _s = _e;
            _e = _t;
        }
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(db.Value.Store_ST.Where(o => o.Time >= _s && o.Time <= _e), o => o, o => o.CatalogId, (a, b) => b).ToList();
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
            obj.S = g.First().I_Start;
            obj.SM = g.First().IM_Start;
            obj.I = g.Last().I_End - g.First().I_Start;
            obj.IM = g.Last().IM_End - g.First().IM_Start;
            obj.U = g.Last().C_End - g.First().C_Start + g.Last().L_End - g.First().L_Start;
            obj.UM = g.Last().CM_End - g.First().CM_Start + g.Last().LM_End - g.First().LM_Start;
            obj.O = g.Last().O_End - g.First().O_Start;
            obj.OM = g.Last().OM_End - g.First().OM_Start;
            obj.E = g.Last().I_End;
            obj.EM = g.Last().IM_End;
            list.Add(obj);
        }
        grid.DataSource = list;
    }
}
