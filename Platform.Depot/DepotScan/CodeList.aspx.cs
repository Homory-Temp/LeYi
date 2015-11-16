using Models;
using System;
using System.Collections.Generic;
using System.IO;
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

    private static readonly char[] InvalidFileNameChars = new[] { '"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f', ':', '*', '?', '\\', '/' };

    public static string CleanInvalidFileName(string fileName)
    {
        fileName = fileName + "";
        fileName = InvalidFileNameChars.Aggregate(fileName, (current, c) => current.Replace(c + "", ""));
        if (fileName.Length > 1)
            if (fileName[0] == '.')
                fileName = "dot" + fileName.TrimStart('.');
        return fileName;
    }

    protected void down_ServerClick(object sender, EventArgs e)
    {
        var now = Server.MapPath("../Common/物资/条码/打包/{0}.zip".Formatted((sender as HtmlInputButton).Attributes["match"]));
        var name = CleanInvalidFileName((sender as HtmlInputButton).Attributes["matchx"]);
        name += "_" + DateTime.UtcNow.Ticks.ToString();
        var next = Server.MapPath("../Common/物资/条码/打包/{0}.zip".Formatted(name));
        var fi = new FileInfo(now);
        fi.CopyTo(next, true);
        var script = "window.open('../Common/物资/条码/打包/{0}.zip','_blank');".Formatted(name);
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
