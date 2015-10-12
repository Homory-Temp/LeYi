using System;
using System.Web.UI;

public partial class StorageEditPopup : StoragePage
{
    protected void Page_Load(object ssender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var storage = db.Value.StorageGet("Id".Query().GlobalId());
            if (storage == null) { ap.Script("cancel();"); return; }
            new Control[] { ordinal, name }.InitialValue(storage.Ordinal, storage.Name);
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写仓库名称")) return;
        db.Value.StorageEdit("Id".Query().GlobalId(), name.Text, ordinal.Value(100));
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
