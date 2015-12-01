using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Order : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Depot.Featured(DepotType.固定资产库))
                @in.Visible = false;
            day.SelectedDate = DateTime.Today;
            source.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.购置来源).ToList();
            source.DataBind();
            usage.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.使用对象).ToList();
            usage.DataBind();
            keep.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList();
            keep.DataBind();
            brokerage.DataSource = DataContext.DepotUserLoad(DepotUser.CampusId).ToList();
            brokerage.DataBind();
            ap.ResponseScripts.Add("t_day = '{0}'; t_source = '', t_usage = ''; genNumber();".Formatted(DateTime.Today.ToString("yyyyMMdd")));
        }
    }

    protected void goon_ServerClick(object sender, EventArgs e)
    {
        if (number.Text.Trim().None() && number.EmptyMessage.Trim().None())
        {
            NotifyError(ap, "请输入购置单号");
            return;
        }
        Save();
        Response.Redirect(Request.Url.PathAndQuery);
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
        var id = DataContext.GlobalId();
        var time = day.SelectedDate.HasValue ? day.SelectedDate.Value : DateTime.Today;
        DataContext.DepotOrderAdd(id, Depot.Id, number.Text.None() ? number.EmptyMessage.Trim() : number.Text.Trim(), receipt.Text.Trim(), source.Text.Trim(), usage.Text.Trim(), content.Text.Trim(), toPay.PeekValue(0.00M), paid.PeekValue(0.00M), (brokerage.SelectedIndex > 0 ? brokerage.SelectedValue.GlobalId() : (Guid?)null), (keep.SelectedIndex > 0 ? keep.SelectedValue.GlobalId() : (Guid?)null), time, DepotUser.Id, fno.Text.Trim());
        DataContext.SaveChanges();
        return id;
    }
}
