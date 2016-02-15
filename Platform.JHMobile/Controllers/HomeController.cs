using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class HomeController : Controller
    {
        private C6Entities db = new C6Entities();

        private string account;

        protected string Account
        {
            get
            {
                if(string.IsNullOrEmpty(account))
                {
                    account = Session["user_id"] == null ? null : Session["user_id"].ToString();
                }
                return account;
            }
        }

        public ActionResult Sso()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Sso", "Home");
            var call = db.未阅寻呼数量(Account).Single().Value;
            var message = 0;
            foreach (var cm in Corp.corp_messages.Keys)
            {
                message += db.待阅信息数量(Account, cm).Single().Value;
            }
            var task = db.待办事项数量(Account).Single().Value;
            var list = new int[] { call, message, task };
            return View(list);
        }

        public ActionResult Call()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Sso", "Home");
            var count = db.未阅寻呼数量(Account).Single().Value;
            if (count == 0)
                return RedirectToAction("Home", "Home");
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var per = 10;
            if (count < id * per)
                return RedirectToAction("Call", "Home", new { id = id - 1 });
            var list = db.未阅寻呼列表(Account).OrderByDescending(o => o.CallTime).Skip(id * per).Take(per).ToList();
            return View(list);
        }

        public ActionResult Message()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Sso", "Home");
            var mo = new List<MessageObject>();
            foreach (var pair in Corp.corp_messages)
            {
                mo.Add(new MessageObject { ModuleTypeId = pair.Key, ModuleTypeName = pair.Value, MessageCount = db.待阅信息数量(Account, pair.Key).Single().Value });
            }
            var sum = mo.Sum(o => o.MessageCount);
            if (sum == 0)
                return RedirectToAction("Home", "Home");
            return View(mo);
        }

        public ActionResult MessageModule()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Sso", "Home");
            var sp = RouteData.Values["id"].ToString().Split(new char[] { '.' });
            var type = sp[0];
            var count = db.待阅信息数量(Account, type).Single().Value;
            if (count == 0)
                return RedirectToAction("Home", "Message");
            var id = sp.Length == 1 ? 0 : int.Parse(sp[1]);
            var per = 10;
            if (count < id * per)
                return RedirectToAction("Call", "Home", new { id = string.Format("{0}.{1}", type,  id - 1) });
            var list = db.待阅信息列表(Account, type).OrderByDescending(o => o.SendTime).Skip(id * per).Take(per).ToList();
            return View(list);
        }
    }
}
