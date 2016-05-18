using Platform.JHMobile.Models;
using System;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 待办工作Controller : OfficeController
    {
        private int per = 10;

        public ActionResult 首页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var count = DB.f______计数待办工作数(Account).Single();
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            return View();
        }

        public ActionResult 待办工作列表单页()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            ViewBag.Current = id + 1;
            var list = DB.f______列表待办工作表(Account).OrderByDescending(o => o.App_BeginTime).Skip(id * per).Take(per).ToList();
            return View(list);
        }

        public ActionResult 待办工作列表内容()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("首页", "待办工作");
            var int_id = int.Parse(id);
            var task = DB.f______列表待办单条表(Account, int_id).FirstOrDefault();
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
            var obj = new 待办工作对象内容();
            obj.对象 = task;
            var flow = DB.f______列表待办流程表(int_id).OrderBy(o => o.App_ID).ToList();
            obj.流程 = flow;
            var button = DB.f______列表待办按钮表(task.AppD_ID, task.Version).OrderBy(o => o.AppDA_Type).ToList();
            obj.按钮 = button;
            return View(obj);
        }

        public static string 待办工作流转处理(int type)
        {
            switch (type)
            {
                case 5:
                    return "返回";
                case 9:
                    return "办结";
                default:
                    return "下步";
            }
        }

        public ActionResult 待办工作流转下步()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("首页", "待办工作");
            var int_id = int.Parse(id);
            var task = DB.f______列表待办单条表(Account, int_id).FirstOrDefault();
            var users = DB.f______列表待办下步表(task.AppD_ID, task.Version, 6).ToList();
            var so = new 待办工作办理内容();
            so.对象 = task;
            so.类型 = "Next";
            so.文本 = Request["stepText"];
            so.按钮 = users;
            return View(so);
        }

        public ActionResult 待办工作流转下步处理()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("首页", "待办工作");
            var int_id = int.Parse(id);
            var task = DB.f______列表待办单条表(Account, int_id).FirstOrDefault();
            var user = Request["stepUser"];
            var idea = Request["stepText"];
            var hint = Request["stepHint"];
            DB.f______待办工作转下步(task.App_ID, user, hint, Account, idea);
            return RedirectToAction("首页", "待办工作");
        }

        public ActionResult 待办工作流转返回()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("首页", "待办工作");
            var int_id = int.Parse(id);
            var task = DB.f______列表待办单条表(Account, int_id).FirstOrDefault();
            var so = new 待办工作办理内容();
            so.对象 = task;
            so.类型 = "Back";
            so.文本 = Request["stepText"];
            return View(so);
        }

        public ActionResult 待办工作流转返回处理()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("首页", "待办工作");
            var int_id = int.Parse(id);
            var task = DB.f______列表待办单条表(Account, int_id).FirstOrDefault();
            var user = Request["stepUser"];
            var idea = Request["stepText"];
            var hint = Request["stepHint"];
            DB.f______待办工作转返回(task.App_ID, hint, Account, idea);
            return RedirectToAction("首页", "待办工作");
        }

        public ActionResult 待办工作流转办结()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("首页", "待办工作");
            var int_id = int.Parse(id);
            var task = DB.f______列表待办单条表(Account, int_id).FirstOrDefault();
            var so = new 待办工作办理内容();
            so.对象 = task;
            so.类型 = "Done";
            so.文本 = Request["stepText"];
            return View(so);
        }

        public ActionResult 待办工作流转办结处理()
        {
            if (string.IsNullOrEmpty(Account))
                return 认证();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("首页", "待办工作");
            var int_id = int.Parse(id);
            var task = DB.f______列表待办单条表(Account, int_id).FirstOrDefault();
            var idea = Request["stepText"];
            DB.f______待办工作转办结(task.App_ID, Account, idea);
            return RedirectToAction("首页", "待办工作");
        }
    }
}
