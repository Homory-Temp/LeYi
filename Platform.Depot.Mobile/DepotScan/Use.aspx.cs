using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotScan_Use : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            time.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "", Value = "", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            //age.Items.Clear();
            //age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            //age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            //age.DataBind();
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
        if (people.SelectedValue.None())
        {
            show = false;
            x1.Visible = x2.Visible = x3.Visible = false;
        }
        else
        {
            var uid = people.SelectedValue.GlobalId();
            var record = DataContext.DepotUseXRecord.Where(o => o.UserId == uid && o.ReturnedAmount < o.Amount && o.Type == 2).OrderBy(o => o.Time).FirstOrDefault();
            if (record == null)
                show = true;
            else
            {
                var period = Depot.DepotSetting.SingleOrDefault(o => o.Key == "PeriodTime");
                var time = period == null ? 0 : int.Parse(period.Value);
                if (time > 0 && record.Time.AddMonths(time) < DateTime.Now)
                {
                    var ids = Depot.DepotSetting.Where(o => o.Key == "PeriodUser").ToList().Select(o => Guid.Parse(o.Value)).ToList();
                    show = ids.Contains(uid);
                }
                else
                {
                    show = true;
                }
            }
            x1.Visible = x2.Visible = show;
            x3.Visible = !show;
        }
        Reset();
    }

    protected void plus_ServerClick(object sender, EventArgs e)
    {
        try
        {
            counter.Value = ((int.Parse(counter.Value)) + 1).ToString();
            var toRem = view_obj.Items.Select(o => (o.FindControl("ObjectUse") as Control_ObjectUse)).Select(o => o.PeekValue()).ToList();
            x.Value = toRem.ToJson();
            view_obj.Rebind();
        }
        catch
        {

        }
    }

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view_obj.DataSource = new int[(int.Parse(counter.Value))];
    }

    protected void do_use_ServerClick(object sender, EventArgs e)
    {
         var gid = DoUse();
        if (gid != Guid.Empty)
            Response.Redirect("~/Depot/DepotHome?DepotId={0}".Formatted(Depot.Id));
    }

    protected Guid DoUse()
    {
        if (people.SelectedValue == null)
        {
            NotifyError(ap, "请选择借领人");
            return Guid.Empty;
        }
        var tn = time.SelectedDate.HasValue ? time.SelectedDate.Value : DateTime.Today;
        var list = new List<InMemoryUse>();
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectUse") as Control_ObjectUse;
            var use = c.PeekValue();
            if (use.ObjectId.HasValue)
            {
                list.Add(use);
            }
        }
        return DataContext.DepotActUse(Depot.Id, time.SelectedDate.HasValue ? time.SelectedDate.Value.Date : DateTime.Today, DepotUser.Id, people.SelectedValue.GlobalId(), list);
    }

    protected void view_obj_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        try
        {
            var c = e.Item.FindControl("ObjectUse") as Control_ObjectUse;
            var list = x.Value.None() ? new List<InMemoryUse>() : x.Value.FromJson<List<InMemoryUse>>();
            if (list.Count < c.ItemIndex + 1)
            {
                c.LoadDefaults(new InMemoryUse { /*Age = age.Text,*/ Ordinals = new List<int>() });
            }
            else
            {
                c.LoadDefaults(list[c.ItemIndex]);
            }
        }
        catch
        { }
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
