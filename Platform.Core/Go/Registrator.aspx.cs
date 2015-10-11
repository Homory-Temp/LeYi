using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoRegistrator : HomoryCorePageWithGrid
	{
		private const string Right = "Registrator";
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                LogOp(OperationType.查询);
            }
		}

		protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			var list =
				HomoryContext.Value.User.Where(o => o.State < State.删除 && o.State > State.内置 && o.Type == UserType.注册)
					.OrderBy(o => o.State)
					.ThenBy(o => o.Account)
					.ToList();
			var query = peek.Text;
			grid.DataSource =
				list.Where(
					o =>
						o.Account.Contains(query) || o.DisplayName.Contains(query) || o.PinYin.Contains(query) || (o.RealName != null && o.RealName.Contains(query)))
					.ToList();
        }

		protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
		{
			foreach (var command in e.Commands)
			{
				var values = command.NewValues;
				var state = Get(values, "State", State.启用);
				switch (command.Type)
				{
					case GridBatchEditingCommandType.Update:
						{
							var key = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.User.Where(o => o.Id == key).Update(o => new User { State = state });
							HomoryContext.Value.SaveChanges();
                            LogOp(state);
                        }
						break;
				}
			}
			Notify(panel, "操作成功", "success");
		}

		protected void peek_Search(object sender, SearchBoxEventArgs e)
		{
			grid.Rebind();
			
        }

		protected override string PageRight
		{
			get { return Right; }
		}
	}
}
