﻿using Models;
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
        }
    }

    protected void im_up_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        string namex = e.File.GetName();
        Session["FileName"] = namex;
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
        IMPRT();
    }



    protected void im_do_Click(object sender, EventArgs e)
    {
    }

    protected void IMPRT()
    {
        var codes = r.InnerText.Split(new[] { '.', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        codes.ForEach(o => o = o.Trim().ToUpper());
        foreach (var code in codes)
        {
            try
            {
                var id = "BatchId".Query().GlobalId();
                var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
                foreach (var item in items)
                {
                    var obj = item.CodeJson.FromJson<List<InMemoryCheck>>();
                    if (obj.Count(o => o.Code == code) > 0)
                    {
                        obj.First(o => o.Code == code).In = true;
                        item.CodeJson = obj.ToJson();
                        DataContext.DepotCheck.First(o => o.State == 1 && o.BatchId == id && o.BatchOrdinal == item.BatchOrdinal);
                        break;
                    }
                }
            }
            catch
            { }
        }
        DataContext.SaveChanges();
        Response.Redirect("~/DepotScan/CheckResult.aspx?DepotId={0}&BatchId={1}".Formatted(Depot.Id, "BatchId".Query()));
    }

    protected void im_ok_Click(object sender, EventArgs e)
    {
    }
}
