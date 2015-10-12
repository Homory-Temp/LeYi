using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoAppManage : HomoryCorePageWithGrid
    {
        private const string Right = "Application";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogOp(OperationType.查询);
            }
        }

        protected string GenText(Application app)
        {
            var r = app.ApplicationRole.Aggregate("&nbsp;", (a, b) => string.Format("{0}&nbsp;{1}", a, b.UserType.ToString()));
            return string.IsNullOrWhiteSpace(r.Replace("&nbsp;", "").Trim()) ? "（无）" : r;
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
			grid.DataSource = HomoryContext.Value.Application.Where(o => o.Type == ApplicationType.平台 && o.State < State.删除).OrderBy(o => o.Ordinal).ToList();
        }

        protected string FormatIcon(string icon)
        {
            return string.IsNullOrWhiteSpace(icon) ? "~/Common/默认/群组.png" : string.Format("{0}?{1}", icon, Guid.NewGuid().ToString("N"));
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
                    var home = Get(values, "Home", string.Empty);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Insert:
							var newId = HomoryContext.Value.GetId();
							HomoryContext.Value.Application.Add(new Application
                            {
                                Id = newId,
                                Name = name,
                                Type = ApplicationType.平台,
                                Icon = "~/Common/默认/群组.png",
                                Ordinal = ordinal,
                                State = state,
                                Home = home
                            });
							HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
                        case GridBatchEditingCommandType.Update:
                            var id = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.Application.Where(o => o.Id == id).Update(o => new Application
                            {
                                Name = name,
                                Ordinal = ordinal,
                                State = state,
                                Home = home
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
