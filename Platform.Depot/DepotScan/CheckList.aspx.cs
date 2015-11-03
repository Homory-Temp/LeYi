﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_CheckList : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view.DataSource = DataContext.DepotCheck.Where(o => o.DepotId == Depot.Id && o.State  == 1).OrderByDescending(o => o.Time).ToList();
    }

    protected void del_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        foreach (var item in DataContext.DepotCheck.Where(o => o.BatchId == bid).ToList())
        {
            item.State = 2;
        }
        DataContext.SaveChanges();
        view.Rebind();
    }
}
