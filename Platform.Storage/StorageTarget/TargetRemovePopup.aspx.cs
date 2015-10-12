using System;
using System.Linq;

public partial class TargetRemovePopup : StoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var id = "Id".Query().GlobalId();
        var target = db.Value.StorageTarget.FirstOrDefault(o => o.Id == id);
        info.Text = "确认删除购置单：“{0}”吗？".Formatted(target.Number);
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var id = "Id".Query().GlobalId();
        var target = db.Value.StorageTarget.FirstOrDefault(o => o.Id == id);
        if (target == null) { ap.Script("cancel();"); return; }
        TargetRemove("Id".Query().GlobalId());
        ap.Script("ok();");
    }

    public void TargetRemove(Guid id)
    {
        var target = db.Value.StorageTarget.FirstOrDefault(o => o.Id == id);
        if (target != null && target.StorageIn.Count == 0)
        {
            db.Value.StorageTarget.Remove(target);
            db.Value.SaveChanges();
        }
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
