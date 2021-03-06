﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_CheckDo : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Reset();
            if (!"Code".Query().None())
            {
                scan.Text = "Code".Query().Trim();
                scanFlow_ServerClick(null, null);
                var id = "BatchId".Query().GlobalId();
                var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
                var c = new List<InMemoryCheck>();
                foreach (var item in items)
                {
                    var obj = item.CodeJson.FromJson<List<InMemoryCheck>>();
                    c.AddRange(obj);
                }
                ____vx.Value = false.ToJson();
                name.InnerText = "总数：{0} 已盘：{1} 未盘：{2}".Formatted(c.Count, c.Count(o => o.In == true), c.Count(o => o.In == false));
            }
        }
    }

    protected void Reset()
    {
        scan.Text = "";
        scan.Focus();
    }

    protected void scanFlow_ServerClick(object sender, EventArgs e)
    {
        var code = scan.Text.Trim();
        h.Value = code;
        //var x = h.Value.None() ? new List<InMemoryCheck>() : h.Value.FromJson<List<InMemoryCheck>>();
        //x.SingleOrDefault(o => o.Code == code).In = true;
        //h.Value = x.ToJson();
        var id = "BatchId".Query().GlobalId();
        var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
        var no = true;
        foreach (var item in items)
        {
            var obj = item.CodeJson.FromJson<List<InMemoryCheck>>();
            if (obj.Count(o => o.Code == code) > 0)
            {
                obj.First(o => o.Code == code).In = true;
                no = false;
                item.CodeJson = obj.ToJson();
                DataContext.SaveChanges();
                break;
            }
        }
        ____vx.Value = no.ToJson();
        view.Rebind();
        Reset();
    }

    //protected List<InMemoryCheck> Codes
    //{
    //    get
    //    {
    //        return h.Value.None() ? new List<InMemoryCheck>() : h.Value.FromJson<List<InMemoryCheck>>();
    //    }
    //}

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var id = "BatchId".Query().GlobalId();
        var items = DataContext.DepotCheck.Where(o => o.State == 1 && o.BatchId == id).ToList();
        var checks = new List<InMemoryCheck>();
        var code = h.Value;
        foreach (var item in items)
        {
            var obj = item.CodeJson.FromJson<List<InMemoryCheck>>();
            if (obj.Count(o => o.Code == code) > 0)
            {
                checks.AddRange(item.CodeJson.FromJson<List<InMemoryCheck>>().Where(o => o.Code == code));
            }
        }
        view.DataSource = checks;
        //name.InnerText = "总数：{0} 已盘：{1} 未盘：{2}".Formatted(checks.Count, checks.Count(o => o.In == true), checks.Count(o => o.In == false));
        namex.InnerText = items[0].Name;
        //h.Value = checks.ToJson();
        var c = new List<InMemoryCheck>();
        foreach (var item in items)
        {
            var obj = item.CodeJson.FromJson<List<InMemoryCheck>>();
            c.AddRange(obj);
        }
        var no = ____vx.Value.None() ? false : ____vx.Value.FromJson<bool>();
        name.InnerText = no ? "您扫描的条码不在此次盘库任务内" : "总数：{0} 已盘：{1} 未盘：{2}".Formatted(c.Count, c.Count(o => o.In == true), c.Count(o => o.In == false));
    }

    protected void scanGo_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotScan/CheckResult.aspx?DepotId={0}&BatchId={1}".Formatted(Depot.Id, "BatchId".Query()));
    }
}
