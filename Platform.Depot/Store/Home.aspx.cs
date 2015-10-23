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
            //db.Value.ActionIn(Guid.Parse("84D8E587-8CEE-46B2-85CA-08D2D6C1C52D"), Guid.Parse("A06CE587-8C40-44BE-AA6B-08D2D708B34C"), "总部教师", "食堂", "", null, "入库说明", new DateTime(2015, 8, 16), CurrentUser, "", 5, 10, 2, 0, 10);
            //db.Value.SaveChanges();
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
