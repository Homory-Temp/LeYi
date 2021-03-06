﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_Check : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList().Where(o => o.Code != "*Homory:Null*").ToList();
            tree.DataBind();
            cName.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ____v.InnerText = (new List<InMemoryCheck>()).ToJson();
            depts.DataSource = DataContext.DepotObjectLoad(Depot.Id, null).Select(o => o.Department).Distinct().OrderBy(o => o).ToList();
            depts.DataBind();
            ol.DataSource = DataContext.DepotOrder.Where(o => o.DepotId == Depot.Id && o.State < State.停用).OrderByDescending(o => o.RecordTime).ToList();
            ol.DataBind();
        }
    }

    protected Guid? CurrentNode
    {
        get
        {
            return tree.SelectedNode == null ? (Guid?)null : tree.SelectedValue.GlobalId();
        }
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        tree.GetAllNodes().Where(o => o.ParentNode == e.Node.ParentNode).ToList().ForEach(o => o.Expanded = false);
        e.Node.Expanded = true;
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        if (toSearchX.Text.None() && depts.SelectedIndex < 0 && ol.SelectedIndex < 1)
        {
            var node = CurrentNode;
            var source = node.HasValue ? DataContext.DepotObjectLoad(Depot.Id, node.Value.GlobalId()).Where(o => o.Single) : new List<DepotObject>();
            if (!toSearch.Text.None())
            {
                source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
            }
            source = source.Where(o => o.DepotInX.Count > 0 || o.Single == false);
            view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
        }
        else
        {
            var source = DataContext.DepotObjectLoad(Depot.Id, null).Where(o => o.Single);
            if (!toSearch.Text.None())
            {
                source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
            }
            if (depts.SelectedIndex >= 0)
            {
                var dept = depts.SelectedItem.Text;
                source = source.Where(o => o.Department == dept).ToList();
            }
            source = source.Where(o => o.DepotInX.Count > 0 || o.Single == false);
            view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
        }
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void all_ServerClick(object sender, EventArgs e)
    {
        var cbs = view.Items.Select(o => o.FindControl("check") as CheckBox).ToList();
        if (cbs.All(o => o.Checked))
        {
            cbs.ForEach(o =>
            {
                o.Checked = false;
                check_CheckedChanged(o, null);
            });
        }
        else
        {
            cbs.ForEach(o =>
            {
                o.Checked = true;
                check_CheckedChanged(o, null);
            });
        }
    }

    public static void DepotObjectEdit(DepotEntities db, Guid id, List<Guid> catalogIds)
    {
        var obj = db.DepotObject.Single(o => o.Id == id);
        var catalogs = db.DepotObjectCatalog.Where(o => o.ObjectId == id && o.IsVirtual == false).ToList();
        for (var i = 0; i < catalogs.Count(); i++)
        {
            db.DepotObjectCatalog.Remove(catalogs.ElementAt(i));
        }
        for (var i = 0; i < catalogIds.Count; i++)
        {
            db.DepotObjectCatalog.Add(new DepotObjectCatalog { ObjectId = id, CatalogId = catalogIds[i], IsVirtual = false, Level = i, IsLeaf = i == catalogIds.Count - 1 });
        }
    }

    public class X
    {
        public int Ordinal { get; set; }
        public string Code { get; set; }
    }

    protected List<DepotInX> Ordinals(Guid objId, RadListViewDataItem container)
    {
        var s = DataContext.DepotInX.Where(o => o.ObjectId == objId).OrderBy(o => o.Ordinal).ToList().Where(o => o.Place.ToLower().Contains(toSearchX.Text.Trim().ToLower())).ToList();
        if (ol.SelectedIndex > 0)
        {
            var oid = Guid.Parse(ol.SelectedValue);
            s = s.Where(o => o.DepotOrder.Id == oid).ToList();
        }
        if (s.Count == 0)
        {
            container.Visible = false;
        }
        return s;
    }

    protected void coding_ServerClick(object sender, EventArgs e)
    {
        if (cName.Text.Trim().None())
        {
            NotifyError(ap, "请输入盘库任务的名称");
            return;
        }
        var codes = ____v.InnerText.FromJson<List<InMemoryCheck>>();
        if (codes.Count == 0)
        {
            NotifyError(ap, "请选择要盘库的物资");
            return;
        }
        var bid = DataContext.GlobalId();
        var bo = 0;
        var bt = DateTime.Now;
        for (var i = 0; i <= codes.Count / 20; i++)
        {
            bo++;
            var dc = new DepotCheck
            {
                DepotId = Depot.Id,
                BatchId = bid,
                BatchOrdinal = bo,
                Name = cName.Text.Trim(),
                CodeJson = codes.Skip(i * 20).Take(20).ToList().ToJson(),
                Time = bt,
                State = 1
            };
            DataContext.DepotCheck.Add(dc);
        }
        DataContext.SaveChanges();
        Response.Redirect("~/DepotScan/CheckList?DepotId={0}".Formatted(Depot.Id));
    }

    protected void check_CheckedChanged(object sender, EventArgs e)
    {
        var control = sender as CheckBox;
        var view = control.NamingContainer.FindControl("viewx") as RadListView;
        //var cb = control.NamingContainer.FindControl("check") as CheckBox;
        //cb.Checked = control.Checked;
        //checkx_CheckedChanged(cb, null);
        var cbs = view.Items.Select(o => o.FindControl("checkx") as CheckBox).ToList();
        cbs.ForEach(o => { o.Checked = control.Checked; checkx_CheckedChanged(o, null); });
    }

    protected void coded_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotScan/CheckList?DepotId={0}".Formatted(Depot.Id));
    }

    protected void cl_ServerClick(object sender, EventArgs e)
    {
        ____v.InnerText = (new List<InMemoryCheck>()).ToJson();
    }

    protected void checkx_CheckedChanged(object sender, EventArgs e)
    {
        var ____list = ____v.InnerText.FromJson<List<InMemoryCheck>>();
        if ((sender as CheckBox).Checked)
        {
            if (____list.Count(o => o.Code == ((sender as CheckBox).Attributes["CC"])) == 0)
            {
                InMemoryCheck _x = new InMemoryCheck();
                _x.Code = (sender as CheckBox).Attributes["CC"];
                _x.In = false;
                _x.Name = ((sender as CheckBox).NamingContainer.NamingContainer.NamingContainer.FindControl("h") as HtmlInputHidden).Value;
                _x.Place = (sender as CheckBox).Attributes["PLACE"];
                _x.Ordinal = int.Parse((sender as CheckBox).Attributes["ORD"]);
                ____list.Add(_x);
            }
        }
        else
        {
            if (____list.Count(o => o.Code == ((sender as CheckBox).Attributes["CC"])) > 0)
            {
                ____list = (____list.Where(o => o.Code != ((sender as CheckBox).Attributes["CC"])).ToList());
            }
        }
        st.Value = "已选（{0}）".Formatted(____list.Count);
        ____v.InnerText = ____list.ToJson();
    }

    protected bool CD(string code)
    {
        var ____list = ____v.InnerText.FromJson<List<InMemoryCheck>>();
        return ____list.Count(o => o.Code == code) > 0;
    }
}
