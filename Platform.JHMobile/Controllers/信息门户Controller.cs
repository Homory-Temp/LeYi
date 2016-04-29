using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 信息门户Controller : OfficeController
    {
        private int per = 10;

        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var mo = DB.f______信息门户模块表().ToList().Select(o => new 信息门户对象模块 { 模块 = o, 数量 = DB.f______计数信息门户数(Account, o.ModuleTypeID.ToString()).Single().Value }).ToList();
            return View(mo);
        }
    }
}
