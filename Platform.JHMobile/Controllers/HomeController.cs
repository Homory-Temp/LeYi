using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
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
            var sp = RouteData.Values["id"].ToString().Split(new char[] { '_' });
            var type = sp[0];
            var count = db.待阅信息数量(Account, type).Single().Value;
            if (count == 0)
                return RedirectToAction("Message", "Home");
            var id = sp.Length == 1 ? 0 : int.Parse(sp[1]);
            var per = 10;
            if (count < id * per)
                return RedirectToAction("MessageModule", "Home", new { id = string.Format("{0}_{1}", type,  id - 1) });
            var list = db.待阅信息列表(Account, type).OrderByDescending(o => o.SendTime).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            ViewBag.MMType = type;
            return View(list);
        }

        public ActionResult MessagePreview()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Sso", "Home");
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Message", "Home");
            var message = db.待阅信息详情(id).FirstOrDefault();
            return View(message);
        }

        private string ConvertDoc(string doc)
        {
            if (string.IsNullOrEmpty(doc))
                return null;
            var file = new FileInfo(doc);
            if (!file.Exists)
                return null;
            var suffix = file.Extension.Replace(".", "").ToLower();
            var path = string.Format("{0}.pdf", file.FullName.Substring(0, file.FullName.LastIndexOf('.')));
            var newFile = new FileInfo(path);
            if (newFile.Exists)
                return path;
            switch (suffix)
            {
                case "doc":
                case "docx":
                case "txt":
                case "rtf":
                    var docW = new Aspose.Words.Document(file.FullName);
                    docW.Save(path, Aspose.Words.SaveFormat.Pdf);
                    return path;
                case "ppt":
                case "pptx":
                    var docP = new Aspose.Slides.Presentation(file.FullName);
                    docP.Save(path, Aspose.Slides.Export.SaveFormat.Pdf);
                    return path;
                case "xls":
                case "xlsx":
                    var docE = new Aspose.Cells.Workbook(file.FullName);
                    docE.Save(path, Aspose.Cells.SaveFormat.Pdf);
                    return path;
                default: return null;
            }
        }
    }
}
