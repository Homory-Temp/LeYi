using Platform.JHMobile.Models;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class 微信Controller : OfficeController
    {
        new public ActionResult 认证()
        {
            return View();
        }

        public ActionResult 授权()
        {
            return View();
        }
    }
}
