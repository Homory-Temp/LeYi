using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_SideBar : StoreControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            u.InnerText = db.Value.GetUserName(CurrentUser);
        }
    }

    protected void qb_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();
        var link = "{0}Go/Board".Formatted(Application["Sso"]);
        Response.Redirect(link);
    }
}
