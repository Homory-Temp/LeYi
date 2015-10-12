using Homory.Model;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Go
{
    public partial class GoViewRoom : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var id = Guid.Parse(Request.QueryString["Id"]);
				var room = HomoryContext.Value.ResourceRoom.First(o => o.Id == id);
				t1.InnerText = room.Name;
				t2.InnerHtml = room.Description;
				t3.Attributes["src"] = room.Url;
				BindC();
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}

		protected void dodo_ServerClick(object sender, EventArgs e)
		{
			if (!IsOnline)
			{
				SignOn();
				return;
			}
			var id = Guid.Parse(Request.QueryString["Id"]);
			var c = new ResourceCommentTemp();
			c.Content = cContent.InnerHtml;
			c.Id = HomoryContext.Value.GetId();
			c.RoomId = id;
			c.State = State.启用;
			c.Time = DateTime.Now;
			c.UserId = CurrentUser.Id;
			HomoryContext.Value.ResourceCommentTemp.Add(c);
			HomoryContext.Value.SaveChanges();
			cContent.InnerHtml = "";
			cPanel.RaisePostBackEvent("Refresh");
		}

		protected void cPanel_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
		{
			BindC();
		}

		protected void BindC()
		{
			var id = Guid.Parse(Request.QueryString["Id"]);
			cComment.DataSource = HomoryContext.Value.ResourceCommentTemp.Where(o => o.RoomId == id).OrderByDescending(o=>o.Time).ToList();
			cComment.DataBind();
		}

		protected void timer_Tick(object sender, EventArgs e)
		{
			BindC();
		}
	}
}
