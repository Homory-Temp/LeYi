using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotSetting_Period : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        search.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList();
        if (!IsPostBack)
        {
            var m = Depot.DepotSetting.SingleOrDefault(o => o.Key == "PeriodTime");
            month.Value = m == null ? 0 : int.Parse(m.Value);
        }
    }

    protected void search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        search.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList().Where(o => o.Name.Contains(e.FilterString) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
        search.DataBind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = Depot.DepotSetting.Where(o => o.Key == "PeriodUser").ToList().Select(o => Guid.Parse(o.Value)).ToList().Join(DataContext.DepotUser, o => o, o => o.Id, (a, b) => b).ToList();
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }

    protected void search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        if (e.Value.None())
            return;
        var ds = new DepotSetting
        {
            Id = DataContext.GlobalId(),
            DepotId = Depot.Id,
            Key = "PeriodUser",
            Value = e.Value
        };
        DataContext.DepotSetting.Add(ds);
        DataContext.SaveChanges();
        search.Text = string.Empty;
        view.Rebind();
        NotifyOK(ap, "成功取消选定用户的借还时限");
    }

    protected void save_ServerClick(object sender, EventArgs e)
    {
        var x = DataContext.DepotSetting.SingleOrDefault(o => o.DepotId == Depot.Id && o.Key == "PeriodTime");
        if (x == null)
        {
            var ds = new DepotSetting
            {
                Id = DataContext.GlobalId(),
                DepotId = Depot.Id,
                Key = "PeriodTime",
                Value = month.PeekValue(0).ToString()
            };
            DataContext.DepotSetting.Add(ds);
        }
        else
        {
            x.Value = month.PeekValue(0).ToString();
        }
        DataContext.SaveChanges();
        NotifyOK(ap, "时限设置成功");
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        var content = (sender as HtmlInputButton).Attributes["match"];
        if (!content.None())
        {
            var x = DataContext.DepotSetting.SingleOrDefault(o => o.DepotId == Depot.Id && o.Key == "PeriodUser" && o.Value == content);
            if(x!=null)
            {
                DataContext.DepotSetting.Remove(x);
                DataContext.SaveChanges();
            }
            view.Rebind();
            NotifyOK(ap, "选定用户借还超时受限");
        }
    }
}
