using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_Use : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            time.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.DataSource = db.Value.Store_User.ToList();
            people.DataBind();
            usage.Items.Clear();
            usage.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "使用对象", Value = "", Selected = true });
            if (CurrentStore.State == StoreState.食品)
            {
                var s = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.ParentId == null && o.State < 2).OrderBy(o => o.Ordinal).ToList();
                usage.DataSource = s;
            }
            else
            {
                var s = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.使用对象).OrderBy(o => o.PinYin).ToList();
                usage.DataSource = s;
            }
            usage.DataBind();
            Detect();
        }
    }

    protected void usage_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Detect();
        x.Value = "";
        view_obj.Rebind();
    }

    protected void people_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Detect();
        x.Value = "";
        view_obj.Rebind();
    }

    protected void Detect()
    {
        var show = true;
        if (people.SelectedValue.Null())
        {
            show = false;
        }
        else
        {
            if (CurrentStore.State == StoreState.食品)
            {
                if (usage.SelectedIndex < 1)
                {
                    show = false;
                }
            }
        }
        x1.Visible = x2.Visible = show;
        counter.Value = show ? "1" : "0";
    }

    protected void plus_ServerClick(object sender, EventArgs e)
    {
        counter.Value = ((int.Parse(counter.Value)) + 1).ToString();
        var toRem = view_obj.Items.Select(o => (o.FindControl("ObjectUseBody") as Control_ObjectUseBody)).Select(o => o.PeekValue()).ToList();
        x.Value = toRem.ToJson();
        view_obj.Rebind();
    }

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        view_obj.DataSource = new int[(int.Parse(counter.Value))];
    }

    protected void do_use_ServerClick(object sender, EventArgs e)
    {
        var gid = DoUse();
        Response.Redirect("~/StoreQuery/UsePrint?StoreId={0}&UseId={1}".Formatted(StoreId, gid));
    }

    protected Guid DoUse()
    {
        var gid = db.Value.GlobalId();
        var list = new List<CachedUse>();
        var tn = (time.SelectedDate.HasValue ? time.SelectedDate.Value : DateTime.Today).ToTimeNode();
        for (var i = 0; i < view_obj.Items.Count; i++)
        {
            var c = view_obj.Items[i].FindControl("ObjectUseBody") as Control_ObjectUseBody;
            var use = c.PeekValue();
            decimal amount = use.Amount.HasValue ? use.Amount.Value : 0M;
            if (use.ObjectId.HasValue && amount > 0M && !use.Type.Null())
            {
                var result = db.Value.ActionConsumeExt(use, people.SelectedValue.GlobalId(), use.Note, tn.ToTime(), CurrentUser, "");
                if (result.Amount.Value > 0)
                {
                    list.Add(result);
                }
            }
        }
        if (list.Count > 0)
        {
            var used = new StoreUsed
            {
                Id = gid,
                TimeNode = tn,
                Time = time.SelectedDate.HasValue ? time.SelectedDate.Value : DateTime.Today,
                Amount = list.Sum(o => o.Amount.Value),
                Money = list.Sum(o => o.Money),
                Content = list.ToJson(),
                PeopleId = people.SelectedValue.GlobalId()
            };
            db.Value.StoreUsed.Add(used);
            db.Value.SaveChanges();
        }
        return gid;
    }

    protected void view_obj_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        var c = e.Item.FindControl("ObjectUseBody") as Control_ObjectUseBody;
        var list = x.Value.Null() ? new List<CachedUse>() : x.Value.FromJson<List<CachedUse>>();
        if (list.Count < c.ItemIndex + 1)
        {
            c.LoadDefaults(new CachedUse { UserTarget = usage.SelectedItem.Text });
        }
        else
        {
            c.LoadDefaults(list[c.ItemIndex]);
        }
    }
}
