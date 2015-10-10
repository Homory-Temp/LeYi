using System;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

namespace Go
{
	public partial class GoRoomsX : HomoryCorePageWithGrid
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadInit();
                LogOp(OperationType.查询);
            }
		}

		private void LoadInit()
		{
			loading.InitialDelayTime = int.Parse("Busy".FromWebConfig());
			DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Common/临时"));
			var files = dir.GetFiles("*.mp4");
			rLeft.DataSource = files.OrderByDescending(o => o.Name).ToList();
			rLeft.DataBind();
			num.DataSource = HomoryContext.Value.ResourceCommentTemp.Select(o => o.RoomId).Distinct().Join(HomoryContext.Value.ResourceRoom.Where(p => p.State < State.删除), o => o, o => o.Id, (a, b) => b).ToList();
			num.DataBind();
			var timeS = ts.SelectedDate;
			var timeE = te.SelectedDate;
			if (!timeE.HasValue)
			{
				te.SelectedDate = DateTime.Today.AddDays(1).AddSeconds(-1);
				timeE = DateTime.Today.AddDays(1).AddSeconds(-1);
			}
			if (!timeS.HasValue)
			{
				ts.SelectedDate = DateTime.Today;
				timeS = DateTime.Today;
			}
			try
			{
				num.SelectedIndex = 0;
				Guid id = Guid.Parse(num.SelectedItem.Value);
				rRight.DataSource = HomoryContext.Value.ResourceCommentTemp.Where(o => o.RoomId == id && o.State == State.启用 && o.Time > timeS && o.Time < timeE).OrderByDescending(o => o.Time).ToList();
				rRight.DataBind();
			}
			catch
			{

			}
		}

		protected User U(object id)
		{
			var gid = Guid.Parse(id.ToString());
			return HomoryContext.Value.User.Single(o => o.Id == gid);
		}

		protected string LabelLeft(object name)
		{
			var s = name.ToString().Split(new char[] { '-' });
			return string.Format("{0}号直播间 {1}-{2}-{3} {4}:{5}:{6}", s[0], s[1], s[2].PadLeft(2, '0'), s[3].PadLeft(2, '0'), s[4].PadLeft(2, '0'), s[5].PadLeft(2, '0'), s[6].Split(new char[] { ' ', '.' })[0].PadLeft(2, '0'));
		}

		protected string LabelRight(object ordinal)
		{
			int i = int.Parse(ordinal.ToString());
			var room = HomoryContext.Value.ResourceRoom.First(o => o.Ordinal == i && o.State < State.删除);
			var s1 = string.Format("{0}号直播间", ordinal);
			var min = HomoryContext.Value.ResourceCommentTemp.Where(o => o.State == State.启用 && o.RoomId == room.Id).Min(o => o.Time);
			var max = HomoryContext.Value.ResourceCommentTemp.Where(o => o.State == State.启用 && o.RoomId == room.Id).Max(o => o.Time);
			return string.Format("{0} {1}至{2}", s1, min.ToString("yyyy-MM-dd HH:mm:ss"), max.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		protected override string PageRight
		{
			get { return "Rooms"; }
		}

		protected void filterGo_ServerClick(object sender, EventArgs e)
		{
			if (num.SelectedIndex < 0)
			{
				return;
			}
			var timeS = ts.SelectedDate;
			var timeE = te.SelectedDate;
			if (!timeE.HasValue)
			{
				timeE = DateTime.Today.AddDays(1).AddSeconds(-1);
			}
			if (!timeS.HasValue)
			{
				timeS = DateTime.Today;
			}
			if (timeS.Value > timeE.Value)
			{
				var timeT = timeS.Value;
				timeS = timeE;
				timeE = timeT;
			}
			Guid id = Guid.Parse(num.SelectedItem.Value);
			rRight.DataSource = HomoryContext.Value.ResourceCommentTemp.Where(o => o.RoomId == id && o.State == State.启用 && o.Time > timeS && o.Time < timeE).OrderByDescending(o => o.Time).ToList();
			rRight.DataBind();
        }

		protected void chk_CheckedChanged(object sender, EventArgs e)
		{
			foreach(RepeaterItem item in rRight.Items)
			{
				var c = ((CheckBox)item.FindControl("chkX")).Checked = chk.Checked;
                LogOp(OperationType.编辑);
            }
		}
	}
}
