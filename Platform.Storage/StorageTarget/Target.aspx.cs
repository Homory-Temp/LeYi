using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Target : SingleStoragePage
{
    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        list.Source(db.Value.StorageTargetGet(StorageId).OrderBy(o => o.TimeNode).ToList());
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageTarget/TargetAdd?{0}".Formatted(StorageId));
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}&TargetId={1}".Formatted(StorageId, sender.ButtonArgs().GlobalId()));
    }

    protected void edit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_edit('{0}', '{1}');".Formatted(sender.ButtonArgs(), StorageId));
    }

    protected void remove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_remove('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void save_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var input = (sender as ImageButton).NamingContainer.FindControl("receipt") as RadTextBox;
        var confirm = (sender as ImageButton).NamingContainer.FindControl("confirm") as RadButton;
        if (input.MissingText("请填写发票编号")) return;
        if (!confirm.Checked) { confirm.ForeColor = Color.Red; return; }
        var id = sender.ButtonArgs().GlobalId();
        var t = db.Value.StorageTargetGetOne(id);
        t.ReceiptNumber = input.Text;
        db.Value.StorageSave();
        list.Rebind();
    }

    protected void done_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var t = db.Value.StorageTargetGetOne(sender.ButtonArgs().GlobalId());
        t.In = true;
        db.Value.StorageSave();
        Response.Redirect("~/StorageTarget/TargetIn?StorageId={0}&TargetId={1}".Formatted(StorageId, t.Id));
    }

    protected void dq_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageQuery/QueryTarget?StorageId={0}".Formatted(StorageId));
    }
}
