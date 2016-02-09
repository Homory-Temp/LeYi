using System;
using System.Linq;

namespace Homory.Model
{
    public abstract class HomoryResourceControl : HomoryControl
    {
        protected bool IsOnline
        {
            get { return Session[HomoryResourceConstant.SessionUserId] != null; }
        }

        private Department homeCampus;

        protected Department HomeCampus
        {
            get
            {
                if (homeCampus != null)
                {
                    return homeCampus;
                }
                if (string.IsNullOrEmpty(Request.QueryString["Campus"]))
                {
                    return null;
                }
                Guid id = Guid.Parse(Request.QueryString["Campus"]);
                homeCampus = HomoryContext.Value.Department.First(o => o.Id == id);
                return homeCampus;
            }
        }

        protected Func<Resource, bool> SR()
        {
            return o => o.User.DepartmentUser.Count(p => p.TopDepartmentId == HomeCampus.Id && p.State < State.审核 && (p.Type == DepartmentUserType.借调后部门主职教师 || p.Type == DepartmentUserType.部门主职教师)) > 0;
        }

        protected Func<User, bool> SU()
        {
            return o => o.DepartmentUser.Count(p => p.TopDepartmentId == HomeCampus.Id && p.State < State.审核 && (p.Type == DepartmentUserType.借调后部门主职教师 || p.Type == DepartmentUserType.部门主职教师)) > 0;
        }

        protected Func<Group, bool> SG()
        {
            return o => o.GroupUser.FirstOrDefault(x => x.State < State.审核 && (x.Type == GroupUserType.创建者 || x.Type == GroupUserType.管理员)).User.DepartmentUser.Count(p => p.TopDepartmentId == HomeCampus.Id && p.State < State.审核 && (p.Type == DepartmentUserType.借调后部门主职教师 || p.Type == DepartmentUserType.部门主职教师)) > 0;
        }

        protected void LogOp(ResourceLogType type, int? value = null)
        {
            HomoryContext.Value.LogOp(CurrentUser.Id, CurrentCampus.Id, type, value);
        }

        protected User CurrentUser
        {
            get
            {
                var id = (Guid)Session[HomoryResourceConstant.SessionUserId];
                return HomoryContext.Value.User.First(o => o.Id == id);
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

        protected string P(object icon)
        {
            return "IconAbsoluteUrl".FromWebConfig() + icon.ToString().Replace("~", "");
        }

        protected string UC(object id)
        {
            var gid = Guid.Parse(id.ToString());
            var user = HomoryContext.Value.User.First(o => o.Id == gid);
            return user.DepartmentUser.Count(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.审核) > 0 ? "[" + user.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.审核).TopDepartment.Name.Replace("无锡市", "").Replace("无锡", "") + "]" : "";
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
