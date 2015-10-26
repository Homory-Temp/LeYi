using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_Redo : DepotPageSingle
{
    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = DataContext.DepotFlow.Where(o => o.Type == FlowType.退货).ToList().Where(o => o.DepotObject.DepotIn.First().DepotOrder.DepotId == Depot.Id).ToList().Select(o => o.Note.FromJson<InMemoryRedo>()).ToList();
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }
}
