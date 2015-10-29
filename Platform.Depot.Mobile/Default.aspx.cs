using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page
{
    private Lazy<DepotEntities> db = new Lazy<DepotEntities>(() => new DepotEntities());

    public DepotEntities DataContext
    {
        get
        {
            return db.Value;
        }
    }

    protected void buttonSign_Click(object sender, EventArgs e)
    {
        var id = ___id.Value.GlobalId();
        Session["DepotUser"] = DataContext.DepotUser.Single(o => o.Id == id);
        Response.Redirect("~/Depot/Home");
    }

    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var doc = XDocument.Load(Server.MapPath("~/Common/配置/Depot.Mobile.xml"));
        var users = doc.Root.Elements();
        var source = users.Select(o => new { Id = o.Attribute("Id").Value, Name = o.Attribute("Name").Value }).ToList();
        list.DataSource = source;
        if (!IsPostBack)
        {
            ___id.Value = source.Count() > 0 ? source.First().Id.ToString() : "";
        }
    }

    protected void do_in_ServerClick(object sender, EventArgs e)
    {
        ___id.Value = (sender as HtmlInputButton).Attributes["uid"];
        list.Rebind();
    }
}
