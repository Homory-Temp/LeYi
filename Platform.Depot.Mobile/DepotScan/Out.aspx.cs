﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_Out : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            time.SelectedDate = DateTime.Today;
            counter.Value = "0";
            plus.Visible = false;
            Detect();
            Reset();
        }
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Detect();
        view_obj.Rebind();
    }

    protected void people_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Detect();
        view_obj.Rebind();
    }

    protected void Detect()
    {
        var show = true;
        x1.Visible = x2.Visible = show;
        Reset();
    }

    protected void plus_ServerClick(object sender, EventArgs e)
    {
        counter.Value = ((int.Parse(counter.Value)) + 1).ToString();
        var toRem = view_obj.Items.Select(o => (o.FindControl("ObjectOut") as Control_ObjectOut)).Select(o => o.PeekValue()).ToList();
        x.Value = toRem.ToJson();
        view_obj.Rebind();
    }

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view_obj.DataSource = new int[(int.Parse(counter.Value))];
    }

    protected void do_out_ServerClick(object sender, EventArgs e)
    {
        DoOut();
        Response.Redirect("~/Depot/DepotHome.aspx?DepotId={0}".Formatted(Depot.Id));
    }

    protected void DoOut()
    {
        var x = people.Text.Trim();
        if (x.None())
        {
            NotifyError(ap, "请输入报废申请人");
            return;
        }
        var g = DataContext.DepotUser.ToList().FirstOrDefault(o => o.Name == x || o.Phone == x || o.Phone.Substring(5) == x);
        if (g == null)
        {
            NotifyError(ap, "请输入报废申请人");
            return;
        }
        var tn = time.SelectedDate.HasValue ? time.SelectedDate.Value : DateTime.Today;
        var list = new List<InMemoryOut>();
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectOut") as Control_ObjectOut;
            var @out = c.PeekValue();
            if (@out.ObjectId.HasValue)
            {
                list.Add(@out);
            }
        }
        DataContext.DepotActOut(Depot.Id, time.SelectedDate.HasValue ? time.SelectedDate.Value.Date : DateTime.Today, DepotUser.Id, g.Id, list);
    }

    protected void view_obj_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        var c = e.Item.FindControl("ObjectOut") as Control_ObjectOut;
        var list = x.Value.None() ? new List<InMemoryOut>() : x.Value.FromJson<List<InMemoryOut>>();
        if (list.Count < c.ItemIndex + 1)
        {
            c.LoadDefaults(new InMemoryOut { Ordinals = new List<int>() });
        }
        else
        {
            c.LoadDefaults(list[c.ItemIndex]);
        }
    }

    protected void Reset()
    {
        scan.Text = "";
        scan.Focus();
    }

    protected void scanAdd_ServerClick(object sender, EventArgs e)
    {
        var list = x.Value.None() ? new List<InMemoryUse>() : x.Value.FromJson<List<InMemoryUse>>();
        var code = scan.Text.Trim();
        if (code.StartsWith("O"))
        {
            var inx = DataContext.DepotInX.FirstOrDefault(o => o.Code == code);
            if (inx == null)
            {
                Reset();
                return;
            }
            var obj = inx.DepotObject;
            var isVirtual = Depot.Featured(DepotType.固定资产库);
            var catalogId = DataContext.DepotObjectCatalog.Single(o => o.ObjectId == obj.Id && o.IsVirtual == isVirtual && o.IsLeaf == true).CatalogId;
            InMemoryUse use = new InMemoryUse { Age = inx.Age, Amount = 1, CatalogId = catalogId, Note = "", ObjectId = obj.Id, Place = inx.Place, Ordinals = new List<int>() };
            list.Add(use);
        }
        else if (code.StartsWith("S"))
        {
            var inx = DataContext.DepotInX.FirstOrDefault(o => o.Code == code);
            if (inx == null)
            {
                Reset();
                return;
            }
            var obj = inx.DepotObject;
            var isVirtual = Depot.Featured(DepotType.固定资产库);
            var catalogId = DataContext.DepotObjectCatalog.Single(o => o.ObjectId == obj.Id && o.IsVirtual == isVirtual && o.IsLeaf == true).CatalogId;
            InMemoryUse use = new InMemoryUse { Age = inx.Age, Amount = 1, CatalogId = catalogId, Note = "", ObjectId = obj.Id, Place = inx.Place, Ordinals = new List<int>() };
            use.Ordinals.Add(inx.Ordinal);
            list.Add(use);
        }
        x.Value = list.ToJson();
        counter.Value = list.Count.ToString();
        view_obj.Rebind();
        Reset();
    }
}
