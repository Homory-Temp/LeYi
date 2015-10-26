using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_InX : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            age.Items.Clear();
            age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            age.DataBind();
            place.Items.Clear();
            place.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "存放地", Value = "", Selected = true });
            place.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.存放地).ToList();
            place.DataBind();
        }
    }

    protected void all_ServerClick(object sender, EventArgs e)
    {
        if (_all.Value == "1")
        {
            tree.UncheckAllNodes();
            _all.Value = "0";
            all.Value = "全部选定";
        }
        else
        {
            tree.CheckAllNodes();
            _all.Value = "1";
            all.Value = "清除选定";
        }
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        var start = (new DateTime(time.Year, time.Month, 1).AddMilliseconds(-1));
        var end = (new DateTime(time.Year, time.Month, 1).AddMonths(1));
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(DataContext.DepotInXRecord.Where(o => o.Time > start && o.Time < end), o => o, o => o.CatalogId, (a, b) => b).ToList().OrderByDescending(o => o.Time).ThenBy(o => o.OrderName).ToList();
        if (age.SelectedIndex > 0 && !age.Text.None())
            source = source.Where(o => o.Age == age.Text).ToList();
        if (place.SelectedIndex > 0 && !place.Text.None())
            source = source.Where(o => o.Place == place.Text).ToList();
        if (people.SelectedIndex > 0)
            source = source.Where(o => o.Operator == people.SelectedItem.Text).ToList();
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/InEdit?DepotId={0}&InId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected void pager_PageIndexChanged(object sender, Telerik.Web.UI.RadDataPagerPageIndexChangeEventArgs e)
    {
        view.Rebind();
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }
}
