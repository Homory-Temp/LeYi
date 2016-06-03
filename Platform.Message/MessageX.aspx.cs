using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Configuration;

public partial class MessageX : System.Web.UI.Page 
{
    protected Lazy<MEntities> db = new Lazy<MEntities>(() => new MEntities());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["OnlineId"]))
        {
            Response.Redirect(WebConfigurationManager.AppSettings["Sso"] + "?SsoRedirect=" + Server.UrlEncode(WebConfigurationManager.AppSettings["Message"]));
            return;
        }
        else
        {
            var u = db.Value.M_寻呼_用户(Guid.Parse(Request.QueryString["OnlineId"])).SingleOrDefault();
            if (u == null || u == "")
            {
                Response.Redirect("https://www.baidu.com/");
                return;
            }
            else
            {
                Session["MU"] = u;
            }
        }
        if (!IsPostBack)
        {
            Session["MA"] = null;
            LoadDepartments();
        }
    }

    private List<M_寻呼> _list;

    protected List<M_寻呼> List
    {
        get
        {
            if (_list == null)
            {
                _list = db.Value.M_寻呼_列表(Session["MU"].ToString()).ToList();
            }
            return _list;
        }
    }

    protected void LoadDepartments()
    {
        tree.DataSource = List.Where(o => o.Type == "D").OrderBy(o => o.Ordinal).ToList();
        tree.DataBind();
        tree.ExpandAllNodes();
        a.InnerText = "主职（" + List.Count(o => o.Prior && !o.Online && o.Type == "U") + "）";
        b.InnerText = "在线（" + List.Count(o => o.Prior && o.Online && o.Type == "U") + "）";
        c.InnerText = "兼职（" + List.Count(o => !o.Prior && !o.Online && o.Type == "U") + "）";
        d.InnerText = "在线（" + List.Count(o => !o.Prior && o.Online && o.Type == "U") + "）";
    }

    protected List<M_寻呼> LoadUsers(M_寻呼 m)
    {
        return List.Where(o => o.Type == "U" && o.ParentId == m.Id).OrderBy(o => o.Ordinal).ToList();
    }

    protected string LoadColorX(M_寻呼 m)
    {
        if (m.Prior && !m.Online)
        {
            return "margin-right: 16px; font-family: SimSun; font-weight: bold; color: #3BAFDA;";
        }
        else if (m.Prior && m.Online)
        {
            return "margin-right: 16px; font-family: SimSun; font-weight: bold; color: #E9573F;";
        }
        else if (!m.Prior && !m.Online)
        {
            return "margin-right: 16px; font-family: SimSun; font-weight: bold; color: #8CC152;";
        }
        else
        {
            return "margin-right: 16px; font-family: SimSun; font-weight: bold; color: #F6BB42;";
        }
    }

    protected string LoadTooltip(M_寻呼 m)
    {
        return "主职单位：" + m.PriorName + "\r\n手机号码：" + m.Phone;
    }

    protected Dictionary<int, string> Attachments
    {
        get
        {
            if (Session["MA"] == null)
            {
                Session["MA"] = new Dictionary<int, string>();
            }
            return (Dictionary<int, string>)Session["MA"];
        }
        set
        {
            Session["MA"] = value;
        }
    }

    protected void post_request(object sender, AjaxRequestEventArgs e)
    {
        var x = e.Argument.Split(new[] { "@*@*@*@*@*@" }, StringSplitOptions.RemoveEmptyEntries);
        var y = x[0].Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        foreach (var i in y)
        {
            try
            {
                var att = Attachments.Keys.ToList();
                var aids = "";
                foreach (var atti in att)
                {
                    aids += atti.ToString() + "|";
                }
                db.Value.M_寻呼_发送(Session["MU"].ToString(), int.Parse(x[1]), x[2], i, aids);
            }
            catch
            {
            }
        }
        Session["MA"] = null;
        xp.ResponseScripts.Add("sent(); rea(0);");
    }
}
