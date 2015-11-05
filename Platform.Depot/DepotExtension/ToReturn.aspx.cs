using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotExtension_ToReturn : DepotPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            counter.Value = "1";
        }
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        view_obj.Rebind();
    }

    protected void people_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        view_obj.Rebind();
    }

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var userId = DepotUser.Id;
        var source = DataContext.DepotUseXRecord.Where(o => o.UserId == userId && o.Type == 2 && o.Amount > o.ReturnedAmount).ToList();
        view_obj.DataSource = source;
    }
}
