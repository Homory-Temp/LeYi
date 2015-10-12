using Models;
using System.Linq;
using Telerik.Web.UI;

public partial class RolePermission : SingleStoragePage
{
    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        list.Source(db.Value.StorageRole.Where(o => o.StorageId == StorageId && o.State < State.删除).OrderBy(o => o.Ordinal).ToList());
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_add();");
    }

    protected void remove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_remove('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void edit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_edit('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void r_Click(object sender, System.EventArgs e)
    {
        var button = sender as RadButton;
        var id = button.Value.Substring(1).GlobalId();
        var right = button.Value.Substring(0, 1);
        var role = db.Value.StorageRole.Single(o => o.Id == id);
        if (button.Checked)
        {
            if (role.StorageRoleRight.Count(o => o.Right == right) == 0)
            {
                role.StorageRoleRight.Add(new StorageRoleRight { RoleId = role.Id, Right = right });
                db.Value.StorageSave();
            }
        }
        else
        {
            if (role.StorageRoleRight.Count(o => o.Right == right) > 0)
            {
                role.StorageRoleRight.Remove(role.StorageRoleRight.Single(o => o.Right == right));
                db.Value.StorageSave();
            }
        }
        list.Rebind();
    }
}
