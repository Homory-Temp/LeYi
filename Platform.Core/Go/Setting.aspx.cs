using Homory.Model;
using System;
using System.IO;
using System.Linq;

namespace Go
{
    public partial class GoSetting : HomoryCorePageWithNotify
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack) return;
			LoadInit();
		}

		private void LoadInit()
		{
            if (CurrentUser.Icon.StartsWith("http") || File.Exists(Server.MapPath(CurrentUser.Icon)))
                viewer.ImageUrl = CurrentUser.Icon;
            else
                viewer.ImageUrl = "~/Common/默认/用户.png";
		}

		protected void upload_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
		{
            var xName = CurrentUser.Id.ToString().ToUpper();
            if (CurrentUser.Teacher != null)
                xName = CurrentUser.Teacher.IDCard;
            if (CurrentUser.Student != null)
                xName = CurrentUser.Student.IDCard;
            var file = string.Format("~/Common/头像/用户/{0}.jpg", xName);
			e.File.SaveAs(Server.MapPath(file), true);
            viewer.ImageUrl = string.Format("{0}?{1}", file, Guid.NewGuid().ToString("N"));
            var id = CurrentUser.Id;
            var u = HomoryContext.Value.User.First(o => o.Id == id);
            u.Icon = file;
            HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
        }

        protected void buttonSave_OnClick(object sender, EventArgs e)
        {
            string k, v;
            var id = CurrentUser.Id;
            var u = HomoryContext.Value.User.First(o => o.Id == id);
            var length = int.Parse(HomoryContext.Value.ApplicationPolicy.Single(o => o.Name == "UserPasswordLength" && o.ApplicationId == Guid.Empty).Value);
            if (string.IsNullOrWhiteSpace(userPassword.Value) || string.IsNullOrWhiteSpace(userPassword2.Value) || !userPassword.Value.Equals(userPassword2.Value, StringComparison.Ordinal) || userPassword.Value.Length < length)
            {
                Notify(panel, string.Format("请输入不少于{0}位的密码，并确保两次输入的密码一致", length), "warn");
                return;
            }
            u.Password = HomoryCryptor.Encrypt(userPassword.Value, out k, out v);
            HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
            try { UserHelper.UpdateUserPassword(id.ToString().ToUpper(), userPassword.Value); } catch { Notify(panel, "办公平台密码更新失败", "warn"); }
            viewer.ImageUrl = CurrentUser.Icon;
            Notify(panel, "保存成功", "success");
        }

        protected override string PageRight
        {
            get { return HomoryCoreConstant.RightEveryone; }
        }
    }
}
