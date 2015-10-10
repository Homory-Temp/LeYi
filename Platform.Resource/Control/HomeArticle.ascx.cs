using System;
using System.Linq;
using Homory.Model;

namespace Control
{
	public partial class ControlHomeArticle : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindArticle();
			}
		}

		public int Count { get; set; }

		public int MaxTitleLength { get; set; }

		public void BindArticle()
		{
            if (HomeCampus == null)
            {
                homory_article.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.文章 && o.State == State.启用)
                            .OrderByDescending(o => o.Time)
                            .Take(Count)
                            .ToList();
            }
            else
            {
                var predicate = SR();
                homory_article.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.文章 && o.State == State.启用).Where(predicate)
                            .OrderByDescending(o => o.Time)
                            .Take(Count)
                            .ToList();
            }
            homory_article.DataBind();
		}

		protected void HomeArticleTimer_OnTick(object sender, EventArgs e)
		{
			BindArticle();
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
