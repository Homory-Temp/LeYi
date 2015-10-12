using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_CenterRight : Homory.Model.HomoryResourceControl
{
    protected void addFavourite_OnClick(object sender, ImageClickEventArgs e)
    {
        try
        {
            var id = Guid.Parse(((ImageButton)sender).CommandArgument);
            if (HomoryContext.Value.UserFavourite.Count(o => o.UserId == CurrentUser.Id && o.FavouriteUserId == id) > 0)
            {
                var fav = HomoryContext.Value.UserFavourite.First(o => o.UserId == CurrentUser.Id && o.FavouriteUserId == id);
                fav.State = State.启用;
            }
            else
            {
                var fav = new UserFavourite { UserId = CurrentUser.Id, FavouriteUserId = id, State = State.启用 };
                HomoryContext.Value.UserFavourite.Add(fav);
            }
            HomoryContext.Value.SaveChanges();
        }
        catch
        {
        }
        favourites.Rebind();
        relatives.Rebind();
    }

    protected void removeFavourite_OnClick(object sender, ImageClickEventArgs e)
    {
        try
        {
            var id = Guid.Parse(((ImageButton)sender).CommandArgument);
            if (HomoryContext.Value.UserFavourite.Count(o => o.UserId == CurrentUser.Id && o.FavouriteUserId == id) > 0)
            {
                var fav = HomoryContext.Value.UserFavourite.First(o => o.UserId == CurrentUser.Id && o.FavouriteUserId == id);
                fav.State = State.删除;
            }
            else
            {
                var fav = new UserFavourite { UserId = CurrentUser.Id, FavouriteUserId = id, State = State.删除 };
                HomoryContext.Value.UserFavourite.Add(fav);
            }
            HomoryContext.Value.SaveChanges();
        }
        catch
        {
        }
        favourites.Rebind();
        relatives.Rebind();
    }

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

        //visit.ImageUrl = user.Icon;
        //visitTime.InnerText = DateTime.Now.ToString("HH:mm");
        time.Text = string.Format("{0} {1}", DateTime.Today.ToString("MM月dd日"),
            DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn")));
        board.DataSource = user.Resource.Where(o => o.State == State.启用).OrderByDescending(o => o.Rate).Take(5).ToList();
        board.DataBind();
        var favouritesSource = HomoryContext.Value.UserFavourite.Where(o => o.State == State.启用 && o.UserId == CurrentUser.Id).ToList();
        var favouritesSourceId = favouritesSource.Select(o => o.FavouriteUserId).ToList();
        favourites.DataSource = favouritesSource.Select(o => o.FavouriteUser).Take(3).ToList();
        favourites.DataBind();
        relatives.DataSource =
            HomoryContext.Value.User.Where(o => o.State < State.审核 && o.Type == UserType.教师 || o.Type == UserType.内置 || o.Type == UserType.注册).ToList().Where(o => !favouritesSourceId.Contains(o.Id)).Take(3).ToList();
        relatives.DataBind();

	    var t1 = CurrentUser.GroupUser.Where(o => o.State < State.审核).ToList();
	    var t2 = t1.Where(o => o.Group.Type == GroupType.教研团队 && o.State < State.审核).ToList().Select(o=>o.GroupId).ToList();
		var t3 = new List<Guid>();
	    foreach (var g in t2)
	    {
		    t3.AddRange(HomoryContext.Value.Catalog.Where(o=>o.TopId==g).Select(o=>o.Id).ToList());
	    }
	    var t4 = t3.Join(HomoryContext.Value.ViewResourceX, o => o, o => o.CatalogId, (a, b) => b.Id).ToList().Distinct();
	    groupRes.DataSource =
		    t4.Join(HomoryContext.Value.Resource, o => o, o => o.Id, (a, b) => b)
			    .OrderByDescending(o => o.Time)
			    .Take(5)
			    .ToList();
		groupRes.DataBind();
    }

    protected override bool ShouldOnline
    {
        get { return true; }
    }

    protected void favourites_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var favouritesSource = HomoryContext.Value.UserFavourite.Where(o => o.State == State.启用 && o.UserId == CurrentUser.Id).ToList();
        // var favouritesSource = CurrentUser.MeFavourite.Where(o => o.State == State.启用).ToList();
        favourites.DataSource = favouritesSource.Select(o => o.FavouriteUser).Take(3).ToList();
    }

    protected void relatives_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var favouritesSource = HomoryContext.Value.UserFavourite.Where(o => o.State == State.启用 && o.UserId == CurrentUser.Id).ToList();
        var favouritesSourceId = favouritesSource.Select(o => o.FavouriteUserId).ToList();
        relatives.DataSource =
            HomoryContext.Value.User.Where(o => o.State < State.审核).ToList().Where(o => !favouritesSourceId.Contains(o.Id)).Take(3).ToList();
    }
}
