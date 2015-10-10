using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoRooms : HomoryCorePageWithGrid
	{
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
			grid.DataSource = HomoryContext.Value.ResourceRoom.Where(o => o.State < State.删除).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
        }

		protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
		{
			if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
			{
				foreach (var command in e.Commands)
				{
					var values = command.NewValues;
					if (NotSet(values, "Name"))
						continue;
					if (NotSet(values, "Ordinal"))
						continue;
					var name = values["Name"].ToString();
					var ordinal = Get(values, "Ordinal", 99);
					var description = Get(values, "Description", "");
					var url = Get(values, "Url", "");
					var state = Get(values, "State", State.启用);
					switch (command.Type)
					{
						case GridBatchEditingCommandType.Insert:
                            var nid = HomoryContext.Value.GetId();
							HomoryContext.Value.ResourceRoom.Add(new Homory.Model.ResourceRoom
							{
								Id = nid,
								Name = name,
                                Description = description,
								Url = url,
								Ordinal = ordinal,
								State = state,
							});
							HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
						case GridBatchEditingCommandType.Update:
							var id = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.ResourceRoom.Where(o => o.Id == id).Update(o => new Homory.Model.ResourceRoom
							{
								Name = name,
								Description = description,
								Url = url,
								Ordinal = ordinal,
								State = state,
							});
							HomoryContext.Value.SaveChanges();
                            LogOp(state);
                            break;
					}
				}
				Notify(panel, "操作成功", "success");
			}
			else
			{
				Notify(panel, "无权限设定直播间", "warn");
			}
		}

		protected override string PageRight
		{
			get { return "Rooms"; }
		}
	}
}
