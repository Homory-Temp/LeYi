using System;
using System.Web.UI;

public partial class CatalogEditPopup : StoragePage
{
    protected void Page_Load(object ssender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var catalog = db.Value.StorageCatalogGetOne("Id".Query().GlobalId());
            if (catalog == null) { ap.Script("cancel();"); return; }
            new Control[] { ordinal, name, code }.InitialValue(catalog.Ordinal, catalog.Name, catalog.Code);
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写分类名称")) return;
        db.Value.StorageCatalogEdit("Id".Query().GlobalId(), name.Text, ordinal.Value(100), code.Text);
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
