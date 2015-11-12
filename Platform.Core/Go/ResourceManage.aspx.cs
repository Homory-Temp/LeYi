using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoResourceManage : HomoryCorePageWithGrid
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                BindCombo();
                InitCombo();
                grid.Rebind();
                LogOp(OperationType.查询);
            }
		}

        protected string U(Guid id)
        {
            return HomoryContext.Value.User.First(o => o.Id == id).RealName;
        }

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
        }

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grid.Rebind();
        }

        private void BindCombo()
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            else
            {
                var c = CurrentCampus;
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.Id == c.Id)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            combo.DataBind();
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
            var cid = combo.SelectedValue;
            try
            {
                var cidg = Guid.Parse(cid);
                grid.DataSource = HomoryContext.Value.Resource.Where(o => o.State < State.审核 && o.CampusId == cidg).OrderByDescending(o => o.Time).ToList();
            }
            catch
            {
                grid.DataSource = HomoryContext.Value.Resource.Where(o => o.State < State.审核).OrderByDescending(o => o.Time).ToList();
            }
        }

		protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
		{
			if (CurrentRights.Contains(PageRight))
			{
				foreach (var command in e.Commands)
				{
					var values = command.NewValues;
					if (NotSet(values, "Title"))
						continue;
					var name = values["Title"].ToString();
					var state = Get(values, "State", State.启用);
					switch (command.Type)
					{
						case GridBatchEditingCommandType.Update:
							var id = Get(values, "Id", Guid.Empty);
							HomoryContext.Value.Resource.Where(o => o.Id == id).Update(o => new Homory.Model.Resource
                            {
								Title = name,
								State = state
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
				Notify(panel, "无权限编辑", "warn");
			}
		}

		protected override string PageRight
		{
			get { return "ResourceManage"; }
		}
	}
}
