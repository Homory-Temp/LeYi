using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

public partial class ObjectQuery : SingleStoragePage
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
            list.Source(db.Value.StorageCatalogObjectGetEx(StorageId, tree.SelectedValue.GlobalId()).Where(o => o.Name.ToLower().Contains(search.Text.ToLower()) || o.PinYin.ToLower().Contains(search.Text.ToLower()) || o.Note.ToLower().Contains(search.Text.ToLower())));
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        e.Node.Expanded = true;
        list.Rebind();
    }

    protected void search_Search(object sender, SearchBoxEventArgs e)
    {
        list.Rebind();
    }

    protected void list_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        var item = e.Item as RadListViewDataItem;
        var label = item.FindControl("lbl").ClientID;
        var tooltip = item.FindControl("tip") as RadToolTip;
        tooltip.TargetControlID = label;
    }
}
