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
            ViewBag.未阅寻呼数 = DB.f____Mobile_Count_CallToRead(Account).Single();
            ViewBag.已阅寻呼数 = DB.f____Mobile_Count_CallRead(Account).Single();
            ViewBag.历史寻呼数 = DB.f____Mobile_Count_CallHistoric(Account, null, null).Single();
            return View();
        }
    }
}
