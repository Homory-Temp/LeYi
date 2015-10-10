using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;
using System.Web.UI.HtmlControls;

namespace Go
{
    public partial class GoCenter : HomoryResourcePage
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
            result.DataSource = HomoryContext.Value.Action.Where(o => o.Id3 == CurrentUser.Id && o.Type == ActionType.用户收藏资源 && o.State == State.启用).Select(o => o.Id2).ToList().Join(HomoryContext.Value.Resource.Where(o => o.State == State.启用), o => o, o => o.Id, (a, b) => b).ToList();
            result.DataBind();
        }

        protected override bool ShouldOnline
        {
            get { return true; }
        }

        protected void filterGo_OnServerClick(object sender, EventArgs e)
        {
            var content = filter.Value.Trim();
            result.DataSource = HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
            result.DataBind();
        }

        protected void del_ServerClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
            HomoryContext.Value.Resource.First(o => o.Id == id).Favourite--;
            var obj = HomoryContext.Value.Action.First(o => o.Id2 == id && o.Id3 != null && o.Id3 == CurrentUser.Id && o.State < State.审核 && o.Type == ActionType.用户收藏资源);
            obj.State = State.删除;
            HomoryContext.Value.ST_ResourceX(obj.Id2, ResourceOperationType.Favourite);
            HomoryContext.Value.SaveChanges();
            var content = filter.Value.Trim();
            result.DataSource = HomoryContext.Value.Resource.Where(o => o.UserId == CurrentUser.Id && o.State == State.启用).ToList().Where(o => o.Title.Contains(content)).ToList();
            result.DataBind();
        }
    }
}
