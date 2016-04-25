using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Homory.Model;

namespace Platform.Contact.Controllers
{
    public class DepartmentController : Controller
    {
        private Entities db = new Entities();

        private static readonly Guid TopDepartmentId = Guid.Parse("A885E587-8CF5-4CDA-B212-08D359727E88");

        public ActionResult Index()
        {
            var departmentId = RouteData.Values.ContainsKey("id") ? Guid.Parse(RouteData.Values["id"].ToString()) : TopDepartmentId;
            var departments = db.Contact_GetDepartments(departmentId).OrderBy(d => d.Ordinal).ToList();
            var users = db.Contact_GetDepartmentUsers(departmentId).OrderBy(u => u.Ordinal).ToList();
            return View(new DepartmentObject { Departments = departments, Users = users });
        }
    }

    public class DepartmentObject
    {
        public List<Department> Departments { get; set; }
        public List<Contact_Users> Users { get; set; }
    }
}
