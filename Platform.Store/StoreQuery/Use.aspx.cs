using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_Use : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = db.Value.Store_Use.Where(o => o.StoreId == StoreId).Select(o => o.OperationUserId).ToList().Join(db.Value.User, o => o, o => o.Id, (o, u) => u).Distinct().ToList();
            people.DataBind();
            peopleX.Items.Clear();
            peopleX.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "借领人", Value = "0", Selected = true });
            peopleX.DataSource = db.Value.Store_Use.Where(o => o.StoreId == StoreId).Select(o => o.UserId).ToList().Join(db.Value.User, o => o, o => o.Id, (o, u) => u).Distinct().ToList();
            peopleX.DataBind();
            usage.Items.Clear();
            usage.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "使用对象", Value = "", Selected = true });
            if (CurrentStore.State == StoreState.食品)
            {
                var s = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.ParentId == null && o.State < 2).OrderBy(o => o.Ordinal).ToList();
                usage.DataSource = s;
            }
            else
            {
                var s = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.使用对象).OrderBy(o => o.PinYin).ToList();
                usage.DataSource = s;
            }
            usage.DataBind();
        }
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        var start = (new DateTime(time.Year, time.Month, 1).AddDays(-1)).ToTimeNode();
        var end = (new DateTime(time.Year, time.Month, 1).AddMonths(1)).ToTimeNode();
        var list = db.Value.Store_Use.Where(o => o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end).OrderByDescending(o => o.TimeNode).ToList();
        if (usage.SelectedIndex > 0)
            list = list.Where(o => o.UsageTarget == usage.SelectedItem.Text).ToList();
        if (people.SelectedIndex > 0)
            list = list.Where(o => o.Operator == people.SelectedItem.Text).ToList();
        if (peopleX.SelectedIndex > 0)
            list = list.Where(o => o.User == peopleX.SelectedItem.Text).ToList();
        view.DataSource = list;
        pager.Visible = list.Count > pager.PageSize;
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreAction/UseEdit?StoreId={0}&UseId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void print_ServerClick(object sender, EventArgs e)
    {
        var url = "../StoreQuery/UsePrint?StoreId={0}&UseId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"].GlobalId());
        ap.ResponseScripts.Add("window.open('{0}', '_blank');".Formatted(url));
    }
}
