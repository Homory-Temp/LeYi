using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_ObjectRemove : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var oid = "ObjectId".Query().GlobalId();
            var obj = db.Value.StoreObject.Single(o => o.Id == oid);
            info.InnerText = "确认删除物资“{0}”吗？".Formatted(obj.Name);
        }
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        var oid = "ObjectId".Query().GlobalId();
        var obj = db.Value.StoreObject.Single(o => o.Id == oid);
        Response.Redirect("~/StoreAction/Object?StoreId={0}&CatalogId={1}".Formatted(StoreId, obj.CatalogId));
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        var oid = "ObjectId".Query().GlobalId();
        var obj = db.Value.StoreObject.Single(o => o.Id == oid);
        if (!name.Text.Trim().Equals(obj.Name, StringComparison.InvariantCultureIgnoreCase))
        {
            Notify(ap, "请输入完整的物资名称", "error");
            return;
        }
        Save();
        Response.Redirect("~/StoreAction/Object?StoreId={0}&CatalogId={1}".Formatted(StoreId, obj.CatalogId));
    }

    protected Guid Save()
    {
        var oid = "ObjectId".Query().GlobalId();
        var obj = db.Value.StoreObject.Single(o => o.Id == oid);
        db.Value.StoreObject.Remove(obj);
        db.Value.SaveChanges();
        return obj.Id;
    }
}
