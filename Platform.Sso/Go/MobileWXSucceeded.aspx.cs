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
    public partial class GoMobileWXSucceeded : HomoryPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            code_btn.InnerText = string.Format("{0}{1}老师您好，您的信息已经绑定，我们将在后期为您提供更为便捷的平台应用服务。", Request.QueryString["Dept"], Request.QueryString["User"]);
        }
    }
}
