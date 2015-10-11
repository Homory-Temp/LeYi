using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoCampus : HomoryCorePageWithGrid
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                LogOp(OperationType.查询);
            }
		}

		protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
			grid.DataSource = HomoryContext.Value.Department.Where(o => o.State < State.删除 && o.ParentId == null).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
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
					var name = values["Name"].ToString();
                    var code = Get(values, "Code", "");
                    var ordinal = Get(values, "Ordinal", 99);
					var state = Get(values, "State", State.启用);
                    var classType = Get(values, "ClassType", ClassType.其他);
                    var buildType = Get(values, "BuildType", BuildType.教育部门社会集体办);
					switch (command.Type)
					{
						case GridBatchEditingCommandType.Insert:
                            var nid = HomoryContext.Value.GetId();
                            HomoryContext.Value.Department.Add(new Homory.Model.Department
                            {
                                Id = nid,
                                Name = name,
                                TopId = nid,
                                DisplayName = name,
                                Level = 0,
                                Hidden = false,
                                Ordinal = ordinal,
                                State = state,
                                Code = code,
                                BuildType = buildType,
                                ClassType = classType,
                                Type = DepartmentType.学校
                            });
                            try { DepartmentHelper.InsertCampus(nid.ToString().ToUpper(), name, ordinal); } catch { Notify(panel, "办公平台学校新增失败", "warn"); }
							HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
						case GridBatchEditingCommandType.Update:
							var id = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.Department.Where(o => o.Id == id).Update(o => new Homory.Model.Department
							{
								Name = name,
								DisplayName = name,
								Ordinal = ordinal,
                                Code = code,
								State = state,
                                BuildType = buildType,
                                ClassType = classType
                            });
                            try { DepartmentHelper.UpdateCampus(id.ToString().ToUpper(), name, ordinal, state); } catch { Notify(panel, "办公平台学校更新失败", "warn"); }
							HomoryContext.Value.SaveChanges();
                            LogOp(state);
                            break;
					}
				}
				Notify(panel, "操作成功", "success");
			}
			else
			{
				Notify(panel, "无权限设定学校", "warn");
			}
		}

		protected override string PageRight
		{
			get { return HomoryCoreConstant.RightGlobal; }
		}
	}
}
