using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform.Contact.Controllers
{
    public class UserController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            var search = RouteData.Values["id"] == null ? "" : Server.UrlDecode(RouteData.Values["id"].ToString()).ToLower();
            var users = db.Contact_GetDepartmentVIPUsers().OrderBy(u => u.RealName).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(o => o.CampusName.ToLower().Contains(search) || o.RealName.ToLower().Contains(search) || o.PinYin.ToLower().Contains(search) || o.Phone.Contains(search)).ToList();
            }
            users.RemoveAll(u => users.Count(x => x.Id == u.Id) > 1 && u.Type == -2);
            return View(users);
        }
    }
}
