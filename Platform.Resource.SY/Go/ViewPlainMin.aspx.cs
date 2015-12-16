using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Go_ViewPlainMin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            var url = string.Format("../Document/web/PdfViewer.aspx?Id={0}&Random={1}", Server.UrlDecode(Request.QueryString["Id"].ToString()),Guid.NewGuid());

            publish_preview_pdf.Attributes["src"] = url;
        }
    }
}