using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class DingController : JinHerController
    {
        public ActionResult Authentication()
        {
            return View(Account);
        }

        public ActionResult Authorization()
        {
            return View(Account);
        }

        public ActionResult Home()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Ding", "Authentication");
            return View();
        }
    }
}
