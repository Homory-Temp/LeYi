using Homory.Model;
using System;
using System.Linq;

namespace Extended
{
	public partial class ExtendedNoteContent : HomoryCorePageWithNotify
	{
		private const string Right = "Note";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
// ReSharper disable Html.PathError
				Response.Redirect("~/Go/Note", false);
// ReSharper restore Html.PathError
				return;
			}
			if (!IsPostBack)
			{
				LoadInit();
			}
		}

		protected void LoadInit()
		{
			homory_editor.LocalizationPath = "~/Language/";
			homory_editor.Content = CurrentNotice.Content;
		}

		private Notice _notice;

		protected Notice CurrentNotice
		{
			get
			{
				if (_notice == null)
				{
					var id = Guid.Parse(Request.QueryString[0]);
					_notice = HomoryContext.Value.Notice.SingleOrDefault(o => o.Id == id);
				}
				return _notice;
			}
		}

		protected override string PageRight
		{
			get { return Right; }
		}

		protected void buttonOk_Click(object sender, EventArgs e)
		{
			var id = Guid.Parse(Request.QueryString[0]);
			var notice = HomoryContext.Value.Notice.SingleOrDefault(o => o.Id == id);
// ReSharper disable PossibleNullReferenceException
			notice.Content = homory_editor.Content;
// ReSharper restore PossibleNullReferenceException
			HomoryContext.Value.SaveChanges();
			panelInner.ResponseScripts.Add("RadClose();");
		}
	}
}
