using Platform.JHMobile.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class MessageController : JinHerController
    {
        public ActionResult MessageModule()
        {
            if (string.IsNullOrEmpty(Account))
                return new DingController().Authentication();
            var mo = new List<MessageModuleObject>();
            foreach (var pair in DingTalk.CorpMessageModules)
            {
                mo.Add(new MessageModuleObject { ModuleTypeId = pair.Key, ModuleTypeName = pair.Value, MessageCount = db.f____Mobile_Count_MessageModule(Account, pair.Key).SingleOrDefault().Value });
            }
            return View(mo);
        }

        public ActionResult MessageModuleSingle()
        {
            if (string.IsNullOrEmpty(Account))
                return new DingController().Authentication();
            var sp = RouteData.Values["id"].ToString().Split(new char[] { '_' });
            var type = sp[0];
            var count = db.f____Mobile_Count_MessageModule(Account, type).Single().Value;
            var id = sp.Length == 1 ? 0 : int.Parse(sp[1]);
            var per = 10;
            if (count < id * per)
                return RedirectToAction("MessageModuleSingle", "Message", new { id = string.Format("{0}_{1}", type, id - 1) });
            var list = db.f____Mobile_List_MessageModule(Account, type).OrderByDescending(o => o.SendTime).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            ViewBag.MMType = type;
            return View(list);
        }
    }
}
