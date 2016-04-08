using Homory.Model;
using System;
using System.IO;
using System.Linq;

namespace Go
{
    public partial class GoSettingX : HomoryCorePageWithNotify
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

        protected void buttonSave_OnClick(object sender, EventArgs e)
        {
            string k, v;
            var id = CurrentUser.Id;
            var u = HomoryContext.Value.User.First(o => o.Id == id);
            var length = int.Parse(HomoryContext.Value.ApplicationPolicy.Single(o => o.Name == "UserPasswordLength" && o.ApplicationId == Guid.Empty).Value);
            if (string.IsNullOrWhiteSpace(userPassword.Value) || string.IsNullOrWhiteSpace(userPassword2.Value) || !userPassword.Value.Equals(userPassword2.Value, StringComparison.Ordinal) || userPassword.Value.Length < length)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "sc" + Guid.NewGuid().ToString("N"), string.Format("alert('{0}');", string.Format("请输入不少于{0}位的密码，并确保两次输入的密码一致", length)), true);
                return;
            }
            u.Password = HomoryCryptor.Encrypt(userPassword.Value, out k, out v);
            HomoryContext.Value.SaveChanges();
            try { UserHelper.UpdateUserPassword(id.ToString().ToUpper(), userPassword.Value); } catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "sc" + Guid.NewGuid().ToString("N"), string.Format("alert('{0}');", "办公平台密码更新失败"), true);
            }
            ClientScript.RegisterStartupScript(this.GetType(), "sc" + Guid.NewGuid().ToString("N"), string.Format("alert('{0}');", "密码修改成功"), true);
        }

        protected override string PageRight
        {
            get { return HomoryCoreConstant.RightEveryone; }
        }
    }
}
