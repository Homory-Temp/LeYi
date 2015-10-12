using System;
using System.Linq;
using Telerik.Web.UI;

public partial class Return : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        obj_search.DataSource = db.Value.QueryStorageObject("", StorageId).ToList();
        tar_search.DataSource = db.Value.QueryStorageBorrow("", StorageId).ToList();
    }

    protected void steps_ActiveStepChanged(object sender, EventArgs e)
    {
        steps.ActiveStepIndex = 0;
    }

    protected void obj_search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        obj_search.DataSource = db.Value.QueryStorageObject(e.FilterString, StorageId).ToList();
        obj_view.Rebind();
    }

    protected void tar_search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        tar_search.DataSource = db.Value.QueryStorageBorrow(e.FilterString, StorageId).ToList();
        tar_view.Rebind();
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

    protected void tar_search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        tar_id.Value = string.Empty;
        tar_view.Rebind();
    }

    protected void tar_view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        tar_view.Source(db.Value.QueryStorageBorrow(tar_search.Text.Null() ? null : tar_search.Text, StorageId).ToList());
        TestIn();
    }

    protected void obj_set_Click(object sender, EventArgs e)
    {
        obj_id.Value = (sender as RadButton).Value;
        obj_view.Rebind();
        TestIn();
    }

    protected void tar_set_Click(object sender, EventArgs e)
    {
        tar_id.Value = (sender as RadButton).Value;
        tar_view.Rebind();
        TestIn();
    }

    protected void TestIn()
    {
        if (obj_id.Value.Null() || tar_id.Value.Null())
            @in.Visible = false;
        else
            @in.Visible = true;
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var obj = db.Value.StorageObjectGetOne(obj_id.Value.GlobalId());
        Response.Redirect("~/StorageReturn/ReturnDoing{3}?StorageId={0}&ObjectId={1}&UserId={2}".Formatted(StorageId, obj_id.Value, tar_id.Value, obj.Single ? "S" : "M"));
    }
}
