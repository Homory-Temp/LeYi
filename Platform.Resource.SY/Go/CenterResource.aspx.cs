using Aspose.Words.Lists;
using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoCenterResource : HomoryResourcePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

                InitializeHomoryPage();
			}
		}

        protected override bool ShouldOnline
        {
            get { return true; }
        }

        protected void InitializeHomoryPage()
		{
              
            Func <Catalog,bool> TreeWhere = o => o.State < State.审核;

            if (VideoResource.Checked)
            {
                TreeWhere += o => o.Type == CatalogType.视频;
            }
            if (ArticleResource.Checked)
            {
                TreeWhere += o => o.Type == CatalogType.文章;
            }
            if (CoursewareResource.Checked)
            {
                TreeWhere += o => o.Type == CatalogType.课件;
            }
            tree.DataSource = HomoryContext.Value.Catalog.Where(TreeWhere).ToList();
            tree.DataBind();
            tree.GetAllNodes().ToList().ForEach(o => o.Checked = true);
            tree.GetAllNodes().Where(o => o.Level < 1).ToList().ForEach(o => o.Expanded = true);
			var user = CurrentUser;
            reBind();

		}

		

		protected void LEFT_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			CenterLeftControl.ReBindCenterLeft();
		}

        protected void VideoResource_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);

            var list = new RadButton[] { VideoResource, ArticleResource, CoursewareResource };

            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            InitializeHomoryPage();
        }
        protected void result_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            reBind();
        }
        protected void a_CheckedChanged(object sender, EventArgs e)
        {
            InitializeHomoryPage();
        }

        protected void reBind()
        {
            var content = publisher.Text.Trim();

            var source = HomoryContext.Value.ResourceMap;

            var catalogIdList = tree.GetAllNodes().Where(o => o.Checked).Select(o => Guid.Parse(o.Value)).ToList();
           
            var list = source.Join(catalogIdList, a => a.CatalogId, b => b, (a, b) => a).ToList();
            
            var final = list.OrderByDescending(o => o.Time).ToList();

            if (!string.IsNullOrEmpty(content))
                final = final.Where(o => o.Title.Contains(content)).ToList();
            if (!a1.Checked)
                final = final.Where(o => o.GradeId != Guid.Parse(a1.Value)).ToList();
            if (!a2.Checked)
                final = final.Where(o => o.GradeId != Guid.Parse(a2.Value)).ToList();
            if (!a3.Checked)
                final = final.Where(o => o.GradeId != Guid.Parse(a3.Value)).ToList();
            if (!a4.Checked)
                final = final.Where(o => o.GradeId != Guid.Parse(a4.Value)).ToList();
            if (!a5.Checked)
                final = final.Where(o => o.GradeId != Guid.Parse(a5.Value)).ToList();

            DateTime ft, tt;
            var f = from.SelectedDate;
            var t = to.SelectedDate;
            if (f.HasValue && t.HasValue)
            {
                ft = f.Value > t.Value ? t.Value : f.Value;
                tt = f.Value > t.Value ? f.Value : t.Value;
                ft = new DateTime(ft.Year, ft.Month, 1).AddMilliseconds(-1);
                tt = new DateTime(tt.Year, tt.Month, 1).AddMonths(1);
                final = final.Where(o => o.ResourceTime > ft && o.ResourceTime < tt).ToList();
            }
            else if (f.HasValue)
            {
                tt = f.Value;
                tt = new DateTime(tt.Year, tt.Month, 1).AddMilliseconds(-1);
                final = final.Where(o => o.ResourceTime > tt).ToList();
            }
            else if (t.HasValue)
            {
                tt = t.Value;
                tt = new DateTime(tt.Year, tt.Month, 1).AddMilliseconds(-1);
                final = final.Where(o => o.ResourceTime > tt).ToList();
            }

            if (!string.IsNullOrEmpty(publisher.Text))
            {
                final = final.Where(o => o.Title.ToLower().Contains(publisher.Text.ToLower())).ToList();
            }

            result.DataSource = final.OrderByDescending(o => o.Stick);

            result.DataBind();
                    
        }
        protected void ArticleResource_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);

            var list = new RadButton[] { VideoResource, ArticleResource, CoursewareResource };

            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            InitializeHomoryPage();
        }

        protected void CoursewareResource_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);

            var list = new RadButton[] { VideoResource, ArticleResource, CoursewareResource };

            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            InitializeHomoryPage();
        }

        protected bool IsEdit(bool Audit,bool AuditEditable, State state) {

            if (Audit&&!AuditEditable && state ==State.启用)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected void tree_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            reBind();
        }

        protected void del2_ServerClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
            HomoryContext.Value.Action.Where(o => o.Id1 == id || o.Id2 == id || o.Id3 == id).Update(o => new Homory.Model.Action { State = State.删除 });
            HomoryContext.Value.Resource.Where(o => o.Id == id).Update(o => new Resource { State = State.删除 });
            HomoryContext.Value.SaveChanges();
            LEFT.RaisePostBackEvent("Re");
            reBind();

        }
        protected void result_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            var ab = (e.Item.FindControl("ab") as HtmlAnchor).ClientID;
            var tip = (e.Item.FindControl("tip") as RadToolTip);
            tip.TargetControlID = ab;
        }
        protected List<ResourceAudit> ExamineList(Guid resourceId) {

            var ra = HomoryContext.Value.ResourceAudit.Where(o => o.ResourceId == resourceId).ToList();

            return ra;

        }

    }
}
