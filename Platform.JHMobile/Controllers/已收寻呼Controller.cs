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
        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            ViewBag.未阅寻呼数 = DB.f______计数寻呼未阅数(Account).Single();
            ViewBag.已阅寻呼数 = DB.f______计数寻呼已阅数(Account).Single();
            ViewBag.历史寻呼数 = DB.f______计数寻呼历史数(Account).Single();
            return View();
        }

        public ActionResult 未阅寻呼列表()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            //var count = db.f____Mobile_Count_CallToRead(Account).Single().Value;
            //var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            //var per = 10;
            //if (count < id * per)
            //    return RedirectToAction("CallToRead", "Call", new { id = id - 1 });
            //var list = db.f____Mobile_List_CallToRead(Account).OrderByDescending(o => o.CallTime).Skip(id * per).Take(per).ToList();
            //ViewBag.Min = 0;
            //ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            //ViewBag.Current = id;
            return View(/*list*/);
        }
    }
}
