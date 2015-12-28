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

                InitPage();
			}
		}

        protected override bool ShouldOnline
        {
            get { return true; }
        }

        protected void InitPage()
		{

            BindTree();

            reBind(FindBindSourceIdFromDB());



		}

        protected void BindTree() {

            Func<Catalog, bool> TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && (o.Type == CatalogType.文章 || o.Type == CatalogType.视频 || o.Type == CatalogType.课件);

            if (VideoResource.Checked && !ArticleResource.Checked && !CoursewareResource.Checked)
            {
                TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && o.Type == CatalogType.视频;
            }
            if (!VideoResource.Checked && ArticleResource.Checked && !CoursewareResource.Checked)
            {
                TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && o.Type == CatalogType.文章;
            }
            if (!VideoResource.Checked && !ArticleResource.Checked && CoursewareResource.Checked)
            {
                TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && o.Type == CatalogType.课件;
            }

            var sourceTree = HomoryContext.Value.Catalog.Where(TreeWhere).ToList();

            var source_str = string.Empty;

            foreach (var source in FindBindSourceIdFromDB())
            {
                source_str += source.ToString() + "|";

            }

            this.hidden_value.Value = source_str;

            secondList.DataSource = sourceTree;

            secondList.DataBind();

        }

        protected List<Guid> FindBindSourceIdFromDB() {

            Func<Catalog, bool> TreeWhere = o =>o.State < State.审核 && (o.Type == CatalogType.文章 || o.Type == CatalogType.视频 || o.Type == CatalogType.课件);

            if (VideoResource.Checked && !ArticleResource.Checked && !CoursewareResource.Checked)
            {
                TreeWhere = o => o.State < State.审核 && o.Type == CatalogType.视频;
            }
            if (!VideoResource.Checked && ArticleResource.Checked && !CoursewareResource.Checked)
            {
                TreeWhere = o => o.State < State.审核 && o.Type == CatalogType.文章;
            }
            if (!VideoResource.Checked && !ArticleResource.Checked && CoursewareResource.Checked)
            {
                TreeWhere = o => o.State < State.审核 && o.Type == CatalogType.课件;
            }

            var result_list = HomoryContext.Value.Catalog.Where(TreeWhere).Select(o => o.Id).ToList();

            var source_str = string.Empty;

            foreach (var source in result_list)
            {
                source_str += source.ToString() + "|";

            }

            this.hidden_value.Value = source_str;

            return result_list;
        }

        protected void LEFT_AjaxRequest(object sender, AjaxRequestEventArgs e)
		{
			CenterLeftControl.ReBindCenterLeft();
		}

   
        protected void result_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            

            reBind(FindSourceTreeChecked());
        }
        protected void a_CheckedChanged(object sender, EventArgs e)
        {

            sa.RaisePostBackEvent(hidden_value.Value);

        }
        protected List<Guid> FindSourceTreeChecked() {

            var source_strlist = this.hidden_value.Value.TrimEnd('|').Split('|');

            List<Guid> source_guid = new List<Guid>();

            foreach (var source in source_strlist)
            {
                if (!string.Empty.Equals(source))
                {
                    source_guid.Add(Guid.Parse(source));
                }
            }

            return source_guid;

        }

        protected void reBind( List<Guid> catalogIdList)
        {
            var content = publisher.Text.Trim();

            var source = HomoryContext.Value.ResourceMap;

            var chkList = new List<CheckBox>();

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

            pager.Visible = final.Count > 20;

            sa.ResponseScripts.Add("menuclick()");


        }
        protected void VideoResource_Click(object sender, EventArgs e)
        {
            VideoResource.Checked = true;

            ArticleResource.Checked = false;

            CoursewareResource.Checked = false;

            InitPage();

            var source_str = string.Empty;

            foreach (var source in FindBindSourceIdFromDB())
            {
                source_str += source.ToString() + "|";

            }

            sa.RaisePostBackEvent(source_str);

            st.RaisePostBackEvent("");
        }
        protected void ArticleResource_Click(object sender, EventArgs e)
        {
            ArticleResource.Checked = true;

            VideoResource.Checked = false;

            CoursewareResource.Checked = false;

            InitPage();

            var source_str = string.Empty;

            foreach (var source in FindBindSourceIdFromDB())
            {
                source_str += source.ToString() + "|";

            }

            sa.RaisePostBackEvent(source_str);

            st.RaisePostBackEvent("");
        }

        protected void CoursewareResource_Click(object sender, EventArgs e)
        {
            CoursewareResource.Checked = true;

            ArticleResource.Checked = false;

            VideoResource.Checked = false;

            InitPage();

            var source_str = string.Empty;

            foreach (var source in FindBindSourceIdFromDB())
            {
                source_str += source.ToString() + "|";

            }

            sa.RaisePostBackEvent(source_str);

            st.RaisePostBackEvent("");
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

        protected void del2_ServerClick(object sender, EventArgs e)
        {
            var id = Guid.Parse(((HtmlAnchor)sender).Attributes["data-id"]);
            HomoryContext.Value.Action.Where(o => o.Id1 == id || o.Id2 == id || o.Id3 == id).Update(o => new Homory.Model.Action { State = State.删除 });
            HomoryContext.Value.Resource.Where(o => o.Id == id).Update(o => new Resource { State = State.删除 });
            HomoryContext.Value.SaveChanges();
            LEFT.RaisePostBackEvent("Re");
            reBind(FindSourceTreeChecked());

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

        protected void Search_Click(object sender, EventArgs e)
        {
            var source_str = string.Empty;

            foreach (var source in FindSourceTreeChecked())
            {
                source_str += source.ToString() + "|";

            }

            sa.RaisePostBackEvent(source_str);

        }

        protected void secondList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            Func<Catalog, bool> TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && (o.Type == CatalogType.文章 || o.Type == CatalogType.视频 || o.Type == CatalogType.课件);

            if (VideoResource.Checked && !ArticleResource.Checked && !CoursewareResource.Checked)
            {
                TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && o.Type == CatalogType.视频;
            }
            if (!VideoResource.Checked && ArticleResource.Checked && !CoursewareResource.Checked)
            {
                TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && o.Type == CatalogType.文章;
            }
            if (!VideoResource.Checked && !ArticleResource.Checked && CoursewareResource.Checked)
            {
                TreeWhere = o => o.Id == o.TopId && o.State < State.审核 && o.Type == CatalogType.课件;
            }
            secondList.DataSource = HomoryContext.Value.Catalog.Where(TreeWhere).ToList();
        }

        protected List<Catalog> LoadCatalog(Guid pid)
        {
            return HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.ParentId == pid).ToList();
        }

        protected bool IsDisplay(Guid pid) {

            if (HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.ParentId == pid).ToList().Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        protected void sa_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var sourceid_list = e.Argument.TrimEnd('|').Split('|');

            List<Guid> source_guid = new List<Guid>();

            foreach (var source in sourceid_list)
            {
                if (string.Empty.Equals(source))
                {
                    continue;
                }
                source_guid.Add(Guid.Parse(source));
            }

            reBind(source_guid);
        }

        protected void st_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            BindTree();
        }
    }
}
