using Models;
using System;
using System.Linq;

public partial class Catalog : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.SourceBind(db.Value.StorageCatalogGet(StorageId).OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList());
            tree.InitialTree(0, 2);
            list.Rebind();
        }
    }

    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (tree.SelectedValue.Null())
            list.Source(null);
        else
            list.Source(db.Value.StorageCatalogGet(StorageId, tree.SelectedValue.GlobalId()).OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList());
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_add('{0}');".Formatted(tree.SelectedValue));
    }

    protected void edit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_edit('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void remove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_remove('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        e.Node.Expanded = true;
        list.Rebind();
    }

    protected void ap_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {
        if (e.Argument == "Rebind")
        {
            tree.RebindTreeCallback(db.Value.StorageCatalogGet(StorageId).OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList());
            list.Rebind();
        }
    }
}
