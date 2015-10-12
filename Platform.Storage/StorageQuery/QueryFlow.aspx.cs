using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

public partial class QueryFlow : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitQueryIn();
        }
    }

    protected void InitQueryIn()
    {
        tree.DataSource = db.Value.StorageCatalog.Where(o => o.State < State.删除 && o.StorageId == StorageId).OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
        tree.DataBind();
        tree.GetAllNodes().Where(o => o.Level < 2).ToList().ForEach(o => o.Expanded = true);
        tree.GetAllNodes().ToList().ForEach(o => o.Checked = true);
        day_start.SelectedDate = DateTime.Today.AddMonths(-1);
        day_end.SelectedDate = DateTime.Today;
        view.Rebind();
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        e.Node.Selected = false;
        e.Node.Checked = !e.Node.Checked;
        view.Rebind();
    }

    public class Flow
    {
        public Guid Id { get; set; }
        public string 物品名称 { get; set; }
        public string 拼音 { get; set; }
        public decimal 期初库存 { get; set; }
        public decimal 期初总量 { get; set; }
        public decimal 期初余额 { get; set; }
        public decimal 入库数量 { get; set; }
        public decimal 入库金额 { get; set; }
        public decimal 借用数量 { get; set; }
        public decimal 借用金额 { get; set; }
        public decimal 耗废数量 { get; set; }
        public decimal 耗废金额 { get; set; }
        public decimal 期末库存 { get; set; }
        public decimal 期末总量 { get; set; }
        public decimal 期末余额 { get; set; }
        public bool 易耗品 { get; set; }
        public string 分类 { get; set; }
        public Guid 分类标识 { get; set; }
        public Guid 物品标识 { get; set; }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var n = name.Text.ToLower();
        var ds = int.Parse(day_start.SelectedDate.Value.AddDays(-1).ToString("yyyyMMdd"));
        var tt = combo.SelectedItem.Value;
        var de = int.Parse(day_end.SelectedDate.Value.AddDays(1).ToString("yyyyMMdd"));
        var x = db.Value.查询_数据流.Where(o => o.TimeNode > ds && o.TimeNode < de && o.StorageId == StorageId).ToList().Join(db.Value.StorageObjectGet(StorageId), h => h.ObjectId, l => l.Id, (c, d) => new { K = c, V = d }).ToList();
        var source = x;
        var p = new List<Flow>();
        foreach (var g in x.GroupBy(o => o.K.ObjectId))
        {
            var list = g.OrderBy(o => o.K.Time);
            var f = g.First().K;
            var l = g.Last().K;
            var flow = new Flow();
            flow.Id = g.Key;
            flow.物品名称 = f.Name;
            flow.拼音 = f.PinYin;
            flow.期初库存 = f.InitialInAmount;
            flow.期初总量 = f.InitialInAmount + f.InitialLendAmount;
            flow.期初余额 = f.InitialInMoney + f.InitialLendMoney;
            flow.入库数量 = l.FinalTotalAmount - f.InitialTotalAmount;
            flow.入库金额 = l.FinalTotalMoney - f.InitialTotalMoney;
            flow.借用数量 = g.Sum(o => o.K.FinalLendAmount - o.K.InitialLendAmount);
            flow.借用金额 = g.Sum(o => o.K.FinalLendMoney - o.K.InitialLendMoney);
            flow.耗废数量 = g.Sum(o => o.K.FinalOutAmount - o.K.InitialOutAmount);
            flow.耗废金额 = g.Sum(o => o.K.FinalOutMoney - o.K.InitialOutMoney);
            flow.期末库存 = l.FinalInAmount;
            flow.期末总量 = l.FinalInAmount + l.FinalLendAmount;
            flow.期末余额 = l.FinalInMoney + l.FinalLendMoney;
            flow.易耗品 = g.First().V.Consumable;
            flow.分类标识 = g.First().V.CatalogId;
            flow.分类 = g.First().V.GeneratePath();
            flow.物品标识 = g.First().V.Id;
            p.Add(flow);
        }
        p = tree.CheckedNodes.Select(o => o.Value.GlobalId()).ToList().Join(p, o => o, o => o.分类标识, (r, y) => y).ToList();
        switch (tt)
        {
            case "1":
                p = p.Where(o => o.易耗品 == true).ToList();
                break;
            case "2":
                p = p.Where(o => o.易耗品 == false).ToList();
                break;
        }
        view.DataSource = p;
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
        storage_panel.RaisePostBackEvent("");
    }

    protected void query_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        view.Rebind();
    }

    protected void view_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            var item = e.Item as GridDataItem;
            var id = item.FindControl("label").ClientID;
            var c = item.FindControl("win") as RadWindow;
            c.OpenerElementID = id;
            c.NavigateUrl = "../StorageObject/ObjectWindow?Id={0}".Formatted(item.GetDataKeyValue("物品标识"));
        }
    }
}
