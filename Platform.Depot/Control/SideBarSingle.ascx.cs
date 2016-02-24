using Models;
using System;
using System.Collections.Generic;
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

    private IEnumerable<DepotIn> ININ;

    protected IEnumerable<DepotIn> XININ
    {
        get
        {
            if (ININ == null)
                ININ = DataContext.DepotObjectInLoad(Depot.Id, null, true);
            return ININ;
        }
    }

    protected decimal CountDone(DepotObject obj)
    {
        var query = obj.DepotIn.ToList();
        var amount = 0M;
        foreach (var g in query.GroupBy(o => o.Note))
        {
            amount += XININ.Where(o => o.Note == g.Key).Sum(o => o.Amount);
        }
        return (amount);
    }

    protected decimal CountTotal(DepotObject obj)
    {
        var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
        var noOut = query.Count() > 0 ? query.Where(o => o.Type == UseType.借用).Sum(o => o.Amount - o.ReturnedAmount) : 0;
        return (obj.Amount + noOut);
    }

    protected bool HasWarn()
    {
        return DataContext.DepotObjectLoad(Depot.Id, null).Count(o => ((o.Amount < o.Low && o.Low > 0) || (o.Amount > o.High && o.High > 0)) && o.State < Models.State.停用) > 0;
    }

    protected bool HasMove()
    {
        if (Depot.Featured(DepotType.固定资产库))
            return false;
        //var source = DataContext.DepotObjectLoadFix(Depot.Id).ToList();
        //foreach (var s in source)
        //{
        //    if (CountTotal(s) > CountDone(s))
        //        return true;
        //}
        return true;
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
