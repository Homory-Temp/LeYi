using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoNote : HomoryCorePageWithGrid
    {
        private const string Right = "Note";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogOp(OperationType.查询);
            }
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grid.DataSource = HomoryContext.Value.Notice.Where(o => o.State < State.删除).OrderByDescending(o => o.Time).ToList();
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                try
                {
                    var values = command.NewValues;
                    if (NotSet(values, "Title"))
                        continue;
                    var title = values["Title"].ToString();
                    var state = Get(values, "State", State.启用);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Insert:
                            var newId = HomoryContext.Value.GetId();
                            HomoryContext.Value.Notice.Add(new Notice
                            {
                                Id = newId,
                                UserId = CurrentUser.Id,
                                Title = title,
                                Content = string.Empty,
                                State = state,
                                Time = DateTime.Now
                            });
                            HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
                        case GridBatchEditingCommandType.Update:
                            var id = Get(values, "Id", Guid.Empty);
                            HomoryContext.Value.Notice.Where(o => o.Id == id).Update(o => new Notice
                            {
                                Title = title,
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
