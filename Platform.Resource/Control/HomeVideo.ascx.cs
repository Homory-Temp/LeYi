using System;
using System.Linq;
using Homory.Model;
using System.Collections.Generic;

namespace Control
{
	public partial class ControlHomeGroupVideo : HomoryResourceControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindVideo();
			}
		}

		public void BindVideo()
		{
            var predicate = SR();
            if (HomeCampus == null)
            {
                video_latest.DataSource =
                    HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频)
                        .OrderByDescending(o => o.Time)
                        .Take(4)
                        .ToList();
                video_latest.DataBind();
                video_popular.DataSource =
                    HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频)
                        .OrderByDescending(o => o.View)
                        .Take(4)
                        .ToList();
                video_popular.DataBind();
            }
            else
            {
                video_latest.DataSource =
                    HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频).Where(predicate)
                        .OrderByDescending(o => o.Time)
                        .Take(4)
                        .ToList();
                video_latest.DataBind();
                video_popular.DataSource =
                    HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频).Where(predicate)
                        .OrderByDescending(o => o.View)
                        .Take(4)
                        .ToList();
                video_popular.DataBind();
            }


            var list = new List<Resource>();
            if (IsOnline)
            {
                var __courses = CurrentUser.Taught.Where(o => o.State < State.审核).Select(o => o.CourseId).ToList();
                foreach (var __c in __courses)
                {
                    if (HomeCampus == null)
                        list.AddRange(HomoryContext.Value.Resource.Where(o => o.State == State.启用 && o.UserId == CurrentUser.Id && o.ResourceCatalog.Count(p => p.CatalogId == __c && p.State < State.启用) > 0).Take(12).ToList());
                    else
                        list.AddRange(HomoryContext.Value.Resource.Where(o => o.State == State.启用 && o.UserId == CurrentUser.Id && o.ResourceCatalog.Count(p => p.CatalogId == __c && p.State < State.启用) > 0).Where(predicate).Take(12).ToList());
                    if (list.Count >= 12)
                        break;
                }
            }

            list = list.OrderByDescending(o=>o.Time).ToList();

            if (list.Count < 12)
            {
                if (HomeCampus == null)
                {
                    list.AddRange(HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频)
                    .OrderByDescending(o => o.Credit)
                    .Take(12 - list.Count)
                    .ToList());
                }
                else
                {
                    list.AddRange(HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.Type == ResourceType.视频).Where(predicate)
                    .OrderByDescending(o => o.Credit)
                    .Take(12 - list.Count)
                    .ToList());
                }
            }

            video_random.DataSource = list.Take(4);
			video_random.DataBind();
            video_randomX.DataSource = list.Count > 4 ? list.Skip(4).Take(list.Count - 4) : list.Take(4);
			video_randomX.DataBind();
            if (HomeCampus == null)
            {
                video_cut.DataSource = HomoryContext.Value.ResourceComment.Where(o => o.State < State.审核 && o.Timed == true).OrderByDescending(o => o.Time).Select(o => o.Resource).Distinct().Take(4).ToList();
                video_cut.DataBind();
            }
            else
            {
                video_cut.DataSource = HomoryContext.Value.ResourceComment.Where(o => o.State < State.审核 && o.Timed == true && o.Resource.User.DepartmentUser.Count(x => x.State < State.审核 && x.TopDepartmentId == HomeCampus.Id && (x.Type == DepartmentUserType.借调后部门主职教师 || x.Type == DepartmentUserType.部门主职教师)) > 0).OrderByDescending(o => o.Time).Select(o => o.Resource).Distinct().Take(4).ToList();
                video_cut.DataBind();
            }
        }

        protected string FormatPeriod(Resource res)
        {
            var comment = res.ResourceComment.Where(o => o.Timed == true).OrderByDescending(o => o.Time).First();
            var s = comment.Start.Value.ToString().Split(new char[] { '.' })[0] + "秒";
            var e = " - " + (comment.End.HasValue ? comment.End.Value.ToString().Split(new char[] { '.' })[0] + "秒" : string.Empty);
            return string.Format("<a onclick=\"popup('{1}');\">{0}</a>", s + e, comment.Id);
        }

        protected string GetCatalogName(Resource resource)
        {
            if (resource.CourseId != null && resource.GradeId != null)
            {
                return string.Format("{0} {1}",
                    HomoryContext.Value.Catalog.First(o => o.Id == resource.GradeId).Name,
                    HomoryContext.Value.Catalog.First(o => o.Id == resource.CourseId).Name);
            }
            return resource.ResourceCatalog.First(o => o.Catalog.Type == CatalogType.视频 && o.State < State.审核).Catalog.Name;
        }

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
