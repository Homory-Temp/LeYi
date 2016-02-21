﻿using Platform.JHMobile.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class CallController : JinHerController
    {
        public ActionResult CallToRead()
        {
            if (string.IsNullOrEmpty(Account))
                return new DingController().Authentication();
            var count = db.f____Mobile_Count_CallToRead(Account).FirstOrDefault().Value;
            var id = RouteData.Values["id"] == null ? 0 : int.Parse(RouteData.Values["id"].ToString());
            var per = 10;
            if (count < id * per)
                return RedirectToAction("CallToRead", "Call", new { id = id - 1 });
            var list = db.f____Mobile_List_CallToRead(Account).OrderByDescending(o => o.CallTime).Skip(id * per).Take(per).ToList();
            ViewBag.Min = 0;
            ViewBag.Max = count % per == 0 ? count / per - 1 : (count + (per - count % per)) / per - 1;
            ViewBag.Current = id;
            return View(list);
        }

        public ActionResult CallPreview()
        {
            if (string.IsNullOrEmpty(Account))
                return new DingController().Authentication();
            var id = RouteData.Values["id"].ToString();
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("CallToRead", "Call");
            var int_id = int.Parse(id);
            var obj = db.f____Mobile_List_CallToRead(Account).FirstOrDefault(o => o.CallNoSeeID == int_id);
            var query = db.f____Mobile_List_CallAttachment(obj.CallID.ToString()).OrderBy(o => o.SlaveID);
            var list = query == null ? new List<f____Mobile_List_CallAttachment_Result>() : query.ToList();
            foreach (var path in list)
            {
                var source = DingTalk.CorpJinHer + path.FilePath.Substring(3).Replace("/", "\\");
                var destination = source.Replace("__", "h__");
                DecryptFile(source, destination);
            }
            var result = new CallObject();
            result.Object = obj;
            result.List = list;
            return View(result);
        }
    }
}
