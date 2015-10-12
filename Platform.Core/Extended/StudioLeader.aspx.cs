using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Telerik.Web.UI;

namespace Extended
{
	public partial class ExtendedStudioLeader : HomoryCorePageWithNotify
	{
		private const string Right = "Studio";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Count == 0)
			{
// ReSharper disable Html.PathError
				Response.Redirect("~/Go/Studio", false);
// ReSharper restore Html.PathError
			}
		}

		private Group _group;

		protected Group CurrentGroup
		{
			get
			{
				if (_group == null)
				{
					var id = Guid.Parse(Request.QueryString[0]);
					_group = HomoryContext.Value.Group.SingleOrDefault(o => o.Id == id);
				}
				return _group;
			}
		}

		protected override string PageRight
		{
			get { return Right; }
		}

		protected void buttonOk_Click(object sender, EventArgs e)
		{
			Guid? id = null;
			foreach (var r in view.Items.Select(o => o.FindControl("btn")).Where(r => ((RadButton) r).Checked))
			{
				id = Guid.Parse(((RadButton) r).Value);
				break;
			}
			if (id.HasValue)
			{
				var groupId = Guid.Parse(Request.QueryString[0]);
				HomoryContext.Value.GroupUser.Where(o => o.GroupId == groupId && o.Type == GroupUserType.创建者 && o.State < State.审核).Update(o => new GroupUser { State = State.删除 });
				var gu = new GroupUser
				{
					GroupId = groupId,
					UserId = id.Value,
					Type = GroupUserType.创建者,
					State = State.启用,
					Ordinal = 0,
					Time = DateTime.Now
				};
				HomoryContext.Value.GroupUser.AddOrUpdate(gu);
				HomoryContext.Value.SaveChanges();
				panelInner.ResponseScripts.Add("RadClose();");
			}
		}

		protected bool Count(Guid id)
		{
			return CurrentGroup.GroupUser.Count(o => o.UserId == id && o.Type == GroupUserType.创建者 && o.State < State.审核) > 0;
		}

		protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
		{
			var obj = HomoryContext.Value.ViewTeacher.Where(o => o.State < State.审核 && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师)).Distinct();
			var query = peek.Text;
			view.DataSource = string.IsNullOrWhiteSpace(query) ? obj.ToList() : obj.Where(
				o =>
					o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) || o.PinYin.Contains(query) ||
					(o.IDCard != null && o.IDCard.Contains(query))).ToList();
		}

		protected void peek_Search(object sender, SearchBoxEventArgs e)
		{
			view.Rebind();
		}

		protected void btn_Click(object sender, EventArgs e)
		{
			foreach (var r in view.Items.Select(o => o.FindControl("btn")))
			{
				((RadButton) r).Checked = false;
			}
			((RadButton) sender).Checked = true;
		}
	}
}
