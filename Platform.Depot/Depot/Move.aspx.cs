using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Depot_Move : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!"Search".Query().None())
            {
                toSearch.Text = Server.UrlDecode("Search".Query());
                view.Rebind();
            }
        }
    }

    protected string CountTotal(DepotObject obj)
    {
        var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
        var noOut = query.Count() > 0 ? query.Where(o => o.Type == UseType.借用).Sum(o => o.Amount - o.ReturnedAmount) : 0;
        return (obj.Amount + noOut).ToAmount(Depot.Featured(DepotType.小数数量库));
    }

    private IEnumerable<DepotIn> ININ;

    protected IEnumerable<DepotIn> XININ
    {
        get
        {
            if (ININ == null)
                ININ = DataContext.DepotObjectInLoad(Depot.Id, null, true);
            return ININ;
        }
    }

    protected string CountDone(DepotObject obj)
    {
        var query = obj.DepotIn.ToList();
        var amount = 0M;
        foreach (var g in query.GroupBy(o => o.Note))
        {
            amount+= XININ.Where(o => o.Note == g.Key).Sum(o => o.Amount);
        }
        return (amount).ToAmount(Depot.Featured(DepotType.小数数量库));
    }

    protected bool IsSimple
    {
        get
        {
            return true;
        }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = DataContext.DepotObjectLoadFix(Depot.Id);
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
        }
        add.InnerText = "待处理物资" + source.Count().EmptyWhenZero();
        view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
        pager.Visible = source.Count() > pager.PageSize;
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected string ToLine(string value)
    {
        var sb = new StringBuilder();
        int xno = int.Parse(line_no.Value);
        for (var i = 0; i < value.Length; i++)
        {
            sb.Append(value[i]);
            if (i % xno == 0 && i > 0)
            {
                sb.Append("<br/ >");
            }
        }
        if (sb.ToString().EndsWith("<br />"))
            sb = sb.Remove(sb.ToString().LastIndexOf("<br />"), 6);
        return sb.ToString();
    }
}
