using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Management;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Go
{
    public partial class GoMobileWX : HomoryPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WeChatOpen();
            if (!IsPostBack)
            {
                var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
                this.Title = doc.Root.Element("Sso").Value;
                var openId = Session["WeChatOpenId"] == null ? "" : Session["WeChatOpenId"].ToString();
                var user = HomoryContext.Value.User.SingleOrDefault(o => o.WXOpenId == openId);
                string v_idcs, v_p, v_phone;
                if (user == null)
                {
                    tname.InnerText = "登记 ";
                    v_idcs = "请输入身份证号";
                    v_p = "请输入手机号码";
                    idcs.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "";
                    p.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "none";
                    v_phone = "请输入手机号码";
                    wxn.Value = "Create";
                    idcs.Value = v_idcs;
                    idcs.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_idcs);
                    idcs.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_idcs);
                    p.Value = v_p;
                    p.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_p);
                    p.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_p);
                    phone.Value = v_phone;
                    phone.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_phone);
                    phone.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_phone);
                    var v_code = "请输入手机收到的验证码";
                    code.Value = v_code;
                    code.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_code);
                    code.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_code);
                }
                else
                {
                    tname.InnerText = "更正 ";
                    v_idcs = "" + user.Teacher.IDCard;
                    v_p = "" + user.Teacher.Phone;
                    idcs.Attributes["readonly"] = "readonly";
                    idcs.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "";
                    p.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "";
                    p.Attributes["readonly"] = "readonly";
                    v_phone = "请输入新手机号码";
                    wxn.Value = "Update";
                    idcs.Value = v_idcs;
                    idcs.Attributes["onfocus"] = "";
                    idcs.Attributes["onblur"] = "";
                    p.Value = v_p;
                    p.Attributes["onfocus"] = "";
                    p.Attributes["onblur"] = "";
                    phone.Value = v_phone;
                    phone.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_phone);
                    phone.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_phone);
                    var v_code = "请输入手机收到的验证码";
                    code.Value = v_code;
                    code.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_code);
                    code.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_code);
                }
            }
        }

        protected void WeChatOpen()
        {
            var session = Session["WeChatOpenId"];
            var wc = new WeChat();
            if (session == null)
            {
                var code = Request.QueryString["code"];
                wx.Value = wc.GetOpenId(code);
                Session["WeChatOpenId"] = wx.Value;
            }
            else
            {
                wx.Value = Session["WeChatOpenId"].ToString();
            }
            var token = WeChat.GetAccessToken().access_token;
            var img = wc.GetUserInfoByOpenId(token, wx.Value).headimgurl;
            if (!string.IsNullOrEmpty(img))
            {
                Session["UserIcon"] = img;
            }
            else
            {
                Session["UserIcon"] = "";
            }
        }

        protected void buttonSign_OnClick(object sender, EventArgs e)
        {
            try
            {
                var t = wxn.Value;
                var v = Session["WeChatOpenId"].ToString();
                if (t == "Create")
                {
                    var _idc = idcs.Value;
                    var _p = phone.Value;
                    var teacher = HomoryContext.Value.Teacher.SingleOrDefault(o => o.IDCard == _idc);
                    if (teacher == null)
                    {
                        Response.Redirect("../Go/MobileWXFailed", false);
                        return;
                    }
                    var user = teacher.User;
                    if (user == null)
                    {
                        Response.Redirect("../Go/MobileWXFailed", false);
                        return;
                    }
                    if (user.WXOpenId == null || user.WXOpenId != v)
                    {
                        var img = Session["UserIcon"].ToString();
                        if (!string.IsNullOrEmpty(img) && !img.Equals(user.Icon, StringComparison.OrdinalIgnoreCase))
                        {
                            user.Icon = img;
                        }
                        HomoryContext.Value.User.Single(o => o.Id == user.Id).WXOpenId = wx.Value;
                        HomoryContext.Value.SaveChanges();
                    }
                    else
                    {
                        var img = Session["UserIcon"].ToString();
                        if (!string.IsNullOrEmpty(img) && !img.Equals(user.Icon, StringComparison.OrdinalIgnoreCase))
                        {
                            user.Icon = img;
                            HomoryContext.Value.SaveChanges();
                        }
                    }
                    HomoryContext.Value.SsoInitialize(user.Id, _p, false);
                    var deptId = user.DepartmentUser.FirstOrDefault(r => r.State < State.审核 && (r.Type == DepartmentUserType.借调后部门主职教师 || r.Type == DepartmentUserType.部门主职教师)).DepartmentId;
                    var dept = HomoryContext.Value.Department.SingleOrDefault(o => o.Id == deptId).DepartmentRoot.Name;
                    Response.Redirect(string.Format("../Go/MobileWXSucceeded?Dept={0}&User={1}", Server.UrlEncode(dept), Server.UrlEncode(user.RealName)), false);
                }
                else if (t == "Update")
                {
                    var _p = phone.Value;
                    var user = HomoryContext.Value.User.SingleOrDefault(o => o.WXOpenId == v);
                    if (user == null)
                    {
                        Response.Redirect("../Go/MobileWXFailed", false);
                        return;
                    }
                    var img = Session["UserIcon"].ToString();
                    if (!string.IsNullOrEmpty(img) && !img.Equals(user.Icon, StringComparison.OrdinalIgnoreCase))
                    {
                        user.Icon = img;
                        HomoryContext.Value.SaveChanges();
                    }
                    HomoryContext.Value.SsoInitialize(user.Id, _p, false);
                    var deptId = user.DepartmentUser.FirstOrDefault(r => r.State < State.审核 && (r.Type == DepartmentUserType.借调后部门主职教师 || r.Type == DepartmentUserType.部门主职教师)).DepartmentId;
                    var dept = HomoryContext.Value.Department.SingleOrDefault(o => o.Id == deptId).DepartmentRoot.Name;
                    Response.Redirect(string.Format("../Go/MobileWXSucceeded?Dept={0}&User={1}", Server.UrlEncode(dept), Server.UrlEncode(user.RealName)), false);
                }
            }
            catch
            {
                Response.Redirect("../Go/MobileWXFailed", false);
            }
        }
    }
}
