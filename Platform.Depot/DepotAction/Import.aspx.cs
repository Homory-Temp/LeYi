using Aspose.Cells;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Import : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void im_up_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        var id = Guid.NewGuid().ToString().ToUpper();
        var name = Server.MapPath(string.Format("~/Common/物资/临时/{0}.xls", id));
        file.Value = name;
        e.File.SaveAs(name, true);
        grid.Rebind();
    }

    protected void im_do_Click(object sender, EventArgs e)
    {
    }

    public class ToImport
    {
        public DepotObjectCatalog DOC { get; set; }
        public DepotObject DO { get; set; }
    }

    protected void im_ok_Click(object sender, EventArgs e)
    {
        try
        {
            var book = new Workbook(file.Value);
            var data = book.Worksheets[0].Cells.ExportDataTableAsString(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null && o[0].Value.ToString().Trim() != "").Count(), 15, true);
            var handled = new DataTable();
            foreach (var index in new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 })
            {
                handled.Columns.Add(data.Columns[index].ColumnName);
            }
            foreach (DataRow row in data.Rows)
            {
                var _row = handled.NewRow();
                foreach (DataColumn col in handled.Columns)
                {
                    _row[col.ColumnName] = row[col.ColumnName];
                }
                handled.Rows.Add(_row);
            }
            if (handled.Rows.Count > 0)
            {
                var 固定资产库Id = Depot.Id;
                var 固定资产库购置单Id = DataContext.GlobalId();

                DataContext.DepotOrderAdd(固定资产库购置单Id, 固定资产库Id, "固定资产导入_{0}".Formatted(DateTime.Now.ToString("yyyyMMddHHmmss")), "导入", "导入", "导入", "导入", 0M, 0M, null, null, DateTime.Now, DepotUser.Id);

                foreach (DataRow row in handled.Rows)
                {
                    try
                    {
                        var 一级分类 = row[4].ToString().Trim();
                        if (string.IsNullOrEmpty(一级分类))
                            continue;

                        Guid 一级分类Id;
                        if (DataContext.DepotCatalog.Count(o => o.DepotId == 固定资产库Id && o.Name == 一级分类 && o.State < State.停用 && o.ParentId == null) == 0)
                        {
                            一级分类Id = DataContext.DepotCatalogAdd(固定资产库Id, null, Guid.Empty, 一级分类, 100, "");
                        }
                        else
                        {
                            一级分类Id = DataContext.DepotCatalog.Single(o => o.DepotId == 固定资产库Id && o.Name == 一级分类 && o.State < State.停用 && o.ParentId == null).Id;
                        }

                        var 二级分类 = row[5].ToString().Trim();
                        Guid 二级分类Id;
                        if (DataContext.DepotCatalog.Count(o => o.DepotId == 固定资产库Id && o.Name == 二级分类 && o.State < State.停用 && o.ParentId == 一级分类Id) == 0)
                        {
                            二级分类Id = DataContext.DepotCatalogAdd(固定资产库Id, 一级分类Id, 一级分类Id, 二级分类, 100, "");
                        }
                        else
                        {
                            二级分类Id = DataContext.DepotCatalog.Single(o => o.DepotId == 固定资产库Id && o.Name == 二级分类 && o.State < State.停用 && o.ParentId == 一级分类Id).Id;
                        }

                        var code = row[0].ToString().Trim().Substring(row[0].ToString().Trim().Length - 7);
                        var name = row[2].ToString().Trim();

                        var catalogs = DataContext.DepotCatalogLoad(固定资产库Id).Select(o => o.Id).Join(DataContext.DepotObjectCatalog, o => o, o => o.CatalogId, (x, y) => y).Join(DataContext.DepotObject, o => o.ObjectId, o => o.Id, (x, y) => new ToImport { DOC = x, DO = y }).ToList();

                        Guid 物资Id;

                        if (catalogs.Count(o => o.DO.Name == name && o.DOC.CatalogId == 二级分类Id && o.DO.State < State.停用) == 0)
                        {
                            物资Id = DataContext.GlobalId();
                            var l = new List<Guid>();
                            l.Add(一级分类Id);
                            l.Add(二级分类Id);
                            DataContext.DepotObjectAddX(物资Id, l, Depot.Id, name, row[14].ToString().Contains("是"), false, true, row[0].ToString(), row[1].ToString(), row[12].ToString(), "", "", row[11].ToString().Trim(), 0, 0, "", "", "", "", "", 100);
                        }
                        else
                        {
                            物资Id = catalogs.First(o => o.DO.Name == name && o.DOC.CatalogId == 二级分类Id && o.DO.State < State.停用).DO.Id;
                        }
                        if (DataContext.DepotIn.Count(o => o.ObjectId == 物资Id && o.Note == code) == 0)
                        {
                            var responsible = row[3].ToString().Trim();
                            var user = DataContext.DepotUserLoad(Depot.CampusId).FirstOrDefault(o => o.Name == responsible && o.State < State.停用);
                            var responsibleId = user == null ? (Guid?)null : user.Id;
                            decimal amount = 0.00M;
                            try
                            {
                                amount = decimal.Parse(row[6].ToString().Trim());
                            }
                            catch
                            {
                            }
                            decimal price = 0.00M;
                            try
                            {
                                price = decimal.Parse(row[7].ToString().Trim().Replace(" ", "").Replace(",", "").Replace(",", ""));
                            }
                            catch
                            {
                            }
                            var @in = new InMemoryIn { Age = "", Place = row[13].ToString().Trim(), Amount = amount, CatalogId = 二级分类Id, Money = price, Note = code, ObjectId = 物资Id, PriceSet = decimal.Divide(price, amount), Time = DateTime.Today, ResponsibleId = responsibleId };
                            var list = new List<InMemoryIn>();
                            list.Add(@in);
                            DataContext.DepotActIn(固定资产库Id, 固定资产库购置单Id, DateTime.Today, DepotUser.Id, list);
                        }
                    }
                    catch
                    { }
                }
            }
        }
        finally
        {
            grid.DataSource = null;
            grid.DataBind();
            Response.Redirect("../DepotAction/Object?DepotId={0}".Formatted(Depot.Id), false);
        }
    }

    protected void grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (file.Value.None())
        {
            grid.DataSource = null;
            grid.Visible = false;
            return;
        }
        grid.Visible = true;
        var book = new Workbook(file.Value);
        var data = book.Worksheets[0].Cells.ExportDataTableAsString(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null && o[0].Value.ToString().Trim() != "").Count(), 15, true);
        var handled = new DataTable();
        foreach (var index in new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 })
        {
            handled.Columns.Add(data.Columns[index].ColumnName);
        }
        foreach (DataRow row in data.Rows)
        {
            var _row = handled.NewRow();
            foreach (DataColumn col in handled.Columns)
            {
                _row[col.ColumnName] = row[col.ColumnName];
            }
            handled.Rows.Add(_row);
        }
        grid.DataSource = handled;
    }
}
