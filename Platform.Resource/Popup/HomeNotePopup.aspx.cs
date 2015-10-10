using System;
using System.Linq;
using Homory.Model;

namespace Popup
{
	public partial class PopupHomeNotePopup : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var id = Guid.Parse(Request.QueryString[0]);
				var note = HomoryContext.Value.Notice.First(o => o.Id == id);
				Title = note.Title;
				content.InnerHtml = note.Content;
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
