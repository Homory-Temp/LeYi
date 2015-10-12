using Models;
using System;

public partial class RolePermissionAddPopup : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写角色名称")) return;
        db.Value.StorageRole.Add(new StorageRole { Id = db.Value.GlobalId(), Ordinal = ordinal.Value(100), Name = name.Text, State = State.启用, StorageId = StorageId });
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
