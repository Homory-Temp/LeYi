using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotQuery_UseX : DepotPageSingle
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
            peopleX.Items.Clear();
            peopleX.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "借领人", Value = "0", Selected = true });
            peopleX.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            peopleX.DataBind();
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            age.Items.Clear();
            age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            age.DataBind();
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
        var start = new DateTime(time.Year, time.Month, 1).AddMilliseconds(-1);
        var end = new DateTime(time.Year, time.Month, 1).AddMonths(1);
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(DataContext.DepotUseXRecord.Where(o => o.Time > start && o.Time < end), o => o, o => o.CatalogId, (a, b) => b).ToList().OrderByDescending(o => o.Time).ThenBy(o => o.UserName).ToList();
        if (useType.SelectedIndex > 0)
        {
            var x = int.Parse(useType.SelectedItem.Value);
            source = source.Where(o => o.Type == x).ToList();
        }
        if (!name.Text.Trim().None())
        {
            source = source.Where(o => o.Name.Equals(name.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        if (!age.Text.Trim().None() && age.SelectedIndex > 0)
        {
            source = source.Where(o => o.Age.Equals(age.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        if (!peopleX.Text.Trim().None() && peopleX.SelectedIndex > 0)
        {
            source = source.Where(o => o.UserName.Equals(peopleX.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        if (!people.Text.Trim().None() && people.SelectedIndex > 0)
        {
            source = source.Where(o => o.OperatorName.Equals(people.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
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
