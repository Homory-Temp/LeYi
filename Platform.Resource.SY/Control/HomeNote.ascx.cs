using Homory.Model;
using System;
using System.Linq;

namespace Control
{
	public partial class ControlHomeNote : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindNote();
			}
		}

		public int Count { get; set; }

		public int MaxTitleLength { get; set; }

		public void BindNote()
		{
			homory_note.DataSource = HomoryContext.Value.Notice.Where(o => o.State < State.审核).OrderByDescending(o => o.Time).Take(Count).ToList();
			homory_note.DataBind();
		}

		protected void HomeNoteTimer_OnTick(object sender, EventArgs e)
		{
			BindNote();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
