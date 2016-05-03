using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;

public partial class Patch_WeChatBinding : Homory.Model.HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            combo.DataSource = HomoryContext.Value.Department.Where(o => o.State < State.审核 && o.Type == DepartmentType.学校).OrderBy(o => o.Ordinal).ToList();
            combo.DataBind();
            combo.SelectedIndex = 0;
            grid.Rebind();
        }
    }

    protected void combo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        grid.Rebind();
    }

    protected void grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var name = combo.SelectedItem.Text;
        var list = HomoryContext.Value.UserWeChat.Where(o => o.用户单位 == name && o.用户在编 && o.微信ID == null);
        grid.DataSource = combo.SelectedIndex > -1 ? list.ToList() : null;
    }
}