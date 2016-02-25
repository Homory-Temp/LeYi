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
            periodx.SelectedDate = DateTime.Today.AddMonths(-1);
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            peopleX.Items.Clear();
            peopleX.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "借领人", Value = "0", Selected = true });
            peopleX.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            peopleX.DataBind();
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList().Where(o => o.Code != "*Homory:Null*").ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            age.Items.Clear();
            age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            age.DataBind();
            age.Visible = Depot.Featured(DepotType.幼儿园);
            name.Focus();
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
        viewx.Rebind();
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
        var start = timex.AddMilliseconds(-1);
        var end = time.AddDays(1);
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var isVirtual = Depot.Featured(DepotType.固定资产库);
        var source = catalogs.Join(DataContext.DepotUseXRecord.Where(o => o.Time > start && o.Time < end && o.IsVirtual == isVirtual), o => o, o => o.CatalogId, (a, b) => b).ToList().OrderByDescending(o => o.Time).ThenBy(o => o.UserName).ToList();
        if (!name.Text.Trim().None())
        {
            var a_source = DataContext.DepotObjectLoad(Depot.Id, null);
            if (!name.Text.None())
            {
                var glist = a_source.Where(o => o.Name.ToLower().Contains(name.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(name.Text.Trim().ToLower()) || o.Code.ToLower() == name.Text.Trim().ToLower()).Select(o => o.Id).ToList();
                source = glist.Join(source, o => o, o => o.ObjectId, (a, b) => b).ToList();
            }
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
        if (useTypeX.SelectedIndex == 1)
        {
            source = source.Where(o => o.Type == 2 && o.ReturnedAmount < o.Amount).ToList();
        }
        else if (useTypeX.SelectedIndex == 2)
        {
            source = source.Where(o => o.Type == 2 && o.ReturnedAmount == o.Amount).ToList();
        }
        else if (useTypeX.SelectedIndex == 3)
        {
            source = source.Where(o => o.Type == 1).ToList();
        }
        source = source.Where(o => o.Amount > 0).ToList();
        view.DataSource = source.OrderByDescending(o => o.Time).ToList();
        //___total.Value = source.Sum(o => o.Amount).ToAmount(Depot.Featured(DepotType.小数数量库)) + "@@@" + source.Sum(o => o.Money).ToMoney();
        if (useTypeX.SelectedIndex == 2)
        {
            view.Visible = pager.Visible = false;
        }
        else
        {
            view.Visible = pager.Visible = true;
            pager.Visible = source.Count > pager.PageSize;
        }
    }

    protected bool IsC(Guid objId)
    {
        return DataContext.DepotObject.Single(o => o.Id == objId).Consumable;
    }

    protected void viewx_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var timex = periodx.SelectedDate.HasValue ? periodx.SelectedDate.Value : DateTime.Today;
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        if (timex > time)
        {
            var time_t = timex;
            timex = time;
            time = time_t;
        }
        var start = timex.AddMilliseconds(-1);
        var end = time.AddDays(1);
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var isVirtual = Depot.Featured(DepotType.固定资产库);
        var source = catalogs.Join(DataContext.DepotReturnRecord.Where(o => o.Time > start && o.Time < end && o.IsVirtual == isVirtual), o => o, o => o.CatalogId, (a, b) => b).ToList().OrderByDescending(o => o.Time).ThenBy(o => o.UserName).ToList();
        if (useTypeX.SelectedIndex  == 3)
        {
            source = new List<DepotReturnRecord>();
        }
        if (!name.Text.Trim().None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(name.Text.Trim().ToLower())).ToList();
        }
        if (!age.Text.Trim().None() && age.SelectedIndex > 0)
        {
            source = source.Where(o => o.Age.Equals(age.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        if (!peopleX.Text.Trim().None() && peopleX.SelectedIndex > 0)
        {
            source = source.Where(o => o.UserName.Equals(peopleX.Text.Trim(), StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        source = source.Where(o => o.Amount > 0).ToList();
        viewx.DataSource = source.OrderByDescending(o => o.Time).ToList();
        //___total.Value = source.Sum(o => o.ReturnAmount).ToAmount(Depot.Featured(DepotType.小数数量库)) + "@@@" + source.Sum(o => o.PriceSet * o.ReturnAmount).ToMoney();
        if (useTypeX.SelectedIndex != 2)
        {
            viewx.Visible = pagerx.Visible = false;
        }
        else
        {
            viewx.Visible = pagerx.Visible = true;
            pagerx.Visible = source.Count > pagerx.PageSize;
        }
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
        viewx.Rebind();
    }

    protected void pager_PageIndexChanged(object sender, Telerik.Web.UI.RadDataPagerPageIndexChangeEventArgs e)
    {
        view.Rebind();
        viewx.Rebind();
    }

    protected void pagerx_PageIndexChanged(object sender, Telerik.Web.UI.RadDataPagerPageIndexChangeEventArgs e)
    {
        viewx.Rebind();
        viewx.Rebind();
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
        viewx.Rebind();
    }

    protected string GC(Guid id, int level)
    {
        return DataContext.ToCatalog(id, level).Single();
    }

    protected DateTime GT(Guid id)
    {
        return DataContext.DepotUse.Single(o => o.Id == id).Time;
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/UseEdit?DepotId={0}&UseXId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }
}
