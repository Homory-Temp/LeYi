using Platform.JHMobile.Models;
using System;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 流程查询Controller : OfficeController
    {
        private int per = 10;

        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var count = DB.f______计数流程查询数(Account).Single();
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            return View();
        }

        public ActionResult 流程查询列表单页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            ViewBag.Current = id + 1;
            var list = DB.f______列表流程查询表(Account).OrderByDescending(o => o.App_Time).Skip(id * per).Take(per).ToList();
            return View(list);
        }

        public ActionResult 流程查询列表内容()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("TaskDone", "Task");
            var int_id = int.Parse(id);
            var task = DB.f______列表流程单条表(int_id).FirstOrDefault();
            if (task.AppT_ID.StartsWith("IOA_Ask", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.XType = "Ask";
                ObjectResult<string> fnx = DB.f______列表流程文件表(ViewBag.XType, task.AppO_ID);
                var fn = fnx.SingleOrDefault();
                if (!string.IsNullOrEmpty(fn))
                {
                    var name = fn.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var dir = new DirectoryInfo(Directory + "\\Resource\\GovFiles");
                    ViewBag.Path = "";
                    foreach (var cDir in dir.GetDirectories().OrderByDescending(o => o.CreationTime))
                    {
                        if (cDir.GetFiles().Count(o => o.Name.ToLower() == name.ToLower()) > 0)
                        {
                            string pathx = dir + "\\" + cDir.Name + "\\" + name;
                            var converted = ConvertDoc(pathx);
                            if (!string.IsNullOrEmpty(converted))
                            {
                                ViewBag.PDF = converted;
                            }
                        }
                    }
                }
            }
            else if (task.AppT_ID.StartsWith("IOA_Accept", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.XType = "Accept";
                ObjectResult<string> fnx = DB.f______列表流程文件表(ViewBag.XType, task.AppO_ID);
                var fn = fnx.SingleOrDefault();
                if (!string.IsNullOrEmpty(fn))
                {
                    var name = fn.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var dir = new DirectoryInfo(Directory + "\\Resource\\GovFiles");
                    ViewBag.Path = "";
                    foreach (var cDir in dir.GetDirectories().OrderByDescending(o => o.CreationTime))
                    {
                        if (cDir.GetFiles().Count(o => o.Name.ToLower() == name.ToLower()) > 0)
                        {
                            string pathx = dir + "\\" + cDir.Name + "\\" + name;
                            var converted = ConvertDoc(pathx);
                            if (!string.IsNullOrEmpty(converted))
                            {
                                ViewBag.PDF = converted;
                            }
                        }
                    }
                }
            }
            else if (task.AppT_ID.StartsWith("IOA_Send", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.XType = "Send";
                ObjectResult<string> fnx = DB.f______列表流程文件表(ViewBag.XType, task.AppO_ID);
                var fn = fnx.SingleOrDefault();
                if (!string.IsNullOrEmpty(fn))
                {
                    var name = fn.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var dir = new DirectoryInfo(Directory + "\\Resource\\GovFiles");
                    ViewBag.Path = "";
                    foreach (var cDir in dir.GetDirectories().OrderByDescending(o => o.CreationTime))
                    {
                        if (cDir.GetFiles().Count(o => o.Name.ToLower() == name.ToLower()) > 0)
                        {
                            string pathx = dir + "\\" + cDir.Name + "\\" + name;
                            var converted = ConvertDoc(pathx);
                            if (!string.IsNullOrEmpty(converted))
                            {
                                ViewBag.PDF = converted;
                            }
                        }
                    }
                }
            }
            else if (task.AppT_ID.StartsWith("IOA_Message", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.XType = "Message";
                var message = DB.f______信息门户内容表(task.AppO_ID).FirstOrDefault();
                ViewBag.Html = message.MessageHTML;
                if (!string.IsNullOrEmpty(message.MessageFileName))
                {
                    var name = message.MessageFileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last();
                    var dir = new DirectoryInfo(Directory + "\\Resource\\MessageFile");
                    ViewBag.Path = "";
                    ViewBag.ModuleTypeID = message.ModuleTypeID;
                    foreach (var cDir in dir.GetDirectories().OrderByDescending(o => o.CreationTime))
                    {
                        if (cDir.GetFiles().Count(o => o.Name.ToLower() == name.ToLower()) > 0)
                        {
                            string pathx = dir + "\\" + cDir.Name + "\\" + name;
                            var converted = ConvertDoc(pathx);
                            if (!string.IsNullOrEmpty(converted))
                            {
                                ViewBag.PDF = converted;
                            }
                        }
                    }
                }
            }
            var obj = new 流程查询对象内容();
            obj.对象 = task;
            var flow = DB.f______列表流程流程表(int_id).OrderBy(o => o.App_ID).ToList();
            obj.流程 = flow;
            return View(obj);
        }
    }
}
