using Homory.Model;
using System;
using System.Linq;

namespace Document.web
{
    public partial class DocumentWebPdfViewerA : HomoryPage
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
					var resource = HomoryContext.Value.ResourceAttachment.First(o => o.Id == key).Source;
                    var xurl = resource.ToString().Substring(0, resource.ToString().LastIndexOf(".")) + ".pdf";
                    var url = string.Format("{0}{1}", Application["Resource"], xurl.Substring((3)));
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
