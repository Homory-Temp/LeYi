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
    public partial class GoMobile : HomoryPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
                this.Title = doc.Root.Element("Sso").Value;
                if (Request.QueryString.Count == 0)
                {
                    Response.Redirect("../Go/SignOff", false);
                    return;
                }
                var idc = Request.QueryString[0];
                var teacher = HomoryContext.Value.Teacher.SingleOrDefault(o => o.IDCard == idc);
                if (teacher == null)
                {
                    Response.Redirect("../Go/SignOff", false);
                    return;
                }
                var user = teacher.User;
                if (user == null)
                {
                    Response.Redirect("../Go/SignOff", false);
                    return;
                }
                idcs.InnerText = Request.QueryString[0];
                var v_phone = "请输入新手机号码";
                phone.Value = v_phone;
                phone.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_phone);
                phone.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_phone);
                var v_code = "请输入手机收到的验证码";
                code.Value = v_code;
                code.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_code);
                code.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_code);
            }
        }

        protected void buttonSign_OnClick(object sender, EventArgs e)
        {
            var x_phone = phone.Value.Replace("手机号码：", "");
            var x_reset = false;
            var idc = Request.QueryString[0];
            var teacher = HomoryContext.Value.Teacher.SingleOrDefault(o => o.IDCard == idc);
            if (teacher == null)
            {
                Response.Redirect("../Go/SignOff", false);
                return;
            }
            var user = teacher.User;
            if (user == null)
            {
                Response.Redirect("../Go/SignOff", false);
                return;
            }
            HomoryContext.Value.SsoInitialize(user.Id, x_phone, x_reset);
            Response.Redirect("../Go/SignOff", false);
        }
    }
}
