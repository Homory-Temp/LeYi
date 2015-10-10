using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Extended
{
	public partial class ExtendedAppUserType : HomoryCorePageWithNotify
	{
		private const string Right = "Application";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
// ReSharper disable Html.PathError
				Response.Redirect("~/Go/AppManage", false);
                return;
// ReSharper restore Html.PathError
			}
            if (!IsPostBack)
            {
                var id = Guid.Parse(Request.QueryString[0]);
                var app = HomoryContext.Value.Application.First(o => o.Id == id);
                var list = app.ApplicationRole.ToList();
                b1.Checked = list.Count(o => o.UserType == UserType.教师) > 0;
                b2.Checked = list.Count(o => o.UserType == UserType.学生) > 0;
                b3.Checked = list.Count(o => o.UserType == UserType.注册) > 0;
            }
        }

		protected override string PageRight
		{
			get { return Right; }
		}

        protected void b_CheckedChanged(object sender, EventArgs e)
        {
            var id = Guid.Parse(Request.QueryString[0]);
            var type = (UserType)(int.Parse((sender as RadButton).Value));
            if ((sender as RadButton).Checked)
            {
                if (HomoryContext.Value.ApplicationRole.Count(o => o.ApplicationId == id && o.UserType == type) == 0)
                    HomoryContext.Value.ApplicationRole.Add(new ApplicationRole { ApplicationId = id, UserType = type });
            }
            else
            {
                if (HomoryContext.Value.ApplicationRole.Count(o => o.ApplicationId == id && o.UserType == type) > 0)
                    HomoryContext.Value.ApplicationRole.Where(o => o.ApplicationId == id && o.UserType == type).Delete();
            }
            HomoryContext.Value.SaveChanges();
        }
    }
}
