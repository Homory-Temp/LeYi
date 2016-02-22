using Platform.JHMobile.Models;
using System.Linq;
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
                return Authenticate();
            var count = db.f____Mobile_Count(Account).FirstOrDefault();
            ViewBag.CallToRead = count.CallToRead;
            ViewBag.CallRead = count.CallRead;
            ViewBag.MessageModule = DingTalk.CorpMessageModules.Count;
            ViewBag.MessageToRead = count.MessageToRead;
            ViewBag.TaskToDo = count.TaskToDo;
            ViewBag.TaskDone = count.TaskDone;
            return View();
        }
    }
}
