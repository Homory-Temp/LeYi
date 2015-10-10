using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;
using System.Data.Entity.Migrations;

namespace Go
{
	public partial class GoStudio : HomoryCorePageWithGrid
    {
        private const string Right = "Studio";

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
			grid.DataSource = HomoryContext.Value.Group.Where(o => o.Type == GroupType.名师团队 && o.State == State.启用).OrderBy(o => o.Ordinal).ToList();
        }

        protected string FormatIcon(string icon)
        {
            return string.IsNullOrWhiteSpace(icon) ? "~/Images/Common/IconUser.png" : string.Format("{0}?{1}", icon, Guid.NewGuid().ToString("N"));
        }

        protected string LoadLeader(Guid id)
        {
			return HomoryContext.Value.GroupUser.Count(o => o.Type == GroupUserType.创建者 && o.State == State.启用 && o.GroupId == id) == 0 ?
				"（未选择）" : HomoryContext.Value.GroupUser.First(o => o.Type == GroupUserType.创建者 && o.State == State.启用 && o.GroupId == id).User.RealName;
        }

        protected string LoadMember(Guid id)
        {
			return HomoryContext.Value.GroupUser.Count(o => o.Type == GroupUserType.组成员 && o.State == State.启用 && o.GroupId == id) == 0 ?
				"（未选择）" : HomoryContext.Value.GroupUser.Where(o => o.Type == GroupUserType.组成员 && o.State == State.启用 && o.GroupId == id).ToList().OrderBy(o => o.Ordinal).Select(o => o.User).ToList().Select(o => o.RealName).ToList().Aggregate((o1, o2) => string.Format("{0}、{1}", o1, o2));
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                try
                {
                    var values = command.NewValues;
                    if (NotSet(values, "Name"))
                        continue;
                    var name = values["Name"].ToString();
                    var ordinal = Get(values, "Ordinal", 99);
                    var state = Get(values, "State", State.启用);
                    var introduction = Get(values, "Introduction", string.Empty);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Insert:
							var newId = HomoryContext.Value.GetId();
							HomoryContext.Value.Group.Add(new Group
                            {
                                Id = newId,
                                Name = name,
                                Serial = string.Empty,
                                Type = GroupType.名师团队,
                                Icon = "~/Common/默认/群组.png",
                                OpenType = OpenType.互联网,
                                Ordinal = ordinal,
                                State = state,
                                Introduction = introduction
                            });
                            var root = new Catalog { Id = newId, Name = string.Format("{0}栏目", name), Ordinal = 0, ParentId = null, TopId = newId, State = State.启用, Type = CatalogType.团队_名师 };
							HomoryContext.Value.Catalog.AddOrUpdate(root);
							HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
                        case GridBatchEditingCommandType.Update:
                            var id = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.Group.Where(o => o.Id == id).Update(o => new Group
                            {
                                Name = name,
                                Ordinal = ordinal,
                                State = state,
                                Introduction = introduction
                            });
                            var rootX = new Catalog { Id = id, Name = string.Format("{0}栏目", name), Ordinal = 0, ParentId = null, TopId = id, State = State.启用, Type = CatalogType.团队_名师 };
							HomoryContext.Value.Catalog.AddOrUpdate(rootX);
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
