using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;
using Telerik.Web.UI;
using Newtonsoft.Json;

public partial class VIP_关系 : Homory.Model.HomoryCorePage
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
    }

    protected void org_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        if (Session["Sp_Org_X"] == null)
            Response.Redirect("../VIP/机构.aspx");
        var json = Session["Sp_Org_X"].ToString();
        var list = JsonConvert.DeserializeObject<List<dynamic>>(json);
        org.DataSource = list;
    }

    protected void user_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        if (Session["Sp_User_X"] == null)
            Response.Redirect("../VIP/用户.aspx");
        var json = Session["Sp_User_X"].ToString();
        var list = JsonConvert.DeserializeObject<List<dynamic>>(json);
        user.DataSource = list;
    }

    protected void pv_ServerClick(object sender, EventArgs e)
    {
        PerformAction("+PV");
    }

    protected void p_ServerClick(object sender, EventArgs e)
    {
        PerformAction("+P");
    }

    protected void v_ServerClick(object sender, EventArgs e)
    {
        PerformAction("+V");
    }

    protected void pv_r_ServerClick(object sender, EventArgs e)
    {
        PerformAction("-PV");
    }

    protected void p_r_ServerClick(object sender, EventArgs e)
    {
        PerformAction("-P");
    }

    protected void v_r_ServerClick(object sender, EventArgs e)
    {
        PerformAction("-V");
    }

    protected void pk_ServerClick(object sender, EventArgs e)
    {
        PerformAction("K");
    }

    protected void PerformAction(string action)
    {
        try
        {
            var batch = Guid.NewGuid();
            var time = DateTime.Now;
            var orgs = JsonConvert.DeserializeObject<List<dynamic>>(Session["Sp_Org_X"].ToString());
            var users = JsonConvert.DeserializeObject<List<dynamic>>(Session["Sp_User_X"].ToString());
            foreach (var o_org in orgs)
            {
                foreach (var o_user in users)
                {
                    var r = new 机构用户关系
                    {
                        Batch = batch,
                        BatchType = action,
                        UserId = Guid.Parse(o_user.id.ToString().Split("@".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]),
                        UserSyncId = o_user.id.ToString().Split("@".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1],
                        OrgId = Guid.Parse(o_org.id.ToString().Split("@".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]),
                        OrgSyncId = o_org.id.ToString().Split("@".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1],
                        Time = time,
                        State = 1
                    };
                    HomoryContext.Value.机构用户关系.Add(r);
                }
            }
            HomoryContext.Value.SaveChanges();
            Session["Sp_Org_X"] = null;
            Session["Sp_User_X"] = null;
            Response.Redirect(string.Format("../VIP/查询.aspx?BatchId={0}", batch));
        }
        catch(System.Exception ex)
        { }
    }
}
