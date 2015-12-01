using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class DepotQuery_InPrint : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var orderId = "OrderId".Query().GlobalId();
            var ord = DataContext.DepotOrder.Single(o => o.Id == orderId);
            x.Text = ord.Name;
            if (!ord.MainID.None())
            {
                var url = "../DepotQuery/InOrderX?DepotId={0}&OrderId={1}".Formatted(Depot.Id, orderId);
                Response.Redirect(url);
            }
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().None())
        {
            NotifyError(ap, "请输入流程编号");
            return;
        }
        var _name = name.Text.Trim();
        var orderId = "OrderId".Query().GlobalId();
        var ord = DataContext.DepotOrder.Single(o => o.Id == orderId);
        ord.MainID = _name;
        DataContext.SaveChanges();
        var url = "../DepotQuery/InOrderX?DepotId={0}&OrderId={1}".Formatted(Depot.Id, orderId);
        Response.Redirect(url);
    }
}
