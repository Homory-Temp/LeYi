using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Control
{
    public partial class ControlPersonalAction : HomoryResourceControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Func<Homory.Model.Action, bool> func = o => (o.Type == ActionType.用户评分资源 || o.Type == ActionType.用户评论资源 || o.Type == ActionType.用户回复评论) && o.State == State.启用;
                if (HomeCampus == null)
                {
                    actions.DataSource = HomoryContext.Value.Action.Where(func).OrderByDescending(o => o.Time).Take(8).ToList();
                    actions.DataBind();
                }
                else
                {
                    var total = HomoryContext.Value.Action.Count(func);
                    var bunch = 0;
                    var step = 100;
                    var full = false;
                    var predicate = SU();
                    var users = HomoryContext.Value.User.Where(predicate).Select(o => o.Id).ToList();
                    List<Homory.Model.Action> list = new List<Homory.Model.Action>();
                    while (!full && bunch <= total)
                    {
                        var skip = bunch * step;
                        var current = HomoryContext.Value.Action.Where(func).OrderByDescending(o => o.Time).Skip(skip).Take(step);
                        list.AddRange(current.Where(o => users.Contains(U(o.Id1).Id) || users.Contains(U(o.Id3).Id) || R(o.Id2).User.DepartmentUser.Count(x => x.TopDepartmentId == HomeCampus.Id && x.State < State.审核 && (x.Type == DepartmentUserType.借调后部门主职教师 || x.Type == DepartmentUserType.部门主职教师)) > 0).ToList());
                        if (list.Count >= 8)
                            full = true;
                        else
                            bunch++;
                    }
                    actions.DataSource = list.OrderByDescending(o => o.Time).Take(8).ToList();
                    actions.DataBind();
                }
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }
    }
}
