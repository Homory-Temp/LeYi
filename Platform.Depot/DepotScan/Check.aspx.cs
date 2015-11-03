using Models;
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
            tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null).Count().EmptyWhenZero());
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
        }
    }

    protected Guid? CurrentNode
    {
        get
        {
            return tree.SelectedNode == null ? (Guid?)null : tree.SelectedValue.GlobalId();
        }
    }

    protected void tree0_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree.SelectedNode != null)
            tree.SelectedNode.Selected = false;
        view.Rebind();
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree0.SelectedNode != null)
            tree0.SelectedNode.Selected = false;
        tree.GetAllNodes().Where(o => o.ParentNode == e.Node.ParentNode).ToList().ForEach(o => o.Expanded = false);
        e.Node.Expanded = true;
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var node = CurrentNode;
        var source = DataContext.DepotObjectLoad(Depot.Id, node.HasValue ? node.Value.GlobalId() : (Guid?)null);
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
        }
        source = source.Where(o => o.DepotInX.Count(ox => ox.Place.ToLower().Contains(toSearchx.Text.Trim().ToLower())) > 0 && o.Single == true);
        view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
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

    protected List<DepotInX> Ordinals(Guid objId)
    {
        return DataContext.DepotInX.Where(o => o.ObjectId == objId).OrderBy(o => o.Ordinal).ToList();
    }

    protected void coding_ServerClick(object sender, EventArgs e)
    {
        var codes = new List<InMemoryCheck>();
        view.Items.ForEach(o =>
        {
            var inner = o.FindControl("viewx") as RadListView;
            var h = o.FindControl("h") as HtmlInputHidden;
            if (inner.Items.Count > 0)
            {
                inner.Items.ForEach(i =>
                {
                    var cbi = i.FindControl("checkx") as CheckBox;
                    var lbl = i.FindControl("lbl") as Label;
                    if (cbi.Checked)
                    {
                        codes.Add(new InMemoryCheck { Code = cbi.Attributes["CC"], In = false, Name = h.Value, Ordinal = int.Parse(cbi.Attributes["ORD"]), Place = lbl.Text });
                    }
                });
            }
        });
        var bid = DataContext.GlobalId();
        var bo = 0;
        var bt = DateTime.Now;
        for (var i = 0; i <= codes.Count / 40; i++)
        {
            bo++;
            var dc = new DepotCheck
            {
                DepotId = Depot.Id,
                BatchId = bid,
                BatchOrdinal = bo,
                CodeJson = codes.Skip(i * 100).Take(100).ToList().ToJson(),
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
        var cb = control.NamingContainer.FindControl("checkx") as CheckBox;
        cb.Checked = control.Checked;
        var cbs = view.Items.Select(o => o.FindControl("checkx") as CheckBox).ToList();
        cbs.ForEach(o => o.Checked = control.Checked);
    }

    protected void coded_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotScan/CheckList?DepotId={0}".Formatted(Depot.Id));
    }
}
