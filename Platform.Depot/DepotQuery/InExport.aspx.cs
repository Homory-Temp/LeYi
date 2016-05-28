using Aspose.Cells;
using Models;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class DepotQuery_InExport : DepotPageSingle
{
    protected string OpName(Guid id)
    {
        try
        {
            return DataContext.DepotUser.Single(o => o.Id == id).Name;
        }
        catch
        {
            return "管理员";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var orderId = "OrderId".Query().GlobalId();
            var ord = DataContext.DepotInRecord.Single(o => o.Id == orderId);
            var oo = DataContext.DepotOrder.Single(o => o.Id == orderId);
            var campusId = Depot.CampusId;
            var source = DataContext.DepotInXRecord.Where(o => o.OrderId == orderId).OrderByDescending(o => o.Time).ToList();
            var s = source.Sum(o => o.Total);
            var amount = source.Sum(o => o.Amount).ToAmount(Depot.Featured(DepotType.小数数量库));
            var money = s.ToMoney();
            var count = source.Select(o => o.ObjectId).Distinct().Count().ToString();

            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            table.Columns.Add(new DataColumn());
            var row_0 = table.NewRow();
            row_0[4] = "入库单";
            table.Rows.Add(row_0);
            var row_1 = table.NewRow();
            row_1[0] = "园区："+ DataContext.Department.Single(o => o.Id == campusId).Name;
            row_1[1] = "购置单号：" + oo.Name;
            row_1[3] = "发票编号：" + oo.Receipt;
            row_1[5] = "入库日期：" + ord.RecordTime.ToDay();
            row_1[7] = "购置来源：" + oo.OrderSource;
            row_1[9] = "使用对象：" + ord.使用对象;
            table.Rows.Add(row_1);
            var row_2 = table.NewRow();
            row_2[0] = "品名";
            row_2[1] = "类别";
            row_2[2] = "单位";
            row_2[3] = "数量";
            row_2[4] = "单价(元)";
            row_2[5] = "总价(元)";
            row_2[6] = "品牌";
            row_2[7] = "规格型号";
            row_2[8] = "存放地点";
            row_2[9] = "适用年龄段";
            row_2[10] = "入库人";
            table.Rows.Add(row_2);
            foreach (var item in source)
            {
                var row_i = table.NewRow();
                row_i[0] = item.Name;
                row_i[1] = item.CatalogName;
                row_i[2] = item.Unit;
                row_i[3] = item.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
                row_i[4] = item.PriceSet.ToMoney();
                row_i[5] = item.Total.ToMoney();
                row_i[6] = item.Brand;
                row_i[7] = item.Specification;
                row_i[8] = item.Place;
                row_i[9] = item.Age;
                row_i[10] = OpName(item.OperatorId);
                table.Rows.Add(row_i);
            }
            var row_3 = table.NewRow();
            row_3[0] = "物资合计：共 " + count + " 种 " + amount + " 件";
            row_3[4] = "金额合计：￥" + money;
            row_3[7] = "经手人：" + ord.经手人;
            row_3[9] = "保管人：" + ord.保管人;
            table.Rows.Add(row_3);

            var name = Server.MapPath(string.Format("~/Common/物资/临时/{0}.xls", ord.购置单号));
            var book = new Workbook();
            book.Worksheets[0].Cells.ImportDataTable(table, false, 0, 0);
            book.Save(name, SaveFormat.Excel97To2003);

            Response.Redirect(string.Format("~/Common/物资/临时/{0}.xls", ord.购置单号));
        }
    }
}
