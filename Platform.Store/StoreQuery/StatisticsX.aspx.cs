using Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class StoreQuery_StatisticsX : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            tree.ExpandAllNodes();
            ps.SelectedDate = DateTime.Today;
            pe.SelectedDate = DateTime.Today;
        }
    }

    protected string Calc(RadTreeNode node, Guid catalogId)
    {
        var x = node.GetAllNodes().Select(o => Guid.Parse(o.Value)).ToList();
        x.Add(catalogId);
        var sv = ps.SelectedDate.HasValue ? ps.SelectedDate.Value : DateTime.Today;
        var ev = pe.SelectedDate.HasValue ? pe.SelectedDate.Value : DateTime.Today;
        var _s = new DateTime(sv.Year, sv.Month, 1).ToTimeNode();
        var _e = new DateTime(ev.Year, ev.Month, 1).ToTimeNode();
        if (_s > _e)
        {
            var _t = _s;
            _s = _e;
            _e = _t;
        }
        var j = x.Join(db.Value.Store_ST.Where(c => c.Time >= _s && c.Time <= _e), o => o, o => o.CatalogId, (a, b) => b).ToList();
        var q = j.Sum(o => o.InMoney);
        var r = j.Sum(o => o.ConsumeMoney);
        if (q == 0 && r == 0)
            return "";
        return string.Format("&nbsp;+&nbsp;{0}&nbsp;&nbsp;&nbsp;&nbsp;-&nbsp;{1}", q.ToString("F2"), r.ToString("F2"));
    }

    protected Guid StoreId
    {
        get
        {
            return Guid.Parse("StoreId".Query());
        }
    }

    protected Lazy<StoreEntity> db = new Lazy<StoreEntity>(() => new StoreEntity());

    protected void Notify(RadAjaxPanel panel, string message, string type)
    {
        panel.ResponseScripts.Add(string.Format("notify(null, '{0}', '{1}');", message, type));
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
        tree.DataBind();
        tree.ExpandAllNodes();
    }
}
