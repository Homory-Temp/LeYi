using Models;
using System;
using System.Linq;

public partial class Menu : System.Web.UI.UserControl
{

    protected bool IsOnline
    {
        get { return Session["Storage__UserId"] != null; }
    }

    protected override void OnLoad(EventArgs e)
    {
        if ("StorageId".Query().Null())
        {
            Response.Redirect("~/Storage/Storage");
            return;
        }
        if (IsOnline)
        {
            base.OnLoad(e);
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
        base.OnLoad(e);
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
                var url = "~/Default.aspx";
                Session["Storage__"] = "Storage__";
                Response.Redirect(url, true);
                return Guid.Empty;
            }
        }
    }

    protected Lazy<StorageEntity> db = new Lazy<StorageEntity>(() => new StorageEntity());

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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            wzbf.Visible = Right_Out;
            //li_1.Visible = Right_Query;
            //li_2.Visible = Right_Set;
            menu_user.InnerText = db.Value.UserName(CurrentUser);
            menu_storage.InnerText = db.Value.Storage.SingleOrDefault(o => o.Id == StorageId).Name;
        }
    }

    protected Guid StorageId
    {
        get { return "StorageId".Query().GlobalId(); }
    }
    protected void off_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();

        var link = "{0}Go/Board".Formatted(Application["Sso"]);
        Response.Redirect(link);
    }
}
