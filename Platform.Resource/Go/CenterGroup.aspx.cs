using Homory.Model;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoCenterGroup : HomoryResourcePage
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

		}

		protected override bool ShouldOnline
		{
			get { return true; }
		}

		protected void gList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			try
			{
				gList.DataSource = HomoryContext.Value.Group.Where(o => o.State < State.审核 && o.Type == GroupType.教研团队 && o.GroupUser.Count(p => p.Type == GroupUserType.创建者 && p.UserId == CurrentUser.Id && p.State < State.审核) > 0).OrderBy(o => o.Name).ToList();
				//gList.DataSource = HomoryContext.Value.Group.Where(o => o.State < State.审核 && o.Type == GroupType.教研团队).OrderBy(o => o.Name).ToList();
			}
			catch
			{

			}
		}

		protected void gListX_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			try
			{
				gListX.DataSource = HomoryContext.Value.Group.Where(o => o.State < State.审核 && o.Type == GroupType.教研团队 && o.GroupUser.Count(p => p.Type == GroupUserType.组成员 && p.UserId == CurrentUser.Id && p.State < State.审核) > 0).OrderBy(o => o.Name).ToList();
				//gList.DataSource = HomoryContext.Value.Group.Where(o => o.State < State.审核 && o.Type == GroupType.教研团队).OrderBy(o => o.Name).ToList();
			}
			catch
			{

			}
		}

		protected string CatalogName(object id)
		{
			if (id == null)
				return "未设定";
			var x = Guid.Parse(id.ToString());
			return HomoryContext.Value.Catalog.First(o => o.Id == x).Name;
		}

		protected void btnDel_OnServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor) sender).Attributes["data-id"]);
			var g = HomoryContext.Value.Group.First(o => o.Id == id);
			g.State = State.删除;
			HomoryContext.Value.SaveChanges();
			gList.Rebind();
		}

		protected void btnQuit_OnServerClick(object sender, EventArgs e)
		{
			var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
			var gu = HomoryContext.Value.GroupUser.First(o => o.GroupId == id && o.UserId == CurrentUser.Id && o.Type == GroupUserType.组成员);
			gu.State = State.删除;
			HomoryContext.Value.SaveChanges();
			gListX.Rebind();
		}
	}
}
