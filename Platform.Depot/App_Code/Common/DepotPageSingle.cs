using Models;
using System;
using System.Linq;
using Telerik.Web.UI;

public class DepotPageSingle : DepotPage, IDepotSingle
{
    private Depot _depot;

    public Depot Depot
    {
        get
        {
            if (_depot == null)
            {
                var depotId = Guid.Parse(Request.QueryString["DepotId"]);
                _depot = DataContext.Depot.Single(o => o.Id == depotId);
            }
            return _depot;
        }
    }

    private string _rights;

    public string DepotRights
    {
        get
        {
            if (_rights == null)
            {
                if (DataContext.DepotMember.Count(o => o.Id == DepotUser.Id && o.DepotId == Depot.Id) == 0)
                    _rights = string.Empty;
                else
                    _rights = DataContext.DepotMember.Where(o => o.Id == DepotUser.Id && o.DepotId == Depot.Id).Select(o => o.Rights).ToList().Aggregate((a, b) => a += b);
            }
            return _rights;
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["DepotId"]))
        {
            Response.Redirect(string.Format("~/Depot/Home?DepotUrl={0}", Server.UrlEncode(Request.Url.PathAndQuery)));
            return;
        }
        base.OnLoad(e);
    }
}
