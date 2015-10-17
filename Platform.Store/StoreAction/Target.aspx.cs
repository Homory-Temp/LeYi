using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_Target : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            day.SelectedDate = DateTime.Today;
            source.DataSource = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.采购来源).OrderBy(o => o.PinYin).ToList();
            source.DataBind();
            keep.DataSource = db.Value.Store_User.ToList();
            keep.DataBind();
            brokerage.DataSource = db.Value.Store_User.ToList();
            brokerage.DataBind();
            string sx;
            if (CurrentStore.State == StoreState.食品)
            {
                usage.AllowCustomText = false;
                usage.Filter = Telerik.Web.UI.RadComboBoxFilter.None;
                usage.Items.Remove(usage.Items[0]);
                var s = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.ParentId == null && o.State < 2).OrderBy(o => o.Ordinal).ToList();
                usage.DataSource = s;
                sx = s.Count > 0 ? s.First().Name : "";
                usage.DataBind();
            }
            else
            {
                var s = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.使用对象).OrderBy(o => o.PinYin).ToList();
                usage.DataSource = s;
                sx = s.Count > 0 ? s.First().Name : "";
                usage.DataBind();
            }
            if (usage.Items.Count > 0)
                usage.SelectedIndex = 0;
            ap.ResponseScripts.Add("t_day = '{0}'; t_source = '', t_usage = '{1}'; genNumber();".Formatted(DateTime.Today.ToString("yyyyMMdd"), sx));
        }
    }

    protected void goon_ServerClick(object sender, EventArgs e)
    {
        Save();
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Save();
        Response.Redirect("~/StoreHome/Home?StoreId={0}".Formatted(StoreId));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreHome/Home?StoreId={0}".Formatted(StoreId));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        var id = Save();
        Response.Redirect("~/StoreAction/In?StoreId={0}&TargetId={1}".Formatted(StoreId, id));
    }

    protected Guid Save()
    {
        var id = db.Value.GlobalId();
        var time = day.SelectedDate.HasValue ? day.SelectedDate.Value : DateTime.Today;
        var target = new StoreTarget
        {
            Id = id,
            StoreId = StoreId,
            Number = number.Text.Null() ? number.EmptyMessage : number.Text,
            ReceiptNumber = receipt.Text,
            OrderSource = source.Text,
            UsageTarget = usage.Text,
            Content = content.Text,
            ToPay = toPay.PeekValue(0.00M),
            Paid = paid.PeekValue(0.00M),
            AdjustedMoney = null,
            BrokerageUserId = brokerage.SelectedIndex > 0 ? brokerage.SelectedValue.GlobalId() : (Guid?)null,
            KeepUserId = keep.SelectedIndex > 0 ? keep.SelectedValue.GlobalId() : (Guid?)null,
            In = false,
            Time = time,
            TimeNode = time.ToTimeNode(),
            OperationUserId = CurrentUser,
            OperationTime = DateTime.Now,
            State = 1
        };
        db.Value.StoreTarget.Add(target);
        if (db.Value.StoreDictionary.Count(o => o.StoreId == StoreId && o.Type == DictionaryType.采购来源 && o.Name == source.Text) == 0)
        {
            var dictionary = new StoreDictionary
            {
                StoreId = StoreId,
                Type = DictionaryType.采购来源,
                Name = source.Text,
                PinYin = db.Value.ToPinYin(source.Text).Single()
            };
            db.Value.StoreDictionary.Add(dictionary);
        }
        if (CurrentStore.State != StoreState.食品)
        {
            if (db.Value.StoreDictionary.Count(o => o.StoreId == StoreId && o.Type == DictionaryType.使用对象 && o.Name == source.Text) == 0)
            {
                var dictionary = new StoreDictionary
                {
                    StoreId = StoreId,
                    Type = DictionaryType.使用对象,
                    Name = source.Text,
                    PinYin = db.Value.ToPinYin(source.Text).Single()
                };
                db.Value.StoreDictionary.Add(dictionary);
            }
        }
        db.Value.SaveChanges();
        return id;
    }
}
