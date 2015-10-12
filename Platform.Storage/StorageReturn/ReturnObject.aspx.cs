using System;
using System.Linq;
using Telerik.Web.UI;

public partial class Return : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tar_search.DataSource = db.Value.QueryStorageBorrow("", StorageId).ToList();
        if (!IsPostBack)
        {
            var objectId = ObjectId;
            var obj = db.Value.StorageObject.Single(o => o.Id == objectId);
            object_id.Value = obj.ToString();
            object_name.Text = obj.Name;
            object_catalog.Text = obj.GeneratePath();
            object_unit.Text = obj.Unit;
            object_specification.Text = obj.Specification;
            object_fixed.Visible = obj.Fixed;
            object_consumable.Visible = obj.Consumable;
            object_inAmount.Text = obj.InAmount.ToString();
            object_low.Visible = obj.Low > 0 && obj.InAmount < obj.Low;
            object_high.Visible = obj.High > 0 && obj.InAmount > obj.High;
            fixedArea.Visible = obj.Fixed;
            object_fixed_serial.Text = obj.FixedSerial;
        }
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }

    protected void steps_ActiveStepChanged(object sender, EventArgs e)
    {
        steps.ActiveStepIndex = 0;
    }

    protected void tar_search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        tar_search.DataSource = db.Value.QueryStorageBorrow(e.FilterString, StorageId).ToList();
        tar_view.Rebind();
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

    protected void tar_set_Click(object sender, EventArgs e)
    {
        tar_id.Value = (sender as RadButton).Value;
        tar_view.Rebind();
        TestIn();
    }

    protected void TestIn()
    {
        if (tar_id.Value.Null())
            @in.Visible = false;
        else
            @in.Visible = true;
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var objectId = ObjectId;
        var obj = db.Value.StorageObject.Single(o => o.Id == objectId);
        Response.Redirect("~/StorageReturn/ReturnDoing{3}?StorageId={0}&ObjectId={1}&UserId={2}".Formatted(StorageId, ObjectId, tar_id.Value, obj.Single ? "S" : "M"));
    }
}
