using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Store_HomeRemove : StorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "StoreId".Query().GlobalId();
            store.InnerText = db.Value.Store.Single(o => o.Id == id).Name;
        }
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        if (!name.Text.Equals(store.InnerText.Trim(), StringComparison.InvariantCultureIgnoreCase))
        {
            Notify(ap, "请输入完整的仓库名称", "error");
            return;
        }
        var id = "StoreId".Query().GlobalId();
        var item = db.Value.Store.Single(o => o.Id == id);
        item.State = StoreState.删除;
        db.Value.SaveChanges();
        Response.Redirect("~/Store/Home");
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Store/Home");
    }
}
