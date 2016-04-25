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
    private Lazy<Entities> db = new Lazy<Entities>(() => new Entities());

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
        tree.DataSource = db.Value.C__机构.ToList();
    }

    protected void tree_PreRender(object sender, EventArgs e)
    {
        tree.ExpandAllItems();
    }

    protected void move_Click(object sender, EventArgs e)
    {

    }

    protected void tree_ItemDataBound(object sender, Telerik.Web.UI.TreeListItemDataBoundEventArgs e)
    {
        if (e.Item is TreeListHeaderItem)
        {
            (e.Item as TreeListHeaderItem).Cells[(e.Item as TreeListHeaderItem).Cells.Count - 3].Controls[0].Visible = false;
        }
        else if (e.Item is TreeListDataItem)
        {
            if ((e.Item as TreeListDataItem).ParentItem == null)
            {
                (e.Item as TreeListDataItem).Cells[(e.Item as TreeListDataItem).Cells.Count - 3].Controls[0].Visible = false;
            }
            else
            {
                var cb = ((e.Item as TreeListDataItem).Cells[(e.Item as TreeListDataItem).Cells.Count - 3].Controls[0] as CheckBox);
                cb.InputAttributes["org_id"] = ((e.Item as TreeListDataItem).DataItem as C__机构).机构ID.ToString();

                cb.InputAttributes["org_name"] = ((e.Item as TreeListDataItem).DataItem as C__机构).机构名称.ToString();
            }
        }
    }
}
