using System;
using System.Collections.Generic;
using System.Linq;
using Homory.Model;

namespace Control
{
	public partial class ControlC6Article : HomoryResourceControl
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
			var list = new List<Resource>();
			if (IsOnline)
			{
				var __courses = CurrentUser.Taught.Where(o => o.State < State.审核).Select(o => o.CourseId).ToList();
				foreach (var __c in __courses)
				{
					list.AddRange(HomoryContext.Value.Resource.Where(o => o.State == State.启用 && o.UserId == CurrentUser.Id && o.ResourceCatalog.Count(p => p.CatalogId == __c && p.State < State.启用) > 0).Take(Count).ToList());
					if (list.Count >= Count)
						break;
				}
			}

			list = list.OrderByDescending(o => o.Time).ToList();

			if (list.Count < Count)
			{
				list.AddRange(HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频)
				.OrderByDescending(o => o.Credit)
				.Take(Count - list.Count)
				.ToList());
			}

			homory_article.DataSource = list.Take(Count);
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
