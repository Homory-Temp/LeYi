﻿using EntityFramework.Extensions;
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
                var v_phone = string.IsNullOrEmpty(teacher.Phone) ? "请输入手机号码" : string.Format("手机号码：{0}", teacher.Phone);
                phone.Value = v_phone;
                phone.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_phone);
                phone.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_phone);
                var v_reset = "密码重置：如忘记密码 请输入Y";
                reset.Value = v_reset;
                reset.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_reset);
                reset.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_reset);
                var v_code = "请输入手机收到的验证码";
                code.Value = v_code;
                code.Attributes["onfocus"] = string.Format("if(this.value=='{0}') this.value='';", v_code);
                code.Attributes["onblur"] = string.Format("if(this.value=='') this.value='{0}';", v_code);
            }
        }

        protected void buttonSign_OnClick(object sender, EventArgs e)
        {
            var script_re = string.Format("top.location.href = '{0}';", "https://www.baidu.com/");
            areaAction.ResponseScripts.Add(script_re);
        }
    }
}
