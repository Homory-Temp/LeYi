using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 首页Controller : OfficeController
    {
        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            ViewBag.已收寻呼数 = DB.f______计数寻呼未阅数(Account).Single() + DB.f______计数寻呼已阅数(Account).Single() + DB.f______计数寻呼历史数(Account).Single();
            return View();
        }
    }
}
