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
                BindSearch();
            }
        }

        protected void BindSearch() {
            var source = LoadDataSource();
            var list = new List<ResourceCatalog>();
            var filter = source.Aggregate(list, (catalogs, resource) =>
            {
                catalogs.AddRange(resource.ResourceCatalog);
                return catalogs;
            }).Where(o => o.State == State.启用).Select(o => o.CatalogId).Distinct().ToList().Join(HomoryContext.Value.Catalog, o => o, o => o.Id, (o1, o2) => o2).Where(o => o.State < State.审核).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
            //学段
            classAreaRepeater.DataSource = HomoryContext.Value.Department.Where(o => o.Type == 0 && o.ClassType != 0).ToList();
            classAreaRepeater.DataBind();
            //课程
            courseRepeater.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程).OrderBy(o => o.Ordinal).ToList();
            courseRepeater.DataBind();
            //年级
            gradeRepeater.DataSource = FindAges();
            gradeRepeater.DataBind();
            //栏目
            catalogRepeater.DataSource = filter.Where(o => o.Type == CatalogType.文章 || o.Type == CatalogType.视频).OrderBy(o => o.Ordinal).ToList();
            catalogRepeater.DataBind();

            result.DataSource = source;
            result.DataBind();

            total.InnerText = source.Count.ToString();

        }

        protected List<Homory.Model.Resource> LoadDataSource()
        {
            //搜索框中的搜索值
            var content = search_content.Value.Trim();
            //获取resource的所有值
            var source = HomoryContext.Value.Resource.Where(o => o.State < State.审核).ToList();
            //根据搜索值和资源便签进行查询
            var final = source.Where(o => o.Title.Contains(content) || o.ResourceTag.Count(ox => ox.Tag == content) > 0).ToList().OrderByDescending(o => o.Time).ToList();
            //根据课程进行资源筛选
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["Course"]))
            {
                Guid cid;

                if (Guid.TryParse(Request.QueryString["Course"], out cid))
                {
                    final = final.Where(o => o.CourseId != null && o.CourseId == cid).ToList();
                }
            }

            search_go.Style[HtmlTextWriterStyle.FontWeight] = "bold";

            search_go.Style[HtmlTextWriterStyle.FontSize] = "12px";

            return final;
        }

        protected void search_go_OnServerClick(object sender, EventArgs e)
        {

            BindSearch();

        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }
        /// <summary>
        /// 最新，最优，最热
        /// </summary>
        protected void itemX_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = new RadButton[] { s1, s2, s3 };
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            reBind();
        }

        protected void result_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            reBind();
        }
        /// <summary>
        /// 课程资源树绑定
        /// </summary>
        protected void BindCatalogSource() {

            commentList.Nodes.Clear();
 
            if (courseRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1 && gradeRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {

                var ParentId = Guid.Parse(gradeRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);

                var TopId = Guid.Parse(courseRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);

                var l1 = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程资源 && o.TopId == TopId && o.State < State.停用).ToList();

                var GradeId = ParentId;

                var catalogType = HomoryContext.Value.Catalog.SingleOrDefault(o => o.Id == GradeId).Type;

                var l2 = HomoryContext.Value.Catalog.Where(o => o.Type == catalogType && o.State < State.停用 && o.Id == ParentId).ToList();

                commentList.DataSource = l1.Union(l2).ToList();

                commentList.DataBind();

                commentList.Nodes[0].Checked = false;

                commentList.Nodes[0].Checkable = false;

                commentList.Nodes[0].Expanded = true;

            }
            else
            {

            }
        }

        
        public Object FindAges()
        {

            var classType = Convert.ToInt32(HomoryContext.Value.Department.Where(o => o.Id == HomoryContext.Value.DepartmentUser.FirstOrDefault(p => p.UserId == CurrentUser.Id).DepartmentId).ToList().Join(HomoryContext.Value.Department, o => o.TopId, x => x.Id, (o, x) => new
            {

                type = x.ClassType

            }).Select(o => new {
                classType = o.type
            }).First().classType);

            var catalog = HomoryContext.Value.Catalog.Where(o => (new CatalogType[] { CatalogType.年级_幼儿园, CatalogType.年级_小学, CatalogType.年级_初中, CatalogType.年级_高中, CatalogType.年级_其他 }.Contains(o.Type)) && o.State < State.审核).OrderBy(o => o.Ordinal).ToList();

            switch (classType)
            {
                case 1:
                    return catalog.Where(o => new CatalogType[] { CatalogType.年级_小学, CatalogType.年级_初中, CatalogType.年级_其他 }.Contains(o.Type)).Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id,
                        Ordinal = o.Ordinal

                    }).OrderBy(o => o.Ordinal).ToList();

                case 2:
                    return catalog.Where(o => o.Type == CatalogType.年级_幼儿园 || o.Type == CatalogType.年级_其他).Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id,
                        Ordinal = o.Ordinal

                    }).OrderBy(o => o.Ordinal).ToList();

                case 3:
                    return catalog.Where(o => o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_其他).Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id,
                        Ordinal = o.Ordinal

                    }).OrderBy(o => o.Ordinal).ToList();
                case 4:
                    return catalog.Where(o => o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_其他).ToList().Select(o => new
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Ordinal = o.Ordinal

                    }).OrderBy(o => o.Ordinal).ToList();
                case 6:
                    return catalog.Where(o => o.Type == CatalogType.年级_高中 || o.Type == CatalogType.年级_其他).ToList().Select(o => new
                    {
                        Id = o.Id,
                        Name = o.Name,
                        Ordinal = o.Ordinal

                    }).OrderBy(o => o.Ordinal).ToList();
                default:
                    return catalog.Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id,
                        Ordinal = o.Ordinal

                    }).OrderBy(o => o.Ordinal).ToList();
            }
        }
        protected CatalogType ConvertFunc(ClassType type) {

            switch (type)
            {
                case ClassType.九年一贯制:
                    return CatalogType.年级_其他;
                case ClassType.幼儿园:
                    return CatalogType.年级_幼儿园;
                case ClassType.小学:
                    return CatalogType.年级_小学;
                case ClassType.初中:
                    return CatalogType.年级_初中;
                case ClassType.高中:
                    return CatalogType.年级_高中;
                default:
                    return CatalogType.年级_高中;
            }

        }
        protected void item0_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);

            var list = classAreaRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());

            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }

            reBindGrade();

            BindCatalogSource();

            reBind();

        }
        /// <summary>
        /// 校区选择重新绑定年级
        /// </summary>
        protected void reBindGrade() {

            if (classAreaRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                var Id = Guid.Parse(classAreaRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);

                var type = (ClassType)HomoryContext.Value.Department.SingleOrDefault(o => o.Id == Id).ClassType;

                var CatalogType = ConvertFunc(type);

                if (CatalogType == CatalogType.年级_其他)
                {
                    var list1 = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.年级_小学).ToList();

                    var list2 = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.年级_初中).ToList();

                    gradeRepeater.DataSource = list1.Union(list2).OrderBy(o => o.Ordinal).ToList();

                    gradeRepeater.DataBind();

                }
                else
                {
                    gradeRepeater.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType).OrderBy(o => o.Ordinal).ToList();

                    gradeRepeater.DataBind();

                }
            }
        }

        protected void reBind() {
            //搜索框中的搜索值
            var content = search_content.Value.Trim();

            var source = HomoryContext.Value.Resource.Where(o => o.State < State.审核);

            List<Guid> catalogIdList = commentList.GetAllNodes().Where(o => o.Checked == true).Select(o => Guid.Parse(o.Value)).ToList();

            if (gradeRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                var gradeId = Guid.Parse(gradeRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);

                source = source.Where(o => o.GradeId == gradeId);
            }

            if (courseRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                var courseId = Guid.Parse(courseRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);

                source = source.Where(o => o.CourseId == courseId);
            }
            if (new RadButton[] { t1, t2, t3, t4 }.Count(o => o.Checked) == 1)
            {
                Homory.Model.ResourceType tt = (Homory.Model.ResourceType)int.Parse(new RadButton[] { t1, t2, t3, t4 }.Single(o => o.Checked).Value);
                source = source.Where(o => o.Type == tt);
            }

            if (catalogRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Count(o => o.Checked) == 1)
            {
                var catalogId = Guid.Parse(catalogRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single()).Single(o => o.Checked).Value);

                catalogIdList.Add(catalogId);

            }
            
            var finalSource = (catalogIdList == null || catalogIdList.Count == 0) ? source.ToList() : catalogIdList.Join(HomoryContext.Value.ResourceCatalog.Where(o => o.State < State.审核), a => a, b => b.CatalogId, (a, b) => b).ToList().Join(source, a => a.ResourceId, b => b.Id, (a, b) => b).ToList();
            //根据搜索值和资源便签进行查询

            var final = (finalSource.Count == 0)? source.Where(o => o.Title.Contains(content) || o.ResourceTag.Count(ox => ox.Tag == content) > 0).ToList().OrderByDescending(o => o.Time).ToList() :finalSource.Where(o => o.Title.Contains(content) || o.ResourceTag.Count(ox => ox.Tag == content) > 0).ToList().OrderByDescending(o => o.Time).ToList();

            if (s2.Checked)
                result.DataSource = final.OrderByDescending(o => o.View);
            else if (s3.Checked)
                result.DataSource = final.OrderByDescending(o => o.Grade);
            else if (s1.Checked)
                result.DataSource = final.OrderByDescending(o => o.Time);

            total.InnerText = final.Count.ToString();
           
        }

        protected void grade_item_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);

            var list = gradeRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());

            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }

            BindCatalogSource();

            reBind();
        }

        protected void course_item_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);

            var list = courseRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());

            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }

            BindCatalogSource();

            reBind();
        }

        protected void catalog_item_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);

            var list = catalogRepeater.Controls.OfType<RepeaterItem>().Select(o => o.Controls.OfType<RadButton>().Single());

            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }

            reBind();
        }

        protected void t_Click(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = new RadButton[] { t1, t2, t3, t4 };
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            reBind();

        }

        protected void commentList_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            reBind();
        }
    }
}
