﻿using Models;
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
            periodx.SelectedDate = DateTime.Today.AddMonths(-1);
            period.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList().Where(o => o.Code != "*Homory:Null*").ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            age.Items.Clear();
            age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            age.DataBind();
            age.Visible = Depot.Featured(DepotType.幼儿园);
            place.Items.Clear();
            place.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "存放地", Value = "", Selected = true });
            place.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.存放地).ToList();
            place.DataBind();
            toSearch.Focus();
        }
    }

    protected bool CanRedo(Guid inId)
    {
        var @in = DataContext.DepotIn.Single(o => o.Id == inId);
        foreach(var inx in @in.DepotInX)
        {
            if (inx.DepotUseX.Count > 0)
                return false;
        }
        return true;
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
        var start = timex.AddMilliseconds(-1);
        var end = time.AddDays(1);
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(DataContext.DepotInXRecord.Where(o => o.Time > start && o.Time < end && o.Amount > 0), o => o, o => o.CatalogId, (a, b) => b).ToList().OrderByDescending(o => o.Time).ThenBy(o => o.OrderName).ToList();
        if (age.SelectedIndex > 0 && !age.Text.None())
            source = source.Where(o => o.Age == age.Text).ToList();
        if (place.SelectedIndex > 0 && !place.Text.None())
            source = source.Where(o => o.Place == place.Text).ToList();
        if (people.SelectedIndex > 0)
            source = source.Where(o => o.Operator == people.SelectedItem.Text).ToList();
        if (!"OrderId".Query().None())
        {
            var oid = "OrderId".Query().GlobalId();
            source = source.Where(o => o.OrderId == oid).ToList();
        }
        if (!toSearch.Text.Trim().None())
        {
            var a_source = DataContext.DepotObjectLoad(Depot.Id, null);
            if (!toSearch.Text.None())
            {
                var glist = a_source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.Code.ToLower() == toSearch.Text.Trim().ToLower()).Select(o => o.Id).ToList();
                source = glist.Join(source, o => o, o => o.ObjectId, (a, b) => b).ToList();
            }
        }
        view.DataSource = source.OrderByDescending(o => o.Time).ThenByDescending(o => o.InId).ToList();
        //___total.Value = source.Sum(o => o.Amount).ToAmount(Depot.Featured(DepotType.小数数量库)) + "@@@" + source.Sum(o => o.Total).ToMoney();
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

    protected void redo_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/InRedo?DepotId={0}&InId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }
}
