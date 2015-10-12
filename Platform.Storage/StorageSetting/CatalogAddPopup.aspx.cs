
public partial class CatalogAddPopup : SingleStoragePage
{
    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写分类名称")) return;
        db.Value.StorageCatalogAdd(StorageId, "Id".Query().GlobalId(), name.Text, ordinal.Value(100), code.Text);
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
