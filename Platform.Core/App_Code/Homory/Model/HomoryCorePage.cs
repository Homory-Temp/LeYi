using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Homory.Model
{
    public abstract class HomoryCorePage : HomoryPage
    {
        protected bool IsOnline
        {
            get { return Session[HomoryCoreConstant.SessionUserId] != null; }
        }

        protected abstract string PageRight { get; }

        protected void LogOp(OperationType type)
        {
            HomoryContext.Value.Operated(type, CurrentUser, CurrentCampus);
        }

        protected void LogOp(State state)
        {
            HomoryContext.Value.Operated(state == State.删除 ? OperationType.删除 : OperationType.编辑, CurrentUser, CurrentCampus);
        }

        protected User CurrentUser
        {
            get
            {
                try
                {
                    var id = (Guid)Session[HomoryCoreConstant.SessionUserId];
                    return HomoryContext.Value.User.Single(o => o.Id == id);
                }
                catch
                {
                    var path = Request.Url.AbsoluteUri;
                    if (path.IndexOf('?') > 0)
                        path = path.Substring(0, path.IndexOf('?'));
                    var query = Request.QueryString.ToString();
                    var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
                        string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                    Session["CORE"] = "CORE";
                    Response.Redirect(url, true);
                    return null;
                }
            }
        }

        private Department _campus;

        protected Department CurrentCampus
        {
            get
            {
                if (_campus != null) return _campus;
                var department = CurrentUser.DepartmentUser.FirstOrDefault(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State == State.启用);
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
                    _isMaster = CurrentUser != null && CurrentUser.DepartmentUser.Count(o => o.Type == DepartmentUserType.班级班主任 && o.State == State.启用) > 0;
                }
                return _isMaster.Value;
            }
        }

        private List<string> _rights;

        protected List<string> CurrentRights
        {
            get
            {
                if (_rights != null) return _rights;
                if (CurrentUser.State == State.内置 || CurrentUser.UserRole.Count(o => o.Role.State == 0) > 0)
                {
                    _rights =
                        HomoryContext.Value.Right.Where(o => o.ApplicationId == HomoryCoreConstant.ApplicationKey)
                            .Select(o => o.Name)
                            .ToList();
                }
                else
                {
                    var role = CurrentUser.UserRole;
                    _rights = role.Count == 0
                        ? new[] { HomoryCoreConstant.RightEveryone }.ToList()
                        : role.Where(o => o.State < State.审核)
                            .ToList()
                            .Join(HomoryContext.Value.RoleRight.Where(o => o.State < State.审核), o => o.RoleId, o => o.RoleId,
                                (o1, o2) => o2.RightName)
                            .ToList()
                            .Union(new[] { HomoryCoreConstant.RightEveryone })
                            .ToList();
                }
                return _rights;
            }
        }

        protected virtual void CheckRight()
        {
            if (!CurrentRights.Contains(PageRight))
            {
                Response.Redirect(Application["Core"] + "Go/Home", false);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
            Title = doc.Root.Element("Core").Value;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["OnlineId"]))
            {
                var id = Guid.Parse(Request.QueryString["OnlineId"]);
                if (HomoryContext.Value.UserOnline.Count(o => o.Id == id) == 0)
                {
                    var path = Request.Url.AbsoluteUri;
                    if (path.IndexOf('?') > 0)
                        path = path.Substring(0, path.IndexOf('?'));
                    var query = Request.QueryString.ToString();
                    var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOff", Server.UrlEncode(path),
                        string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                    Session[HomoryCoreConstant.SessionUserId] = null;
                    Session["CORE"] = null;
                    Response.Redirect(url, false);
                }
                else
                {
                    Session[HomoryCoreConstant.SessionUserId] = HomoryContext.Value.UserOnline.Single(o => o.Id == id).UserId;
                    base.OnLoad(e);
                    CheckRight();
                }
            }
            if (IsOnline && Session["CORE"] != null)
            {
                base.OnLoad(e);
                CheckRight();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["OnlineId"]))
                {
                    var path = Request.Url.AbsoluteUri;
                    if (path.IndexOf('?') > 0)
                        path = path.Substring(0, path.IndexOf('?'));
                    var query = Request.QueryString.ToString();
                    var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
                        string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                    Session["CORE"] = "CORE";
                    Response.Redirect(url, false);
                }
            }
        }
    }
}
