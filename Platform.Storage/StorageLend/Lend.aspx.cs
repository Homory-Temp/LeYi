using System;
using System.Linq;
using Telerik.Web.UI;

public partial class Lend : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        obj_search.DataSource = db.Value.QueryStorageObject("", StorageId).Where(o => o.InAmount > 0 && o.Consumable == false).ToList();
    }


    protected void obj_search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        obj_search.DataSource = db.Value.QueryStorageObject(e.FilterString, StorageId).Where(o => o.InAmount > 0 && o.Consumable == false).ToList();
        obj_view.Rebind();
    }

    protected void obj_search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        obj_id.Value = string.Empty;
        obj_view.Rebind();
    }

    protected void obj_view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        obj_view.Source(db.Value.QueryStorageObject(obj_search.Text.Null() ? null : obj_search.Text, StorageId));
        TestIn();
    }

    protected void obj_set_Click(object sender, EventArgs e)
    {
        obj_id.Value = (sender as RadButton).Value;
        obj_view.Rebind();
        TestIn();
    }

    protected void TestIn()
    {
        if (obj_id.Value.Null())
            @in.Visible = false;
        else
            @in.Visible = true;
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var obj = db.Value.StorageObjectGetOne(obj_id.Value.GlobalId());
        Response.Redirect("~/StorageLend/LendDoing{2}?StorageId={0}&ObjectId={1}".Formatted(StorageId, obj_id.Value, obj.Single ? "S" : "M"));
    }

    protected void new_obj_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}".Formatted(StorageId));
    }
}
