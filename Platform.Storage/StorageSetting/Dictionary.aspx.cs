using Models;
using System.Linq;

public partial class Dictionary : SingleStoragePage
{
    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (tree.SelectedValue.Null())
            list.Source(null);
        else
            list.Source(db.Value.StorageDictionaryGet(StorageId, (DictionaryType)int.Parse(tree.SelectedValue)).OrderBy(o => o.Name).ToList());
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_add('{0}');".Formatted(tree.SelectedValue));
    }

    protected void remove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_remove('{0}', '{1}');".Formatted(sender.ButtonArgs(), tree.SelectedValue));
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        list.Rebind();
    }
}
