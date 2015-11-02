using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Depot_Warn : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void view_action_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var s = DataContext.DepotObject.Where(o => o.Amount < o.Low && o.Low > 0 && o.State < State.停用).ToList();
        view_action.DataSource = s;
    }

    protected void view_query_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var s = DataContext.DepotObject.Where(o => o.Amount > o.High && o.High > 0 && o.State < State.停用).ToList();
        view_query.DataSource = s;
    }

    protected string GetCatalog(object id)
    {
        var objId = id.GlobalId();
        var isVirtual = Depot.Featured(DepotType.固定资产库);
        var doc = DataContext.DepotObjectCatalog.FirstOrDefault(o => o.IsLeaf == true && o.IsVirtual == isVirtual && o.ObjectId == objId);
        if (doc == null)
            return string.Empty;
        else
            return DataContext.ToCatalog(doc.CatalogId, doc.Level).Single();
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        view_action.Rebind();
        view_query.Rebind();
    }
}
