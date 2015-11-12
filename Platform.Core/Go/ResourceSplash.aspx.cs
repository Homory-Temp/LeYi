using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoResourceSplash : HomoryCorePageWithGrid
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                string initialPath = Page.ResolveUrl("../Common/配置/ResourceSplash");
                if (!IsPostBack)
                {
                    exp.Configuration.ViewPaths = new string[] { "~/Common/配置/ResourceSplash" };
                    exp.Configuration.UploadPaths = new string[] { "~/Common/配置/ResourceSplash" };
                    exp.Configuration.DeletePaths = new string[] { "~/Common/配置/ResourceSplash" };
                    exp.InitialPath = initialPath;
                    var doc = XDocument.Load(Server.MapPath("~/Common/配置/ResourceSplash.xml"));
                    var sb = new StringBuilder();
                    foreach (var v in Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(doc.Root.Value))
                    {
                        sb.Append(v);
                        sb.Append("\r\n");
                    }
                    urls.Text = sb.ToString();
                }
            }
		}

		protected override string PageRight
		{
			get { return "ResourceManage"; }
		}

        protected void save_ServerClick(object sender, EventArgs e)
        {
            var to = urls.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(to);
            var doc = XDocument.Load(Server.MapPath("~/Common/配置/ResourceSplash.xml"));
            doc.Root.SetValue(json);
            doc.Save(Server.MapPath("~/Common/配置/ResourceSplash.xml"));
        }
    }
}
