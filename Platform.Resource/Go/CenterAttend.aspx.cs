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
    public partial class GoCenterAttend : HomoryResourcePage
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

            var meFavouriteList = HomoryContext.Value.UserFavourite.Where(o => o.UserId == CurrentUser.Id && o.State < State.审核).Select(o => o.FavouriteUserId).Distinct().ToList();
            var favouriteMeList = HomoryContext.Value.UserFavourite.Where(o => o.FavouriteUserId == CurrentUser.Id && o.State < State.审核).Select(o => o.UserId).Distinct().ToList();
            var bothList = meFavouriteList.Intersect(favouriteMeList);
            var l1 = meFavouriteList.Except(bothList);
            var l2 = favouriteMeList.Except(bothList);

            both.DataSource = bothList.Join(HomoryContext.Value.User.Where(o => o.State < State.审核 && o.Type != UserType.学生), o => o, o => o.Id, (a, b) => b).ToList();
            both.DataBind();
            positive.DataSource = l1.Join(HomoryContext.Value.User.Where(o => o.State < State.审核 && o.Type != UserType.学生), o => o, o => o.Id, (a, b) => b).ToList();
            positive.DataBind();
            negative.DataSource = l2.Join(HomoryContext.Value.User.Where(o => o.State < State.审核 && o.Type != UserType.学生), o => o, o => o.Id, (a, b) => b).ToList();
            negative.DataBind();

        }

        protected override bool ShouldOnline
        {
            get { return true; }
        }

        protected void refreshFavourite_OnServerClick(object sender, EventArgs e)
        {
        }

        protected void del_ServerClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
            var obj = HomoryContext.Value.Action.First(o => o.Id == id);
            obj.State = State.删除;
            HomoryContext.Value.ST_ResourceX(obj.Id2, ResourceOperationType.Favourite);
            HomoryContext.Value.SaveChanges();
        }

        protected void removeAttend1_ServerClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
            HomoryContext.Value.UserFavourite.AddOrUpdate(new UserFavourite { UserId = CurrentUser.Id, FavouriteUserId = id, State = State.删除 });
            HomoryContext.Value.SaveChanges();
            up2.RaisePostBackEvent("Refresh");
            up3.RaisePostBackEvent("Refresh");
        }

        protected void removeAttend2_ServerClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
            HomoryContext.Value.UserFavourite.AddOrUpdate(new UserFavourite { UserId = CurrentUser.Id, FavouriteUserId = id, State = State.删除 });
            HomoryContext.Value.SaveChanges();
            up1.RaisePostBackEvent("Refresh");
            up3.RaisePostBackEvent("Refresh");
        }

        protected void addAttend_ServerClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
            HomoryContext.Value.UserFavourite.AddOrUpdate(new UserFavourite { UserId = CurrentUser.Id, FavouriteUserId = id, State = State.启用 });
            HomoryContext.Value.SaveChanges();
            up1.RaisePostBackEvent("Refresh");
            up2.RaisePostBackEvent("Refresh");
        }

        protected void ajax1(object sender, AjaxRequestEventArgs e)
        {
            InitializeHomoryPage();
        }

        protected void ajax2(object sender, AjaxRequestEventArgs e)
        {
            InitializeHomoryPage();
        }

        protected void ajax3(object sender, AjaxRequestEventArgs e)
        {
            InitializeHomoryPage();
        }
    }
}
