using Aspose.Cells;
using Homory.Model;
using System;
using System.Data;
using System.Linq;

public partial class Extended_Import : HomoryCorePage
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
        var data = book.Worksheets[0].Cells.ExportDataTable(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null).Count(), 6, false);
        grid.DataSource = data;
        grid.DataBind();
    }

    protected void im_do_Click(object sender, EventArgs e)
    {
    }

    protected override string PageRight
    {
        get
        {
            return "Teacher";
        }
    }

    public bool TeacherAdd(Entities db, Guid campusId, Guid departmentId, int ordinal, string name, string phone, string passwordInitial, State state, string email, string idCard, bool? gender, DateTime? birthday, string nationality, string birthplace, string address, string account, bool? perstaff, bool sync, out Guid gid, string ex)
    {
        gid = db.GetId();
        try
        {
            string key, salt;
            var password = HomoryCryptor.Encrypt(passwordInitial, out key, out salt);
            var user = new User
            {
                Id = gid,
                Account = account,
                RealName = name,
                DisplayName = name,
                Stamp = Guid.NewGuid(),
                Type = UserType.教师,
                Password = password,
                PasswordEx = null,
                CryptoKey = key,
                CryptoSalt = salt,
                Icon = "~/CommonX/默认/用户.png",
                State = state,
                Ordinal = ordinal,
                Description = null
            };
            var userTeacher = new Homory.Model.Teacher
            {
                Id = user.Id,
                Phone = phone,
                Email = email,
                Gender = gender,
                Birthday = birthday,
                Birthplace = birthplace,
                Address = address,
                Nationality = nationality,
                IDCard = idCard,
                PerStaff = perstaff ?? true,
                Sync = sync
            };
            var relation = new DepartmentUser
            {
                DepartmentId = departmentId,
                UserId = user.Id,
                TopDepartmentId = campusId,
                Type = DepartmentUserType.部门主职教师,
                State = State.启用,
                Ordinal = 0,
                Time = DateTime.Now
            };
            db.User.Add(user);
            db.Teacher.Add(userTeacher);
            db.DepartmentUser.Add(relation);
            db.SaveChanges();
            try { UserHelper.InsertUserEx(name, "C984AED014AEC7623A54F0591DA07A85FD4B762D", sync, state, account, departmentId.ToString().ToUpper(), gid.ToString().ToUpper(), phone, idCard, ordinal, 0, "1000", ex); } catch { }
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
        var data = book.Worksheets[0].Cells.ExportDataTable(0, 0, book.Worksheets[0].Cells.Rows.Where(o => o[0].Value != null).Count(), 6, false);
        var campusId = Guid.Parse(Request.QueryString["CampusId"]);
        var departmentId = Guid.Parse(Request.QueryString["DepartmentId"]);
        foreach (DataRow row in data.Rows)
        {
            try
            {
                int ordinal = 100;
                try
                {
                    ordinal = int.Parse(row[0].ToString());
                }
                catch
                {
                }
                Guid gid;
                TeacherAdd(HomoryContext.Value, campusId, departmentId, ordinal, row[1].ToString(), row[3].ToString(), "000000", State.启用, "", row[4].ToString(), (bool?)null, (DateTime?)null, "", "", "", row[2].ToString(), true, true, out gid, row[5] == null ? "" : row[5].ToString());
                HomoryContext.Value.SaveChanges();
            }
            catch
            {
            }
        }
        Response.Redirect("../Go/Teacher", false);
    }
}
