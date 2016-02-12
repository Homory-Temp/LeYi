using Aspose.Cells;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class Extended_CampusSync : HomoryCorePage
{
    protected override void OnInit(EventArgs e)
    {
        var id = Request.QueryString["CampusId"];
        if (string.IsNullOrEmpty(id))
            Response.Redirect("Campus.aspx");
        else
            base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = Guid.Parse(Request.QueryString["CampusId"]);
            var campus = HomoryContext.Value.Department.First(o => o.Id == id);
            var corp = string.IsNullOrEmpty(campus.DingKey) ? new string[] { "", "" } : HomoryCryptor.Decrypt(campus.DingKey, "7U7x0keu+d5EbThVQZzgFlfdVelKNmqml2RRmSi3Y/4=", "l46OWQ3WRn4RBpAQpUZhDg==").Split(new char[] { ' ' });
            panel.ResponseScripts.Add(string.Format("corp_id = '{0}'; corp_secret = '{1}'; check_corp(); load_depts();", corp[0], corp[1]));
        }
    }

    protected override string PageRight
    {
        get
        {
            return HomoryCoreConstant.RightEveryone;
        }
    }

    protected void panel_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {
        dept_list_ding_copy.InnerText = e.Argument;
        dept_grid_ding.Rebind();
    }

    protected void dept_grid_ding_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DingDepartment>>(dept_list_ding_copy.InnerText);
        dept_grid_ding.DataSource = obj;
    }

    protected void dept_grid_core_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var id = Guid.Parse(Request.QueryString["CampusId"]);
        dept_list_core.InnerText = Newtonsoft.Json.JsonConvert.SerializeObject(HomoryContext.Value.Department.Where(o => o.TopId == id && (o.Type == DepartmentType.部门 || o.Type == DepartmentType.学校) && o.State < State.审核).OrderBy(o => o.Type).ThenBy(o => o.Ordinal).ToList().Select(o =>
        new CoreDepartment
        {
            id = o.Id.ToString().ToLower(),
            name = o.DisplayName,
            parentid = o.ParentId.HasValue ? o.ParentId.Value.ToString().ToLower() : "",
            ordinal = o.Ordinal,
            dingid = o.Type == DepartmentType.学校 ? "1" : o.DingKey,
            ding = o.Type == DepartmentType.学校
        }).ToList());
        var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CoreDepartment>>(dept_list_core.InnerText);
        dept_grid_core.DataSource = obj;
    }

    protected void panel_depts_refresh_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {

        var parts_x = e.Argument.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var part_s in parts_x)
        {
            var parts = part_s.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            var id = Guid.Parse(parts[0]);
            var dingid = parts[1];
            var d = HomoryContext.Value.Department.First(o => o.Id == id);
            d.DingKey = dingid;
            d.Ding = true;
        }
        HomoryContext.Value.SaveChanges();
    }

    public class DingDepartment
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parentid { get; set; }
        public bool createDeptGroup { get; set; }
        public bool autoAddUser { get; set; }
        public bool core { get; set; }
    }

    public class CoreDepartment
    {
        public string id { get; set; }
        public string name { get; set; }
        public string parentid { get; set; }
        public int ordinal { get; set; }
        public string dingid { get; set; }
        public bool ding { get; set; }
    }
}
