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
                mo.Add(new MessageModuleObject { ModuleTypeId = pair.Key, ModuleTypeName = pair.Value, MessageCount = db.f____Mobile_Count_MessageModule(Account, pair.Key) });
            }
            return View(mo);
        }
    }
}
