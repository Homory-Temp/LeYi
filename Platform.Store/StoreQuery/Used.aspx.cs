using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_Used : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            periodx.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = db.Value.User.Where(o => o.State < 2 && o.Type == 1).ToList();
            people.DataBind();
            peopleX.Items.Clear();
            peopleX.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "借领人", Value = "0", Selected = true });
            peopleX.DataSource = db.Value.User.Where(o => o.State < 2 && o.Type == 1).ToList();
            peopleX.DataBind();
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            age.Items.Clear();
            age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            if (CurrentStore.State == StoreState.食品)
            {
                var s = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.ParentId == null && o.State < 2).OrderBy(o => o.Ordinal).ToList();
                age.DataSource = s;
            }
            else
            {
                var s = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).ToList().Join(db.Value.Store_In, o => o.Id, o => o.CatalogId, (a, b) => b.Age).Distinct().ToList();
                age.DataSource = s;
            }
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
        var timex = periodx.SelectedDate.HasValue ? periodx.SelectedDate.Value : DateTime.Today;
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        if (timex > time)
        {
            var time_t = timex;
            timex = time;
            time = time_t;
        }
        var start = timex.AddMilliseconds(-1).ToTimeNode();
        var end = time.AddDays(1).ToTimeNode();
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(db.Value.Store_LC.Where(o => o.TimeNode > start && o.TimeNode < end), o => o, o => o.CatalogId, (a, b) => b).ToList().OrderByDescending(o => o.TimeNode).ThenBy(o => o.User).ToList();
        if (useType.SelectedIndex > 0)
        {
            var x = useType.SelectedItem.Text;
            source = source.Where(o => o.Type == x).ToList();
        }
        if (!name.Text.Trim().Null())
        {
            source = source.Where(o => o.Name.Equals(name.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        if (!age.Text.Trim().Null() && age.SelectedIndex > 0)
        {
            source = source.Where(o => o.Age.Equals(age.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        if (!peopleX.Text.Trim().Null() && peopleX.SelectedIndex > 0)
        {
            source = source.Where(o => o.User.Equals(peopleX.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        if (!people.Text.Trim().Null() && people.SelectedIndex > 0)
        {
            source = source.Where(o => o.Operator.Equals(people.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        source = source.Where(o => o.Amount > 0).ToList();
        view.DataSource = source;
        ___total.Value = source.Sum(o => o.Money).ToMoney();
        //pager.Visible = source.Count > pager.PageSize;
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreAction/UseSingleEdit?StoreId={0}&LCId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"]));
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
