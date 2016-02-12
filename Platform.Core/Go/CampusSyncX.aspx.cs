using Aspose.Cells;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public partial class Extended_CampusSyncX : HomoryCorePage
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
            panel.ResponseScripts.Add(string.Format("corp_id = '{0}'; corp_secret = '{1}'; check_corp(); do_dingding();", corp[0], corp[1]));
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

    }

    protected void panel_depts_refresh_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    {
        grid.Rebind();
        panel_depts_refresh.ResponseScripts.Add("start_dingding();");
    }

    protected void grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var id = Guid.Parse(Request.QueryString["CampusId"]);
        var source = HomoryContext.Value.ViewDingDing.Where(o => o.TopDepartmentId == id).OrderBy(o => o.Account).ThenBy(o => o.DingKey).ToList();
        grid.DataSource = source;
        var list = new List<DingUser>();
        list.AddRange(source.Select(o => o.Account).Distinct().Select(o => new DingUser { userid = o }));
        foreach (var x in list)
        {
            var first = source.First(o => o.Account == x.userid);
            x.name = first.RealName;
            x.mobile = first.Phone;
            x.position = first.PerStaff;
            x.department = source.Where(o => o.Account == x.userid).Select(o => int.Parse(o.DingKey)).ToArray();
            x.orderInDepts = "{";
            foreach (var key in x.department)
            {
                x.orderInDepts += key.ToString() + ":" + source.First(o => o.Account == x.userid && o.DingKey == key.ToString()).Ordinal + ",";
            }
            if (x.orderInDepts.Length > 1)
                x.orderInDepts = x.orderInDepts.Substring(0, x.orderInDepts.Length - 1);
            x.orderInDepts += "}";
        }
        user_list.InnerText = Newtonsoft.Json.JsonConvert.SerializeObject(list);
    }

    public class DingUser
    {
        public string userid { get; set; }
        public string name { get; set; }
        public int[] department { get; set; } 
        public string orderInDepts { get; set; }
        public string mobile { get; set; }
        public string position { get; set; }
    }
}
