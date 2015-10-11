using System.Collections.Generic;
using System.Globalization;
using EntityFramework.Extensions;
using System;
using System.Linq;
using Homory.Model;

namespace Go
{
	public partial class GoPolicy : HomoryCorePageWithGrid
	{
        private const string Right = "Policy";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadInit();
                LogOp(OperationType.查询);
            }
		}

// ReSharper disable InconsistentNaming
        protected List<ApplicationPolicy> _policies;
// ReSharper restore InconsistentNaming

        public List<ApplicationPolicy> Policies
		{
			get
			{
				if (_policies == null || _policies.Count == 0)
				{
					_policies = HomoryContext.Value.ApplicationPolicy.ToList();
				}
				return _policies;
			}
		}

		protected dynamic Get<T>(string name)
		{
			var value = Policies.Single(o => o.Name == name).Value;
			if (typeof(T) == typeof(string))
				return value;
			if (typeof(T) == typeof(bool))
				return bool.Parse(value);
			return null;
		}

		private void LoadInit()
		{
			p1.SelectedToggleStateIndex = Get<bool>("UserRegistration") ? 0 : 1;
			p2.Value = Get<string>("UserEmailRegex");
			p3.Value = Get<string>("UserPhoneRegex");
			p4.Value = Get<string>("UserPasswordLength");
			p5.Value = Get<string>("UserCookieExpire");
			p6.Value = Get<string>("SmtpHost");
			p7.Value = Get<string>("SmtpAccount");
			p8.Value = Get<string>("SmtpPassword");
			p9.Value = Get<string>("SmtpPort");
			p10.Value = Get<string>("SmtpSender");
			p12.SelectedToggleStateIndex = Get<bool>("PermanentParttime") ? 0 : 1;
		}

		protected void restore_OnClick(object sender, EventArgs e)
		{
			HomoryContext.Value.ResetPolicyCommon();
			LoadInit();
			Notify(panel, "还原成功", "success");
		}

		protected void save_OnClick(object sender, EventArgs e)
		{
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "UserRegistration")
                .Update(o => new ApplicationPolicy { Value = (p1.SelectedToggleStateIndex == 0).ToString() });
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "UserEmailRegex")
                .Update(o => new ApplicationPolicy { Value = p2.Value });
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "UserPhoneRegex")
                .Update(o => new ApplicationPolicy { Value = p3.Value });
			int n4;
			int.TryParse(p4.Value, out n4);
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "UserPasswordLength")
                .Update(o => new ApplicationPolicy { Value = n4.ToString(CultureInfo.InvariantCulture) });
			int n5;
			int.TryParse(p5.Value, out n5);
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "UserCookieExpire")
                .Update(o => new ApplicationPolicy { Value = n5.ToString(CultureInfo.InvariantCulture) });
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "SmtpHost")
                .Update(o => new ApplicationPolicy { Value = p6.Value });
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "SmtpAccount")
                .Update(o => new ApplicationPolicy { Value = p7.Value });
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "SmtpPassword")
                 .Update(o => new ApplicationPolicy { Value = p8.Value });
			int n9;
			int.TryParse(p9.Value, out n9);
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "SmtpPort")
                 .Update(o => new ApplicationPolicy { Value = n9.ToString(CultureInfo.InvariantCulture) });
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "SmtpSender")
                 .Update(o => new ApplicationPolicy { Value = p10.Value });
			HomoryContext.Value.ApplicationPolicy.Where(o => o.Name == "PermanentParttime")
				.Update(o => new ApplicationPolicy { Value = (p12.SelectedToggleStateIndex == 0).ToString() });
			HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
            LoadInit();
			Notify(panel, "保存成功", "success");
		}

        protected override string PageRight
        {
            get { return Right; }
        }
    }
}
