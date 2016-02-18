﻿using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
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

        public ActionResult CallPreview()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Sso", "Home");
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Call", "Home");
            var int_id = int.Parse(id);
            var obj = db.未阅寻呼列表(Account).FirstOrDefault(o => o.CallNoSeeID == int_id);
            var query = db.未阅寻呼附件(obj.CallID.ToString()).OrderBy(o => o.SlaveID);
            var list = query == null ? new List<未阅寻呼附件_Result>() : query.ToList();
            foreach (var path in list)
            {
                var source = Corp.corp_c6 + path.FilePath.Substring(3).Replace("/", "\\");
                var destination = source.Replace("__", "h__");
                DecryptFile(source, destination);
            }
            var result = new CallObject();
            result.Object = obj;
            result.List = list;
            return View(result);
        }

        public ActionResult CallRead()
        {
            if (string.IsNullOrEmpty(Account))
                return RedirectToAction("Sso", "Home");
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Call", "Home");
            var int_id = int.Parse(id);
            db.未阅寻呼已阅(int_id);
            return RedirectToAction("Call", "Home");
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
            var name = message.MessageFileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
            var dir = new DirectoryInfo(Server.MapPath("~/Resource/MessageFile"));
            ViewBag.Path = "";
            foreach (var cDir in dir.GetDirectories().OrderByDescending(o => o.CreationTime))
            {
                if (cDir.GetFiles().Count(o => o.Name.ToLower() == name.ToLower()) > 0)
                {
                    string path = dir + "\\" + cDir.Name + "\\" + name;
                    var converted = ConvertDoc(path);
                    if (!string.IsNullOrEmpty(converted))
                    {
                        ViewBag.PDF = converted;
                    }
                }
            }
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

        private void DecryptFile(string path, string output)
        {
            try
            {
                var aes = new AES();
                aes.CreateKey(Encoding.Default.GetBytes("12345678123456781234567812345678"), Encoding.Default.GetBytes("1234567812345678"));
                if (!System.IO.File.Exists(path))
                    return;
                var stream = System.IO.File.OpenRead(path);
                var length = stream.Length;
                byte[] bytes = new byte[Convert.ToInt32(length.ToString())];
                stream.Read(bytes, 0, (int)bytes.Length);
                stream.Close();
                bytes = aes.Decrypt(bytes);
                var stream_x = new FileStream(output, FileMode.Create, FileAccess.Write);
                stream_x.Write(bytes, 0, (int)bytes.Length);
                stream_x.Close();
            }
            catch
            { }
        }
    }
}
