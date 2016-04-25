using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Platform.Contact.Controllers
{
    public class QueryController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            var search = RouteData.Values["id"] == null ? "" : Server.UrlDecode(RouteData.Values["id"].ToString()).Trim().ToLower();
            if (search.Length > 0)
            {
                var users = db.Contact_GetUsers(search).OrderBy(u => u.RealName).ToList();
                return View(users);
            }
            else
            {
                return View(new List<Contact_Users>());
            }
        }
    }
}