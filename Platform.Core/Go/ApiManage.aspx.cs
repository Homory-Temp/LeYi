using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoApiManage : HomoryCorePageWithGrid
    {
        private const string Right = "Api";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogOp(OperationType.查询);
            }
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
	        var item = c.SelectedItem;
	        if (item == null)
		        grid.DataSource = null;
	        else
	        {
		        var gid = Guid.Parse(item.Value);
				grid.DataSource = HomoryContext.Value.Api.Where(o => o.Id == gid && o.State < State.删除).OrderBy(o => o.ProviderId).ToList();
			}
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
			var item = c.SelectedItem;
	        if (item == null)
		        return;
	        var cv = Guid.Parse(item.Value);
			foreach (var command in e.Commands)
            {
                try
                {
                    var values = command.NewValues;
                    if (NotSet(values, "ProviderId"))
                        continue;
					var providerId = values["ProviderId"].ToString();
                    var state = Get(values, "State", State.启用);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Insert:
							HomoryContext.Value.Api.Add(new Api
                            {
                                Id = cv,
                                State = state,
                                ProviderId = providerId,
							    ProviderKey = Guid.NewGuid()
                            });
							HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
                        case GridBatchEditingCommandType.Update:
							var providerIdE = values["ProviderId"].ToString();
							HomoryContext.Value.Api.Where(o => o.Id == cv && o.ProviderId == providerIdE).Update(o => new Api
                            {
                                State = state
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

		protected void c_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			grid.Rebind();
		}
    }
}
