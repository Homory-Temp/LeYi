using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_Home : StorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            creating.Visible = Right_Create;
        }
    }

    protected bool CanVisit(Guid storeId)
    {
        return db.Value.Store_Visitor.Count(o => o.Id == CurrentUser && o.StoreId == storeId) > 0;
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Store/HomeAdd");
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view.DataSource = db.Value.Store.Where(o => o.State < Models.StoreState.删除).OrderBy(o => o.Ordinal).ToList();
    }
}
