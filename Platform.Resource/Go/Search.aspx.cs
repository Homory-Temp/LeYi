using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoSearch : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                search_content.Value = Request.QueryString["Content"];
                if (string.IsNullOrEmpty(Request.QueryString["Inner"]))
                    search_go_OnServerClick(search_go, null);
                else
                    search_go_OnServerClick(search_go_inner, null);
            }
        }

        protected List<Homory.Model.Resource> LoadDataSource()
        {
            var content = search_content.Value.Trim();
            var source = HomoryContext.Value.Resource.Where(o => o.State < State.审核).ToList();
            var final = source.Where(o => o.Title.Contains(content) || o.ResourceTag.Count(ox => ox.Tag == content) > 0).ToList().OrderByDescending(o => o.Time).ToList();
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["Assistant"]))
            {
                ss.Checked = true;
            }
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["Course"]))
            {
                Guid cid;
                if (Guid.TryParse(Request.QueryString["Course"], out cid))
                {
                    if (cid == Guid.Parse("f0b82122-4e2f-3522-22d7-9e5a7ffa8b13"))
                    {
                        foreach (var c in HomoryContext.Value.Catalog.Where(p => p.Type == CatalogType.课程 && p.State == State.内置))
                        {
                            final = final.Where(o => o.CourseId != c.Id).ToList();
                        }
                    }
                    else
                    {
                        final = final.Where(o => o.CourseId != null && o.CourseId == cid).ToList();
                    }
                }
            }
            if (hhhh.Value == "0")
            {
                search_go.Style[HtmlTextWriterStyle.FontWeight] = "normal";
                search_go.Style[HtmlTextWriterStyle.FontSize] = "10px";
                search_go_inner.Style[HtmlTextWriterStyle.FontWeight] = "bold";
                search_go_inner.Style[HtmlTextWriterStyle.FontSize] = "12px";
                var xFinal = final.Where(o => o.User.DepartmentUser.FirstOrDefault(p => p.State < State.审核 && (p.Type == DepartmentUserType.借调后部门主职教师 || p.Type == DepartmentUserType.部门主职教师)).TopDepartmentId == CurrentCampus.Id).ToList();
                return xFinal;
            }
            else
            {
                search_go_inner.Style[HtmlTextWriterStyle.FontWeight] = "normal";
                search_go_inner.Style[HtmlTextWriterStyle.FontSize] = "10px";
                search_go.Style[HtmlTextWriterStyle.FontWeight] = "bold";
                search_go.Style[HtmlTextWriterStyle.FontSize] = "12px";
                return final;
            }
        }

        protected void search_go_OnServerClick(object sender, EventArgs e)
        {
            hhhh.Value = (sender as HtmlAnchor).ID == search_go.ID ? "1" : "0";
            var source = LoadDataSource();
            total.InnerText = source.Count.ToString();
            var list = new List<ResourceCatalog>();
            var filter = source.Aggregate(list, (catalogs, resource) =>
            {
                catalogs.AddRange(resource.ResourceCatalog);
                return catalogs;
            }).Where(o => o.State == State.启用).Select(o => o.CatalogId).Distinct().ToList().Join(HomoryContext.Value.Catalog, o => o, o => o.Id, (o1, o2) => o2).Where(o => o.State < State.审核).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
            period.DataSource = source.Where(s => s.GradeId != null).Select(s => s.GradeId).Distinct().ToList().Join(HomoryContext.Value.Catalog.Where(c => c.State < State.审核), a => a, b => b.Id, (a, b) => b).OrderBy(o => o.Ordinal).Select(o => o.Type).Distinct().ToList();
            period.DataBind();
            course.DataSource = source.Where(s => s.CourseId != null).Select(s => s.CourseId).Distinct().ToList().Join(HomoryContext.Value.Catalog.Where(c => c.State < State.审核), a => a, b => b.Id, (a, b) => b).ToList().OrderBy(o => o.State).ThenBy(o => o.Ordinal);
            course.DataBind();
            grade.DataSource = source.Where(s => s.GradeId != null).Select(s => s.GradeId).Distinct().ToList().Join(HomoryContext.Value.Catalog.Where(c => c.State < State.审核), a => a, b => b.Id, (a, b) => b).OrderBy(o => o.Ordinal).ToList();
            grade.DataBind();
            catalog.DataSource = filter.Where(o => o.Type == CatalogType.文章 || o.Type == CatalogType.视频).OrderBy(o => o.Ordinal).ToList();
            catalog.DataBind();
            t1.Checked = true;
            t2.Checked = true;
            t3.Checked = true;
            t4.Checked = true;
            if (ss.Checked)
            {
                source = source.Where(o => o.AssistantType == 1).ToList();
            }
            result.DataSource = source;
            result.DataBind();
            total.InnerText = source.Count.ToString();
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void item0_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = period.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B(true);
        }

        protected void item1_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = course.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void item2_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = grade.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void item3_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = catalog.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void item_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = new RadButton[] { t1, t2, t3, t4 };
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        protected void itemS_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            B();
        }

        protected void itemX_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = new RadButton[] { s1, s2, s3 };
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            B();
        }

        private void B(bool reset = false)
        {
            var source = LoadDataSource();
            Guid id;
            CatalogType? period_type = null;
            List<Guid> idList = new List<Guid>();
            if (period.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                period_type = (CatalogType)int.Parse(period.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);
                idList = HomoryContext.Value.Catalog.Where(o => o.Type == period_type && o.State < State.审核).Select(o => o.Id).ToList();
                source = source.Where(o => o.GradeId != null && idList.Contains(o.GradeId.Value)).ToList();
                if (reset)
                {
                    grade.DataSource = source.Where(s => s.GradeId != null).Select(s => s.GradeId).Distinct().ToList().Join(HomoryContext.Value.Catalog.Where(c => c.State < State.审核 && c.Type == period_type), a => a, b => b.Id, (a, b) => b).OrderBy(o => o.Ordinal).ToList();
                    grade.DataBind();
                }
            }
            if (course.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                id = Guid.Parse(course.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);
                source = source.Where(o => o.CourseId == id).ToList();
            }
            if (grade.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                id = Guid.Parse(grade.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);
                source = source.Where(o => o.GradeId == id).ToList();
                if (period_type.HasValue)
                {
                    source = source.Where(o => o.GradeId != null && idList.Contains(o.GradeId.Value)).ToList();
                }
            }
            if (catalog.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                id = Guid.Parse(catalog.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);
                source = source.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == id) > 0).ToList();
            }
            if (new RadButton[] { t1, t2, t3, t4 }.Count(o => o.Checked) == 1)
            {
                Homory.Model.ResourceType tt = (Homory.Model.ResourceType)int.Parse(new RadButton[] { t1, t2, t3, t4 }.Single(o => o.Checked).Value);
                source = source.Where(o => o.Type == tt).ToList();
            }
            if (ss.Checked)
            {
                source = source.Where(o => o.AssistantType == 1).ToList();
            }
            if (s2.Checked)
                result.DataSource = source.OrderByDescending(o => o.View);
            else if (s3.Checked)
                result.DataSource = source.OrderByDescending(o => o.Grade);
            else if (s1.Checked)
                result.DataSource = source.OrderByDescending(o => o.Time);
            total.InnerText = source.Count.ToString();
        }

        protected void result_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            B();
        }
    }
}
