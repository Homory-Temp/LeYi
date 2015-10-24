using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotSetting_Dictionary : DepotPageSingle
{
    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var type = (DictionaryType)tree.PeekValue(1);
        var source = DataContext.DepotDictionaryLoad(Depot.Id, type).ToList();
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.None())
        {
            NotifyError(ap, "请输入要添加的基础数据");
            return;
        }
        var type = (DictionaryType)tree.PeekValue(1);
        var content = name.Text.Trim();
        DataContext.DepotDictionaryAdd(Depot.Id, type, content);
        name.Text = string.Empty;
        view.Rebind();
        NotifyOK(ap, "基础数据添加成功");
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        var content = (sender as HtmlInputButton).Attributes["match"];
        var type = (DictionaryType)tree.PeekValue(1);
        DataContext.DepotDictionaryRemove(Depot.Id, type, content);
        view.Rebind();
        NotifyOK(ap, "基础数据删除成功");
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }
}
