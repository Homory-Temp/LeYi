using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;

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
        if (___id.Value.None())
        {
            Notify(ap, "请选择用户", "error");
            return;
        }
        var id = ___id.Value.GlobalId();
        Session["DepotUser"] = DataContext.DepotUser.Single(o => o.Id == id);
        Response.Redirect("~/Depot/Home");
    }

    private void Notify(RadAjaxPanel panel, string message, string type)
    {
        panel.ResponseScripts.Add(string.Format("notify(null, '{0}', '{1}');", message, type));
    }


    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var doc = XDocument.Load(Server.MapPath("~/Common/配置/Depot.Mobile.xml"));
        var users = doc.Root.Elements();
        var source = users.Select(o => new { Id = o.Attribute("Id").Value, Name = o.Attribute("Name").Value }).ToList();
        list.DataSource = source;
    }

    protected void do_in_ServerClick(object sender, EventArgs e)
    {
        ___id.Value = (sender as HtmlInputButton).Attributes["uid"];
        list.Rebind();
    }
}
