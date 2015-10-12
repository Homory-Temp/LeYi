using System;
using System.Linq;

public partial class Storage : StoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            add.Visible = Right_Create;
            name.InnerText = db.Value.UserName(CurrentUser);
        }
    }

    protected bool HasRight(Guid id)
    {
        return Right_In(id) || Right_Out(id) || Right_Query(id) || Right_Set(id) || Right_Use(id);
    }


    protected bool Right_Query(Guid StorageId)
    {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "?")).Single();
            return query > 0;
    }

    protected bool Right_In(Guid StorageId)
    {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "+")).Single();
            return query > 0;
    }

    protected bool Right_Use(Guid StorageId)
    {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "*")).Single();
            return query > 0;
    }

    protected bool Right_Out(Guid StorageId)
    {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "-")).Single();
            return query > 0;
    }

    protected bool Right_Set(Guid StorageId)
    {
            var query = db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}' AND [Right] = '{2}'".Formatted(StorageId, CurrentUser, "!")).Single();
            return query > 0;
    }

    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        list.Source(db.Value.StorageGet().OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList());
    }
    public bool GetVisible() {

        return Right_Create;

    }
    protected string GenerateUrl(object id)
    {
        return "../StorageHome/Home?StorageId={0}".Formatted(id);
        //var query = "RequestUrl".Query(true);
        //return query.Null() ? "../StorageHome/Home?StorageId={0}".Formatted(id) : query.Contains("&") ? "{0}&StorageId={1}".Formatted(query, id) : "{0}?StorageId={1}".Formatted(query, id);
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_add();");
    }

    protected void edit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_edit('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void remove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_remove('{0}');".Formatted(sender.ButtonArgs()));
    }
    protected void off_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();

        var link = "{0}Go/Board".Formatted(Application["Sso"]);
        Response.Redirect(link);
    }
}
