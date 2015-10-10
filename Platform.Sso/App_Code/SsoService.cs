using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EntityFramework.Extensions;

[WebService(Namespace = "http://i.btedu.gov.cn/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class SsoService : System.Web.Services.WebService
{
    public SsoService()
    {
    }

    protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());

    private static readonly Guid SsoApi = Guid.Parse("90B09EDE-1F32-3F31-95A2-EF6B9191BA9A");

    protected void LogApi(Guid apiId, string providerId)
    {
        HomoryContext.Value.ApiLog.Add(new ApiLog
        {
            Id = HomoryContext.Value.GetId(),
            ApiId = apiId,
            ProviderId = providerId,
            Time = DateTime.Now
        });
        HomoryContext.Value.SaveChanges();
    }

    [WebMethod]
    public Guid GetToken()
    {
        if (HttpContext.Current.Session[HomoryConstant.SessionOnlineId] == null)
        {
            return Guid.Empty;
        }
        return (Guid)HttpContext.Current.Session[HomoryConstant.SessionOnlineId];
    }

    [WebMethod]
    public string[] GetUserByToken(Guid token, string provider)
    {
        if (HomoryContext.Value.UserOnline.Count(o => o.Id == token) == 0)
            return new string[] { };
        var uo = HomoryContext.Value.UserOnline.First(o => o.Id == token);
        var u = uo.User;
        var array = new string[6];
        array[0] = token.ToString();
        array[1] = u.DisplayName;
        array[2] = u.Account;
        array[3] = u.RealName;
        array[4] = u.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.删除).DepartmentId.ToString();
        array[5] = u.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.删除).Department.Name;
        array[6] = u.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.删除).Department.TopId.ToString();
        array[7] = u.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.删除).Department.DepartmentRoot.Name;
        array[8] = u.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.删除).User.State.ToString();
        LogApi(SsoApi, provider);
        return array;
    }

    [WebMethod]
    public void RemoveToken(string provider)
    {
        UserSignOff(HomoryContext.Value);
    }

    private bool UserSignOff(Entities db)
    {
        try
        {
            var oid = string.Empty;
            if (Session[HomoryConstant.SessionOnlineId] != null)
            {
                oid = Session[HomoryConstant.SessionOnlineId].ToString();
            }
            else if (HttpContext.Current.Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
            {
                var httpCookie = HttpContext.Current.Request.Cookies[HomoryConstant.CookieOnlineId];
                if (httpCookie != null) oid = httpCookie.Value;
            }
            if (string.IsNullOrEmpty(oid))
            {
                return true;
            }
            var onlineGuid = Guid.Parse(oid);
            db.UserOnline.Where(o => o.Id == onlineGuid).Delete();
            db.SaveChanges();
            Session.Remove(HomoryConstant.SessionOnlineId);
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
            {
                var cookie = HttpContext.Current.Request.Cookies[HomoryConstant.CookieOnlineId];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddSeconds(-1);
                    HttpContext.Current.Response.SetCookie(cookie);
                }
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}
