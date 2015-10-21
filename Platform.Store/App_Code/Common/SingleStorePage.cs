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

    private string r = null;

    protected string CurrentRight
    {
        get
        {
            if (r == null)
            {
                var sid = CurrentStore.Id;
                if (db.Value.Store_Visitor.Count(o => o.Id == CurrentUser && StoreId == CurrentStore.Id) == 0)
                    r = "";
                else
                {
                    r = db.Value.Store_Visitor.Where(o => o.Id == CurrentUser && StoreId == CurrentStore.Id).Select(o => o.Right).ToList().Aggregate("", (a, b) => a += b, o => o);
                    r = r.ToList().Aggregate("", (a, b) => a += b, o => o);
                }
            }
            return r;
        }
    }

    protected bool RightIn
    {
        get
        {
            return CurrentRight.Contains("1") || CurrentRight.Contains("*");
        }
    }

    protected bool RightUse
    {
        get
        {
            return CurrentRight.Contains("2") || CurrentRight.Contains("*");
        }
    }

    protected bool RightReturn
    {
        get
        {
            return CurrentRight.Contains("3") || CurrentRight.Contains("*");
        }
    }

    protected bool RightAdvanced
    {
        get
        {
            return CurrentRight.Contains("4") || CurrentRight.Contains("*");
        }
    }

    protected bool RightReport
    {
        get
        {
            return CurrentRight.Contains("5") || CurrentRight.Contains("*");
        }
    }

    protected bool RightSetting
    {
        get
        {
            return CurrentRight.Contains("6") || CurrentRight.Contains("*");
        }
    }
}
