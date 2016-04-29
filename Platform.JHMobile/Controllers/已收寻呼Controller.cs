using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 已收寻呼Controller : OfficeController
    {
        private int per = 10;

        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            ViewBag.未阅寻呼数 = DB.f______计数寻呼未阅数(Account).Single();
            ViewBag.已阅寻呼数 = DB.f______计数寻呼已阅数(Account).Single();
            ViewBag.历史寻呼数 = DB.f______计数寻呼历史数(Account).Single();
            return View();
        }

        public ActionResult 寻呼列表未阅()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var count = DB.f______计数寻呼未阅数(Account).Single();
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            return View();
        }
    }
}
