using Homory.Model;
using System;
using System.Linq;

namespace Go
{
    public partial class GoInitialize : HomoryPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HomoryContext.Value.User.Count() == 0)
            {
                string key, salt;
                var password = HomoryCryptor.Encrypt("Homory", out key, out salt);
                HomoryContext.Value.User.Add(new User { Account = "Homory", CryptoKey = key, CryptoSalt = salt, Description = "Homory", DisplayName = "凌俊伟", Icon = "~/Common/头像/用户/320000000000000000.jpg", Id = Guid.Empty, Ordinal = 0, Password = password, PasswordEx = "", PinYin = "LJW", RealName = "凌俊伟", Stamp = Guid.Empty, State = State.内置, Type = UserType.内置 });
                HomoryContext.Value.SaveChanges();
            }
            if (HomoryContext.Value.Department.Count() == 0)
            {
                HomoryContext.Value.Department.Add(new Department { BuildType = BuildType.无, ClassType = ClassType.无, Code = "", DisplayName = "乐翼教育云平台", Hidden = false, Id = Guid.Empty, Level = 0, Name = "乐翼教育云平台", Ordinal = 0, ParentId = null, State = State.启用, TopId = Guid.Empty, Type = DepartmentType.学校 });
                HomoryContext.Value.Department.Add(new Department { BuildType = BuildType.无, ClassType = ClassType.无, Code = "", DisplayName = "乐翼教育云平台管理", Hidden = false, Id = Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), Level = 1, Name = "乐翼教育云平台管理", Ordinal = 0, ParentId = Guid.Empty, State = State.启用, TopId = Guid.Empty, Type = DepartmentType.部门 });
                HomoryContext.Value.SaveChanges();
            }
            if (HomoryContext.Value.DepartmentUser.Count() == 0)
            {
                HomoryContext.Value.DepartmentUser.Add(new DepartmentUser { DepartmentId = Guid.Parse("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), Ordinal = 0, State = State.启用, Time = DateTime.Today, TopDepartmentId = Guid.Empty, Type = DepartmentUserType.部门主职教师, UserId = Guid.Empty });
                HomoryContext.Value.SaveChanges();
            }
        }
    }
}
