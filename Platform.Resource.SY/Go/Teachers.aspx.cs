using Homory.Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Go_Teachers : HomoryResourcePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            combo.DataSource = HomoryContext.Value.Department.Where(o => o.Type == DepartmentType.学校 && o.State < State.审核).OrderBy(o => o.ClassType).ThenBy(o => o.Ordinal).ToList();
            combo.DataBind();
            Bind();
        }
    }

    protected override bool ShouldOnline
    {
        get
        {
            return true;
        }
    }

    protected void combo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Bind();
    }

    protected void Bind()
    {
        var id = Guid.Parse(combo.SelectedItem.Value);
        var users = HomoryContext.Value.User.Where(p => p.DepartmentUser.Count(o => o.State < State.审核 && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.TopDepartmentId == id) > 0 && p.State < State.审核).ToList();
        list.DataSource = users;
        list.DataBind();
    }
}
