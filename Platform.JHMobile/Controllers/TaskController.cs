using Platform.JHMobile.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Platform.JHMobile.Controllers
{
    public class TaskController : JinHerController
    {
        public ActionResult TaskToDo()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var count = db.f____Mobile_Count_TaskToDo(Account).Single().Value;
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var per = 10;
            if (count < id * per)
                return RedirectToAction("TaskToDo", "Task", new { id = id - 1 });
            var list = db.f____Mobile_List_TaskToDo(Account).OrderByDescending(o => o.App_BeginTime).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            return View(list);
        }

        public ActionResult TaskDone()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var count = db.f____Mobile_Count_TaskDone(Account).Single().Value;
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var per = 10;
            if (count < id * per)
                return RedirectToAction("TaskDone", "Task", new { id = id - 1 });
            var list = db.f____Mobile_List_TaskDone(Account).OrderByDescending(o => o.App_Time).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            return View(list);
        }

        public ActionResult TaskToDoPreview()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("TaskToDo", "Task");
            var int_id = int.Parse(id);
            var task = db.f____Mobile_List_TaskToDoSingle(Account, int_id).FirstOrDefault();
            var path = Server.MapPath(string.Format("~/Views/Task/Config/{0}.xml", task.Form_ID));
            var obj = new TaskToDoObject();
            obj.Object = task;
            var flow = db.f____Mobile_List_TaskToDoFlow(int_id).OrderBy(o => o.App_ID).ToList();
            obj.Flow = flow;
            var button = db.f____Mobile_List_TaskToDoSingleButton(task.AppD_ID, task.Version).OrderBy(o => o.AppDA_Type).ToList();
            obj.Button = button;
            if (System.IO.File.Exists(path))
            {
                var doc = XDocument.Load(path);
                var sql = doc.Root.Element("sql").Value.Replace("@AppO_Values", task.AppO_Values);
                var fields = doc.Root.Element("fields").Elements("field");
                var dict = new List<TaskToDoConfigObject>();
                foreach (var field in fields)
                {
                    var visible = field.Element("visible").Value == "是";
                    var name = field.Element("fieldname").Value;
                    var display = field.Element("displaylabel").Value;
                    var typeName = field.Element("datatype").Value;
                    Type t;
                    switch (typeName)
                    {
                        default:
                            {
                                t = typeof(string);
                                break;
                            }
                    }
                    dict.Add(new TaskToDoConfigObject { Name = name, DisplayName = display, Visible = visible, Type = t });
                }
                obj.Config = dict;
                var builder = JinHerDynamic.CreateTypeBuilder("Homory", "JinHerTask", "TaskToDoForm");
                foreach (var item in dict)
                {
                    JinHerDynamic.CreateAutoImplementedProperty(builder, item.Name, item.Type);
                }
                var type = builder.CreateType();
                var form = db.Database.SqlQuery(type, sql).ToListAsync().Result;
                obj.Form = form;
            }
            return View(obj);
        }

        public ActionResult TaskDonePreview()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("TaskDone", "Task");
            var int_id = int.Parse(id);
            var task = db.f____Mobile_List_TaskDoneSingle(int_id).FirstOrDefault();
            var path = Server.MapPath(string.Format("~/Views/Task/Config/{0}.xml", task.Form_ID));
            var obj = new TaskDoneObject();
            obj.Object = task;
            var flow = db.f____Mobile_List_TaskDoneFlow(int_id).OrderBy(o => o.App_ID).ToList();
            obj.Flow = flow;
            if (System.IO.File.Exists(path))
            {
                var doc = XDocument.Load(path);
                var sql = doc.Root.Element("sql").Value.Replace("@AppO_Values", task.AppO_Values);
                var fields = doc.Root.Element("fields").Elements("field");
                var dict = new List<TaskDoneConfigObject>();
                foreach (var field in fields)
                {
                    var visible = field.Element("visible").Value == "是";
                    var name = field.Element("fieldname").Value;
                    var display = field.Element("displaylabel").Value;
                    var typeName = field.Element("datatype").Value;
                    Type t;
                    switch (typeName)
                    {
                        default:
                            {
                                t = typeof(string);
                                break;
                            }
                    }
                    dict.Add(new TaskDoneConfigObject { Name = name, DisplayName = display, Visible = visible, Type = t });
                }
                obj.Config = dict;
                var builder = JinHerDynamic.CreateTypeBuilder("Homory", "JinHerTask", "TaskDoneForm");
                foreach (var item in dict)
                {
                    JinHerDynamic.CreateAutoImplementedProperty(builder, item.Name, item.Type);
                }
                var type = builder.CreateType();
                var form = db.Database.SqlQuery(type, sql).ToListAsync().Result;
                obj.Form = form;
            }
            return View(obj);
        }

        public static string ConvertTaskToDoStep(int type)
        {
            switch (type)
            {
                case 5:
                    return "Back";
                case 9:
                    return "Done";
                default:
                    return "Next";
            }
        }

        public ActionResult TaskToDoStepNext()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("TaskToDo", "Task");
            var int_id = int.Parse(id);
            var task = db.f____Mobile_List_TaskToDoSingle(Account, int_id).FirstOrDefault();
            var users = db.f____Mobile_List_TaskToDoSingleButtonNext(task.AppD_ID, task.Version, 6).ToList();
            var so = new TaskToDoStepObject();
            so.Object = task;
            so.Type = "Next";
            so.StepText = Request["stepText"];
            so.Next = users;
            return View(so);
        }

        public ActionResult TaskToDoStepNextDo()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("TaskToDo", "Task");
            var int_id = int.Parse(id);
            var task = db.f____Mobile_List_TaskToDoSingle(Account, int_id).FirstOrDefault();
            var user = Request["stepUser"];
            var idea = Request["stepText"];
            var hint = Request["stepHint"];
            db.f____Mobile_List_TaskDoingNext(task.App_ID, "next", "hint", Account, "idea");
            return RedirectToAction("TaskToDo", "Task");
        }

        public ActionResult TaskToDoStepBack()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("TaskDone", "Task");
            var int_id = int.Parse(id);
            var task = db.f____Mobile_List_TaskToDoSingle(Account, int_id).FirstOrDefault();
            var so = new TaskToDoStepObject();
            so.Object = task;
            so.Type = "Back";
            so.StepText = Request["stepText"];
            return View(so);
        }

        public ActionResult TaskToDoStepDone()
        {
            if (string.IsNullOrEmpty(Account))
                return Authenticate();
            var id = RouteData.Values["id"]?.ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("TaskDone", "Task");
            var int_id = int.Parse(id);
            var task = db.f____Mobile_List_TaskToDoSingle(Account, int_id).FirstOrDefault();
            var so = new TaskToDoStepObject();
            so.Object = task;
            so.Type = "Done";
            so.StepText = Request["stepText"];
            return View(so);
        }
    }
}
