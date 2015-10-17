using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_Target : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "操作人", Value = "0", Selected = true });
            people.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId).Select(o => o.OperationUserId).ToList().Join(db.Value.User, o => o, o => o.Id, (o, u) => u).Distinct().ToList();
            people.DataBind();
            source.Items.Clear();
            source.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "采购来源", Value = "0", Selected = true });
            source.DataSource = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId).Select(o => o.采购来源).Distinct().ToList();
            source.DataBind();
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
        List<Store_Target> list = new List<Store_Target>();
        switch (combo.SelectedValue)
        {
            case "0":
                {
                    list = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end && o.In == false).OrderByDescending(o => o.TimeNode).ToList();
                    break;
                }
            case "1":
                {
                    list = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end && o.In == true).OrderByDescending(o => o.TimeNode).ToList();
                    break;
                }
            case "2":
                {
                    list = db.Value.Store_Target.Where(o => o.State < 2 && o.StoreId == StoreId && o.TimeNode > start && o.TimeNode < end).OrderByDescending(o => o.TimeNode).ToList();
                    break;
                }
        }
        if (source.SelectedIndex > 0)
            list = list.Where(o => o.采购来源 == source.SelectedItem.Text).ToList();
        if (usage.SelectedIndex > 0)
            list = list.Where(o => o.使用对象 == usage.SelectedItem.Text).ToList();
        if (people.SelectedIndex > 0)
            list = list.Where(o => o.操作人 == people.SelectedItem.Text).ToList();
        view.DataSource = list;
        pager.Visible = list.Count > pager.PageSize;
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {

    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreAction/In?StoreId={0}&TargetId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {

    }

    protected void done_ServerClick(object sender, EventArgs e)
    {

    }
}
