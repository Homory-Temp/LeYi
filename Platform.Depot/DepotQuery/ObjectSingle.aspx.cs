using Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotQuery_ObjectSingle : DepotPageSingle
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
            if (!"Number".Query().None())
            {
                no.Text = Server.UrlDecode("Number".Query());
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

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var catalogs = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList().Where(o => o.Code != "*Homory:Null*").ToList();
        var source = catalogs.Join(DataContext.DepotInXRecord.Where(o => o.Note == no.Text), o => o.Id, o => o.CatalogId, (a, b) => b).ToList();
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
        }
        view.DataSource = source.ToList();
        pager.Visible = source.Count() > pager.PageSize;
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void only_CheckedChanged(object sender, EventArgs e)
    {
        view.Rebind();
    }
}
