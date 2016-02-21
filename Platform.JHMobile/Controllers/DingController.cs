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
                return Authentication();
            ViewBag.CallToRead = 0;
            ViewBag.CallRead = 1;
            ViewBag.MessageModule = 2;
            ViewBag.MessageToRead = 3;
            ViewBag.TaskToDo = 4;
            ViewBag.TaskDone = 5;
            return View();
        }
    }
}
