using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;
using Telerik.Web.UI;

public partial class VIP_机构 : Homory.Model.HomoryCorePage
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

    protected void tree_NeedDataSource(object sender, Telerik.Web.UI.TreeListNeedDataSourceEventArgs e)
    {
        tree.DataSource = HomoryContext.Value.C__机构.ToList();
    }

    protected void tree_PreRender(object sender, EventArgs e)
    {
        tree.ExpandAllItems();
    }

    protected void tree_ItemDataBound(object sender, Telerik.Web.UI.TreeListItemDataBoundEventArgs e)
    {
        if (e.Item is TreeListHeaderItem)
        {
            (e.Item as TreeListHeaderItem).Cells[(e.Item as TreeListHeaderItem).Cells.Count - 3].Controls[0].Visible = false;
        }
        else if (e.Item is TreeListDataItem)
        {
            var item = e.Item as TreeListDataItem;
            if (item.ParentItem == null)
            {
                item.Cells[item.Cells.Count - 3].Controls[0].Visible = false;
            }
        }
    }

    protected void sub_ServerClick(object sender, EventArgs e)
    {
        Session["Sp_Org_X"] = v.Value;
        if (Session["Sp_User_X"] == null)
            Response.Redirect("../VIP/用户.aspx");
        else
            Response.Redirect("../VIP/关系.aspx");
    }
}
