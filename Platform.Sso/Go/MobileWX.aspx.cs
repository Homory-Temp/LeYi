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
                    v_idcs = "请输入身份证号";
                    v_p = "请输入手机号码";
                    p.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "none";
                    v_phone = "请输入手机号码";
                    wxn.Value = "Create";
                }
                else
                {
                    v_idcs = "身份证号：" + user.Teacher.IDCard;
                    v_p = "手机号码：" + user.Teacher.Phone;
                    p.Style[System.Web.UI.HtmlTextWriterStyle.Display] = "";
                    v_phone = "请输入新手机号码";
                    wxn.Value = "Update";
                }
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
        }

        protected void WeChatOpen()
        {
            var session = Session["WeChatOpenId"];
            if (session == null)
            {
                var code = Request.QueryString["code"];
                var wc = new WeChat();
                Session["WeChatOpenId"] = wc.GetOpenId(code);
            }
        }

        protected void buttonSign_OnClick(object sender, EventArgs e)
        {
            //var x_phone = phone.Value.Replace("手机号码：", "");
            //var x_reset = false;
            //var idc = Request.QueryString[0];
            //var teacher = HomoryContext.Value.Teacher.SingleOrDefault(o => o.IDCard == idc);
            //if (teacher == null)
            //{
            //    Response.Redirect("../Go/SignOff", false);
            //    return;
            //}
            //var user = teacher.User;
            //if (user == null)
            //{
            //    Response.Redirect("../Go/SignOff", false);
            //    return;
            //}
            //HomoryContext.Value.SsoInitialize(user.Id, x_phone, x_reset);
            //Response.Redirect("../Go/SignOff", false);
        }
    }
}
