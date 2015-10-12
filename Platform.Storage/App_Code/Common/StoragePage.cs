using Models;
using System;
using System.Linq;
using System.Xml.Linq;

public class StoragePage : System.Web.UI.Page
{
    protected Lazy<StorageEntity> db = new Lazy<StorageEntity>(() => new StorageEntity());

    protected bool IsOnline
    {
        get { return Session["Storage__UserId"] != null; }
    }

    protected Guid CurrentCampus
    {
        get
        {
            if (CurrentUser == Guid.Empty)
                return Guid.Empty;
            var user = db.Value.User.Single(o => o.Id == CurrentUser);
            var department = user.DepartmentUser.FirstOrDefault(o => (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.State == State.启用);
            if (department == null) return Guid.Empty;
            return department.TopDepartmentId;
        }
    }

    protected Guid CurrentUser
    {
        get
        {
            try
            {
                var id = (Guid)Session["Storage__UserId"];
                return id;
            }
            catch
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
                    string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Storage__"] = "Storage__";
                Response.Redirect(url, true);
                return Guid.Empty;
            }
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        #region 
        var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
        Title = doc.Root.Element("Storage").Value;
        if (!string.IsNullOrWhiteSpace("OnlineId".Query()))
        {
            var id = "OnlineId".Query().GlobalId();
            if (db.Value.UserOnline.Count(o => o.Id == id) == 0)
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOff", Server.UrlEncode(path),
                    string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Storage__UserId"] = null;
                Session["Storage__"] = null;
                Response.Redirect(url, false);
            }
            else
            {
                Session["Storage__UserId"] = db.Value.UserOnline.Single(o => o.Id == id).UserId;
                base.OnLoad(e);
                // CheckRight();
            }
        }
        if (IsOnline && Session["Storage__"] != null)
        {
            base.OnLoad(e);
            // CheckRight();
        }
        else
        {
            if (string.IsNullOrWhiteSpace("OnlineId".Query()))
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = string.Format("{0}?SsoRedirect={1}{2}{3}", Application["Sso"] + "Go/SignOn", Server.UrlEncode(path),
                    string.IsNullOrWhiteSpace(query) ? string.Empty : "&", query);
                Session["Storage__"] = "Storage__";
                Response.Redirect(url, false);
            }
        }
        #endregion
    }

    protected bool Right_Create
    {
        get
        {
            var query = db.Value.Database.SqlQuery<User>("SELECT * FROM 仓库创建员");
            var user = query.SingleOrDefault(o => o.Id == CurrentUser);
            return user != null;
        }
    }
}

public class SingleStoragePage : StoragePage
{
    protected override void OnLoad(EventArgs e)
    {
        if ("StorageId".Query().Null())
        {
            Response.Redirect("~/Storage/Storage");
            return;
        }
        if (db.Value.StorageGet(StorageId).StorageRole.Count == 0)
        {
            db.Value.InitializePermission(CurrentUser, StorageId);
            db.Value.StorageSave();
        }
        base.OnLoad(e);
    }

    protected Guid StorageId
    {
        get { return "StorageId".Query().GlobalId(); }
    }

    protected bool Right_Query
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "?")).Single();
            return query > 0;
        }
    }

    protected bool Right_In
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "+")).Single();
            return query > 0;
        }
    }

    protected bool Right_Use
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "*")).Single();
            return query > 0;
        }
    }

    protected bool Right_Out
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "-")).Single();
            return query > 0;
        }
    }

    protected bool Right_Set
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "!")).Single();
            return query > 0;
        }
    }
}

public class StoragePageMobile : System.Web.UI.Page
{
    protected Lazy<StorageEntity> db = new Lazy<StorageEntity>(() => new StorageEntity());

    protected bool IsOnline
    {
        get { return Session["Storage__UserId"] != null; }
    }

    protected Guid CurrentCampus
    {
        get
        {
            if (CurrentUser == Guid.Empty)
                return Guid.Empty;
            var user = db.Value.User.Single(o => o.Id == CurrentUser);
            var department = user.DepartmentUser.FirstOrDefault(o => (o.Type == DepartmentUserType.主职 || o.Type == DepartmentUserType.借调) && o.State == State.启用);
            if (department == null) return Guid.Empty;
            return department.TopDepartmentId;
        }
    }

    protected Guid CurrentUser
    {
        get
        {
            try
            {
                var id = (Guid)Session["Storage__UserId"];
                return id;
            }
            catch
            {
                var path = Request.Url.AbsoluteUri;
                if (path.IndexOf('?') > 0)
                    path = path.Substring(0, path.IndexOf('?'));
                var query = Request.QueryString.ToString();
                var url = "~/Login.aspx";
                Session["Storage__"] = "Storage__";
                Response.Redirect(url, true);
                return Guid.Empty;
            }
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
        Title = doc.Root.Element("Storage").Value;
        if (IsOnline)
        {
            base.OnLoad(e);
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected bool Right_Create
    {
        get
        {
            var query = db.Value.Database.SqlQuery<User>("SELECT * FROM 仓库创建员");
            var user = query.SingleOrDefault(o => o.Id == CurrentUser);
            return user != null;
        }
    }
}

public class SingleStoragePageMobile : StoragePageMobile
{
    protected override void OnLoad(EventArgs e)
    {
        if ("StorageId".Query().Null())
        {
            Response.Redirect("~/Storage/StorageMobile");
            return;
        }
        if (db.Value.StorageGet(StorageId).StorageRole.Count == 0)
        {
            db.Value.InitializePermission(CurrentUser, StorageId);
            db.Value.StorageSave();
        }
        base.OnLoad(e);
    }

    protected Guid StorageId
    {
        get { return "StorageId".Query().GlobalId(); }
    }

    protected bool Right_Query
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "?")).Single();
            return query > 0;
        }
    }

    protected bool Right_In
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "+")).Single();
            return query > 0;
        }
    }

    protected bool Right_Use
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "*")).Single();
            return query > 0;
        }
    }

    protected bool Right_Out
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "-")).Single();
            return query > 0;
        }
    }

    protected bool Right_Set
    {
        get
        {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "!")).Single();
            return query > 0;
        }
    }
}
