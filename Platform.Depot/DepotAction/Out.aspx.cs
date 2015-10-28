using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Out : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            time.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "报废申请人", Value = "", Selected = true });
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            age.Items.Clear();
            age.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "年龄段", Value = "", Selected = true });
            age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            age.DataBind();
            counter.Value = "1";
            if (!"ObjectId".Query().None())
            {
                var objId = "ObjectId".Query().GlobalId();
                var obj = DataContext.DepotObject.Single(o => o.Id == objId);
                var isVirtual = Depot.Featured(DepotType.固定资产库);
                var catalogId = DataContext.DepotObjectCatalog.Single(o => o.ObjectId == objId && o.IsLeaf == true && o.IsVirtual == isVirtual).CatalogId;
                var list = new List<InMemoryIn>();
                list.Add(new InMemoryIn { Time = time.SelectedDate.HasValue ? time.SelectedDate.Value.Date : DateTime.Today, CatalogId = catalogId, ObjectId = objId });
                x.Value = list.ToJson();
                x1.Visible = x2.Visible= false;
                plus.Visible = false;
                back.Visible = true;
            }
            else
            {
                plus.Visible = true;
                back.Visible = false;
            }
            Detect();
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
        }
        else
        {
            show = true;
        }
        x1.Visible = x2.Visible = show;
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
        Response.Redirect("~/DepotQuery/Out?DepotId={0}".Formatted(Depot.Id));
    }

    protected void DoOut()
    {
        if (people.SelectedValue == null)
        {
            NotifyError(ap, "请选择报废申请人");
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
        DataContext.DepotActOut(Depot.Id, time.SelectedDate.HasValue ? time.SelectedDate.Value.Date : DateTime.Today, DepotUser.Id, people.SelectedValue.GlobalId(), list);
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

    protected void back_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Object?DepotId={0}".Formatted(Depot.Id));
    }
}
