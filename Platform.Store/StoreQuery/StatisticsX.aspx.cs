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

public partial class StoreQuery_StatisticsX : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            orderSource.DataSource = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.采购来源).ToList().Where(o => o.Name.Trim().Length > 0).ToList();
            orderSource.DataBind();
            orderSource.SelectedIndex = 0;
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            periodx.SelectedDate = DateTime.Today.AddMonths(-1);
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

    protected string Fix(int level, string text)
    {
        var s = new System.Text.StringBuilder();
        s.Append("&nbsp;&nbsp;");
        for (var i = 0; i < level; i++)
        {
            s.Append("&nbsp;-&nbsp;-&nbsp;-&nbsp;-&nbsp;");
        }
        s.Append(text);
        if (level == 0)
        {
            s.Insert(0, "<span style='color: red;'>");
            s.Append("</span>");
        }
        else if (level == 1)
        {
            s.Insert(0, "<span style='color: purple; font-weight: bold;'>");
            s.Append("</span>");
        }
        else if (level == 2)
        {
            s.Insert(0, "<span style='color: blue;'>");
            s.Append("</span>");
        }
        return s.ToString();
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

        if (orderSource.SelectedIndex < 0)
            return;
        var os = orderSource.SelectedItem.Text;

        var list_i = db.Value.GetStoreX(start, end, "I").ToList().Where(o => o.OrderSource == os).ToList();
        var list_c = db.Value.GetStoreX(start, end, "C").ToList().Where(o => o.OrderSource == os).ToList();

        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => new InMemoryCatalog { Id = o.Value.GlobalId(), Name = Fix(o.Level, o.Text)}).ToList();

        list_i = catalogs.Select(o => o.Id).ToList().Join(list_i, o => o, o => o.CatalogId, (a, b) => b).ToList();
        list_c = catalogs.Select(o => o.Id).ToList().Join(list_c, o => o, o => o.CatalogId, (a, b) => b).ToList();

        var list = new List<InMemoryXObj>();
        foreach (var b in catalogs)
        {
            var x = new InMemoryXObj();
            x.CatalogId = b.Id;
            x.CatalogName = b.Name;
            x.I = (list_i.Count(o => o.CatalogId == b.Id) == 0 ? 0M : list_i.Where(o => o.CatalogId == b.Id).Sum(o => o.Money));
            x.C = (list_c.Count(o => o.CatalogId == b.Id) == 0 ? 0M : list_c.Where(o => o.CatalogId == b.Id).Sum(o => o.Money));
            list.Add(x);
        }
        view.DataSource = list;
    }
}
