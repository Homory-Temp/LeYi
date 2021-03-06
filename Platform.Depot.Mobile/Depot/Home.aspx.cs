﻿using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Depot_Home : DepotPage
{
    protected bool CanVisit(Guid depotId)
    {
        var userId = DepotUser.Id;
        return DataContext.DepotMember.Count(o => o.Id == userId && o.DepotId == depotId) > 0;
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/HomeAdd.aspx");
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view.DataSource = DataContext.Depot.Where(o => o.State < State.停用).OrderBy(o => o.Ordinal).ToList();
    }
}
