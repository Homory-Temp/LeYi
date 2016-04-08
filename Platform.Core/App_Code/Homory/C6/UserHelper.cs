using JHSoft.IDAL;
using System.Security.Cryptography;
using System.Text;

public class UserHelper
{
    private static DBOperator db = DBOperatorFactory.GetDBOperator();

    // 新增用户
    public static void InsertUser(string name, string password, bool sync, Homory.Model.State state, string account, string deptId, string userId, string phone, string idCard, int ordinal, int relState = 0, string groupId = "1000")
    {
        var id = IDCreator.CreateMaxStrID("Users");
        var param = new object[] { id, name, password, (sync && state < Homory.Model.State.审核) ? 0 : 1, account, deptId, userId, phone, idCard, ordinal, relState, groupId };
        db.ExecProcReDataSet("__InsertUser", param);
    }

    // 新增用户
    public static void InsertUserEx(string name, string password, bool sync, Homory.Model.State state, string account, string deptId, string userId, string phone, string idCard, int ordinal, int relState = 0, string groupId = "1000", string ex = "")
    {
        var id = IDCreator.CreateMaxStrID("Users");
        var param = new object[] { id, name, password, (sync && state < Homory.Model.State.审核) ? 0 : 1, account, deptId, userId, phone, idCard, ordinal, relState, groupId, ex };
        db.ExecProcReDataSet("__InsertUserEx", param);
    }

    // 更新用户
    public static void UpdateUser(string name, bool sync, Homory.Model.State state, string account, string userId, string phone, string idCard, int ordinal)
    {
        var param = new object[] { name, (sync && state < Homory.Model.State.审核) ? 0 : 1, account, userId, phone, idCard, ordinal };
        db.ExecProcReDataSet("__UpdateUser", param);
    }

    // 更新用户主职
    public static void ResetUserFulltime(string userId, string deptId, int ordinal, string groupId = "1000", int delParttime = 1)
    {
        var param = new object[] { userId, deptId, ordinal, delParttime, groupId };
        db.ExecProcReDataSet("__ResetUserFulltime", param);
    }

    // 新增用户兼职
    public static void InsertUserParttime(string userId, string deptId, int ordinal)
    {
        var param = new object[] { userId, deptId, ordinal };
        db.ExecProcReDataSet("__InsertUserParttime", param);
    }

    // 更新用户兼职
    public static void UpdateUserParttime(string userId, string deptId, int ordinal, Homory.Model.State state)
    {
        var param = new object[] { userId, deptId, ordinal, state < Homory.Model.State.审核 ? 0 : 1 };
        db.ExecProcReDataSet("__UpdateUserParttime", param);
    }

    // 新增用户可查看部门
    public static void InsertUserVisitable(string userId, string deptId, string campusId)
    {
        var param = new object[] { userId, deptId, campusId };
        db.ExecProcReDataSet("__InsertUserVisitable", param);
    }

    // 更新用户可查看部门
    public static void UpdateUserVisitable(string userId, string deptId)
    {
        var param = new object[] { userId, deptId };
        db.ExecProcReDataSet("__UpdateUserVisitable", param);
    }

    // 更新用户密码
    public static void UpdateUserPassword(string userId, string password)
    {
        var param = new object[] { userId, HashPassword(password) };
        db.ExecProcReDataSet("__UpdateUserPassword", param);
    }

    private static string HashPassword(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var output = new SHA1Managed().ComputeHash(bytes);
        var sb = new StringBuilder();
        foreach (byte b in output)
        {
            sb.Append(b.ToString("X2"));
        }
        return sb.ToString();
    }
}
