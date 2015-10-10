using Aspose.Cells;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Extended_Import : HomoryCorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void im_up_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        var id = Guid.NewGuid().ToString().ToUpper();
        var name = Server.MapPath(string.Format("~/Temp/{0}.xls", id));
        file.Value = name;
        e.File.SaveAs(name, true);
        var book = new Workbook(name);
        var data = book.Worksheets[0].Cells.ExportDataTable(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null).Count(), 14, false);
        grid.DataSource = data;
        grid.DataBind();
    }

    protected void im_do_Click(object sender, EventArgs e)
    {
    }

    protected int GradeCount(ClassType ct)
    {
        switch (ct)
        {
            case ClassType.九年一贯制:
                return 9;
            case ClassType.小学:
                return 6;
            default:
                return 3;
        }
    }

    protected override string PageRight
    {
        get
        {
            return "Student";
        }
    }

    public bool StudentAdd(Entities db, Guid campusId, Guid classId, int ordinal, string name, string account, string passwordInitial, State state, string uniqueId, string idCard, bool? gender, DateTime? birthday, string nationality, string birthplace, string address, string charger, string chargerContact)
    {
        try
        {
            string key, salt;
            var password = HomoryCryptor.Encrypt(passwordInitial, out key, out salt);
            var user = new User
            {
                Id = db.GetId(),
                Account = account,
                RealName = name,
                DisplayName = name,
                Stamp = Guid.NewGuid(),
                Type = UserType.学生,
                Password = password,
                PasswordEx = null,
                CryptoKey = key,
                CryptoSalt = salt,
                Icon = "~/Common/默认/用户.png",
                State = state,
                Ordinal = ordinal,
                Description = null
            };
            var userStudent = new Homory.Model.Student
            {
                Id = user.Id,
                UniqueId = uniqueId,
                Gender = gender,
                Birthday = birthday,
                Birthplace = birthplace,
                Address = address,
                Nationality = nationality,
                IDCard = idCard,
                Charger = charger,
                ChargerContact = chargerContact
            };
            var relation = new DepartmentUser
            {
                DepartmentId = classId,
                UserId = user.Id,
                TopDepartmentId = campusId,
                Type = DepartmentUserType.班级学生,
                State = State.启用,
                Ordinal = 0,
                Time = DateTime.Now
            };
            db.User.Add(user);
            db.Student.Add(userStudent);
            db.DepartmentUser.Add(relation);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void im_ok_Click(object sender, EventArgs e)
    {
        var book = new Workbook(file.Value);
        var data = book.Worksheets[0].Cells.ExportDataTable(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null).Count(), 14, false);
        foreach (DataRow row in data.Rows)
        {
            var campusName = row[0].ToString();
            var campus = HomoryContext.Value.Department.First(o => o.Name == campusName);
            var yearFinish = int.Parse(row[1].ToString()) + GradeCount(campus.ClassType);
            var year = campus.DepartmentChildren.First(o => o.Ordinal == yearFinish);
            var classNo = int.Parse(row[2].ToString());
            var @class = year.DepartmentChildren.First(o => o.Ordinal == classNo);
            StudentAdd(HomoryContext.Value, campus.Id, @class.Id, int.Parse(row[3].ToString()), row[4].ToString(), row[5].ToString(), row[5].ToString().Substring(12), State.启用, row[6].ToString(), row[5].ToString(), row[7] == null ? null : (row[7].ToString() == "" ? (bool?)null : (row[7].ToString() == "男" ? true : false)), row[8] == null ? null : (row[8].ToString() == "" ? (DateTime?)null : DateTime.Parse(row[8].ToString())), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString());
            LogOp(OperationType.新增);
        }
        HomoryContext.Value.SaveChanges();
        Response.Redirect("../Go/Student", false);
    }
}
