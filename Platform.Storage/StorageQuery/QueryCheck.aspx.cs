using Models;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class QueryCheck : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitQueryIn();
        }
    }

    protected void InitQueryIn()
    {
        day_start.SelectedDate = DateTime.Today.AddMonths(-1);
        day_end.SelectedDate = DateTime.Today;
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!File.Exists(Server.MapPath("~/StorageCheck/Check.table")))
        {
            view.DataSource = null;
            return;
        }
        var n = name.Text.ToLower();
        var ds = int.Parse(day_start.SelectedDate.Value.AddDays(-1).ToString("yyyyMMdd"));
        var de = int.Parse(day_end.SelectedDate.Value.AddDays(1).ToString("yyyyMMdd"));
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageCheck/Check.table"));
        try
        {
            var table = engine.OpenXTablePortable<Guid, CheckTable>("CheckList");
            var source = table.Where(o => o.Value.TimeNode > ds && o.Value.TimeNode < de && o.Value.Name.ToLower().Contains(n)).Select(o => o.Value).OrderByDescending(o => o.Time).ToList();
            view.DataSource = source;
        }
        finally
        {
            engine.Close();
        }
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected void query_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        view.Rebind();
    }

    protected void view_ItemDataBound(object sender, GridItemEventArgs e)
    {
        var link = e.Item.FindControl("c");
        if (link != null)
        {
            var c = link as HyperLink;
            c.NavigateUrl = "~/StorageQuery/QueryCheckContent?Id={0}&StorageId={1}".Formatted((e.Item as GridEditableItem).GetDataKeyValue("Id"), StorageId);
            //var tip = e.Item.FindControl("tip") as RadWindow;
            //tip.OpenerElementID = c.ClientID;
            //tip.NavigateUrl = "~/StorageQuery/QueryCheckContent?Id={0}&StorageId={1}".Formatted((e.Item as GridEditableItem).GetDataKeyValue("Id"), StorageId);
        }
    }
}
