using Models;
using System;
using System.Linq;
using System.Web.UI;

public partial class RolePermissionAddPopup : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "Id".Query().GlobalId();
            var role = db.Value.StorageRole.Single(o => o.Id == id);
            new Control[] { name, ordinal }.InitialValue(role.Name, role.Ordinal);
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写角色名称")) return;
        var id = "Id".Query().GlobalId();
        var role = db.Value.StorageRole.Single(o => o.Id == id);
        role.Name = name.Text;
        role.Ordinal = ordinal.Value(role.Ordinal);
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
