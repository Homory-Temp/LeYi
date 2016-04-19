using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Management;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Go
{
    public partial class GoMobile : HomoryPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
                this.Title = doc.Root.Element("Sso").Value;
            }
        }

        protected void buttonSign_OnClick(object sender, EventArgs e)
        {
            var script_re = string.Format("top.location.href = '{0}';", "https://www.baidu.com/");
            areaAction.ResponseScripts.Add(script_re);
        }

        protected void buttonRegister_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Application["Sso"] + "Go/Register", false);
        }
    }
}
