using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 待阅信息Controller : OfficeController
    {
        private int per = 10;

        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var count = DB.f______计数待阅信息数(Account).Single();
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            return View();
        }

        public ActionResult 待阅信息列表单页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            ViewBag.Current = id + 1;
            var list = DB.f______列表待阅信息表(Account).OrderByDescending(o => o.AppG_Begintime).Skip(id * per).Take(per).ToList();
            return View(list);
        }

        public ActionResult 待阅信息列表内容()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            var int_id = int.Parse(id);
            var type = DB.f______待阅信息类型表(int_id).FirstOrDefault();
            if (type.AppT_ID.StartsWith("IOA_Message", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("待阅信息列表内容通知", "待阅信息", new { id = id });
            }
            else if (type.AppT_ID.StartsWith("IOA_Send", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("待阅信息列表内容发文", "待阅信息", new { id = id });
            }
            else if (type.AppT_ID.StartsWith("IOA_Accept", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("待阅信息列表内容收文", "待阅信息", new { id = id });
            }
            return new EmptyResult();
        }

        public ActionResult 待阅信息列表内容通知()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            var int_idx = int.Parse(id);
            var giveOut = DB.f______待阅信息类型表(int_idx).FirstOrDefault();
            ViewBag.GiveOutId = giveOut.AppG_ID;
            var int_id = int.Parse(giveOut.AppO_ID);
            var message = DB.f______信息门户内容表(int_id).FirstOrDefault();
            ViewBag.ModuleTypeID = message.ModuleTypeID;
            if (!string.IsNullOrEmpty(message.MessageFileName))
            {
                var name = message.MessageFileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                var dir = new DirectoryInfo(Directory + "\\Resource\\MessageFile");
                ViewBag.Path = "";
                foreach (var cDir in dir.GetDirectories().OrderByDescending(o => o.CreationTime))
                {
                    if (cDir.GetFiles().Count(o => o.Name.ToLower() == name.ToLower()) > 0)
                    {
                        string path = dir + "\\" + cDir.Name + "\\" + name;
                        ViewBag.Link = path;
                        var converted = ConvertDoc(path);
                        if (!string.IsNullOrEmpty(converted))
                        {
                            ViewBag.PDF = converted;
                        }
                    }
                }
            }
            var query = DB.f______信息门户附件表(message.MessageID.ToString()).OrderBy(o => o.FileID);
            var list = query == null ? new List<f______信息门户附件表_Result>() : query.ToList();
            foreach (var path in list)
            {
                var source = Directory + path.FilePath.Substring(3).Replace("/", "\\");
                var destination = source.Replace("__", "h__");
                DecryptFile(source, destination);
            }
            var mo = new 信息门户对象内容();
            mo.内容 = message;
            mo.附件 = list;
            return View(mo);
        }
    }
}
