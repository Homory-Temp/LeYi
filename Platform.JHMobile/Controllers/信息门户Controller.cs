using Platform.JHMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 信息门户Controller : OfficeController
    {
        private int per = 10;

        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var mo = DB.f______信息门户模块表().ToList().Select(o => new 信息门户对象模块 { 模块 = o, 数量 = DB.f______计数信息门户数(Account, o.ModuleTypeID.ToString()).Single().Value }).ToList();
            return View(mo);
        }

        public ActionResult 信息门户列表()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var sp = RouteData.Values["id"].ToString().Split(new char[] { '_' });
            var type = sp[0];
            var count = DB.f______计数信息门户数(Account, type).Single();
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Module = type;
            ViewBag.Title = sp[1];
            return View();
        }

        public ActionResult 信息门户列表单页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var sp = RouteData.Values["id"].ToString().Split(new char[] { '_' });
            var type = sp[0];
            var id = int.Parse(sp[1]);
            ViewBag.Current = id + 1;
            var list = DB.f______列表信息门户表(Account, type).OrderByDescending(o => o.SendTime).Skip(id * per).Take(per).ToList();
            return View(list);
        }

        public ActionResult 信息门户列表内容()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            var int_id = int.Parse(id);
            var message = DB.f______信息门户内容表(int_id).FirstOrDefault();
            ViewBag.ModuleTypeID = message.ModuleTypeID;
            ViewBag.ModuleTypeName = DB.f______信息门户模块表().Single(o => o.ModuleTypeID == message.ModuleTypeID.Value).ModuleTypeName;
            DB.f______信息门户转已阅(int_id, message.ModuleTypeID.ToString(), Account);
            if (!string.IsNullOrEmpty(message.MessageFileName))
            {
                var name = message.MessageFileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                var dir = new DirectoryInfo(Directory + "\\Resource\\MessageFile");
                ViewBag.Path = "";
                var people = DB.f______信息门户已阅表(message.MessageID, message.ModuleTypeID.ToString()).OrderBy(o => o).ToList();
                ViewBag.PeopleCount = people.Count;
                ViewBag.PeopleRead = people.Aggregate("", (o, s) => o += s + "、", o => (o.Length == 0 ? "无" : o.Substring(0, o.Length - 1)));
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
