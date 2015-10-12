using System.Web.UI;

public partial class StorageRemovePopup : StoragePage
{
    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var storage = db.Value.StorageGet("Id".Query().GlobalId());
        if (storage == null) { ap.Script("cancel();"); return; }
        if (name.MissingText("请填写完整的仓库名称")) return;
        if (!name.Text.Equals(storage.Name, System.StringComparison.OrdinalIgnoreCase)) { name.Text = string.Empty; name.MissingText("请填写完整的仓库名称"); return; }
        db.Value.StorageRemove("Id".Query().GlobalId());
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
