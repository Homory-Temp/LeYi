using Models;
using System;
using System.Linq;

public partial class DepotAction_ObjectRemove : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var oid = "ObjectId".Query().GlobalId();
            var obj = DataContext.DepotObject.Single(o => o.Id == oid);
            info.InnerText = "确认删除物资“{0}”吗？".Formatted(obj.Name);
        }
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        var oid = "ObjectId".Query().GlobalId();
        Response.Redirect("~/DepotAction/Object?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, "CatalogId".Query()));
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        var oid = "ObjectId".Query().GlobalId();
        var obj = DataContext.DepotObject.Single(o => o.Id == oid);
        if (!name.Text.Trim().Equals(obj.Name, StringComparison.InvariantCultureIgnoreCase))
        {
            NotifyError(ap, "请输入完整的物资名称");
            return;
        }
        Save();
        Response.Redirect("~/DepotAction/Object?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, "CatalogId".Query()));
    }

    protected Guid Save()
    {
        var oid = "ObjectId".Query().GlobalId();
        DataContext.DepotObjectRemove(oid);
        return oid;
    }
}
