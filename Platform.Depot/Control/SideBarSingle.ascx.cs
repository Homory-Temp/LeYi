using System;
using System.Linq;

public partial class Control_SideBarSingle : DepotControlSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            u.InnerText = DepotUser.Name;
            storeName.Value = Depot.Name;
        }
    }

    protected void qb_ServerClick(object sender, EventArgs e)
    {
        Session.Clear();
        var link = "{0}Go/Board".Formatted(Application["Sso"]);
        Response.Redirect(link);
    }

    protected bool HasWarn()
    {
        return DataContext.DepotObject.Count(o => ((o.Amount < o.Low && o.Low > 0) || (o.Amount > o.High && o.High > 0)) && o.State < Models.State.停用) > 0;
    }

    protected bool HasMove()
    {
        if (Depot.Featured(Models.DepotType.固定资产库))
            return false;
        var source = DataContext.DepotObjectInLoadFix(Depot.Id);
        var actual = DataContext.DepotObjectInLoad(Depot.Id, null, true);
        foreach (var g in source.GroupBy(o => o.Note))
        {
            var amount = g.Sum(o => o.Amount);
            var xamount = actual.Where(o => o.Note == g.Key).Sum(o => o.Amount);
            if (amount > xamount)
            {
                return true;
            }
        }
        return false;
    }

    public string Crumb
    {
        get
        {
            return crumb.InnerText;
        }
        set
        {
            crumb.InnerText = value;
        }
    }

    public bool NoCrumb
    {
        get
        {
            return !crumb.Visible;
        }
        set
        {
            crumb.Visible = !value;
        }
    }

    protected void storeName_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/DepotHome?DepotId={0}".Formatted(Depot.Id));
    }
}
