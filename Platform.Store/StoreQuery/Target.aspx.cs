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
            periodx.SelectedDate = DateTime.Today;
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

    protected bool CanDelete(Store_Target t)
    {
        var tar = db.Value.StoreTarget.Single(o => o.Id == t.Id);
        return tar.StoreIn.Count == 0;
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
        var list = new List<Store_Target>();
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
        ___total.Value = list.Sum(o => o.实付金额).ToMoney();
        //pager.Visible = list.Count > pager.PageSize;
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreAction/TargetEdit?StoreId={0}&TargetId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreAction/In?StoreId={0}&TargetId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void done_ServerClick(object sender, EventArgs e)
    {
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var target = db.Value.StoreTarget.Single(o => o.Id == id);
        target.In = true;
        db.Value.SaveChanges();
        Response.Redirect("~/StoreQuery/TargetPrint?StoreId={0}&TargetId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"].GlobalId()));
    }

    protected void redo_ServerClick(object sender, EventArgs e)
    {
        var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var target = db.Value.StoreTarget.Single(o => o.Id == id);
        target.In = false;
        db.Value.SaveChanges();
        view.Rebind();
    }

    protected void print_ServerClick(object sender, EventArgs e)
    {
        var url = "../StoreQuery/TargetPrint?StoreId={0}&TargetId={1}".Formatted(StoreId, (sender as HtmlInputButton).Attributes["match"].GlobalId());
        ap.ResponseScripts.Add("window.open('{0}', '_blank');".Formatted(url));
    }

    protected void delx_ServerClick(object sender, EventArgs e)
    {
        try
        {
            var id = (sender as HtmlInputButton).Attributes["match"].GlobalId();
            var tar = db.Value.StoreTarget.Single(o => o.Id == id);
            db.Value.StoreTarget.Remove(tar);
            db.Value.SaveChanges();
            view.Rebind();
            Notify(ap, "购置单删除成功", "success");
        }
        catch
        { }
    }
}
