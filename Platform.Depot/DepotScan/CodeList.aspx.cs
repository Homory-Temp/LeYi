using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_CodeList : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view.DataSource = DataContext.DepotCode.Where(o => o.DepotId == Depot.Id && o.State < 3).OrderByDescending(o => o.Time).ToList();
    }

    protected void down_ServerClick(object sender, EventArgs e)
    {

    }

    protected void del_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        foreach (var item in DataContext.DepotCode.Where(o => o.BatchId == bid).ToList())
        {
            item.State = 3;
        }
        DataContext.SaveChanges();
        view.Rebind();
    }
}
