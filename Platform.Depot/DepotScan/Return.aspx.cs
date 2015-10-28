using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotScan_Return : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            time.SelectedDate = DateTime.Today;
            counter.Value = "0";
            Detect();
            Reset();
        }
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Detect();
        view_obj.Rebind();
    }

    protected void Detect()
    {
        Reset();
    }

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view_obj.DataSource = new int[(int.Parse(counter.Value))];
        do_return.Visible = (int.Parse(counter.Value)) > 0;
    }

    protected string Gen(DepotUseXRecord r)
    {
        var inx = DataContext.DepotInX.Single(o => o.Id == r.InXId);
        return inx.Code;
    }

    protected void do_return_ServerClick(object sender, EventArgs e)
    {
        var list = new List<InMemoryReturn>();
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectReturn") as Control_ObjectReturn;
            var r = c.PeekValue();
            if (r.Amount.HasValue && r.Amount.Value > 0)
            {
                list.Add(r);
            }
        }
        DataContext.DepotActReturn(Depot.Id, time.SelectedDate.HasValue ? time.SelectedDate.Value.Date : DateTime.Today, DepotUser.Id, list);
        Response.Redirect("~/DepotQuery/Return?DepotId={0}".Formatted(Depot.Id));
    }

    protected void Reset()
    {
        scan.Text = "";
        scan.Focus();
    }

    protected void scanAdd_ServerClick(object sender, EventArgs e)
    {
        var list = x.Value.None() ? new List<InMemoryReturn>() : x.Value.FromJson<List<InMemoryReturn>>();
        var code = scan.Text.Trim();
        if (code.StartsWith("O"))
        {
            Reset();
            return;
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
            var usex = inx.DepotUseX.FirstOrDefault(o => o.ReturnedAmount == 0);
            if (usex == null)
                return;
            var @return = new InMemoryReturn { Amount = 1, Code = code, Note = "", OutAmount = null, UseX = usex.Id };
            list.Add(@return);
        }
        x.Value = list.ToJson();
        counter.Value = list.Count.ToString();
        view_obj.Rebind();
        Reset();
    }

    protected void view_obj_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        var c = e.Item.FindControl("ObjectReturn") as Control_ObjectReturn;
        var list = x.Value.None() ? new List<InMemoryReturn>() : x.Value.FromJson<List<InMemoryReturn>>();
        if (list.Count < c.ItemIndex + 1)
        {
            c.LoadDefaults(new InMemoryReturn { });
        }
        else
        {
            c.LoadDefaults(list[c.ItemIndex]);
        }
    }
}
