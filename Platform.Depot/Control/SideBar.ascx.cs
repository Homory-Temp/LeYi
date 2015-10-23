﻿using System;

public partial class Control_SideBar : DepotControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            u.InnerText = DepotUser.Name;
        }
    }

    protected void qb_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();
        var link = "{0}Go/Board".Formatted(Application["Sso"]);
        Response.Redirect(link);
    }
}
