using Models;
using System;
using System.Web.UI;

public partial class DictionaryRemovePopup : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.TitleAppend("：{0}".Formatted((DictionaryType)int.Parse("Type".Query())).ToString());
            message.InnerText = "确认删除“{0}”吗？".Formatted("Name".Query(true));
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        db.Value.StorageDictionaryRemove(StorageId, "Name".Query(true), (DictionaryType)int.Parse("Type".Query()));
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
