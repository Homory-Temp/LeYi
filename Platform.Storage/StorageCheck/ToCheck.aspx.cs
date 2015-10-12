using Models;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ToCheck : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void pkList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageCheck/Check.table"));
        var l = new List<ToCheckTable>();
        try
        {
            var table = engine.OpenXTablePortable<Guid, ToCheckTable>("ToCheckList");
            l = table.Where(o => o.Value.UserId == CurrentUser).OrderByDescending(o => o.Value.Time).Select(o => o.Value).ToList();
        }
        finally
        {
            engine.Close();
        }
        pkList.DataSource = l.OrderByDescending(o => o.Time).ToList();
    }

    protected void go_Click(object sender, EventArgs e)
    {
        var id = (sender as LinkButton).CommandArgument.GlobalId();
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageCheck/Check.table"));
        var v = new ToCheckTable();
        try
        {
            var table = engine.OpenXTablePortable<Guid, ToCheckTable>("ToCheckList");
            v = table[id];
        }
        finally
        {
            engine.Close();
        }
        Session["CheckList"] = v.List.ToJson();
        Response.Redirect("~/StorageCheck/CheckStart?StorageId={0}&Name={1}&Place={2}".Formatted(StorageId, Server.UrlEncode(v.Name), Server.UrlEncode(v.Place)));
    }
}
