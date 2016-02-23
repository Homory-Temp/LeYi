using Platform.JHMobile.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class TaskController : JinHerController
    {
        public ActionResult TaskDone()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var count = db.f____Mobile_Count_TaskDone(Account).Single().Value;
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var per = 10;
            if (count < id * per)
                return RedirectToAction("TaskDone", "Task", new { id = id - 1 });
            var list = db.f____Mobile_List_TaskDone(Account).OrderByDescending(o => o.App_Time).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            return View(list);
        }
    }
}
