using System;
using System.Web.UI;

public partial class CatalogRemovePopup : StoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var catalog = db.Value.StorageCatalogGetOne("Id".Query().GlobalId());
            message.InnerText = "确认删除分类“{0}”吗？".Formatted(catalog.Name);
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var catalog = db.Value.StorageCatalogGetOne("Id".Query().GlobalId());
        if (catalog == null) { ap.Script("cancel();"); return; }
        db.Value.StorageCatalogRemove("Id".Query().GlobalId());
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
