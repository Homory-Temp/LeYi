using System;
using System.Linq;
using Homory.Model;

namespace Control
{
    public partial class ControlPersonalActionPersonal : HomoryResourceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        public Guid ActionUserId
        {
            get;
            set;
        }

        public int? PAPType
        {
            get;
            set;
        }

        protected Func<string, ResourceCatalog, string> Combine = (a, o) => string.Format("{0}{1}、", a, o.Catalog.Name);

        protected override bool ShouldOnline
        {
            get { return true; }
        }
        protected void resultX_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            if (PAPType.HasValue)
            {
                switch (PAPType.Value)
                {
                    case 1:
                        {
                            actions.DataSource = HomoryContext.Value.Action.Where(o => o.Type == ActionType.用户评论资源 && o.State == State.启用 && (o.Id1 == ActionUserId || o.Id3 == ActionUserId)).OrderByDescending(o => o.Time).ToList();
                            break;
                        }
                    case 2:
                        {
                            actions.DataSource = HomoryContext.Value.Action.Where(o => o.Type == ActionType.用户评分资源 && o.State == State.启用 && (o.Id1 == ActionUserId || o.Id3 == ActionUserId)).OrderByDescending(o => o.Time).ToList();
                            break;
                        }
                    default:
                        {
                            actions.DataSource = HomoryContext.Value.Action.Where(o => o.Type == ActionType.用户回复评论 && o.State == State.启用 && (o.Id1 == ActionUserId || o.Id3 == ActionUserId)).OrderByDescending(o => o.Time).ToList();
                            break;
                        }
                }
            }
            else
            {
                actions.DataSource = HomoryContext.Value.Action.Where(o => (o.Type == ActionType.用户评分资源 || o.Type == ActionType.用户评论资源 || o.Type == ActionType.用户回复评论) && o.State == State.启用 && (o.Id1 == ActionUserId || o.Id3 == ActionUserId)).OrderByDescending(o => o.Time).ToList();
            }
        }
    }
}
