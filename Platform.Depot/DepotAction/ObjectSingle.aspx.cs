using Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotAction_ObjectSingle : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null).Count().EmptyWhenZero());
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            if (!"CatalogId".Query().None())
            {
                var cid = "CatalogId".Query();
                if (tree.GetAllNodes().Count(o => o.Value == cid) == 1)
                {
                    var node = tree.GetAllNodes().Single(o => o.Value == cid);
                    node.Selected = true;
                    node.ExpandParentNodes();
                    node.ExpandChildNodes();
                    tree0.Nodes[0].Selected = false;
                }
            }
            if (!"Search".Query().None())
            {
                toSearch.Text = Server.UrlDecode("Search".Query());
                view.Rebind();
            }
        }
    }

    protected string CountTotal(DepotObject obj)
    {
        var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
        var noOut = query.Count() > 0 ? query.Where(o => o.Type == UseType.借用).Sum(o => o.Amount - o.ReturnedAmount) : 0;
        return (obj.Amount + noOut).ToAmount(Depot.Featured(DepotType.小数数量库));
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
        var timex = periodx.SelectedDate.HasValue ? periodx.SelectedDate.Value : DateTime.MinValue.AddMilliseconds(1);
        var time = period.SelectedDate.HasValue ? period.SelectedDate.Value : DateTime.Today;
        if (timex > time)
        {
            var time_t = timex;
            timex = time;
            time = time_t;
        }
        var start = timex.AddMilliseconds(-1);
        var end = time.AddDays(1);
        var node = CurrentNode;
        var source = DataContext.DepotObjectSingleLoad(Depot.Id, node.HasValue ? node.Value.GlobalId() : (Guid?)null).ToList();
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.物资.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.拼音.ToLower().Contains(toSearch.Text.Trim().ToLower())).ToList();
        }
        source = source.Where(o => o.购置日期 > start && o.购置日期 < end).ToList();
        if (!no.Text.None())
        {
            source = source.Where(o => o.卡片编号.ToLower().Contains(no.Text.ToLower())).ToList();
        }
        if (!qr.Text.None())
        {
            source = source.Where(o => o.条码.ToLower().Contains(qr.Text.ToLower())).ToList();
        }
        if (!place.Text.None())
        {
            source = source.Where(o => o.存放地.ToLower().Contains(place.Text.ToLower())).ToList();
        }
        view.DataSource = source.ToList();
        pager.Visible = source.Count() > pager.PageSize;
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void only_CheckedChanged(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void view_ItemDataBound(object sender, Telerik.Web.UI.RadListViewItemEventArgs e)
    {
        var c = e.Item.FindControl("xp");
        if (c == null)
            return;
        var t = e.Item.FindControl("xt") as RadToolTip;
        t.TargetControlID = c.ClientID;
        var v = e.Item.FindControl("xv") as GridView;
        var k = (c as HtmlTableCell).Attributes["match"].ToString();
        var p = (c as HtmlTableCell).Attributes["matchp"].ToString();
        v.EmptyDataText = "初始存放于：{0}".Formatted(p);
        var source = DataContext.DepotPlace.Where(o => o.Code == k).OrderByDescending(o => o.AutoId).ToList();
        v.DataSource = source;
        v.DataBind();
    }
}
