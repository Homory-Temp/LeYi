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

        public ActionResult 寻呼列表未阅单页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var count = DB.f______计数寻呼未阅数(Account).Single();
            ViewBag.Current = id + 1;
            var list = DB.f______列表寻呼未阅表(Account).Skip(id * per).Take(per).ToList();
            return View(list);
        }

        public ActionResult 寻呼列表未阅内容()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("寻呼列表未阅", "已收寻呼");
            var int_id = int.Parse(id);
            var obj = DB.f______列表寻呼未阅表(Account).FirstOrDefault(o => o.CallNoSeeID == int_id);
            var query = DB.f______列表寻呼附件表(obj.CallID.ToString()).OrderBy(o => o.SlaveID);
            var list = query == null ? new List<f______列表寻呼附件表_Result>() : query.ToList();
            foreach (var path in list)
            {
                var source = Directory + path.FilePath.Substring(3).Replace("/", "\\");
                ViewBag.X = source;
                var destination = source.Replace("__", "h__");
                DecryptFile(source, destination);
            }
            var result = new 已收寻呼对象未阅();
            result.列表寻呼未阅表 = obj;
            result.已收寻呼附件表 = list;
            //DB.f______列表寻呼转已阅(int_id);
            return View(result);
        }
    }
}
