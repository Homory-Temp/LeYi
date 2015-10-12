using Homory.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Telerik.Web.UI;

namespace Popup
{
	public partial class ExtendedStudioMember : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
// ReSharper disable Html.PathError
				Response.Redirect("~/Go/CenterGroup", false);
// ReSharper restore Html.PathError
			}
		}

		private Group _group;

		protected Group CurrentGroup
		{
			get
			{
				if (_group != null) return _group;
				var id = Guid.Parse(Request.QueryString[0]);
				_group = HomoryContext.Value.Group.SingleOrDefault(o => o.Id == id);
				return _group;
			}
		}

		protected bool Count(Guid id)
		{
			return CurrentGroup.GroupUser.Count(o => o.UserId == id && o.Type == GroupUserType.组成员 && o.State == State.启用) > 0;
		}

		protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			var id = Guid.Parse(Request.QueryString[0]);
            var camId = CurrentCampus.Id;
			var obj = HomoryContext.Value.ViewTeacher.Where(o => o.State < State.审核 && o.TopDepartmentId == camId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师));
			if (CurrentGroup.GroupUser.Count(o => o.GroupId == id && o.Type == GroupUserType.创建者 && o.State < State.审核) > 0)
			{
				var userId = CurrentGroup.GroupUser.First(o => o.GroupId == id && o.Type == GroupUserType.创建者 && o.State < State.审核).UserId;
				obj = obj.Where(o => o.Id != userId);
			}
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
			var groupId = Guid.Parse(Request.QueryString[0]);
			var id = Guid.Parse(((RadButton) sender).Value);
			var gu = new GroupUser
			{
				GroupId = groupId,
				UserId = id,
				Type = GroupUserType.组成员,
				State = state,
				Ordinal = 1,
				Time = DateTime.Now
			};
			HomoryContext.Value.GroupUser.AddOrUpdate(gu);
			HomoryContext.Value.SaveChanges();
		}

		protected void buttonOk_Click(object sender, EventArgs e)
		{
			panelInner.ResponseScripts.Add("RadClose();");
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}
	}
}
