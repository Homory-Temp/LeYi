using System;
using System.Linq;
using System.Xml.Linq;
using Homory.Model;

namespace Control
{
	public partial class ControlHomeTopic : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindTopic();
			}
		}

		public void BindTopic()
		{
			var doc = XDocument.Load(Server.MapPath("../Common/配置/ResourceTopic.xml"));
			homory_topic.DataSource = doc.Root.Elements().ToList();
			homory_topic.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
