using System;
using System.Linq;
using Homory.Model;

namespace Control
{
	public partial class ControlC6Action : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var list = HomoryContext.Value.Action.Where(o => (o.Type == ActionType.用户评分资源 || o.Type == ActionType.用户评论资源) && o.State == State.启用).ToList();
				actions.DataSource = list.Where(o => R(o.Id2).UserId == CurrentUser.Id).OrderByDescending(o => o.Time).Take(8).ToList();
				actions.DataBind();
			}
		}

		protected override bool ShouldOnline
		{
			get { return true; }
		}

	}
}
