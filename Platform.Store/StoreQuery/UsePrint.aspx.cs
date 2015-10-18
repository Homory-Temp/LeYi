using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_UsePrint : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "UseId".Query().GlobalId();
            var use = db.Value.StoreUsed.Single(o => o.Id == id);
            campus.InnerText = db.Value.Department.Single(o => o.Id == CurrentCampus).Name;
            var s = use.Content.FromJson<List<CachedUse>>();
            total.Value = s.Select(o => Math.Round(o.Money, 2, MidpointRounding.AwayFromZero)).ToList().Sum().ToMoney();
            people.Value = db.Value.GetUserName(use.PeopleId);
            time.InnerText = use.TimeNode.FromTimeNode();
        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var id = "UseId".Query().GlobalId();
        var use = db.Value.StoreUsed.Single(o => o.Id == id);
        view_record.DataSource = use.Content.FromJson<List<CachedUse>>();
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../StoreQuery/Use?StoreId={0}".Formatted(StoreId));
    }
}
