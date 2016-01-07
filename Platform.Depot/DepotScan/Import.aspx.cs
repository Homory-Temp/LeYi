using Aspose.Cells;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Import : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "BatchId".Query().GlobalId();
            var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
            var checks = new List<InMemoryCheck>();
            foreach (var item in items)
            {
                checks.AddRange(item.CodeJson.FromJson<List<InMemoryCheck>>());
            }
            h.Value = checks.ToJson();
        }
    }

    protected void im_up_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        var id = Guid.NewGuid().ToString().ToUpper();
        var name = Server.MapPath(string.Format("~/Common/物资/临时/{0}.txt", id));
        file.Value = name;
        e.File.SaveAs(name, true);
        var stream = File.OpenText(name);
        try
        {
            r.InnerText = stream.ReadToEnd();
        }
        finally
        {
            try
            {
                stream.Close();
            }
            catch { }
        }
    }

    protected void im_do_Click(object sender, EventArgs e)
    {
    }

    protected void im_ok_Click(object sender, EventArgs e)
    {
        var codes = r.InnerText.Split(new[] { '.', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        codes.ForEach(o => o = o.Trim().ToUpper());
        var x = h.Value.None() ? new List<InMemoryCheck>() : h.Value.FromJson<List<InMemoryCheck>>();
        foreach (var code in codes)
        {
            if (code.Length != 12)
                continue;
            try
            {
                x.SingleOrDefault(o => o.Code == code).In = true;
                h.Value = x.ToJson();
                var id = "BatchId".Query().GlobalId();
                var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
                foreach (var item in items)
                {
                    var obj = item.CodeJson.FromJson<List<InMemoryCheck>>();
                    if (obj.Count(o => o.Code == code) > 0)
                    {
                        obj.First(o => o.Code == code).In = true;
                    }
                    item.CodeJson = obj.ToJson();
                    break;
                }
            }
            catch
            { }
        }
        DataContext.SaveChanges();
        Response.Redirect("~/DepotScan/CheckResult?DepotId={0}&BatchId={1}".Formatted(Depot.Id, "BatchId".Query()));
    }
}
