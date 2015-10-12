using Models;
using System;
using System.Drawing;
using System.Linq;
using Telerik.Web.UI;

public partial class InObject : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tar_search.DataSource = db.Value.QueryStorageTarget("", StorageId).Where(o => o.In == false).ToList();
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
            //fixedArea.Visible = obj.Fixed;
            object_fixed_serial.Text = obj.FixedSerial;
        }
    }

    protected void tar_search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        tar_search.DataSource = db.Value.QueryStorageTarget(e.FilterString, StorageId).Where(o => o.In == false).ToList();
        tar_view.Rebind();
    }

    protected Guid ObjectId
    {
        get { return "ObjectId".Query().GlobalId(); }
    }



    protected void tar_view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = db.Value.QueryStorageTarget(tar_search.Text, StorageId).Where(o => o.In == false).OrderByDescending(o => o.TimeNode).ToList();
        tar_view.Source(source);
        if (source.Count == 1)
        {
            tar_id.Value = source.Single().Id.ToString();
        }
        TestIn();
    }

    protected void tar_set_Click(object sender, EventArgs e)
    {
        tar_id.Value = (sender as RadButton).Value;
        tar_view.Rebind();
        TestIn();
    }

    protected void tar_search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        tar_id.Value = string.Empty;
        tar_view.Rebind();
    }

    protected void tarj_search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        tar_id.Value = string.Empty;
        tar_view.Rebind();
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
        Response.Redirect("~/StorageIn/InDoing?StorageId={0}&ObjectId={1}&TargetId={2}".Formatted(StorageId, ObjectId, tar_id.Value));
    }

    protected void new_tar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageTarget/TargetAdd?StorageId={0}".Formatted(StorageId));
    }
}
