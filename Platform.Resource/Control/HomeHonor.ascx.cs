using System;
using System.Linq;
using Homory.Model;

namespace Control
{
	public partial class ControlHomeHonor : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindHonor();
			}
		}

		public void BindHonor()
		{
            if (HomeCampus == null)
            {
                homory_article.DataSource = HomoryContext.Value.ViewTS.Where(o => o.State < State.审核).OrderByDescending(o => o.Credit).Take(5).ToList();
                homory_article.DataBind();
            }
            else
            {
                var predicate = SU();
                homory_article.DataSource = HomoryContext.Value.ViewTS.Where(o => o.State < State.审核).OrderByDescending(o => o.Credit)
                    .ToList().Join(HomoryContext.Value.User.Where(predicate), o => o.Id, o => o.Id, (a, b) => a)
                    .Take(5).ToList();
                homory_article.DataBind();
            }
        }

		protected void HomeHonorTimer_OnTick(object sender, EventArgs e)
		{
			BindHonor();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
