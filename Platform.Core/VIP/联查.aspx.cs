using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;
using Telerik.Web.UI;
using Newtonsoft.Json;

public partial class VIP_联查 : Homory.Model.HomoryCorePage
{
    protected override string PageRight
    {
        get
        {
            return HomoryCoreConstant.RightEveryone;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new[] { "Homory", "lj" }.Contains(CurrentUser.Account))
        {
            Response.Redirect("http://oa.wxlxjy.com:888/Sso/Go/SB.html");
        }
    }

    protected void search_Click(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (string.IsNullOrEmpty(text.Text))
        {
            view.DataSource = new List<C__用户>();
        }
        else
        {
            view.DataSource = HomoryContext.Value.Database.SqlQuery<C__用户>(string.Format("EXEC 用户联合查询 '{0}'", text.Text.Trim())).ToList();
        }
    }
}
