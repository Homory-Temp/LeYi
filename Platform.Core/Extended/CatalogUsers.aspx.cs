using Homory.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Telerik.Web.UI;

namespace Extended
{
	public partial class ExtendedCatalogUsers : HomoryCorePageWithNotify
	{
		private const string Right = "Article";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
// ReSharper disable Html.PathError
				Response.Redirect("~/Go/Home", false);
// ReSharper restore Html.PathError
			}
		}

		private Catalog _catalog;

		protected Catalog CurrentCatalog
        {
			get
			{
				if (_catalog != null) return _catalog;
				var id = Guid.Parse(Request.QueryString[0]);
                _catalog = HomoryContext.Value.Catalog.SingleOrDefault(o => o.Id == id);
				return _catalog;
			}
		}

		protected override string PageRight
		{
			get { return Right; }
		}

		protected bool Count(Guid id)
		{
            return CurrentCatalog.AuditUsers.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(o => Guid.Parse(o)).ToList().Count(o => o == id) > 0;
		}

		protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			var id = Guid.Parse(Request.QueryString[0]);
			var obj = HomoryContext.Value.ViewTeacher.Where(o => o.State < State.审核 && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师));
			var query = peek.Text;
			view.DataSource = string.IsNullOrWhiteSpace(query) ? obj.ToList() : obj.Where(
				o =>
					o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) ||
					(o.IDCard != null && o.IDCard.Contains(query))).ToList();
		}

		protected void peek_Search(object sender, SearchBoxEventArgs e)
		{
			view.Rebind();
		}

		protected void btn_Click(object sender, EventArgs e)
		{
			var state = ((RadButton) sender).Checked ? State.启用 : State.删除;
			var id = Guid.Parse(((RadButton) sender).Value);
            if (state == State.启用 && !Count(id))
            {
                CurrentCatalog.AuditUsers += id.ToString().ToUpper() + ".";
                HomoryContext.Value.SaveChanges();
            }
            else if (state == State.删除 && Count(id))
            {
                CurrentCatalog.AuditUsers = CurrentCatalog.AuditUsers.Replace(id.ToString().ToUpper() + ".", "");
                HomoryContext.Value.SaveChanges();
            }
        }

		protected void buttonOk_Click(object sender, EventArgs e)
		{
			panelInner.ResponseScripts.Add("RadClose();");
		}
	}
}
