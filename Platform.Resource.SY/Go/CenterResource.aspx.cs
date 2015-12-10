using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoCenterResource : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				InitializeHomoryPage();
			}
		}

		protected void InitializeHomoryPage()
		{
			var user = CurrentUser;
			result.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.文章 && o.UserId == CurrentUser.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
			result.DataBind();
			Repeater1.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.课件 && o.UserId == CurrentUser.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
			Repeater1.DataBind();
			Repeater2.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.试卷 && o.UserId == CurrentUser.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
			Repeater2.DataBind();
			Repeater3.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.视频 && o.UserId == CurrentUser.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
			Repeater3.DataBind();
		}

		protected override bool ShouldOnline
		{
			get { return true; }
		}

		protected void filterGo_OnServerClick(object sender, EventArgs e)
		{
			var content = filter.Value.Trim();
			result.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.文章 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			result.DataBind();
			Repeater1.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.课件 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			Repeater1.DataBind();
			Repeater2.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.试卷 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			Repeater2.DataBind();
			Repeater3.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.视频 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			Repeater3.DataBind();
		}

		protected void del1_ServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			HomoryContext.Value.Action.Where(o => o.Id1 == id || o.Id2 == id || o.Id3 == id).Update(o => new Homory.Model.Action { State = State.删除 });
			HomoryContext.Value.Resource.Where(o => o.Id == id).Update(o => new Resource { State = State.删除 });
			HomoryContext.Value.SaveChanges();
			var content = filter.Value.Trim();
			Repeater3.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.视频 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			Repeater3.DataBind();
			LEFT.RaisePostBackEvent("Re");
		}
		protected void del2_ServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			HomoryContext.Value.Action.Where(o => o.Id1 == id || o.Id2 == id || o.Id3 == id).Update(o => new Homory.Model.Action { State = State.删除 });
			HomoryContext.Value.Resource.Where(o => o.Id == id).Update(o => new Resource { State = State.删除 });
			HomoryContext.Value.SaveChanges();
			var content = filter.Value.Trim();
			result.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.文章 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			result.DataBind();
			LEFT.RaisePostBackEvent("Re");
		}
		protected void del3_ServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			HomoryContext.Value.Action.Where(o => o.Id1 == id || o.Id2 == id || o.Id3 == id).Update(o => new Homory.Model.Action { State = State.删除 });
			HomoryContext.Value.Resource.Where(o => o.Id == id).Update(o => new Resource { State = State.删除 });
			HomoryContext.Value.SaveChanges();
			var content = filter.Value.Trim();
			Repeater1.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.课件 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			Repeater1.DataBind();
			LEFT.RaisePostBackEvent("Re");
		}
		protected void del4_ServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			HomoryContext.Value.Action.Where(o => o.Id1 == id || o.Id2 == id || o.Id3 == id).Update(o => new Homory.Model.Action { State = State.删除 });
			HomoryContext.Value.Resource.Where(o => o.Id == id).Update(o => new Resource { State = State.删除 });
			HomoryContext.Value.SaveChanges();
			var content = filter.Value.Trim();
			Repeater2.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.试卷 && o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
			Repeater2.DataBind();
			LEFT.RaisePostBackEvent("Re");
		}
		protected void LEFT_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			CenterLeftControl.ReBindCenterLeft();
		}
	}
}
