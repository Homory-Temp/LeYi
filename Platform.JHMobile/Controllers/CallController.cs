using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class CallController : JinHerController
    {
        public ActionResult CallToRead()
        {
            if (string.IsNullOrEmpty(Account))
                return new DingController().Authentication();
            var count = db.f____Mobile_Count_CallToRead(Account).FirstOrDefault().Value;
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var per = 10;
            if (count < id * per)
                return RedirectToAction("CallToRead", "Call", new { id = id - 1 });
            var list = db.f____Mobile_List_CallToRead(Account).OrderByDescending(o => o.CallTime).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            return View(list);
        }
    }
}
