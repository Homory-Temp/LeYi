using Aspose.Cells;
using STSdb4.Database;
using System;
using System.Data;
using System.Linq;

public partial class Import : SsoPage
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
        var name = Server.MapPath(string.Format("~/Temp/{0}.xls", id));
        file.Value = name;
        e.File.SaveAs(name, true);
        var book = new Workbook(name);
        var data = book.Worksheets[0].Cells.ExportDataTable(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null).Count(), 7, false);
        grid.DataSource = data;
        grid.DataBind();
    }

    protected void im_do_Click(object sender, EventArgs e)
    {
    }

    protected void im_ok_Click(object sender, EventArgs e)
    {
        var book = new Workbook(file.Value);
        var data = book.Worksheets[0].Cells.ExportDataTable(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null).Count(), 7, false);
        var campusId = Guid.Parse(Request.QueryString["CampusId"]);
        using (IStorageEngine engine = STSdb.FromFile(Server.MapPath(string.Format("App_Data/Housing_{0}.record", campusId))))
        {
            var table = engine.OpenXTablePortable<HousingKey, HousingValue>("Record");
            foreach (DataRow row in data.Rows)
            {
                try
                {
                    var key = new HousingKey { 学校 = campusId, 姓名 = row[0].V(), 住址 = row[4].V(), 入学年份 = row[2].V(), 户籍 = row[3].V(), 身份证号 = row[1].V() };
                    var value = new HousingValue { 班号 = row[5].V(), 备注 = row[6].V(), 时间 = DateTime.Now };
                    table[key] = value;
                    engine.Commit();
                }
                catch
                {
                }
            }
        }
        Response.Redirect(string.Format("Grid.aspx?OnlineId={0}", Request.QueryString["OnlineId"]), false);
    }
}
