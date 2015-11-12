#define ForceOnline

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Homory.Model
{
    public abstract class HomoryResourcePage : HomoryPage
	{
		protected bool IsOnline
		{
			get { return Session[HomoryResourceConstant.SessionUserId] != null; }
		}

        protected string P(object icon)
        {
            return "IconAbsoluteUrl".FromWebConfig() + icon.ToString().Replace("~", "");
            //if (!url.Equals("~/Common/默认/用户.png") && !url.Equals("~/Common/默认/群组.png") && File.Exists(Server.MapPath(url)))
            //{
            //    return url;
            //}
            //else
            //{
            //    var files = new DirectoryInfo(Server.MapPath("~/Common/头像/随机")).GetFiles();
            //    var r = new Random(Guid.NewGuid().GetHashCode());
            //    return "~/Common/头像/随机/" + files[r.Next(0, files.Length)].Name;
            //}
        }

        private List<string> _rights;

        protected List<string> CurrentRights
        {
            get
            {
                var id = Guid.Parse("3047E587-8CC1-4645-8536-08D1AF49409F");
                if (_rights != null) return _rights;
                if (CurrentUser.State == State.内置 || CurrentUser.UserRole.Count(o => o.Role.State == 0) > 0)
                {
                    _rights =
                        HomoryContext.Value.Right.Where(o => o.ApplicationId == id)
                            .Select(o => o.Name)
                            .ToList();
                }
                else
                {
                    var role = CurrentUser.UserRole;
                    _rights = role.Count == 0
                        ? new[] { "Everyone" }.ToList()
                        : role.Where(o => o.State < State.审核)
                            .ToList()
                            .Join(HomoryContext.Value.RoleRight.Where(o => o.State < State.审核), o => o.RoleId, o => o.RoleId,
                                (o1, o2) => o2.RightName)
                            .ToList()
                            .Union(new[] { "Everyone" })
                            .ToList();
                }
                return _rights;
            }
        }

        protected void LogOp(ResourceLogType type, int? value = null)
        {
            if (!IsOnline)
                return;
            try
            {
                HomoryContext.Value.LogOp(CurrentUser.Id, CurrentCampus.Id, type, value);
            }
            catch
            {
            }
        }

        protected Resource R(object id)
        {
            var gid = Guid.Parse(id.ToString());
            return HomoryContext.Value.Resource.First(o => o.Id == gid);
        }

        protected User U(object id)
        {
            var gid = Guid.Parse(id.ToString());
            return HomoryContext.Value.User.First(o => o.Id == gid);
        }

        protected string UC(object id)
        {
            var gid = Guid.Parse(id.ToString());
            var user = HomoryContext.Value.User.First(o => o.Id == gid);
            return user.DepartmentUser.Count(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.审核) > 0 ? "[" + user.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.审核).TopDepartment.Name.Replace("无锡市", "").Replace("无锡", "") + "]" : "";
        }

        protected User CurrentUser
		{
			get
			{
				var id = (Guid) Session[HomoryResourceConstant.SessionUserId];
				return HomoryContext.Value.User.First(o => o.Id == id);
			}
		}

		private Department _campus;

        protected Department CurrentCampus
        {
            get
            {
                if (_campus != null) return _campus;
                var department =
                    CurrentUser.DepartmentUser.FirstOrDefault(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.审核);
                if (department == null) return null;
                _campus = department.TopDepartment;
                return _campus;
            }
        }

        private bool? _isMaster;

		protected bool IsMaster
		{
			get
			{
				if (!_isMaster.HasValue)
				{
					_isMaster = CurrentUser != null &&
					            CurrentUser.DepartmentUser.Count(o => o.Type == DepartmentUserType.班级班主任 && o.State == State.启用) > 0;
				}
				return _isMaster.Value;
			}
		}

		protected void SignOn()
		{
			var path = Request.Url.AbsoluteUri;
			if (path.IndexOf('?') > 0)
				path = path.Substring(0, path.IndexOf('?'));
			var query = Request.QueryString.ToString();
			var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
				string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
			Response.Redirect(url, false);
		}

		protected void SignOff()
		{
            Session.Clear();
			var path = Request.Url.AbsoluteUri;
			if (path.IndexOf('?') > 0)
				path = path.Substring(0, path.IndexOf('?'));
			var query = Request.QueryString.ToString();
			var url = string.Format("{0}?SsoRedirect={1}", Application["Sso"] + "Go/SignOff", Server.UrlEncode(Application["Resource"] + "Go/Home"));
			Response.Redirect(url, false);
		}

		protected void TrySignOn()
		{
			if (Session["RESOURCE"] == null && !Request.QueryString.AllKeys.Contains("OnlineId"))
			{
				Session["RESOURCE"] = "RESOURCE";
				var path = Request.Url.AbsoluteUri;
				if (path.IndexOf('?') > 0)
					path = path.Substring(0, path.IndexOf('?'));
				var query = Request.QueryString.ToString();
				var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignApi", Server.UrlEncode(path),
					string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
				Response.Redirect(url, false);
			}
		}

        protected abstract bool ShouldOnline { get; }

        protected override void OnLoad(EventArgs e)
		{
            var doc = XDocument.Load(Server.MapPath("~/Common/配置/Title.xml"));
            this.Title = doc.Root.Element("Resource").Value;

            if (Request.QueryString.AllKeys.Contains("OnlineId"))
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["OnlineId"]))
                {
                    var id = Guid.Parse(Request.QueryString["OnlineId"]);
                    if (HomoryContext.Value.UserOnline.Count(o => o.Id == id) == 0)
                    {
                        SignOff();
                        return;
                    }
                    Session[HomoryResourceConstant.SessionUserId] = HomoryContext.Value.UserOnline.First(o => o.Id == id).UserId;
                    Session["RESOURCE"] = "RESOURCE";
                    base.OnLoad(e);
                    return;
                }
            }

            if (IsOnline)
			{
				if (Session["RESOURCE"] == null)
					Session["RESOURCE"] = "RESOURCE";
				base.OnLoad(e);
				return;
			}

            if (Session["RESOURCE"] == null && !Request.QueryString.AllKeys.Contains("OnlineId"))
			{
				TrySignOn();
				return;
			}

#if ForceOnline
            if (!IsOnline)
#else
            if (!IsOnline && ShouldOnline)
#endif
            {
                SignOn();
				return;
			}

			base.OnLoad(e);
		}
	}
}
