using Platform.JHMobile.Models;
using System.Linq;
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
            var mo = DB.f______信息门户模块表().ToList().Select(o => new 信息门户对象模块 { 模块 = o, 数量 = DB.f______计数信息门户数(Account, o.ModuleTypeID.ToString()).Single().Value }).ToList();
            ViewBag.信息门户数 = mo.Sum(o => o.数量);
            ViewBag.待阅信息数 = DB.f______计数待阅信息数(Account).Single();
            ViewBag.待办工作数 = DB.f______计数待办工作数(Account).Single();
            return View();
        }
    }
}
