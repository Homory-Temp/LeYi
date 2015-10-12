using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class UserRole : SingleStoragePage
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

    protected void list_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        var item = e.Item as RadListViewDataItem;
        var repeater = item.FindControl("r") as Repeater;
        var id = item.GetDataKeyValue("Id").ToString().GlobalId();
        var role = db.Value.StorageRole.Single(o => o.Id == id);
        repeater.DataSource = role.User.ToList();
        repeater.DataBind();
    }

    protected void people_DataSourceSelect(object sender, SearchBoxDataSourceSelectEventArgs e)
    {
        var source = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        (sender as RadSearchBox).DataSource = source.Where(o => o.Name.ToLower().Contains(e.FilterString.ToLower()) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
    }

    protected void people_Search(object sender, SearchBoxEventArgs e)
    {
        var uid = e.Value.GlobalId();
        var id = ((sender as RadSearchBox).NamingContainer as RadListViewDataItem).GetDataKeyValue("Id").ToString().GlobalId();
        var role = db.Value.StorageRole.Single(o => o.Id == id);
        var u = db.Value.User.Single(o => o.Id == uid);
        if (!role.User.Contains(u))
        {
            role.User.Add(u);
            db.Value.StorageSave();
        }
        list.Rebind();
    }

    protected void people_Load(object sender, EventArgs e)
    {
        var sourceX = db.Value.ViewTeacher.Where(o => (o.State == State.启用 || o.State == State.内置) && (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.TopDepartmentId == CurrentCampus).ToList().Select(o => new { Name = o.RealName, Id = o.Id, PinYin = o.PinYin }).ToList();
        (sender as RadSearchBox).DataSource = sourceX;
    }

    protected void del_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var uid = (sender as ImageButton).CommandArgument.GlobalId();
        var id = ((sender as ImageButton).NamingContainer.NamingContainer.NamingContainer as RadListViewDataItem).GetDataKeyValue("Id").ToString().GlobalId();
        var role = db.Value.StorageRole.Single(o => o.Id == id);
        var u = db.Value.User.Single(o => o.Id == uid);
        if (role.User.Contains(u))
        {
            if ((role.State == State.内置 && role.User.Count > 1) || role.State == State.启用)
            {
                role.User.Remove(u);
                db.Value.StorageSave();
            }
        }
        list.Rebind();
    }

    protected void del_Load(object sender, EventArgs e)
    {
        var uid = (sender as ImageButton).CommandArgument.GlobalId();
        var id = ((sender as ImageButton).NamingContainer.NamingContainer.NamingContainer as RadListViewDataItem).GetDataKeyValue("Id").ToString().GlobalId();
        var role = db.Value.StorageRole.Single(o => o.Id == id);
        (sender as ImageButton).Visible = role.User.Count > 1;
    }
}
