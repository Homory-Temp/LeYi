using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_Return : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            time.SelectedDate = DateTime.Today;
            people.Items.Clear();
            people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
            people.DataBind();
            counter.Value = "1";
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

    protected void view_obj_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (people.SelectedValue.None())
        {
            view_obj.DataSource = null;
            do_return.Visible = false;
        }
        else
        {
            var userId = people.SelectedValue.GlobalId();
            var source = DataContext.DepotUseXRecord.Where(o => o.UserId == userId && o.Type == 2 && o.Amount > o.ReturnedAmount).ToList();
            view_obj.DataSource = source;
            do_return.Visible = source.Count > 0;
        }
    }

    protected void do_return_ServerClick(object sender, EventArgs e)
    {
        var list = new List<InMemoryReturn>();
        foreach (var item in view_obj.Items)
        {
            var r = new InMemoryReturn(); 
            r.UseX = (item.FindControl("id") as Label).Text.GlobalId();
            r.Amount = (item.FindControl("amount") as RadNumericTextBox).PeekValue(0M);
            r.OutAmount = (item.FindControl("outAmount") as RadNumericTextBox).PeekValue(0M);
            if (r.OutAmount > r.Amount)
            {
                r.OutAmount = r.Amount;
            }
            r.Note = (item.FindControl("note") as RadTextBox).Text;
            if (r.Amount > 0)
            {
                list.Add(r);
            }
        }
        var userId = people.SelectedValue.GlobalId();
        DataContext.DepotActReturn(Depot.Id, time.SelectedDate.HasValue ? time.SelectedDate.Value.Date : DateTime.Today, DepotUser.Id, list);
        Response.Redirect("~/DepotQuery/Return?DepotId={0}".Formatted(Depot.Id));
    }
}
