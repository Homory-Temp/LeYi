using Homory.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoViewVideo : HomoryResourcePage
	{

        protected void preview_timer_Tick(object sender, EventArgs e)
        {
            var path = Server.MapPath(CurrentResource.Preview);
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                try
                {
                    var s = info.OpenWrite();
                    try
                    {
                        s.Close();
                    }
                    catch
                    {
                    }
                    Response.Redirect(Request.Url.PathAndQuery.ToString(), true);
                }
                catch
                {
                }
            }
        }
        private readonly string[] _units = { "B", "K", "M", "G", "T" };
        private string ConvertResourceCount(decimal count)
        {
            var index = 0;
            var value = count;
            while (value > 1024)
            {
                value = decimal.Divide(value, 1024);
                index++;
            }
            return string.Format("{0}{1}", Math.Round(value, 2), _units[index]);
        }

        protected string MB()
        {
            var p = new FileInfo(Server.MapPath(CurrentResource.Preview));
            var s = new FileInfo(Server.MapPath(CurrentResource.Source));
            if (p.Length == s.Length)
            {
                return string.Format("{0}", ConvertResourceCount(p.Length));
            }
            else
            {
                return string.Format("高清&nbsp;{0}&nbsp;&nbsp;高速&nbsp;{1}", ConvertResourceCount(s.Length), ConvertResourceCount(p.Length));
            }
        }

        private AssessTable _assessTable;

        protected AssessTable CurrentAssessTable
        {
            get
            {
                if (_assessTable == null)
                {
                    var id = Guid.Parse(Request.QueryString["Id"]);
                    _assessTable = HomoryContext.Value.AssessTable.SingleOrDefault(o => o.GradeId == CurrentResource.GradeId && o.CourseId == CurrentResource.CourseId && o.State < State.审核);
                    //_assessTable = HomoryContext.Value.ResourceAssess.SingleOrDefault(o => o.ResourceId == id).AssessTable;
                }
                return _assessTable;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                try
                {
                    var cat = CurrentAssessTable;
                    if (cat != null)
                        this.accessId_hidden.Text = CurrentResource.Id.ToString();
                }
                catch
                {
                }
                var path = Server.MapPath(CurrentResource.Preview);
                bool yes = false;
                if (File.Exists(path))
                {
                    //FileInfo info = new FileInfo(path);
                    //try
                    //{
                        //var s = info.OpenWrite();
                        //try
                        //{
                            //s.Close();
                        //}
                        //catch
                        //{
                        //}
                        yes = true;
                    //}
                    //catch
                    //{
                    //}
                }
                // var hideRes = CurrentResource.OpenType == OpenType.不公开 || (CurrentResource.OpenType == OpenType.所在学校) && !IsOnline;
                var hideRes = CurrentResource.OpenType == OpenType.不公开 || ((CurrentResource.OpenType == OpenType.全部学校 || CurrentResource.OpenType == OpenType.所在学校) && !IsOnline);
                var showRes = IsOnline && CurrentUser.Id == CurrentResource.UserId;

                if (hideRes && !showRes)
                {
                    ri.Visible = ri2.Visible = ri3.Visible = assessPanel.Visible = ri4.Visible = commentPanel.Visible = ni.Visible = pppp1.Visible = pppp2.Visible = false;
                    notAllowed.Visible = true;
                }
                else
                {
                    notAllowed.Visible = false;
                    if (!yes)
                    {
                        ni.Visible = true;
                        ri.Visible = ri2.Visible = ri3.Visible = assessPanel.Visible = ri4.Visible = commentPanel.Visible = ri5.Visible = pppp1.Visible = pppp2.Visible = false;
                        preview_timer.Enabled = true;
                    }
                    else
                    {
                        ni.Visible = false;
                        ri.Visible = ri2.Visible = ri3.Visible = assessPanel.Visible = ri4.Visible = commentPanel.Visible = ri5.Visible = pppp1.Visible = pppp2.Visible = true;
                        preview_timer.Enabled = false;
                    }
                }
                player.Video = CurrentResource.Preview;
                catalog.Visible = CurrentResource.Type == ResourceType.视频 && CurrentResource.ResourceCatalog.Count(y => y.State < State.审核 && y.Catalog.State < State.审核 && y.Catalog.Type == CatalogType.视频) > 0;
                cg.Visible = CanCombineCourse() || CanCombineGrade();
                tag.Visible = CanCombineTags();
                var p =
					TargetUser.Resource.Where(
						o => o.State == State.启用 && o.Type == CurrentResource.Type && o.Time > CurrentResource.Time)
						.OrderByDescending(o => o.Time).FirstOrDefault();
				if (p != null)
				{
					previous.Visible = true;
					previousLink.InnerText = p.Title;
					previousLink.HRef = string.Format("../Go/ViewVideo?Id={0}", p.Id);
				}
				var n =
					TargetUser.Resource.Where(
						o => o.State == State.启用 && o.Type == CurrentResource.Type && o.Time < CurrentResource.Time)
						.OrderByDescending(o => o.Time)
						.FirstOrDefault();
				if (n != null)
				{
					next.Visible = true;
					nextLink.InnerText = n.Title;
					nextLink.HRef = string.Format("../Go/ViewVideo?Id={0}", n.Id);
				}
				if (IsOnline)
				{
                    LogOp(ResourceLogType.浏览资源, 1);
                    var rate = HomoryContext.Value.Action.FirstOrDefault(o => o.Id2 == CurrentResource.Id && o.Id3 == CurrentUser.Id && o.Type == ActionType.用户评分资源 && o.State == State.启用);
					if (rate != null)
					{
						rating.Enabled = false;
						rating.Value = decimal.Parse(rate.Content1);
					}
                    if (HomoryContext.Value.MediaNote.Count(o => o.ResourceId == CurrentResource.Id && o.UserId == CurrentUser.Id) > 0)
                    {
                        var mn = HomoryContext.Value.MediaNote.First(o => o.ResourceId == CurrentResource.Id && o.UserId == CurrentUser.Id);
                        mn1.InnerText = mn.A;
                        mn2.InnerText = mn.B;
                    }
                }
                HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.View, 1);
				if (IsOnline)
				{
					var action = HomoryContext.Value.Action.FirstOrDefault(o => o.Id1 == TargetUser.Id && o.Id2 == CurrentResource.Id && o.Id3 == CurrentUser.Id && o.Type == ActionType.用户访问资源);
					if (action == null)
					{
						action = new Homory.Model.Action
						{
							Id = HomoryContext.Value.GetId(),
							Id1 = TargetUser.Id,
							Id2 = CurrentResource.Id,
							Id3 = CurrentUser.Id,
							Type = ActionType.用户访问资源,
							State = State.启用,
							Time = DateTime.Now,
						};
						HomoryContext.Value.Action.Add(action);
					}
					else
					{
						action.Time = DateTime.Now;
					}
					if (Queryable.Count(HomoryContext.Value.Action, o => o.Id2 == CurrentResource.Id && o.Id3 == CurrentUser.Id && o.Type == ActionType.用户收藏资源 && o.State == State.启用) == 0)
					{
                        xxxxx.BgColor = "#adadad";
                    }
                    else
					{
                        xxxxx.BgColor = "#4fc0e8";
                    }
                }
				else
				{
                    xxxxx.BgColor = "#adadad";
                }
                CurrentResource.View += 1;
				HomoryContext.Value.SaveChanges();
				downloadCount.InnerText = CurrentResource.Download.ToString();
				favouriteCount.InnerText = CurrentResource.Favourite.ToString();
				icon.ImageUrl = P(TargetUser.Icon);
                name.Text = UC(TargetUser.Id).Replace("无锡市", "").Replace("无锡", "");
                nameX.Text = TargetUser.DisplayName;
                viewCount.Text = CurrentResource.View.ToString();
				viewList.DataSource = Queryable.Take(HomoryContext.Value.Action.Where(o => o.Id2 == CurrentResource.Id && o.Type == ActionType.用户访问资源).OrderByDescending(q => q.Time), 9)
						.ToList();
				viewList.DataBind();
				var rt = CurrentResource.Type;
				latest.DataSource =
					TargetUser.Resource.Where(o => o.State == State.启用 && o.Type == rt)
						.OrderByDescending(o => o.Time)
						.Take(9)
						.ToList();
				latest.DataBind();
				popular.DataSource =
				TargetUser.Resource.Where(o => o.State == State.启用 && o.Type == rt)
					.OrderByDescending(o => o.View)
					.Take(9)
					.ToList();
				popular.DataBind();
				best.DataSource =
		TargetUser.Resource.Where(o => o.State == State.启用 && o.Type == rt)
			.OrderByDescending(o => o.Grade)
			.Take(9)
			.ToList();
				best.DataBind();
				BindComment();
				score.InnerText = CurrentResource.Grade.ToString();

			}
		}

        protected bool CanCombineGrade()
        {
            return CurrentResource.GradeId.HasValue;
        }

        protected string CombineGrade()
        {
            return CanCombineGrade() ? string.Format("年级：<a target='_blank' href='../Go/Search?{1}={2}'>{0}</a>", HomoryContext.Value.Catalog.First(o => o.Id == CurrentResource.GradeId).Name, QueryType(HomoryContext.Value.Catalog.First(o => o.Id == CurrentResource.GradeId).Type), CurrentResource.GradeId) : "";
        }

        protected bool CanCombineCourse()
        {
            return CurrentResource.CourseId.HasValue;
        }

        protected string CombineCourse()
        {
            return CanCombineCourse() ? string.Format("学科：<a target='_blank' href='../Go/Search?{1}={2}'>{0}</a>", HomoryContext.Value.Catalog.First(o => o.Id == CurrentResource.CourseId).Name, QueryType(CatalogType.课程), CurrentResource.CourseId) : "";
        }

        protected bool CanCombineTags()
        {
            return CurrentResource.ResourceTag.Count(o => o.State < State.审核) > 0;
        }

        protected string CombineTags()
        {
            var format = "、<a target='_blank' href='../Go/Search?Content={1}'>{0}</a>";
            var sb = new StringBuilder();
            foreach (var t in CurrentResource.ResourceTag.Where(o => o.State < State.审核).ToList())
                sb.Append(string.Format(format, t.Tag, Server.UrlEncode(t.Tag)));
            return CanCombineTags() ? sb.ToString().Substring(1) : "";
        }

        public class AssessItem
		{
			public string Name { get; set; }
			public decimal Score { get; set; }
			public decimal Me { get; set; }
			public string All { get; set; }
		}

		private Resource _resource;

		protected static string QueryType(CatalogType type)
		{
			switch (type)
			{
				case CatalogType.年级_幼儿园:
                case CatalogType.年级_小学:
                case CatalogType.年级_初中:
                case CatalogType.年级_高中:
					{
						return "Grade";
					}
				case CatalogType.课程:
					{
						return "Course";
					}
				default:
					{
						return "Catalog";
					}
			}
		}

		protected Func<string, ResourceCatalog, string> Combine = (a, o) => string.Format("{0}<a target='_blank' href='../Go/Search?{2}={3}'>{1}</a>、", a, o.Catalog.Name, QueryType(o.Catalog.Type), o.CatalogId);

		protected Resource CurrentResource
		{
			get
			{
				if (_resource == null)
				{
					var id = Guid.Parse(Request.QueryString["Id"]);
					_resource = HomoryContext.Value.Resource.Single(o => o.Id == id);
				}
				return _resource;
			}
		}

		private User _user;

		protected User TargetUser
		{
			get
			{
				if (_user == null)
				{
					_user = CurrentResource.User;
				}
				return _user;
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}

		protected void rating_OnRate(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Rate, 1);
			HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Grade, rating.Value);
			// 1次3.5分
			int low = CurrentResource.Rate;
			decimal lowV = CurrentResource.Grade;
			CurrentResource.Rate += 1;
			CurrentResource.Grade = decimal.Divide(low * lowV + rating.Value,
				low + 1);
			// (3.5*1+4.5)/(1+1)
			var action = new Homory.Model.Action
			{
				Id = HomoryContext.Value.GetId(),
				Id1 = TargetUser.Id,
				Id2 = CurrentResource.Id,
				Id3 = CurrentUser.Id,
				Content1 = rating.Value.ToString(),
				Type = ActionType.用户评分资源,
				State = State.启用,
				Time = DateTime.Now,
			};
			HomoryContext.Value.Action.Add(action);
			HomoryContext.Value.SaveChanges();
			rating.Enabled = false;
			bestPanel.RaisePostBackEvent("Refresh");
			scorePanel.RaisePostBackEvent("Refresh");
		}

		protected void download_OnServerClick(object sender, EventArgs e)
		{
			HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Download, 1);
			if (IsOnline)
			{
				var action = new Homory.Model.Action
				{
					Id = HomoryContext.Value.GetId(),
					Id1 = TargetUser.Id,
					Id2 = CurrentResource.Id,
					Id3 = CurrentUser.Id,
					Type = ActionType.用户下载资源,
					State = State.启用,
					Time = DateTime.Now,
				};
				HomoryContext.Value.Action.Add(action);
			}
			CurrentResource.Download += 1;
            LogOp(ResourceLogType.下载资源, 1);
            HomoryContext.Value.SaveChanges();
			downloadCount.InnerText = CurrentResource.Download.ToString();
			downloadPanel.ResponseScripts.Add(string.Format("window.open('{0}');", CurrentResource.Source));
		}

        protected void downloadX_OnServerClick(object sender, EventArgs e)
        {
            HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Download, 1);
            if (IsOnline)
            {
                var action = new Homory.Model.Action
                {
                    Id = HomoryContext.Value.GetId(),
                    Id1 = TargetUser.Id,
                    Id2 = CurrentResource.Id,
                    Id3 = CurrentUser.Id,
                    Type = ActionType.用户下载资源,
                    State = State.启用,
                    Time = DateTime.Now,
                };
                HomoryContext.Value.Action.Add(action);
            }
            CurrentResource.Download += 1;
            LogOp(ResourceLogType.下载资源, 1);
            HomoryContext.Value.SaveChanges();
            downloadCount.InnerText = CurrentResource.Download.ToString();
            downloadPanel.ResponseScripts.Add(string.Format("window.open('{0}');", CurrentResource.Preview));
        }

        protected void favourite_OnServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			if (HomoryContext.Value.Action.Count(o => o.Id2 == CurrentResource.Id && o.Id3 == CurrentUser.Id && o.Type == ActionType.用户收藏资源 && o.State == State.启用) == 0)
			{
				HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Favourite, 1);
				var action = new Homory.Model.Action
				{
					Id = HomoryContext.Value.GetId(),
					Id1 = TargetUser.Id,
					Id2 = CurrentResource.Id,
					Id3 = CurrentUser.Id,
					Type = ActionType.用户收藏资源,
					State = State.启用,
					Time = DateTime.Now,
				};
				HomoryContext.Value.Action.Add(action);
				CurrentResource.Favourite += 1;
                LogOp(ResourceLogType.收藏资源, 1);
                HomoryContext.Value.SaveChanges();
                xxxxx.BgColor = "#4fc0e8";
            }
			favouriteCount.InnerText = CurrentResource.Favourite.ToString();
		}

		protected void BindComment()
		{
			commentList.DataSource = CurrentResource.ResourceComment.Where(o => o.State == State.启用).OrderByDescending(o => o.Time).ToList();
			commentList.DataBind();
			commentList.ExpandAllNodes();
		}

		protected string FormatPeriod(ResourceComment comment)
		{
			if (!comment.Timed.HasValue || !comment.Timed.Value)
				return string.Empty;
			var s = comment.Start.Value.ToString().Split(new char[] { '.' })[0] + "秒";
			var e = " - " + (comment.End.HasValue ? comment.End.Value.ToString().Split(new char[] { '.' })[0] + "秒" : string.Empty);
			return string.Format("<a onclick=\"popup('{1}');\">{0}</a>", s + e, comment.Id);
		}

		protected void doComment_OnServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			if (string.IsNullOrWhiteSpace(comment.Value.Trim()))
				return;
			var c = new ResourceComment
			{
				Id = HomoryContext.Value.GetId(),
				ParentId = null,
				ResourceId = CurrentResource.Id,
				UserId = CurrentUser.Id,
				Content = comment.Value.Trim(),
				Timed = false,
				Level = 0,
				Time = DateTime.Now,
				State = State.启用
			};
			HomoryContext.Value.ResourceComment.Add(c);
			HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Comment, 1);
			CurrentResource.Comment += 1;
			var action = new Homory.Model.Action
			{
				Id = HomoryContext.Value.GetId(),
				Id1 = TargetUser.Id,
				Id2 = CurrentResource.Id,
				Id3 = CurrentUser.Id,
				Content1 = comment.Value.Trim(),
				Type = ActionType.用户评论资源,
				State = State.启用,
				Time = DateTime.Now,
			};
			HomoryContext.Value.Action.Add(action);
            LogOp(ResourceLogType.评论资源, 1);
            HomoryContext.Value.SaveChanges();
			comment.Value = string.Empty;
			BindComment();
		}

		protected void goReply_OnServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			var x = (sender as HtmlAnchor).Parent.FindControl("reply");
			x.Visible = true;
		}

		protected void noReply_OnServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			var x = (sender as HtmlAnchor).Parent;
			x.Visible = false;
		}

		protected void replyReply_OnServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			var x = (sender as HtmlAnchor).Parent.FindControl("replyContent") as HtmlTextArea;
			var id = Guid.Parse(((sender as HtmlAnchor).Parent.FindControl("replyId") as HtmlInputHidden).Value);
			if (string.IsNullOrWhiteSpace(x.Value.Trim()))
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(x.Value.Trim()))
				return;
			var c = new ResourceComment
			{
				Id = HomoryContext.Value.GetId(),
				ParentId = id,
				ResourceId = CurrentResource.Id,
				UserId = CurrentUser.Id,
				Content = x.Value.Trim(),
				Timed = false,
				Level = HomoryContext.Value.ResourceComment.First(o => o.Id == id).Level + 1,
				Time = DateTime.Now,
				State = State.启用
			};
			HomoryContext.Value.ResourceComment.Add(c);
			HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Comment, 1);
			CurrentResource.Comment += 1;
			var action = new Homory.Model.Action
			{
				Id = HomoryContext.Value.GetId(),
				Id1 = TargetUser.Id,
				Id2 = CurrentResource.Id,
				Id3 = CurrentUser.Id,
				Id4 = id,
				Content1 = x.Value.Trim(),
				Type = ActionType.用户回复评论,
				State = State.启用,
				Time = DateTime.Now,
			};
			HomoryContext.Value.Action.Add(action);
            LogOp(ResourceLogType.回复评论, 1);
            HomoryContext.Value.SaveChanges();
			BindComment();

		}

		protected void bestPanel_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
		{
			var rt = CurrentResource.Type;
			best.DataSource =
TargetUser.Resource.Where(o => o.State == State.启用 && o.Type == rt)
	.OrderByDescending(o => o.Grade)
	.Take(9)
	.ToList();
			best.DataBind();
		}

		protected void scorePanel_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
		{
			score.InnerText = CurrentResource.Grade.ToString();
		}

		protected void dotSend_Click(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			if (string.IsNullOrWhiteSpace(dotContent.Text.Trim()))
				return;
			decimal t1, t2;
			decimal? ta, tb;
			decimal.TryParse(startDot.Text.Replace("'", ""), out t1);
			decimal.TryParse(endDot.Text.Replace("'", ""), out t2);
			if (t1 == 0 && t2 == 0)
			{
				ta = tb = (decimal?)null;
			}
			else if (t1 == 0)
			{
				ta = 0;
				tb = t2;
			}
			else if (t2 == 0)
			{
				ta = t1;
				tb = (decimal?)null;
			}
			else
			{
				ta = t1;
				tb = t2;
			}
			var c = new ResourceComment
			{
				Id = HomoryContext.Value.GetId(),
				ParentId = null,
				ResourceId = CurrentResource.Id,
				UserId = CurrentUser.Id,
				Content = dotContent.Text.Trim(),
				Start = ta,
				End = tb,
				Timed = t1 > 0 || t2 > 0,
				Level = 0,
				Time = DateTime.Now,
				State = State.启用
			};
			HomoryContext.Value.ResourceComment.Add(c);
			HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Comment, 1);
			CurrentResource.Comment += 1;
			var action = new Homory.Model.Action
			{
				Id = HomoryContext.Value.GetId(),
				Id1 = TargetUser.Id,
				Id2 = CurrentResource.Id,
				Id3 = CurrentUser.Id,
				Content1 = dotContent.Text.Trim(),
				Type = ActionType.用户评论资源,
				State = State.启用,
				Time = DateTime.Now,
			};
			HomoryContext.Value.Action.Add(action);
			HomoryContext.Value.SaveChanges();
			dotContent.Text = string.Empty;
			Response.Redirect(Request.Url.PathAndQuery, false);
		}

		protected void commentPanel_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
		{
			BindComment();
		}

		protected void mnbtn_ServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			if (mn1.InnerText.Length < 50)
			{
                Rwm.RadAlert(string.Format("过程记录不得少于50字，当前{0}字！", 50 - mn1.InnerText.Length), 300,  80, "提示：", null);
				return;
			}
			if (mn2.InnerText.Length < 50)
			{
                Rwm.RadAlert(string.Format("重点摘要不得少于50字，当前{0}字！", 50 - mn1.InnerText.Length), 300, 80, "提示：", null);
				return;
			}
            if (HomoryContext.Value.MediaNote.Count(o => o.ResourceId == CurrentResource.Id && o.UserId == CurrentUser.Id) == 0)
            {
                var mn = new MediaNote
                {
                    A = mn1.InnerText,
                    B = mn2.InnerText,
                    Month = DateTime.Today.Month,
                    Year = DateTime.Today.Year,
                    Time = DateTime.Now,
                    ResourceId = CurrentResource.Id,
                    UserId = CurrentUser.Id
                };
                HomoryContext.Value.MediaNote.Add(mn);
            }
            else
            {
                var mn = HomoryContext.Value.MediaNote.First(o => o.ResourceId == CurrentResource.Id && o.UserId == CurrentUser.Id);
                mn.A = mn1.InnerText;
                mn.B = mn2.InnerText;
            }
			HomoryContext.Value.SaveChanges();
            Rwm.RadAlert("保存成功！", 200, 80, "提示：", null, "../Image/true.png");
			//mnp.ResponseScripts.Add("window.alert('保存成功！');");
		}

		protected void assessTable_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
            var path = Server.MapPath(CurrentResource.Preview);
            bool yes = false;
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                try
                {
                    var s = info.OpenWrite();
                    try
                    {
                        s.Close();
                    }
                    catch
                    {
                    }
                    yes = true;
                }
                catch
                {
                }
            }

            if (!yes)
            {
                return;
            }


            if (CurrentResource.GradeId.HasValue && CurrentResource.CourseId.HasValue)
			{
                assessPanel.Visible = true;
                var 课程 = CurrentResource.CourseId.Value;
				var 年级 = CurrentResource.GradeId.Value;
				if (HomoryContext.Value.AssessTable.Count(o => o.CourseId == 课程 && o.GradeId == 年级 && o.State == State.启用) > 0)
				{
					var t =
						HomoryContext.Value.AssessTable.First(o => o.CourseId == 课程 && o.GradeId == 年级 && o.State == State.启用);
					var list = t.Content.FromJson<List<AssessItem>>();
					var total = 0.0;
					var ____c = CurrentResource.ResourceAssess.Count(
							o => o.AssessTableId == t.Id && o.ResourceId == CurrentResource.Id);
					var ras = CurrentResource.ResourceAssess.Where(
							o => o.AssessTableId == t.Id && o.ResourceId == CurrentResource.Id)
							.Select(o => o.Content)
							.ToList()
							.Select(o => o.FromJson<List<int>>())
							.ToList();
					for (int k = 0; k < list.Count; k++)
					{
						if (____c > 0)
						{
							total += ras.Average(p => p[k]);
							list[k].All = string.Format("（{0}/{1}分）", ras.Average(p => p[k]).ToString("F1"), list[k].Score.ToString("F1"));
						}
						else
						{
							list[k].All = string.Format("（{0}/{1}分）", 0, list[k].Score.ToString("F1"));
						}
					}
					sss.InnerText = string.Format("共{0}次评估，得分{1}分", ras.Count, total.ToString("F1"));

					if (IsOnline)
					{
						if (CurrentResource.ResourceAssess.Count(o => o.UserId == CurrentUser.Id && o.AssessTableId == t.Id && o.ResourceId == CurrentResource.Id) > 0)
						{
							rr.Visible = false;
							var ra = CurrentResource.ResourceAssess.First(o => o.UserId == CurrentUser.Id && o.AssessTableId == t.Id && o.ResourceId == CurrentResource.Id);
							List<int> me = ra.Content.FromJson<List<int>>();
							for (int i = 0; i < me.Count; i++)
							{
								list[i].Me = me[i];
							}
						}
					}
					assessTable.DataSource = list;
				}
				else
				{
					assessPanel.Visible = false;
				}
			}
		}

		protected void rr_OnServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
            if (CurrentResource.GradeId.HasValue && CurrentResource.CourseId.HasValue)
            {
                var 课程 = CurrentResource.CourseId.Value;
                var 年级 = CurrentResource.GradeId.Value;
                if (HomoryContext.Value.AssessTable.Count(o => o.CourseId == 课程 && o.GradeId == 年级 && o.State == State.启用) > 0)
				{
					var t =
						HomoryContext.Value.AssessTable.First(o => o.CourseId == 课程 && o.GradeId == 年级 && o.State == State.启用).Id;

					var list = new List<int>();
					foreach (var obj in assessTable.Items)
					{
						list.Add((int)((RadSlider)obj.FindControl("ss")).Value);
					}
					ResourceAssess ra = new ResourceAssess();
					ra.AssessTableId = t;
					ra.ResourceId = CurrentResource.Id;
					ra.UserId = CurrentUser.Id;
					ra.Content = list.ToJson();
					ra.Time = DateTime.Now;
					HomoryContext.Value.ResourceAssess.Add(ra);
					HomoryContext.Value.SaveChanges();
					assessTable.Rebind();
					assessPanel.RaisePostBackEvent("Refresh");
					mnp.ResponseScripts.Add("window.alert('评估成功！');");
					assessPanel.RaisePostBackEvent("Refresh");
				}
			}


		}
		protected void goDelP_ServerClick(object sender, EventArgs e)
		{
			try
			{
				HtmlAnchor a = (HtmlAnchor)sender;
				var id = Guid.Parse(a.Attributes["alt"]);
				var rc = HomoryContext.Value.ResourceComment.First(o => o.Id == id);
				rc.State = State.删除;
				HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.Comment, -1);
				CurrentResource.Comment -= 1;
				HomoryContext.Value.SaveChanges();
				BindComment();
			}
			catch
			{

			}
		}

        protected void publish_attachment_list_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var resource = CurrentResource;
            var files = HomoryContext.Value.Resource.Single(o => o.Id == resource.Id).ResourceAttachment.OrderBy(o => o.Id).ToList();
            publish_attachment_list.DataSource = files;
            pppp1.Visible = pppp2.Visible = HomoryContext.Value.Resource.Single(o => o.Id == resource.Id).ResourceAttachment.Count > 0;
        }
    }
}
