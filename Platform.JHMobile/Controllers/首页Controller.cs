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
            ViewBag.已收寻呼数 = DB.f____Mobile_Count_CallToRead(Account).Single() + DB.f____Mobile_Count_CallRead(Account).Single() + DB.f____Mobile_Count_CallHistoric(Account, null, null).Single();
            return View();
        }
    }
}
