using System;
using System.Linq;

public partial class StorageMobile : StoragePageMobile
{
    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        list.Source(db.Value.StorageGet().OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList());
    }

    protected string GenerateUrl(object id)
    {
        return "../StorageHome/HomeMobile?StorageId={0}".Formatted(id);
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_add();");
    }

    protected void edit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_edit('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void remove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_remove('{0}');".Formatted(sender.ButtonArgs()));
    }
}
