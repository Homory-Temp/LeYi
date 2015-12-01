using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotQuery_Out  : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            period.SelectedDate = DateTime.Today;
            periodx.SelectedDate = DateTime.Today.AddMonths(-1);
            peopleX.Items.Clear();
            peopleX.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "报废申请人", Value = "0", Selected = true });
            peopleX.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            peopleX.DataBind();
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
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

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
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
        var list = catalogs.Join(DataContext.DepotOutRecord.Where(o => o.Time > start && o.Time < end), o => o, o => o.CatalogId, (a, b) => b).ToList().OrderByDescending(o => o.State).ThenByDescending(o => o.Time).ThenBy(o => o.UserName).ToList();
        if (peopleX.SelectedIndex > 0)
        {
            var ou = peopleX.SelectedItem.Value.GlobalId();
            list = list.Where(o => o.UserId == ou).ToList();
        }
        if (outType.SelectedIndex > 0)
        {
            var x = int.Parse(outType.SelectedItem.Value);
            list = list.Where(o => o.State == x).ToList();
        }
        view.DataSource = list;
        pager.Visible = list.Count > pager.PageSize;
    }

    protected void go_out_ServerClick(object sender, EventArgs e)
    {
        var btn = (sender as HtmlInputButton);
        var id = int.Parse(btn.Attributes["match"]);
        var nc = btn.NamingContainer.FindControl("amount") as RadNumericTextBox;
        var amount = nc.PeekValue(-1);
        var dto = DataContext.DepotToOut.Single(o => o.Id == id);
        if (amount == 0)
        {
            dto.State = 1;
            dto.Amount = amount;
        }
        else if (amount > 0)
        {
            dto.State = 3;
            dto.Amount = amount;
        }
        DataContext.SaveChanges();
        DataContext.DoOut(id);
        view.Rebind();
    }

    protected void fs_ServerClick(object sender, EventArgs e)
    {
        var btn = (sender as HtmlInputButton);
        var url = "../DepotQuery/OutX?DepotId={0}&MainID={1}".Formatted(Depot.Id, btn.Attributes["match"]);
        Response.Redirect(url);
    }
}
