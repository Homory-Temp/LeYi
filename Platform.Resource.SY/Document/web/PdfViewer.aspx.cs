using Homory.Model;
using System;
using System.Linq;

namespace Document.web
{
    public partial class DocumentWebPdfViewer : HomoryPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected string Path
		{
			get
			{
				try
				{
					var id = Request.QueryString["Id"];
					var key = Guid.Parse(id);
					var resource = HomoryContext.Value.Resource.First(o => o.Id == key).Preview;
					var url = string.Format("{0}{1}", Application["Resource"], resource.Substring((3)));
					return url;
				}
				catch (Exception)
				{
					return string.Empty;
				}
			}
		}
	}
}
