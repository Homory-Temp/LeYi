using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;
using Telerik.Web.UI;

public partial class VIP_用户 : Homory.Model.HomoryCorePage
{
    protected override string PageRight
    {
        get
        {
            return HomoryCoreConstant.RightEveryone;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new[] { "Homory", "lj" }.Contains(CurrentUser.Account))
        {
            Response.Redirect("http://oa.wxlxjy.com:888/Sso/Go/SB.html");
        }
        if (!IsPostBack)
        {
            box.DataSource = HomoryContext.Value.Department.Where(o => o.Type == DepartmentType.学校 && o.State < State.审核).OrderBy(o => o.Ordinal).ToList();
            box.DataBind();
            box.SelectedIndex = 0;
            view.Rebind();
        }
    }

    protected void sub_ServerClick(object sender, EventArgs e)
    {
        Session["Sp_User_X"] = v.Value;
        if (Session["Sp_Org_X"] == null)
            Response.Redirect("../VIP/机构.aspx");
        else
            Response.Redirect("../VIP/关系.aspx");
    }

    protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        if (box.SelectedIndex < 0)
            return;
        view.DataSource = HomoryContext.Value.机构用户(Guid.Parse(box.SelectedValue)).OrderBy(o => o.用户部门).ToList();
    }

    protected void box_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        view.Rebind();
        ap.ResponseScripts.Add("matchUser();");
    }
}
