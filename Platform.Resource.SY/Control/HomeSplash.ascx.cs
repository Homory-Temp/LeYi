using Homory.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Control
{
	public partial class ControlHomeSplash : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindSplash();
			}
		}

        protected string GetLink(int index)
        {
            return "#";
            var doc = XDocument.Load(Server.MapPath("../Common/配置/ResourceSplash.xml"));
            var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(doc.Root.Value);
            return list.Count > index ? list[index] : "#";
        }

        public void BindSplash()
		{
            var di = new DirectoryInfo(Server.MapPath("../Common/配置/ResourceSplash"));
            List<string> list = new List<string>();
            foreach (var item in di.GetFiles("*.*"))
            {
                if (item.Name.ToLower().EndsWith(".jpg") || item.Name.ToLower().EndsWith(".jpeg") || item.Name.ToLower().EndsWith(".png") || item.Name.ToLower().EndsWith(".gif"))
                {
                    list.Add("../Common/配置/ResourceSplash/" + item.Name);
                }
            }
            r_img.DataSource = list.OrderBy(o => o).ToList();
            r_img.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
