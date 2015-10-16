using System;
using System.Linq;

public class SingleStorePage : StorePage
{
    protected override void OnLoad(EventArgs e)
    {
        if ("StoreId".Query().Null())
        {
            Response.Redirect("~/Store/Home?StoreUrl={0}".Formatted(Server.UrlEncode(Request.Url.PathAndQuery)));
            return;
        }
        base.OnLoad(e);
    }

    protected Guid StoreId
    {
        get { return "StoreId".Query().GlobalId(); }
    }

    protected string StoreName
    {
        get
        {
            return db.Value.Store.Single(o => o.Id == StoreId).Name;
        }
    }
}
