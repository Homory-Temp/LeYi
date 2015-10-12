
public partial class StorageAddPopup : StoragePage
{
    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写仓库名称")) return;
        var StorageId = db.Value.StorageAdd(CurrentCampus, name.Text, ordinal.Value(100));
        db.Value.StorageSave();
        if (db.Value.StorageGet(StorageId).StorageRole.Count == 0)
        {
            db.Value.InitializePermission(CurrentUser, StorageId);
            db.Value.StorageSave();
        }
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
