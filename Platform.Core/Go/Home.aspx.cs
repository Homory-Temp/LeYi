using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Go
{
	public partial class GoHome : HomoryCorePageWithNotify
    {
		private List<Menu> _menus;

        /// <summary>
        /// WarnDays天前预警
        /// </summary>
        private const int WarnDays = 3;

        /// <summary>
        /// WarnPeriod分钟内预警
        /// </summary>
        private const int WarnPeriod = 1;

        /// <summary>
        /// 超出WarnTimes次登录失败预警
        /// </summary>
        private const int WarnTimes = 1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var doc = XDocument.Load(Server.MapPath("../Common/配置/Title.xml"));
				this.Title = doc.Root.Element("Core").Value;
				LoadInit();
                //LoadWarning();
			}
		}

        private int? syear;

        protected int __SchoolYear
        {
            get
            {
                if (!syear.HasValue)
                {
                    syear = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "SchoolYear").Value);
                }
                return syear.Value;
            }
            set
            {
                syear = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!IsPostBack)
            {
                var today = DateTime.Today;
                var targetYear = DateTime.Now < new DateTime(today.Year, 8, 1) ? today.Year - 1 : today.Year;
                if (__SchoolYear != targetYear)
                {
                    Notify(panel, "重要提示：当前学年与实际不符，请到年级管理页变更学年！", "error");
                }
            }
            base.OnPreRender(e);
        }

        protected void LoadWarning()
        {
            var time = DateTime.Now.AddDays(-WarnDays);
            var failed = HomoryContext.Value.SignLog.Where(o => o.Login == false && o.Time >= time).OrderBy(o => o.Time).GroupBy(o => o.IP).ToList();
            string warn = string.Empty;
            foreach (var group in failed)
            {
                var key = group.Key;
                int i = 0;
                var first = group.First();
                var result = group.OrderBy(o => o.Time).Aggregate<SignLog, SignLog, bool>(first, (a, b) =>
                {
                    if ((b.Time - a.Time).Minutes <= WarnPeriod) i++;
                    else
                    {
                        if (i <= WarnTimes)
                        {
                            i = 0;
                        }
                    }
                    return b;
                }, l =>
                 {
                     return i <= WarnTimes;
                 });
                if (!result)
                {
                    warn += string.Format("\\r\\nIP：{0}", key);
                }
            }
            if (!string.IsNullOrEmpty(warn))
            {
                Notify(panel, string.Format("登录预警：以下客户端在{0}分钟内登录失败超出{1}次{2}", WarnPeriod, WarnTimes, warn), "error");
            }
        }

        private void LoadInit()
		{
			u.Text = string.Format("你好，{0}", CurrentUser.DisplayName);
			_menus =
				HomoryContext.Value.Menu.Where(o => o.ApplicationId == HomoryCoreConstant.ApplicationKey && o.State == State.启用)
					.ToList();
			var menuList = _menus.Where(o => o.ParentId == null)
				.OrderBy(o => o.Ordinal)
				.ToList();
			repeater.DataSource = menuList;
			repeater.DataBind();
		}

		protected string SubMenu(Menu menu)
		{
			var query = _menus.Where(o => o.ParentId == menu.Id).OrderBy(o => o.Ordinal).ToList();
			var sb = new StringBuilder();
			foreach (var item in query)
			{
				if (CurrentRights.Contains(item.RightName))
					sb.Append(string.Format("&nbsp;<div class='padSubMenu btn btn-{2}' data-url='{0}'>{1}</div>&nbsp;",
						item.Redirect.StartsWith("+") ? Application["Sso"] + "Go/Board" : item.Redirect, item.Name,
						item.Icon));
				else
				{
					sb.Append(string.Format("&nbsp;<div class='padSubMenu btn btn-default' data-url=''>{0}</div>&nbsp;",
						item.Name));
				}
			}
			return sb.ToString();
		}

		protected override string PageRight
		{
			get { return HomoryCoreConstant.RightEveryone; }
		}

		protected void SignOff()
		{
			var path = Request.Url.AbsoluteUri;
			if (path.IndexOf('?') > 0)
				path = path.Substring(0, path.IndexOf('?'));
			var query = Request.QueryString.ToString();
			var url = string.Format("{0}?SsoRedirect={1}", Application["Sso"] + "Go/SignOff", Application["Core"] + "Go/Home");
			Response.Redirect(url, false);
		}

		protected void qb_click(object sender, EventArgs e)
		{
			Session.Clear();
			SignOff();
		}
	}
}
