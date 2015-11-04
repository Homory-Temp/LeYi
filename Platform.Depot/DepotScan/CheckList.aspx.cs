using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_CheckList : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public class CheckListItem
    {
        public string Name { get; set; }
        public Guid BatchId { get; set; }
        public DateTime Time { get; set; }
        public string ToDo { get; set; }
        public string Done { get; set; }
        public string Total { get; set; }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = DataContext.DepotCheck.Where(o => o.DepotId == Depot.Id && o.State == 1).OrderByDescending(o => o.Time).ToList();
        var items = new List<CheckListItem>();
        foreach (var group in source.GroupBy(o => o.BatchId))
        {
            var checks = new List<InMemoryCheck>();
            foreach (var item in group)
            {
                checks.AddRange(item.CodeJson.FromJson<List<InMemoryCheck>>());
            }
            items.Add(new CheckListItem { Name = group.First().Name, BatchId = group.First().BatchId, Time = group.First().Time, Total = checks.Count().ToString("F0"), Done = checks.Count(o=>o.In==true).ToString("F0"), ToDo = checks.Count(o => o.In == false).ToString("F0") });
        }
        view.DataSource = items;
    }

    protected void del_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        foreach (var item in DataContext.DepotCheck.Where(o => o.BatchId == bid).ToList())
        {
            item.State = 2;
        }
        DataContext.SaveChanges();
        view.Rebind();
    }

    protected void view_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        Response.Redirect("~/DepotScan/CheckResult?DepotId={0}&BatchId={1}".Formatted(Depot.Id, bid));
    }

    protected void start_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        Response.Redirect("~/DepotScan/CheckDo?DepotId={0}&BatchId={1}".Formatted(Depot.Id, bid));
    }

    protected void copy_ServerClick(object sender, EventArgs e)
    {
        var bid = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var bidx = DataContext.GlobalId();
        foreach (var item in DataContext.DepotCheck.Where(o => o.BatchId == bid).ToList())
        {
            var jsons = item.CodeJson.FromJson<List<InMemoryCheck>>();
            jsons.ForEach(o => { o.In = false; });
            var itemx = new DepotCheck { BatchId = bidx, BatchOrdinal = item.BatchOrdinal, CodeJson = jsons.ToJson(), DepotId = item.DepotId, Name = item.Name, State = 1, Time = DateTime.Now };
            DataContext.DepotCheck.Add(itemx);
        }
        DataContext.SaveChanges();
        view.Rebind();
    }
}
