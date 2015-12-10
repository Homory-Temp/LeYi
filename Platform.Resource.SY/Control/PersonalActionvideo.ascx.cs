using Homory.Model;
using System;
using System.Linq;

namespace Control
{
    public partial class ControlPersonalActionvideo : HomoryResourceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                actions.DataSource = HomoryContext.Value.Resource.Where(o => o.Type == ResourceType.视频 && o.UserId == CurrentUser.Id && o.State == State.启用).OrderByDescending(o => o.Time).ToList();
                actions.DataBind();
            }
        }

        protected override bool ShouldOnline
        {
            get { return true; }
        }
    }
}
