using System;
using System.Web.UI;

public partial class ObjectRemovePopup : StoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var obj = db.Value.StorageObjectGetOne("Id".Query().GlobalId());
            message.InnerText = "确认删除物资“{0}”吗？".Formatted(obj.Name);
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var obj = db.Value.StorageObjectGetOne("Id".Query().GlobalId());
        if (obj == null) { ap.Script("cancel();"); return; }
        var  r = db.Value.StorageObjectRemove("Id".Query().GlobalId());
        if (r == 1)
        {
            wm.RadAlert("物资有外借，删除失败", 250, 125, "提示：", null);
        }
        else if (r == 2)
        {
            wm.RadAlert("物资有库存，删除失败", 250, 125, "提示：", null);
        }
        else
        {
            db.Value.StorageSave();
            ap.Script("ok();");
        }
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
