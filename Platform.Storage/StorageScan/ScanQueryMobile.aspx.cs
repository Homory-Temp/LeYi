using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using Telerik.Web.UI;

public partial class ScanQueryMobile : StoragePageMobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitQueryIn();
        }
        code.Focus();
    }

    protected void InitQueryIn()
    {
        if (!"Id".Query().Null())
        {
            var obj = db.Value.StorageObjectGetOne("Id".Query().GlobalId());
            code.Text = db.Value.ToQR("W", obj.AutoId);
        }
        //day_start.SelectedDate = DateTime.Today.AddMonths(-1);
        //day_end.SelectedDate = DateTime.Today;
    }
    
    protected void query_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        view.Rebind();
    }

    public class Expo
    {
        public decimal 库存 { get; set; }
        public string 单位 { get; set; }
        public Guid 标识 { get; set; }
        public DateTime 时间 { get; set; }
        public string 日期 { get; set; }
        public string 类型 { get; set; }
        public decimal 数量 { get; set; }
        public string 人员 { get; set; }
    }

    protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        var c = code.Text;
        //var ds = int.Parse(day_start.SelectedDate.Value.AddDays(-1).ToString("yyyyMMdd"));
        //var de = int.Parse(day_end.SelectedDate.Value.AddDays(1).ToString("yyyyMMdd"));
        code.Text = string.Empty;
        if (c.Length != 12)
            return;
        string type;
        Guid id;
        var piles = new List<Expo>();
        db.Value.FromQR(c, out type, out id);
        switch (type)
        {
            case "W":
                {
                    var obj = db.Value.StorageObjectGetOne(id);
                    name.Text = obj.Name;
                    number.Text = obj.InAmount.ToString();
                    var source = obj.StorageIn.ToList();
                    piles.AddRange(source.Select(o => new Expo { 库存 = 0, 单位 = obj.Unit, 标识 = o.Id, 时间 = o.Time, 日期 = o.TimeNode.TimeNode(), 类型 = "入库", 数量 = o.Amount, 人员 = db.Value.UserName(o.ResponsibleUserId) }));
                    //piles.AddRange(source.Where(o => o.TimeNode > ds && o.TimeNode < de).Select(o => new { 标识 = o.Id, 时间 = o.Time, 日期 = o.TimeNode.TimeNode(), 类型 = "入库", 数量 = o.Amount, 合计 = o.TotalMoney.Money(), 人员 = db.Value.UserName(o.ResponsibleUserId) }));
                    foreach (var pile in source)
                    {
                        foreach (var sp in db.Value.StoragePlace.Where(o => o.IsSingle == false && o.InId == pile.Id).ToList())
                            piles.Add(new Expo { 库存 = 0, 标识 = sp.Id, 单位 = obj.Unit, 时间 = sp.Time, 日期 = sp.TimeNode.TimeNode(), 类型 = "变更存放地：{0}".Formatted(sp.Place), 数量 = 0, 人员 = db.Value.UserName(sp.UserId) });
                        foreach (var ox in pile.StorageLendSingle)
                        {
                            var oxp = ox.StorageLend;
                            foreach (var xyz in oxp.StorageReturnSingle)
                            {
                                var abc = xyz.StorageReturn;
                                //if (piles.Count(o => o.标识 == abc.Id) == 0 && abc.TimeNode > ds && abc.TimeNode < de)
                                if (piles.Count(o => o.标识 == abc.Id) == 0)
                                {
                                    piles.Add(new Expo { 库存 = 0, 标识 = abc.Id, 单位 = obj.Unit, 时间 = abc.Time, 日期 = abc.TimeNode.TimeNode(), 类型 = "归还", 数量 = abc.Amount, 人员 = db.Value.UserName(abc.ReturnUserId) });
                                }
                            }
                            if (piles.Count(o => o.标识 == oxp.Id) == 0)
                            //if (piles.Count(o => o.标识 == oxp.Id) == 0 && oxp.TimeNode > ds && oxp.TimeNode < de)
                            {
                                piles.Add(new Expo { 库存 = 0, 标识 = oxp.Id, 单位 = obj.Unit, 时间 = oxp.Time, 日期 = oxp.TimeNode.TimeNode(), 类型 = "借用", 数量 = -oxp.Amount, 人员 = db.Value.UserName(oxp.BorrowUserId) });
                            }
                        }
                    }
                    foreach (var pile in source)
                    {
                        foreach (var ox in pile.StorageConsumeSingle)
                        {
                            var oxp = ox.StorageConsume;
                            //if (piles.Count(o => o.标识 == oxp.Id) == 0 && oxp.TimeNode > ds && oxp.TimeNode < de)
                            if (piles.Count(o => o.标识 == oxp.Id) == 0)
                            {
                                piles.Add(new Expo { 库存 = 0, 标识 = oxp.Id, 单位 = obj.Unit, 时间 = oxp.Time, 日期 = oxp.TimeNode.TimeNode(), 类型 = "领用", 数量 = -oxp.Amount, 人员 = db.Value.UserName(oxp.ConsumeUserId) });
                            }
                        }
                    }
                    foreach (var pile in source)
                    {
                        foreach (var ox in pile.StorageOutSingle)
                        {
                            var oxp = ox.StorageOut;
                            if (piles.Count(o => o.标识 == oxp.Id) == 0)
                            //if (piles.Count(o => o.标识 == oxp.Id) == 0 && oxp.TimeNode > ds && oxp.TimeNode < de)
                            {
                                piles.Add(new Expo { 库存 = 0, 标识 = oxp.Id, 单位 = obj.Unit, 时间 = oxp.Time, 日期 = oxp.TimeNode.TimeNode(), 类型 = "报废", 数量 = -oxp.Amount, 人员 = db.Value.UserName(oxp.OutUserId) });
                            }
                        }
                    }
                    piles = piles.OrderBy(o => o.时间).ToList();
                    for (var i = 0; i < piles.Count; i++)
                    {
                        if (i == 0)
                            piles[i].库存 = piles[i].数量;
                        else
                            piles[i].库存 = piles[i - 1].库存 + piles[i].数量;
                    }
                    piles = piles.OrderByDescending(o => o.时间).ToList();
                    view.Source(piles);
                    break;
                }
            case "D":
                {
                    var obj = db.Value.StorageInSingle.Single(o => o.Id == id);
                    var so = obj.StorageObject;
                    var unit = so.Unit;
                    name.Text = so.Name;
                    number.Text = obj.In ? "1" : "0";
                    //if (obj.StorageIn.TimeNode > ds && obj.StorageIn.TimeNode < de)
                    piles.Add(new Expo { 库存 = 0, 标识 = obj.StorageIn.Id, 单位 = unit, 时间 = obj.StorageIn.Time, 日期 = obj.StorageIn.TimeNode.TimeNode(), 类型 = "入库", 数量 = 1, 人员 = db.Value.UserName(obj.StorageIn.ResponsibleUserId) });
                    foreach (var ox in obj.StorageIn.StorageLendSingle.Where(k => k.Ordinal == obj.InOrdinal))
                    {
                        var oxp = ox.StorageLend;
                        foreach (var xyz in oxp.StorageReturnSingle.Where(o=>o.Ordinal == ox.Ordinal))
                        {
                            //if (xyz.StorageReturn.TimeNode > ds && xyz.StorageReturn.TimeNode < de)
                            {
                                piles.Add(new Expo { 库存 = 0, 标识 = xyz.Id, 时间 = xyz.StorageReturn.Time, 单位 = unit, 日期 = xyz.StorageReturn.TimeNode.TimeNode(), 类型 = "归还", 数量 = 1, 人员 = db.Value.UserName(xyz.StorageReturn.ReturnUserId) });
                            }
                        }
                        //if (ox.StorageLend.TimeNode> ds&& ox.StorageLend.TimeNode<de)
                        {
                            piles.Add(new Expo { 库存 = 0, 标识 = ox.Id, 时间 = ox.StorageLend.Time, 单位 = unit, 日期 = ox.StorageLend.TimeNode.TimeNode(), 类型 = "借用", 数量 = -1, 人员 = db.Value.UserName(ox.StorageLend.BorrowUserId) });
                        }
                    }
                    foreach (var ox in obj.StorageIn.StorageOutSingle.Where(j => j.Ordinal == obj.InOrdinal))
                    {
                        var oxp = ox.StorageOut;
                        //if (ox.StorageOut.TimeNode > ds && ox.StorageOut.TimeNode < de)
                        {
                            piles.Add(new Expo { 库存 = 0, 标识 = ox.Id, 时间 = ox.StorageOut.Time, 单位 = unit, 日期 = ox.StorageOut.TimeNode.TimeNode(), 类型 = "报废", 数量 = -1, 人员 = db.Value.UserName(ox.StorageOut.OutUserId) });
                        }
                    }
                    foreach (var sp in db.Value.StoragePlace.Where(o => o.IsSingle == true && o.InId == obj.Id).ToList())
                        piles.Add(new Expo { 库存 = 0, 标识 = sp.Id, 时间 = sp.Time, 单位 = unit, 日期 = sp.TimeNode.TimeNode(), 类型 = "变更存放地：{0}".Formatted(sp.Place), 数量 = 0, 人员 = db.Value.UserName(sp.UserId) });
                    piles = piles.OrderBy(o => o.时间).ToList();
                    for (var i = 0; i < piles.Count; i++)
                    {
                        if (i == 0)
                            piles[i].库存 = piles[i].数量;
                        else
                            piles[i].库存 = piles[i - 1].库存 + piles[i].数量;
                    }
                    piles = piles.OrderByDescending(o => o.时间).ToList();
                    view.Source(piles);
                    break;
                }
            default:
                {
                    view.Source(null);
                    break;
                }
        }
    }
}
