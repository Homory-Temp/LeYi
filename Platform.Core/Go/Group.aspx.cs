using System;
using System.Globalization;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoGroup : HomoryCorePageWithGrid
    {
        private const string Right = "Group";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInit();
                LogOp(OperationType.查询);
            }
        }

        private void LoadInit()
        {
			loading.InitialDelayTime = int.Parse("Busy".FromWebConfig());
			
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
			grid.DataSource = HomoryContext.Value.Group.Where(o => o.Type == GroupType.教研团队 && o.State < State.删除).OrderBy(o => o.Ordinal).ToList();
        }

        protected string LoadLeader(Guid id)
        {
			return HomoryContext.Value.GroupUser.First(o => o.Type == GroupUserType.创建者 && o.State == State.启用 && o.GroupId == id).User.RealName;
        }

        protected string LoadMember(Guid id)
        {
			return (HomoryContext.Value.GroupUser.Count(o => o.State < State.审核 && o.GroupId == id)).ToString(CultureInfo.InvariantCulture);
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                try
                {
                    var values = command.NewValues;
                    var ordinal = Get(values, "Ordinal", 99);
                    var state = Get(values, "State", State.启用);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Update:
                            var id = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.Group.Where(o => o.Id == id).Update(o => new Group
                            {
                                Ordinal = ordinal,
                                State = state,
                            });
							HomoryContext.Value.SaveChanges();
                            LogOp(state);
                            break;
                    }
                }
// ReSharper disable EmptyGeneralCatchClause
                catch
// ReSharper restore EmptyGeneralCatchClause
                {
                }
            }
            Notify(panel, "操作成功", "success");
        }

        protected override string PageRight
        {
            get { return Right; }
        }
    }
}
