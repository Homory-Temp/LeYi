using Homory.Model;
using System;
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

		public void BindSplash()
		{
			var doc = XDocument.Load(Server.MapPath("../Common/配置/ResourceSplash.xml"));
			r_img.DataSource = doc.Root.Elements().ToList();
			r_img.DataBind();
			r_div.DataSource = doc.Root.Elements().ToList();
			r_div.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
