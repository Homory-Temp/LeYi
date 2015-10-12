using System;
using System.Linq;
using System.Web.UI;

public partial class RolePermissionRemovePopup : StoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "Id".Query().GlobalId();
            var role = db.Value.StorageRole.Single(o => o.Id == id);
            message.InnerText = "确认删除角色“{0}”吗？".Formatted(role.Name);
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var id = "Id".Query().GlobalId();
        var role = db.Value.StorageRole.Single(o => o.Id == id);
        if (role == null) { ap.Script("cancel();"); return; }
        role.State = Models.State.删除;
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
