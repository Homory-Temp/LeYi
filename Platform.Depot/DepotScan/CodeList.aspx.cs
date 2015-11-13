using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_CodeList : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public class CodeListItem
    {
        public string Name { get; set; }
        public Guid BatchId { get; set; }
        public DateTime Time { get; set; }
        public int State { get; set; }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = DataContext.DepotCode.Where(o => o.DepotId == Depot.Id && o.State < 3).OrderByDescending(o => o.Time).ToList();
        var codes = new List<CodeListItem>();
        foreach (var group in source.GroupBy(o => o.BatchId))
        {
            codes.Add(new CodeListItem { Name = group.First().Name, BatchId = group.First().BatchId, Time = group.First().Time, State = group.First().State });
        }
        view.DataSource = codes;
    }

    protected void down_ServerClick(object sender, EventArgs e)
    {
        var script = "window.open('../Common/物资/条码/打包/{0}.zip','_blank');".Formatted((sender as HtmlInputButton).Attributes["match"]);
        ap.ResponseScripts.Add(script);
    }

    protected void del_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        foreach (var item in DataContext.DepotCode.Where(o => o.BatchId == bid).ToList())
        {
            item.State = 3;
        }
        DataContext.SaveChanges();
        view.Rebind();
    }

    protected void ap_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        view.Rebind();
    }
}
