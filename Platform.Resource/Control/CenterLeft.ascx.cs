using Homory.Model;
using System;
using System.Linq;

public partial class Control_CenterLeft : Homory.Model.HomoryResourceControl
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

        icon.ImageUrl = P(user.Icon);
        name.Text = user.DisplayName;
        favourite.Text = HomoryContext.Value.UserFavourite.Count(o => o.FavouriteUserId == user.Id && o.State == State.启用).ToString();
        honor.Text = HomoryContext.Value.ViewTS.First(o => o.Id == user.Id).Credit.ToString();

        fav.Text = HomoryContext.Value.Action.Count(o => o.Id3 == CurrentUser.Id && o.Type == ActionType.用户收藏资源 && o.State == State.启用).ToString();

		cc1.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.文章).ToString();
		cc2.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.课件).ToString();
		cc3.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.试卷).ToString();
		cc4.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.视频).ToString();
	}

    protected override bool ShouldOnline
    {
        get { return true; }
    }

	public void ReBindCenterLeft()
	{
		cc1.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.文章).ToString();
		cc2.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.课件).ToString();
		cc3.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.试卷).ToString();
		cc4.InnerText = HomoryContext.Value.Resource.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核 && o.Type == ResourceType.视频).ToString();
	}
}