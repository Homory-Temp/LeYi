using Models;
using System;
using System.Collections.Generic;
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
            var s = DataContext.DepotCatalog.Where(o => o.DepotId == Depot.Id && o.State < State.停用 && o.ParentId == null).OrderBy(o => o.Ordinal).ToList();
            combo.DataSource = s;
            combo.DataBind();
            if (s.Count > 0)
            {
                combo.SelectedIndex = 0;
                var v = combo.SelectedValue.GlobalId();
                var d = DataContext.DepotPeriod.SingleOrDefault(o => o.CatalogId == v);
                if (d == null)
                {
                    DataContext.DepotPeriod.Add(new DepotPeriod { CatalogId = v, DepotId = Depot.Id, Users = (new List<Guid>()).ToJson(), Time = 0 });
                    DataContext.SaveChanges();
                    day.Value = 0;
                    view.Rebind();
                }
                else
                {
                    day.Value = d.Time;
                    view.Rebind();
                }
            }
            else
            {
                day.Enabled = false;
                search.Enabled = false;
            }
        }
    }

    protected void search_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        search.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList().Where(o => o.Name.Contains(e.FilterString) || o.PinYin.ToLower().Contains(e.FilterString.ToLower())).ToList();
        search.DataBind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (combo.SelectedIndex > -1)
        {
            var v = combo.SelectedValue.GlobalId();
            var d = DataContext.DepotPeriod.SingleOrDefault(o => o.CatalogId == v);
            if (d == null)
            {
                view.DataSource = null;
                pager.Visible = false;
                return;
            }
            var users = d.Users.FromJson<List<Guid>>();
            var source = users.Join(DataContext.DepotUser, o => o, o => o.Id, (a, b) => b).ToList();
            view.DataSource = source;
            pager.Visible = source.Count > pager.PageSize;
        }
        else
        {
            view.DataSource = null;
            pager.Visible = false;
        }
    }

    protected void search_Search(object sender, Telerik.Web.UI.SearchBoxEventArgs e)
    {
        if (e.Value.None())
            return;
        if (combo.SelectedIndex > -1)
        {
            var v = combo.SelectedValue.GlobalId();
            var d = DataContext.DepotPeriod.SingleOrDefault(o => o.CatalogId == v);
            if (d == null)
                return;
            var users = d.Users.FromJson<List<Guid>>();
            if (!users.Contains(e.Value.GlobalId()))
            {
                users.Add(e.Value.GlobalId());
            }
            d.Users = users.ToJson();
            DataContext.SaveChanges();
        }
        search.Text = string.Empty;
        view.Rebind();
        NotifyOK(ap, "成功免除选定用户的借还时限");
    }

    protected void save_ServerClick(object sender, EventArgs e)
    {
        if (combo.SelectedIndex > -1)
        {
            var v = combo.SelectedValue.GlobalId();
            var d = DataContext.DepotPeriod.SingleOrDefault(o => o.CatalogId == v);
            if (d == null)
            {
                return;
            }
            d.Time = day.PeekValue(0);
            DataContext.SaveChanges();
        }
        NotifyOK(ap, "时限设置成功");
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        var content = (sender as HtmlInputButton).Attributes["match"];
        if (!content.None())
        {

            if (combo.SelectedIndex > -1)
            {
                var v = combo.SelectedValue.GlobalId();
                var d = DataContext.DepotPeriod.SingleOrDefault(o => o.CatalogId == v);
                if (d == null)
                    return;
                var users = d.Users.FromJson<List<Guid>>();
                if (users.Contains(content.GlobalId()))
                {
                    users.Remove(content.GlobalId());
                }
                d.Users = users.ToJson();
                DataContext.SaveChanges();
            }
            view.Rebind();
            NotifyOK(ap, "选定用户借还超时受限");
        }
    }

    protected void combo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        var v = combo.SelectedValue.GlobalId();
        var d = DataContext.DepotPeriod.SingleOrDefault(o => o.CatalogId == v);
        if (d == null)
        {
            DataContext.DepotPeriod.Add(new DepotPeriod { CatalogId = v, DepotId = Depot.Id, Users = (new List<Guid>()).ToJson(), Time = 0 });
            DataContext.SaveChanges();
            day.Value = 0;
            view.Rebind();
        }
        else
        {
            day.Value = d.Time;
            view.Rebind();
        }
    }
}
