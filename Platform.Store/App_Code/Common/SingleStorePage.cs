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

    private Models.Store s;

    protected Models.Store CurrentStore
    {
        get
        {
            if (s == null)
            {
                var id = "StoreId".Query().GlobalId();
                s = db.Value.Store.SingleOrDefault(o => o.Id == id);
                if (s == null)
                    s = new Models.Store { Id = Guid.Empty, Name = string.Empty, State = Models.StoreState.删除 };
            }
            return s;
        }
    }
}
