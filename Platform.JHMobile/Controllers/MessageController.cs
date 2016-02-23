using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class MessageController : JinHerController
    {
        public ActionResult Message()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var count = db.f____Mobile_Count_Message(Account).Single().Value;
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var per = 10;
            if (count < id * per)
                return RedirectToAction("Message", "Message", new { id = id - 1 });
            var list = db.f____Mobile_List_Message(Account).OrderByDescending(o => o.AppG_Begintime).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            return View(list);
        }

        public ActionResult MessageModule()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
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
                return Authenticate();
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

        public ActionResult MessageModulePreview()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("MessageModule", "Message");
            var int_id = int.Parse(id);
            var message = db.f____Mobile_List_MessageModuleSingle(int_id).FirstOrDefault();
            db.f____Mobile_Do_MessageRead(int_id, message.ModuleTypeID.ToString(), Account);
            if (!string.IsNullOrEmpty(message.MessageFileName))
            {
                var name = message.MessageFileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                var dir = new DirectoryInfo(Server.MapPath("~/Resource/MessageFile"));
                ViewBag.Path = "";
                ViewBag.ModuleTypeID = message.ModuleTypeID;
                ViewBag.PeopleRead = db.f____Mobile_List_MessageModuleSingleRead(message.MessageID, message.ModuleTypeID.ToString()).OrderBy(o => o).ToList().Aggregate("", (o, s) => o += s + "、", o => (o.Length == 0 ? "无" : o.Substring(0, o.Length - 1)));
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
            }
            var query = db.f____Mobile_List_MessageAttachment(message.MessageID.ToString()).OrderBy(o => o.FileID);
            var list = query == null ? new List<f____Mobile_List_MessageAttachment_Result>() : query.ToList();
            foreach (var path in list)
            {
                var source = DingTalk.CorpJinHer + path.FilePath.Substring(3).Replace("/", "\\");
                var destination = source.Replace("__", "h__");
                DecryptFile(source, destination);
            }
            var mo = new MessageModuleSingleObject();
            mo.Object = message;
            mo.List = list;
            return View(mo);
        }

        public ActionResult DoMessageReadDone()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Message", "Message");
            var int_id = int.Parse(id);
            db.f____Mobile_Do_MessageReadDone(int_id, Account);
            return RedirectToAction("Message", "Message");
        }

        public ActionResult MessageToPreview()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Message", "Message");
            var int_id = int.Parse(id);
            var type = db.f____Mobile_List_MessageType(int_id).FirstOrDefault();
            if (type.AppT_ID.StartsWith("IOA_Message", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("MessageMessagePreview", "Message", new { id = id });
            }
            else if (type.AppT_ID.StartsWith("IOA_Send", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("MessageSendPreview", "Message", new { id = id });
            }
            else if (type.AppT_ID.StartsWith("IOA_Accept", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("MessageReceivePreview", "Message", new { id = id });
            }
            return RedirectToAction("Message", "Message");
        }

        public ActionResult MessageMessagePreview()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Message", "Message");
            var int_idx = int.Parse(id);
            var giveOut = db.f____Mobile_List_MessageType(int_idx).FirstOrDefault();
            ViewBag.GiveOutId = giveOut.AppG_ID;
            var int_id = int.Parse(giveOut.AppO_ID);
            var message = db.f____Mobile_List_MessageModuleSingle(int_id).FirstOrDefault();
            if (!string.IsNullOrEmpty(message.MessageFileName))
            {
                var name = message.MessageFileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                var dir = new DirectoryInfo(Server.MapPath("~/Resource/MessageFile"));
                ViewBag.Path = "";
                ViewBag.ModuleTypeID = message.ModuleTypeID;
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
            }
            var query = db.f____Mobile_List_MessageAttachment(message.MessageID.ToString()).OrderBy(o => o.FileID);
            var list = query == null ? new List<f____Mobile_List_MessageAttachment_Result>() : query.ToList();
            foreach (var path in list)
            {
                var source = DingTalk.CorpJinHer + path.FilePath.Substring(3).Replace("/", "\\");
                var destination = source.Replace("__", "h__");
                DecryptFile(source, destination);
            }
            var mo = new MessageModuleSingleObject();
            mo.Object = message;
            mo.List = list;
            return View(mo);
        }

        public ActionResult MessageSendPreview()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Message", "Message");
            var int_id = int.Parse(id);
            ViewBag.GiveOutId = int_id;
            var send = db.f____Mobile_List_MessageSend(int_id).FirstOrDefault();
            if (!string.IsNullOrEmpty(send.FileName))
            {
                var name = send.FileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                var dir = new DirectoryInfo(Server.MapPath("~/Resource/GovFiles"));
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
            }
            return View(send);
        }

        public ActionResult MessageReceivePreview()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Message", "Message");
            var int_id = int.Parse(id);
            ViewBag.GiveOutId = int_id;
            var receive = db.f____Mobile_List_MessageReceive(int_id).FirstOrDefault();
            if (!string.IsNullOrEmpty(receive.FileName))
            {
                var name = receive.FileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                var dir = new DirectoryInfo(Server.MapPath("~/Resource/GovFiles"));
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
            }
            return View(receive);
        }
    }
}
