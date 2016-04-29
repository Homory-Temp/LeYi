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

        public ActionResult 信息门户列表()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var sp = RouteData.Values["id"].ToString().Split(new char[] { '_' });
            var type = sp[0];
            var count = DB.f______计数信息门户数(Account, type).Single();
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Module = type;
            ViewBag.Title = sp[1];
            return View();
        }

        public ActionResult 信息门户列表单页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var sp = RouteData.Values["id"].ToString().Split(new char[] { '_' });
            var type = sp[0];
            var id = int.Parse(sp[1]);
            ViewBag.Current = id + 1;
            var list = DB.f______列表信息门户表(Account, type).Skip(id * per).Take(per).ToList();
            return View(list);
        }
    }
}
