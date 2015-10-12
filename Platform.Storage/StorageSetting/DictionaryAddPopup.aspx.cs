using Models;
using System;

public partial class DictionaryAddPopup : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.TitleAppend("：{0}".Formatted((DictionaryType)int.Parse("Type".Query())).ToString());
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写{0}名称".Formatted(((DictionaryType)int.Parse("Type".Query())).ToString()))) return;
        db.Value.StorageDictionaryAdd(StorageId, name.Text, (DictionaryType)int.Parse("Type".Query()));
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }
}
