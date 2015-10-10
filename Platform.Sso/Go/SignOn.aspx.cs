using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Management;
using System.Web;
using EntityFramework.Extensions;
using Homory.Model;
using System.Xml.Linq;
using System.Text;

namespace Go
{
    public partial class GoSignOn : HomoryPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
                this.Title = doc.Root.Element("Sso").Value;
                User user;
                if (IsSsoOnline(out user))
                {
                    if (string.IsNullOrEmpty(Request.QueryString["Permanent"]))
                    {
                        if (string.IsNullOrEmpty(Request.QueryString["SsoRedirect"]))
                        {
                            Response.Redirect(string.Format("SsoRedirect".FromWebConfig() == "" ? Application["Sso"] + "Go/Board" : "SsoRedirect".FromWebConfig(), user.Account, HomoryCryptor.Decrypt(user.Password, user.CryptoKey, user.CryptoSalt), user.UserOnline.First().Id), false);
                        }
                        else
                        {
                            var path = Server.UrlDecode(Request.QueryString["SsoRedirect"]);
                            var query = Request.QueryString.ToString();
                            string url;
                            if (query.IndexOf("OnlineId=") >= 0)
                            {
                                url = path;
                            }
                            else
                            {
                                if (query.IndexOf('&') <= 0)
                                {
                                    url = string.Format("{0}?OnlineId={1}", path, user.UserOnline.First().Id);
                                }
                                else
                                {
                                    var index = query.IndexOf('&');
                                    url = string.Format("{0}?OnlineId={1}{2}", path, user.UserOnline.First().Id, query.Substring(index));
                                }
                            }
                            Response.Redirect(url, false);
                        }
                    }
                    else
                    {
                        RedierectPermanent(user);
                    }
                    LoadInit();
                }
                else
                {
                    LoadInit();
                }
            }
        }

        protected void RedierectPermanent(User user)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Permanent"]) && Request.QueryString["Permanent"].Equals("Jcms", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string url = string.Format("Jcms".FromWebConfig(), user.UserOnline.First().Id.ToString(), user.Teacher.AutoId.ToString(), Server.UrlEncode(user.Account.Trim()), Server.UrlEncode(user.RealName.Trim()), Server.UrlEncode(user.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.审核).TopDepartment.Name.Trim()));
                    Response.Redirect(url, false);
                }
                catch
                {
                    string url = string.Format("Jcms".FromWebConfig(), "", "", "", "", "");
                    Response.Redirect(url, false);
                }
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["Permanent"]) && Request.QueryString["Permanent"].Equals("JMail", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    string url = string.Format("JMail".FromWebConfig(), user.UserOnline.First().Id.ToString(), user.Teacher.AutoId.ToString(), Server.UrlEncode(user.Account.Trim()), Server.UrlEncode(user.RealName.Trim()), Server.UrlEncode(user.DepartmentUser.First(o => (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State < State.审核).TopDepartment.Name.Trim()));
                    Response.Redirect(url, false);
                }
                catch
                {
                    string url = string.Format("JMail".FromWebConfig(), "", "", "", "", "");
                    Response.Redirect(url, false);
                }
            }
        }

        protected bool IsSsoOnline(out User user)
        {
            try
            {
                var onlineStringId = string.Empty;
                if (Session[HomoryConstant.SessionOnlineId] != null)
                    onlineStringId = Session[HomoryConstant.SessionOnlineId].ToString();
                else if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
                {
                    var httpCookie = Request.Cookies[HomoryConstant.CookieOnlineId];
                    if (httpCookie != null)
                    {
                        var value = httpCookie.Value;
                        HttpContext.Current.Session[HomoryConstant.SessionOnlineId] = Guid.Parse(value);
                        onlineStringId = value;
                    }
                }
                if (string.IsNullOrEmpty(onlineStringId))
                {
                    user = null;
                    return false;
                }
                var onlineId = Guid.Parse(onlineStringId);
                var online = HomoryContext.Value.UserOnline.SingleOrDefault(o => o.Id == onlineId);
                if (online == null)
                {
                    Session.Remove(HomoryConstant.SessionOnlineId);
                    if (Request.Cookies.AllKeys.Contains(HomoryConstant.CookieOnlineId))
                    {
                        var cookie = Request.Cookies[HomoryConstant.CookieOnlineId];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddSeconds(-1);
                            Response.SetCookie(cookie);
                        }
                    }
                    user = null;
                    return false;
                }
                else
                {
                    var cookie = new HttpCookie(HomoryConstant.CookieOnlineId, online.Id.ToString().ToUpper());
                    var expire = int.Parse(HomoryContext.Value.ApplicationPolicy.Single(o => o.Name == "UserCookieExpire" && o.ApplicationId == Guid.Empty).Value);
                    cookie.Expires = DateTime.Now.AddMinutes(expire);
                    HttpContext.Current.Response.SetCookie(cookie);
                    online.TimeStamp = DateTime.Now;
                    HomoryContext.Value.SaveChanges();
                }
                user = online.User;
                return true;
            }
            catch
            {
                user = null;
                return false;
            }
        }

        private void LoadInit()
        {
            var open =
                HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "UserRegistration" && o.ApplicationId == Guid.Empty)
                    .FutureFirstOrDefault();
            var favourite = HomoryContext.Value.Dictionary.Where(o => o.Key == "FavouriteCount").FutureFirstOrDefault();
            var platform =
                HomoryContext.Value.Application.Where(o => o.Type == ApplicationType.平台 && o.State == State.启用).FutureCount();
            var resource = HomoryContext.Value.Dictionary.Where(o => o.Key == "ResourceAmount").FutureFirstOrDefault();
            var user = HomoryContext.Value.User.Where(o => o.State == State.启用).FutureCount();
            var visit = HomoryContext.Value.Dictionary.Where(o => o.Key == "ApplicationLogin").FutureFirstOrDefault();
            buttonRegister.Visible = bool.Parse(open.Value.Value);
            FavouriteCount.Text = string.Format("{0}个赞", favourite.Value.Value);
            ApplicationCount.Text = platform.Value.ToString();
            ResourceCount.Text =
                ConvertResourceCount(decimal.Parse(resource.Value.Value));
            UserCount.Text = user.Value.ToString();
            VisitCount.Text = visit.Value.Value;
        }

        private readonly string[] _units = { "B", "K", "M", "G", "T" };

        private string ConvertResourceCount(decimal count)
        {
            var index = 0;
            var value = count;
            while (value > 1024)
            {
                value = decimal.Divide(value, 1024);
                index++;
            }
            return string.Format("{0}{1}", Math.Round(value, 2), _units[index]);
        }

        protected void signThumbPost_OnClick(object sender, EventArgs e)
        {
            var count = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "FavouriteCount").Value) + 1;
            HomoryContext.Value.Dictionary.Where(o => o.Key == "FavouriteCount")
                .Update(o => new Dictionary { Value = count.ToString() });
            HomoryContext.Value.SaveChanges();
            FavouriteCount.Text = string.Format("{0}个赞", count);
            areaFavourite.ResponseScripts.Add("doInit();");
        }

        protected dynamic UserSignOn(Entities db, string account, string password)
        {
            dynamic output = new ExpandoObject();
            output.Ok = false;
            output.Data = new ExpandoObject();
            try
            {
                var user = db.User.SingleOrDefault(o => o.Account == account && o.State < State.删除);
                if (user == null ||
                    !password.Equals(HomoryCryptor.Decrypt(user.Password, user.CryptoKey, user.CryptoSalt),
                        StringComparison.OrdinalIgnoreCase))
                {
                    output.Data.Message = "请输入正确的账号和密码";
                    output.Entity = null;
                    return output;
                }
                if (user.State == State.停用)
                {
                    output.Data.Message = "用户已被停用";
                    output.Entity = null;
                    return output;
                }
                if (user.State == State.默认 || user.State == State.审核)
                {
                    Session[HomoryConstant.SessionRegisterId] = user.Id;
                    output.Ok = true;
                    output.Data.Redirect = true;
                    output.Data.RedirectUrl = Application["Sso"] + "Go/ToVerify";
                    output.Entity = null;
                    return output;
                }
                var online = user.UserOnline.SingleOrDefault();
                if (online == null)
                {
                    online = new UserOnline
                    {
                        Id = db.GetId(),
                        UserId = user.Id,
                        TimeStamp = DateTime.Now
                    };
                    db.UserOnline.Add(online);
                }
                else
                {
                    online.TimeStamp = DateTime.Now;
                }
                db.SaveChanges();
                var cookie = new HttpCookie(HomoryConstant.CookieOnlineId, online.Id.ToString().ToUpper());
                var expire = int.Parse(db.ApplicationPolicy.Single(o => o.Name == "UserCookieExpire" && o.ApplicationId == Guid.Empty).Value);
                cookie.Expires = DateTime.Now.AddMinutes(expire);
                Response.SetCookie(cookie);
                Session[HomoryConstant.SessionOnlineId] = online.Id;
                output.Ok = true;
                output.Data.Redirect = true;
                var query = Server.UrlDecode(Request.QueryString["SsoRedirect"]);
                var query_x = Request.QueryString;
                if (string.IsNullOrEmpty(Request.QueryString["Permanent"]))
                {
                    if (string.IsNullOrWhiteSpace(Request.QueryString["SsoRedirect"]))
                    {
                        output.Data.RedirectUrl = string.Format(string.Format("SsoRedirect".FromWebConfig() == "" ? Application["Sso"] + "Go/Board" : "SsoRedirect".FromWebConfig(), user.Account, HomoryCryptor.Decrypt(user.Password, user.CryptoKey, user.CryptoSalt)));
                    }
                    else
                    {
                        string url;
                        if (!string.IsNullOrWhiteSpace(Request.QueryString["OnlineId"]))
                        {
                            url = query;
                        }
                        else
                        {
                            if (query.IndexOf('&') <= 0)
                            {
                                url = string.Format("{0}?OnlineId={1}", query, user.UserOnline.Single().Id);
                            }
                            else
                            {
                                var index = query.IndexOf('&');
                                url =
                                    Server.UrlDecode(query.Remove(index, 1)
                                        .Insert(index, string.Format("?OnlineId={0}&", user.UserOnline.Single().Id)));
                            }
                        }
                        foreach (var qx in query_x)
                        {
                            if (!qx.ToString().Equals("SsoRedirect", StringComparison.OrdinalIgnoreCase))
                                url += "&" + qx + "=" + query_x[qx.ToString()];
                        }
                        output.Entity = null;
                        output.Data.RedirectUrl = string.IsNullOrWhiteSpace(url) ? string.Format(string.Format("SsoRedirect".FromWebConfig() == "" ? Application["Sso"] + "Go/Board" : "SsoRedirect".FromWebConfig(), user.Account, HomoryCryptor.Decrypt(user.Password, user.CryptoKey, user.CryptoSalt))) : url;
                    }
                }
                else
                {
                    output.Entity = user;
                    output.Data.RedirectUrl = "*Permanent*";
                }
                return output;
            }
            catch (Exception exception)
            {
                output.Data.Message = exception.Message;
                output.Entity = null;
                return output;
            }
        }

        protected void LogLogin(bool ok, string account)
        {
            SignLog ll = new SignLog();
            ll.Id = HomoryContext.Value.GetId();
            ll.Login = ok;
            ll.Time = DateTime.Now;
            ll.TriedAccount = account;
            ll.Browser = Request.Browser.Browser;
            ll.IP = Request.UserHostAddress;
            if (ok)
            {
                var user = HomoryContext.Value.User.SingleOrDefault(o => o.Account == account && o.State < State.删除);
                if (user != null)
                {
                    ll.UserId = user.Id;
                    var campus = user.DepartmentUser.SingleOrDefault(o => o.State < State.审核 && (o.Type == DepartmentUserType.班级学生 || o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师));
                    if (campus != null)
                    {
                        ll.CampusId = campus.TopDepartmentId;
                    }
                }
            }
            HomoryContext.Value.SignLog.Add(ll);
        }

        public string GetMac(string IP)
        {
            string dirResults = "";
            ProcessStartInfo psi = new ProcessStartInfo();
            Process proc = new Process();
            psi.FileName = "nbtstat";
            psi.RedirectStandardInput = false;
            psi.RedirectStandardOutput = true;
            psi.Arguments = "-A " + IP;
            psi.UseShellExecute = false;
            proc = Process.Start(psi);
            dirResults = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            dirResults = dirResults.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            int i = dirResults.LastIndexOf("=");
            dirResults = dirResults.Substring(i + 2, 17);
            if (dirResults.IndexOf("本地连接") != -1)
            {
                dirResults = "没有得到mac";
            }
            return dirResults;
        }

        protected void buttonSign_OnClick(object sender, EventArgs e)
        {
            var loginName = userName.Value;
            var output = UserSignOn(HomoryContext.Value, loginName, userPassword.Value);
            if (output.Ok)
            {
                LogLogin(true, loginName);
                var count = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "ApplicationLogin").Value) + 1;
                HomoryContext.Value.Dictionary.Where(o => o.Key == "ApplicationLogin")
                    .Update(o => new Dictionary { Value = count.ToString() });
                HomoryContext.Value.SaveChanges();
                if (output.Data.Redirect)
                {
                    if (output.Data.RedirectUrl == "*Permanent*" && output.Entity != null)
                    {
                        RedierectPermanent(output.Entity);
                    }
                    else
                    {
                        string url = output.Data.RedirectUrl;
                        StringBuilder sb = new StringBuilder(url);
                        if (url.IndexOf('&') > 0 && url.IndexOf('?') < 0)
                        {
                            var index = url.IndexOf('&');
                            sb[index] = '?';
                        }
                        // Response.Redirect(sb.ToString(), false);
                        var script_re = string.Format("top.location.href = '{0}';", sb.ToString());
                        areaAction.ResponseScripts.Add(script_re);
                    }
                    return;
                }
            }
            LogLogin(false, loginName);
            HomoryContext.Value.SaveChanges();
            var script = string.Format("doInit(); notify('#{0}', '{1}', 'error');", buttonSign.ClientID, output.Data.Message);
            areaAction.ResponseScripts.Add(script);
        }

        protected void buttonRegister_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Application["Sso"] + "Go/Register", false);
        }
    }
}
