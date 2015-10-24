using System;
using System.Linq;

public partial class Depot_HomeRemove : DepotPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "DepotId".Query().GlobalId();
            store.InnerText = DataContext.Depot.Single(o => o.Id == id).Name;
        }
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        if (!name.Text.Equals(store.InnerText.Trim(), StringComparison.InvariantCultureIgnoreCase))
        {
            NotifyError(ap, "请输入完整的仓库名称");
            return;
        }
        var id = "DepotId".Query().GlobalId();
        DataContext.DepotRemove(id);
        Response.Redirect("~/Depot/Home");
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/Home");
    }
}
