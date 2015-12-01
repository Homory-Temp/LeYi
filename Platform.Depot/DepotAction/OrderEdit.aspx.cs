using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_OrderEdit : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Depot.Featured(DepotType.固定资产库))
                @in.Visible = false;
            var orderId = "OrderId".Query().GlobalId();
            var order = DataContext.DepotOrder.Single(o => o.Id == orderId);
            fno.Text = order.MainID ?? string.Empty;
            day.SelectedDate = order.RecordTime;
            source.Items.Clear();
            source.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.购置来源).ToList();
            source.DataBind();
            if (source.FindItemByText(order.OrderSource) == null)
            {
                source.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = order.OrderSource, Value = order.OrderSource });
            }
            source.FindItemByText(order.OrderSource).Selected = true;
            usage.Items.Clear();
            usage.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.使用对象).ToList();
            usage.DataBind();
            if (usage.FindItemByText(order.UsageTarget) == null)
            {
                usage.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = order.UsageTarget, Value = order.UsageTarget });
            }
            usage.FindItemByText(order.UsageTarget).Selected = true;
            keep.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList();
            keep.DataBind();
            brokerage.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList();
            brokerage.DataBind();
            receipt.Text = order.Receipt;
            content.Text = order.Note;
            toPay.Value = (double)order.ToPay;
            paid.Value = (double)order.Paid;
            if (order.BrokerageId.HasValue)
            {
                brokerage.SelectedIndex = brokerage.Items.FindItemIndexByValue(order.BrokerageId.Value.ToString());
            }
            if (order.KeeperId.HasValue)
            {
                keep.SelectedIndex = keep.Items.FindItemIndexByValue(order.KeeperId.Value.ToString());
            }
            @in.Visible = !order.Done;
            ap.ResponseScripts.Add("t_day = '{0}'; t_source = '{1}', t_usage = '{2}'; genNumber();".Formatted(order.RecordTime.ToString("yyyyMMdd"), order.OrderSource, order.UsageTarget));
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (number.Text.Trim().None() && number.EmptyMessage.Trim().None())
        {
            NotifyError(ap, "请输入购置单号");
            return;
        }
        Save();
        Response.Redirect("~/DepotQuery/In?DepotId={0}".Formatted(Depot.Id));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/DepotHome?DepotId={0}".Formatted(Depot.Id));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        if (number.Text.Trim().None() && number.EmptyMessage.Trim().None())
        {
            NotifyError(ap, "请输入购置单号");
            return;
        }
        var id = Save();
        Response.Redirect("~/DepotAction/In?DepotId={0}&OrderId={1}".Formatted(Depot.Id, id));
    }

    protected Guid Save()
    {
        var orderId = "OrderId".Query().GlobalId();
        var order = DataContext.DepotOrder.Single(o => o.Id == orderId);
        var time = day.SelectedDate.HasValue ? day.SelectedDate.Value : DateTime.Today;
        order.Name = number.Text.None() ? number.EmptyMessage.Trim() : number.Text.Trim();
        order.Receipt = receipt.Text.Trim();
        order.OrderSource = source.Text.Trim();
        order.UsageTarget = usage.Text.Trim();
        order.Note = content.Text.Trim();
        order.ToPay = toPay.PeekValue(0.00M);
        order.Paid = paid.PeekValue(0.00M);
        order.BrokerageId = brokerage.SelectedIndex > 0 ? brokerage.SelectedValue.GlobalId() : (Guid?)null;
        order.KeeperId = keep.SelectedIndex > 0 ? keep.SelectedValue.GlobalId() : (Guid?)null;
        order.RecordTime = time;
        order.MainID = fno.Text.Trim();
        DataContext.SaveChanges();
        DataContext.DepotDictionaryAdd(Depot.Id, DictionaryType.购置来源, source.Text.Trim());
        DataContext.DepotDictionaryAdd(Depot.Id, DictionaryType.使用对象, usage.Text.Trim());
        return order.Id;
    }
}
