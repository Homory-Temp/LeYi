using System.Data.Entity.Migrations;
using System;
using System.Linq;
using Telerik.Web.UI;
using Homory.Model;

namespace Go
{
	public partial class GoRole : HomoryCorePageWithGrid
    {
        private const string Right = "Role";

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
			
            right.Visible = false;
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var source = HomoryContext.Value.Role.Where(o => o.State < State.删除 && o.State > State.内置).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
            if (source.Count > 0)
            {
                grid.SelectedIndexes.Add(0);
                InitRight(source.First().Id);
                right.Visible = true;
            }
            grid.DataSource = source;
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                var values = command.NewValues;
                if (NotSet(values, "Name"))
                    continue;
                var ordinal = Get(values, "Ordinal", 1);
                var name = Get(values, "Name", string.Empty);
                var state = Get(values, "State", State.启用);
                switch (command.Type)
                {
                    case GridBatchEditingCommandType.Insert:
                        {
                            var role = new Role
                            {
								Id = HomoryContext.Value.GetId(),
                                Name = name,
                                State = state,
                                Ordinal = ordinal
                            };
							HomoryContext.Value.Role.Add(role);
							HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                        }
                        break;
                    case GridBatchEditingCommandType.Update:
                        {
                            var id = Get(values, "Id", Guid.Empty);
							var role = HomoryContext.Value.Role.Single(o => o.Id == id);
                            role.Name = name;
                            role.State = state;
                            role.Ordinal = ordinal;
							HomoryContext.Value.SaveChanges();
                            LogOp(state);
                        }
                        break;
                }
            }
            Notify(panel, "操作成功", "success");
        }

        protected void grid_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                right.Visible = true;
                gid.Value = ((GridEditableItem)e.Item).GetDataKeyValue("Id").ToString();
                var key = Guid.Parse(gid.Value);
                InitRight(key);
            }
        }

        private void InitRight(Guid key)
        {
			var rights = HomoryContext.Value.RoleRight.Where(o => o.RoleId == key && o.State < State.审核).Select(o => o.RightName).ToList();
			foreach (var x in HomoryContext.Value.Right.Where(o => o.ApplicationId == HomoryCoreConstant.ApplicationKey).ToList().Select(rightName => rightName.Name))
            {
                var button = FindControl(x) as RadButton;
	            if (button == null)
		            continue;
	            button.SelectedToggleStateIndex = rights.Contains(button.ID) ? 0 : 1;
            }
        }

        protected void Right_OnClick(object sender, EventArgs e)
        {
            var roleId = Guid.Parse(gid.Value);
            var button = sender as RadButton;
// ReSharper disable PossibleNullReferenceException
            var state = button.SelectedToggleStateIndex == 0 ? State.启用 : State.删除;
// ReSharper restore PossibleNullReferenceException
            var rr = new RoleRight
            {
                RoleId = roleId,
                RightName = button.ID,
                State = state
            };
			HomoryContext.Value.RoleRight.AddOrUpdate(rr);
			HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
        }

        protected override string PageRight
        {
            get { return Right; }
        }
    }
}
