using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Linq;

public partial class Message : System.Web.UI.Page 
{
    protected Lazy<MEntities> db = new Lazy<MEntities>(() => new MEntities());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDepartments();
        }
    }

    protected void LoadDepartments()
    {
        tree.DataSource = db.Value.M_寻呼机构.OrderBy(o => o.序号).ToList();
        tree.DataBind();
    }
}
