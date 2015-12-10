﻿using Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_Statistics : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            tree.CheckAllNodes();
            ps.SelectedDate = DateTime.Today;
            pe.SelectedDate = DateTime.Today;
            view.Rebind();
        }
    }

    protected void all_ServerClick(object sender, EventArgs e)
    {
        if (_all.Value == "1")
        {
            tree.UncheckAllNodes();
            _all.Value = "0";
            all.Value = "全部选定";
        }
        else
        {
            tree.CheckAllNodes();
            _all.Value = "1";
            all.Value = "清除选定";
        }
        view.Rebind();
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected void query_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var sv = ps.SelectedDate.HasValue ? ps.SelectedDate.Value : DateTime.Today;
        var ev = pe.SelectedDate.HasValue ? pe.SelectedDate.Value : DateTime.Today;
        var _s = new DateTime(sv.Year, sv.Month, 1).ToTimeNode();
        var _e = new DateTime(ev.Year, ev.Month, 1).ToTimeNode();
        if (_s > _e)
        {
            var _t = _s;
            _s = _e;
            _e = _t;
        }
        var catalogs = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList();
        var source = catalogs.Join(db.Value.Store_ST.Where(o => o.Time >= _s && o.Time <= _e), o => o, o => o.CatalogId, (a, b) => b).ToList();
        var list = new List<InMemoryST>();
        var ___objs = catalogs.Join(db.Value.StoreObject.Where(o => o.State < 2), o => o, o => o.CatalogId, (a, b) => b.Id).ToList();
        foreach (var ___id in ___objs)
        {
            if (source.Count(o => o.ObjectId == ___id && o.Time == _s) == 0)
            {
                db.Value.ActionRecord(___id, DateTime.Parse(_s.FromTimeNode()), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                db.Value.SaveChanges();
            }
        }
        foreach (var g in source.GroupBy(o => o.ObjectId))
        {
            var obj = new InMemoryST();
            var v = g.OrderBy(o => o.Time).ToList();
            obj.CatalogPath = v.First().CatalogPath;
            obj.Name = v.First().Name;
            obj.Single = v.First().Single;
            obj.Consumable = v.First().Consumable;
            obj.Fixed = v.First().Fixed;
            obj.S = g.First().StartAmount;
            obj.SM = g.First().StartMoney;
            obj.SP = obj.S == 0 ? 0M : decimal.Divide(obj.SM, obj.S);
            obj.I = g.Sum(o => o.InAmount);
            obj.IM = g.Sum(o => o.InMoney);
            obj.IP = obj.I == 0 ? 0M : decimal.Divide(obj.IM, obj.I);
            obj.U = g.Sum(o => o.LendAmount + o.ConsumeAmount);
            obj.UM = g.Sum(o => o.LendMoney + o.ConsumeMoney);
            obj.UP = obj.U == 0 ? 0M : decimal.Divide(obj.UM, obj.U);
            obj.R = g.Sum(o => o.RedoAmount);
            obj.RM = g.Sum(o => o.RedoMoney);
            obj.RP = obj.R == 0 ? 0M : decimal.Divide(obj.RM, obj.R);
            obj.O = g.Sum(o => o.OutAmount);
            obj.OM = g.Sum(o => o.OutMoney);
            obj.OP = obj.O == 0 ? 0M : decimal.Divide(obj.OM, obj.O);
            obj.E = g.Last().EndAmount;
            obj.EM = g.Last().EndMoney;
            obj.EP = obj.E == 0 ? 0M : decimal.Divide(obj.EM, obj.E);
            if (obj.S > 0 || obj.SM > 0 || obj.I > 0 || obj.IM > 0 || obj.U > 0 || obj.UM > 0 || obj.R > 0 || obj.RM > 0 || obj.O > 0 || obj.OM > 0 || obj.E > 0 || obj.EM > 0)
                list.Add(obj);
        }
        if (!name.Text.Trim().Null())
        {
            list = list.Where(o => o.Name == name.Text.Trim()).ToList();
        }
        ___total.Value = list.Sum(o => o.S).ToMoney() + "@" + list.Sum(o => o.SM).ToMoney() + "@" + list.Sum(o => o.I).ToMoney() + "@" + list.Sum(o => o.IM).ToMoney() + "@" + list.Sum(o => o.U).ToMoney() + "@" + list.Sum(o => o.UM).ToMoney() + "@" + list.Sum(o => o.R).ToMoney() + "@" + list.Sum(o => o.RM).ToMoney() + "@" + list.Sum(o => o.E).ToMoney() + "@" + list.Sum(o => o.EM).ToMoney();
        view.DataSource = list.OrderBy(o => o.CatalogPath).ThenBy(o => o.Name).ToList();
    }
}
