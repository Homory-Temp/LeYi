using Homory.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoPersonal : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeHomoryPage();
                PersonalActionPersonal.ActionUserId = TargetUser.Id;
            }
        }

        private User _target;

        protected User TargetUser
        {
            get
            {
                if (_target == null)
                {
                    var id = Guid.Parse(Request.QueryString["Id"]);
                    _target = HomoryContext.Value.User.First(o => o.Id == id);
                }
                return _target;
            }
        }

        protected bool IsTargetMaster
        {
            get
            {
                var _isMaster =
                             TargetUser.DepartmentUser.Count(o => o.Type == DepartmentUserType.班级班主任 && o.State == State.启用) > 0;
                return _isMaster;
            }
        }

        protected void InitializeHomoryPage()
        {
            var user = TargetUser;
            icon.ImageUrl = P(user.Icon);
            name.Text = string.Format("{0}&nbsp;{1}", UC(user.Id), user.DisplayName);
            count1.Text = HomoryContext.Value.UserFavourite.Count(o => o.UserId == user.Id && o.State == State.启用).ToString();
            count2.Text = HomoryContext.Value.UserFavourite.Count(o => o.FavouriteUserId == user.Id && o.State == State.启用).ToString();
            count3.Text = HomoryContext.Value.Action.Count(o => o.Id3 == user.Id && o.Type == ActionType.用户收藏资源 && o.State == State.启用).ToString();

            var query = HomoryContext.Value.ResourceLog.Where(o => o.Id == user.Id);
            Label1.Text = query.Count() == 0 ? "0" : query.Sum(o => o.Media).ToString();
            Label2.Text = query.Count() == 0 ? "0" : query.Sum(o => o.Article).ToString();
            Label3.Text = query.Count() == 0 ? "0" : query.Sum(o => o.Courseware).ToString();
            Label4.Text = query.Count() == 0 ? "0" : query.Sum(o => o.Paper).ToString();

            saying.InnerText = sayings[new Random().Next(0, sayings.Length - 1)];

            var favouritesSource = TargetUser.MeFavourite.Where(o => o.State == State.启用).ToList();
            var favouritesSourceId = favouritesSource.Select(o => o.FavouriteUserId).ToList();
            //favourites.DataSource = favouritesSource.Select(o => o.FavouriteUser).ToList();
            //favourites.DataBind();
            //relatives.DataSource =
            //    HomoryContext.Value.User.Where(o => o.State < State.审核).ToList().Where(o => !favouritesSourceId.Contains(o.Id)).ToList();
            //relatives.DataBind();

            latest.DataSource =
    TargetUser.Resource.Where(o => o.State == State.启用)
        .OrderByDescending(o => o.Time)
        .Take(9)
        .ToList();
            latest.DataBind();
            popular.DataSource =
            TargetUser.Resource.Where(o => o.State == State.启用)
                .OrderByDescending(o => o.View)
                .Take(9)
                .ToList();
            popular.DataBind();
            best.DataSource =
    TargetUser.Resource.Where(o => o.State == State.启用)
        .OrderByDescending(o => o.Grade)
        .Take(9)
        .ToList();
            best.DataBind();

            if (!IsOnline || HomoryContext.Value.UserFavourite.Count(o => o.UserId == CurrentUser.Id && o.FavouriteUserId == TargetUser.Id && o.State == State.启用) == 0)
            {
                h_fav.Text = "+ 关注";
            }
            else
            {
                h_fav.Text = "- 取消关注";
            }

            if (IsOnline)
            {
                var action = HomoryContext.Value.Action.FirstOrDefault(o => o.Id1 == TargetUser.Id && o.Id2 == CurrentUser.Id && o.Type == ActionType.用户访问用户);
                if (action == null)
                {
                    action = new Homory.Model.Action
                    {
                        Id = HomoryContext.Value.GetId(),
                        Id1 = TargetUser.Id,
                        Id2 = CurrentUser.Id,
                        Type = ActionType.用户访问用户,
                        State = State.启用,
                        Time = DateTime.Now,
                    };
                    HomoryContext.Value.Action.Add(action);
                }
                else
                {
                    action.Time = DateTime.Now;
                }
            }
            var vcq = HomoryContext.Value.ResourceLog.Where(o => o.Id == TargetUser.Id);
            viewCount.Text = vcq.Count() == 0 ? "0" : vcq.Sum(o => o.View).ToString();
            viewList.DataSource = HomoryContext.Value.Action.Where(o => o.Id1 == TargetUser.Id && o.Type == ActionType.用户访问用户).OrderByDescending(q => q.Time)
                    .Take(9)
                    .ToList();
            viewList.DataBind();

            HomoryContext.Value.SaveChanges();

            if (IsOnline && CurrentUser.Id == TargetUser.Id)
            {
                h_fav.Visible = false;
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void refreshFavourite_OnServerClick(object sender, EventArgs e)
        {
        }

        protected string[] sayings = new[] { "不是生活决定何种品味，而是何种品味决定何种生活。", "不要生气要争气，不要看破要突破，不要嫉妒要欣赏，不要拖延要积极。", "机会从来就不会失去，你错过的别人会接住。", "沉默不是指一味的不说话，沉不住气的人容易失败，适时的沉默是一种智慧、一种技巧、一种优势在握的心态。", "爱人是路，朋友是树，人生只有一条路，一条路上有多颗树，有钱的时候莫忘树，缺钱的时候靠靠树，幸福的时候别迷路，休息的时候浇浇树。", "若要优美的嘴唇，就要多讲亲切的话语，若要可爱的眼神，就要看到别人的好处，若要苗条的身材，就要把你的食物分给饥饿的人。", "心态决定状态，心胸决定格局，眼界决定境界。", "对朋友的失败，请闭上你的眼睛，但对他的劣迹，请睁开眼睛。", "木桶最短一节决定其容量，铁链最弱一环决定其强度，人最大的缺点决定其是否成功。", "人生为棋，我愿为卒，行动虽慢，可谁见我都会后退一步。", "用快乐去奔跑，用心去倾听，用思维去发展，用努力去奋斗，用目标去衡量，用爱去生活。", "真正的好朋友，并不是在一起有聊不玩的话题，而是在一起，就算不说话也不会觉得尴尬。", "在思想上大手大脚，在生活上适可而止。", "对亲人最大的爱，是看好自己别惹事。", "性格本身没有好坏之分，乐观和悲观对这个世界都有贡献，前者发明了飞机，后者发明了降落伞。", "停下来休息的时候不要忘记别人还在奔跑。", "嫉妒是一把刀，最后不是插在别人身上，就是插进自己心里。", "一个输不起的人，往往就是赢不了的人。", "年轻的朋友，请不要把生活当作风景来欣赏，也不要把生活当作沙发来坐卧。", "朋友落难的时候，主动伸手去拉一把，自己不顺的时候，尽量不要去麻烦朋友，这是交友之道也是为人之道。", "成功者总是处境不佳时候振作起来，从而改变处境，失败者总是在处境不佳时自暴自弃，结果处境更糟糕。", "过错是短暂的遗憾，错过是永远的遗憾。", "大海倘若没有千竿深度，哪有如山浪头。", "雨骤，打不湿鸭子的翅膀，风狂，吹不灭萤火的灯光。", "当你以为自己一无所有时，你至少还有时间，时间能抚平一切创伤，所以请不要流泪。" };

        protected void countPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var i = int.Parse(e.Argument);
            count2.Text = (int.Parse(count2.Text) + i).ToString();
        }

        protected void h_fav_Click(object sender, EventArgs e)
        {
            if (!IsOnline)
            {
                SignOn();
                return;
            }
            if (h_fav.Text == "+ 关注")
            {
                HomoryContext.Value.UserFavourite.AddOrUpdate(new UserFavourite { UserId = CurrentUser.Id, FavouriteUserId = TargetUser.Id, State = State.启用 });
                HomoryContext.Value.SaveChanges();
                h_fav.Text = "- 取消关注";
                countPanel.RaisePostBackEvent("1");
            }
            else
            {
                HomoryContext.Value.UserFavourite.AddOrUpdate(new UserFavourite { UserId = CurrentUser.Id, FavouriteUserId = TargetUser.Id, State = State.删除 });
                HomoryContext.Value.SaveChanges();
                h_fav.Text = "+ 关注";
                countPanel.RaisePostBackEvent("-1");
            }
        }

        protected void result1_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var user = TargetUser;
            result1.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.视频 && o.UserId == user.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
        }

        protected void result2_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var user = TargetUser;
            result2.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.文章 && o.UserId == user.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
        }

        protected void result3_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var user = TargetUser;
            result3.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.课件 && o.UserId == user.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
        }

        protected void result4_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var user = TargetUser;
            result4.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.试卷 && o.UserId == user.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
        }
    }
}
