﻿using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 待阅信息Controller : OfficeController
    {
        private int per = 10;

        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var count = DB.f______计数待阅信息数(Account).Single();
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            return View();
        }

        public ActionResult 待阅信息列表单页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            ViewBag.Current = id + 1;
            var list = DB.f______列表待阅信息表(Account).OrderByDescending(o => o.AppG_Begintime).Skip(id * per).Take(per).ToList();
            return View(list);
        }
    }
}
