using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StoreHome_Warn : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

    protected void view_action_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var s = db.Value.StoreObject.Where(o => o.Amount < o.Low && o.Low > 0 && o.State < 2).ToList();
        if (usage.SelectedIndex>0)
        {
            s = s.Where(o => db.Value.GetCatalogPath(o.CatalogId).Single().StartsWith(usage.SelectedItem.Text)).ToList();
        }
        view_action.DataSource = s;
    }

    protected void view_query_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var s = db.Value.StoreObject.Where(o => o.Amount > o.High && o.High > 0 && o.State < 2).ToList();
        if (usage.SelectedIndex > 0)
        {
            s = s.Where(o => db.Value.GetCatalogPath(o.CatalogId).Single().StartsWith(usage.SelectedItem.Text)).ToList();
        }
        view_query.DataSource = s;
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        view_action.Rebind();
        view_query.Rebind();
    }
}
