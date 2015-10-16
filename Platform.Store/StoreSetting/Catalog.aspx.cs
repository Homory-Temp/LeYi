using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreSetting_Catalog : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            sp.Visible = CurrentStore.State == StoreState.食品;
        }
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {

    }
}
