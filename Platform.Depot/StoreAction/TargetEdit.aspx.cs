using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_TargetEdit : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var tid = "TargetId".Query().GlobalId();
            var target = db.Value.StoreTarget.Single(o => o.Id == tid);
            day.SelectedDate = target.Time;
            source.Items.Clear();
            source.DataSource = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.采购来源).OrderBy(o => o.PinYin).ToList();
            source.DataBind();
            if (source.FindItemByText(target.OrderSource) == null)
            {
                source.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = target.OrderSource, Value = target.OrderSource });
            }
            source.FindItemByText(target.OrderSource).Selected = true;
            keep.DataSource = db.Value.Store_User.ToList();
            keep.DataBind();
            brokerage.DataSource = db.Value.Store_User.ToList();
            brokerage.DataBind();
            string sx;
            usage.Items.Clear();
            usage.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = "", Value = "" });
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
                usage.DataBind();
            }
            if (usage.FindItemByValue(target.UsageTarget) == null)
            {
                usage.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = target.UsageTarget, Value = target.UsageTarget });
            }
            usage.FindItemByValue(target.UsageTarget).Selected = true;
            sx = target.UsageTarget;
            receipt.Text = target.ReceiptNumber;
            content.Text = target.Content;
            toPay.Value = (double)target.ToPay;
            paid.Value = (double)target.Paid;
            if (target.BrokerageUserId.HasValue)
            {
                brokerage.SelectedIndex = brokerage.Items.FindItemIndexByValue(target.BrokerageUserId.Value.ToString());
            }
            if (target.KeepUserId.HasValue)
            {
                keep.SelectedIndex = keep.Items.FindItemIndexByValue(target.KeepUserId.Value.ToString());
            }
            @in.Visible = !target.In;
            ap.ResponseScripts.Add("t_day = '{0}'; t_source = '', t_usage = '{1}'; genNumber();".Formatted(target.Time.ToString("yyyyMMdd"), sx));
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (number.Text.Trim().Null() && number.EmptyMessage.Trim().Null())
        {
            Notify(ap, "请输入购置单号", "error");
            return;
        }
        Save();
        Response.Redirect("~/StoreQuery/Target?StoreId={0}".Formatted(StoreId));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreQuery/Target?StoreId={0}".Formatted(StoreId));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        if (number.Text.Trim().Null() && number.EmptyMessage.Trim().Null())
        {
            Notify(ap, "请输入购置单号", "error");
            return;
        }
        var id = Save();
        Response.Redirect("~/StoreAction/In?StoreId={0}&TargetId={1}".Formatted(StoreId, id));
    }

    protected Guid Save()
    {
        var tid = "TargetId".Query().GlobalId();
        var target = db.Value.StoreTarget.Single(o => o.Id == tid);
        var time = day.SelectedDate.HasValue ? day.SelectedDate.Value : DateTime.Today;
        target.Number = number.Text.Null() ? number.EmptyMessage : number.Text;
        target.ReceiptNumber = receipt.Text;
        target.OrderSource = source.Text;
        target.UsageTarget = usage.Text;
        target.Content = content.Text;
        target.ToPay = toPay.PeekValue(0.00M);
        target.Paid = paid.PeekValue(0.00M);
        target.BrokerageUserId = brokerage.SelectedIndex > 0 ? brokerage.SelectedValue.GlobalId() : (Guid?)null;
        target.KeepUserId = keep.SelectedIndex > 0 ? keep.SelectedValue.GlobalId() : (Guid?)null;
        target.Time = time;
        target.TimeNode = time.ToTimeNode();
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
        return target.Id;
    }
}
