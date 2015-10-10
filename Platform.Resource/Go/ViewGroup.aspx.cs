using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.DynamicData;
using System.Web.Services.Description;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Words.Lists;
using Homory.Model;

namespace Go
{
	public partial class GoViewGroup : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				leader.DataSource = CurrentGroup.GroupUser.Where(o => o.Type == GroupUserType.创建者 && o.State < State.审核).Select(o => o.User).ToList();
                leader.DataBind();

                members.DataSource = CurrentGroup.GroupUser.Where(o => o.Type != GroupUserType.创建者 && o.State < State.审核).Select(o => o.User).ToList();
                members.DataBind();

                var list = new List<Catalog>();
				list.AddRange(HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_教研 && o.ParentId == CurrentGroup.Id && o.State < State.审核).ToList());
                foreach (var catalog in HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_教研 && o.ParentId == CurrentGroup.Id && o.State < State.审核).ToList())
				{
                    list.AddRange(HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.团队_教研 && o.ParentId == catalog.Id && o.State < State.审核).ToList());
				}
                catalogs.DataSource = list.Where(o => o.ResourceCatalog.Count(p => p.State == State.启用) > 0).ToList().OrderBy(o => o.Ordinal).ToList();
				catalogs.DataBind();

                introduction.InnerText = CurrentGroup.Introduction;

                bbbb.Visible = CurrentGroup.GroupUser.Count(o => o.UserId == CurrentUser.Id && o.State < State.审核) > 0;

                BindC();
            }
        }

		private Group _group;

		protected Group CurrentGroup
		{
			get
			{
				if (_group == null)
				{
					var id = Guid.Parse(Request.QueryString["Id"]);
					_group = HomoryContext.Value.Group.Single(o => o.Id == id);
				}
				return _group;
			}
		}

		protected override bool ShouldOnline
		{
			get { return false; }
		}

        protected void catalogs_OnItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			var catalog = e.Item.DataItem as Catalog;
			var item = e.Item.DataItem;
			dynamic row = e.Item.DataItem;
            var id = row.Id;
			var control = e.Item.FindControl("resources") as Repeater;
            var gid = Guid.Parse(Request.QueryString["Id"]);
            var link = e.Item.FindControl("aMore") as HtmlAnchor;
            link.HRef = string.Format("./ViewGroupX.aspx?Id={0}&CatalogId={1}", gid, catalog.Id);
            link.Target = "_blank";
            control.DataSource =
				HomoryContext.Value.Resource.ToList().Where(
					o => o.ResourceCatalog.Count(p => p.CatalogId == id && p.State < State.审核) > 0 && o.State == State.启用).OrderByDescending(o => o.Time).Take(6)
					.ToList();
			control.DataBind();
		}

        protected void cPanel_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            BindC();
        }

        protected void BindC()
        {
            var time = DateTime.Today.AddDays(-7);
            cComment.DataSource = CurrentGroup.GroupBoard.Where(o => o.Time > time && o.State == State.启用).OrderBy(o => o.Time).ToList();
            cComment.DataBind();
            var script = "window.document.getElementById('" + cccccc.ClientID + "').scrollTop = window.document.getElementById('" + cccccc.ClientID + "').scrollHeight;";
            cDo.ResponseScripts.Add(script);
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            BindC();
        }

        protected void dodo_ServerClick(object sender, EventArgs e)
        {
            if (!IsOnline)
            {
                SignOn();
                return;
            }
            var c = new GroupBoard();
            c.Content = cContent.Value;
            c.Id = HomoryContext.Value.GetId();
            c.GroupId = CurrentGroup.Id;
            c.State = State.启用;
            c.Time = DateTime.Now;
            c.UserId = CurrentUser.Id;
            HomoryContext.Value.GroupBoard.Add(c);
            HomoryContext.Value.SaveChanges();
            cContent.Value = "";
            cPanel.RaisePostBackEvent("Refresh");
        }
    }
}
