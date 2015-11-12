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
                    BindSearch(1);
                else
                    BindSearch(0);
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

            var catalog = HomoryContext.Value.Catalog.Where(o => (new CatalogType[] { CatalogType.年级_幼儿园, CatalogType.年级_小学, CatalogType.年级_初中, CatalogType.年级_高中, CatalogType.年级_其他 }.Contains(o.Type)) && o.State < State.审核).ToList();

            switch (classType)
            {
                case 1:
                    return catalog.Where(o => new CatalogType[] { CatalogType.年级_小学, CatalogType.年级_初中, CatalogType.年级_其他 }.Contains(o.Type)).Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id

                    }).ToList();

                case 2:
                    return catalog.Where(o => o.Type == CatalogType.年级_幼儿园 || o.Type == CatalogType.年级_其他).Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id

                    }).ToList();

                case 3:
                    return catalog.Where(o => o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_其他).Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id

                    }).ToList();
                case 4:
                    return catalog.Where(o => o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_其他).ToList().Select(o => new
                    {
                        Id = o.Id,
                        Name = o.Name

                    }).ToList();
                case 6:
                    return catalog.Where(o => o.Type == CatalogType.年级_高中 || o.Type == CatalogType.年级_其他).ToList().Select(o => new
                    {
                        Id = o.Id,
                        Name = o.Name

                    }).ToList();
                default:
                    return catalog.Select(o => new
                    {
                        Name = o.Name,
                        Id = o.Id

                    }).ToList();
            }
        }

        protected void BindSearch(int isInner) {

            //判断是否是搜索全部，全部的话给hhhh赋值1，否则是0；
            hhhh.Value = isInner.ToString();
            var source = LoadDataSource();
            total.InnerText = source.Count.ToString();
            var list = new List<ResourceCatalog>();
            var filter = source.Aggregate(list, (catalogs, resource) =>
            {
                catalogs.AddRange(resource.ResourceCatalog);
                return catalogs;
            }).Where(o => o.State == State.启用).Select(o => o.CatalogId).Distinct().ToList().Join(HomoryContext.Value.Catalog, o => o, o => o.Id, (o1, o2) => o2).Where(o => o.State < State.审核).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
            //学段
            period.DataSource = HomoryContext.Value.Department.Where(o => o.Type == 0 && o.ClassType != 0).ToList();
            period.DataBind();
            period.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));
            //课程
            courseDDL.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程).ToList();
            courseDDL.DataBind();
            courseDDL.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));
            courseDDL.SelectedIndex = 0;
            //年级
            gradeList.DataSource = FindAges();
            gradeList.DataBind();
            gradeList.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));
            gradeList.SelectedIndex = 0;

            //栏目
            catalogDDL.DataSource = filter.Where(o => o.Type == CatalogType.文章 || o.Type == CatalogType.视频).OrderBy(o => o.Ordinal).ToList();
            catalogDDL.DataBind();
            catalogDDL.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));
            catalogDDL.SelectedIndex = 0;
            //类型
            typeDDL.Items.Add(new ListItem("全部", "-1"));
            typeDDL.Items.Add(new ListItem("视频", "1"));
            typeDDL.Items.Add(new ListItem("文章", "2"));
            typeDDL.Items.Add(new ListItem("课件", "3"));
            typeDDL.Items.Add(new ListItem("试卷", "4"));
            typeDDL.SelectedIndex = 0;


            //if (ss.Checked)
            //{
            //    source = source.Where(o => o.AssistantType == 1).ToList();
            //}
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
            //判断是否是小助手
            //if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["Assistant"]))
            //{
            //    ss.Checked = true;
            //}
            //根据课程进行资源筛选
            if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["Course"]))
            {
                Guid cid;

                if (Guid.TryParse(Request.QueryString["Course"], out cid))
                {
                    final = final.Where(o => o.CourseId != null && o.CourseId == cid).ToList();
                }
            }
            //当前校区内的资源筛选
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

            //搜索框中的搜索值
            var content = search_content.Value.Trim();

            Func<Homory.Model.Resource,bool> FunWhere = o=> o.State < State.审核;

            var catalog = Guid.Parse(this.catalogDDL.SelectedValue);

            var course = Guid.Parse(this.courseDDL.SelectedValue);

            var grade = Guid.Parse(this.gradeList.SelectedValue);

            var type = Convert.ToInt32(this.typeDDL.SelectedValue);

            var resourceContent = this.commentList;

            //List<Guid> idList = new List<Guid>();

            if (course != Guid.Empty)
            {
                FunWhere += o => o.CourseId == course;
                //source = source.Where(o => o.CourseId == course).ToList();
            }
            if (grade != Guid.Empty)
            {
                FunWhere += o => o.GradeId == grade;
                //source = source.Where(o => o.GradeId == grade).ToList();
            }

            if (type != -1)
            {
                var EnType = (Homory.Model.ResourceType)type;

                FunWhere += o => o.Type == EnType;
                //source = source.Where(o => o.Type == EnType).ToList();
            }
            //if (ss.Checked)
            //{
            //    FunWhere += o => o.AssistantType == 1;
            //    source = source.Where(o => o.AssistantType == 1).ToList();
            //}

            var source1 = HomoryContext.Value.Resource.Where(FunWhere).ToList();


            List<Guid> catalogIdList = commentList.GetAllNodes().Where(o => o.Checked == true).Select(o => Guid.Parse(o.Value)).ToList();

            catalogIdList.Add(catalog);

            var finalSource = (catalogIdList == null || catalogIdList.Count == 0) || catalogIdList[0] == Guid.Empty ? source1: catalogIdList.Join(HomoryContext.Value.ResourceCatalog.Where(o => o.State < State.审核), a => a, b => b.CatalogId, (a, b) => b).ToList().Join(source1, a => a.ResourceId, b => b.Id, (a, b) => b).ToList();
            //根据搜索值和资源便签进行查询
            var final = finalSource.Where(o => o.Title.Contains(content) || o.ResourceTag.Count(ox => ox.Tag == content) > 0).ToList().OrderByDescending(o => o.Time).ToList();
            //判断是否是小助手
            //if (!IsPostBack && !string.IsNullOrEmpty(Request.QueryString["Assistant"]))
            //{
            //    ss.Checked = true;
            //}
            //当前校区内的资源筛选
            if (hhhh.Value == "0")
            {

                final = final.Where(o => o.User.DepartmentUser.FirstOrDefault(p => p.State < State.审核 && (p.Type == DepartmentUserType.借调后部门主职教师 || p.Type == DepartmentUserType.部门主职教师)).TopDepartmentId == CurrentCampus.Id).ToList();

            }

            if (s2.Checked)
                result.DataSource = final.OrderByDescending(o => o.View);
            else if (s3.Checked)
                result.DataSource = final.OrderByDescending(o => o.Grade);
            else if (s1.Checked)
                result.DataSource = final.OrderByDescending(o => o.Time);
            total.InnerText = final.Count.ToString();


        }

        protected override bool ShouldOnline
        {
            get { return false; }
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

            Func<Homory.Model.Resource> FunWhere;

            var catalog = Guid.Parse(this.catalogDDL.SelectedValue);

            var course = Guid.Parse(this.courseDDL.SelectedValue);

            var grade = Guid.Parse(this.gradeList.SelectedValue);

            var type = Convert.ToInt32(this.typeDDL.SelectedValue);

            List<Guid> idList = new List<Guid>();

            if (course != Guid.Empty)
            {
                 source = source.Where(o => o.CourseId == course).ToList();
            }
            if (grade != Guid.Empty)
            {
                source = source.Where(o => o.GradeId == grade).ToList();
            }
            if (catalog!=Guid.Empty)
            {
                source = source.Where(o => o.ResourceCatalog.Count(p => p.CatalogId == catalog) > 0).ToList();
            }
            if (type != -1)
            {
                var EnType = (Homory.Model.ResourceType)type;

                source = source.Where(o => o.Type == EnType).ToList();
            }
            //if (ss.Checked)
            //{
            //    source = source.Where(o => o.AssistantType == 1).ToList();
            //}
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

        protected void gradeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCatalogSource();
        }

        protected void courseDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCatalogSource();
        }
        protected void BindCatalogSource() {

            var GradeSelectedItem = this.gradeList.SelectedItem;

            var CourseSelectItem = this.courseDDL.SelectedItem;

            commentList.Nodes.Clear();

            var ParentId = Guid.Parse(GradeSelectedItem.Value);

            var TopId = Guid.Parse(CourseSelectItem.Value);

            if (ParentId != Guid.Empty && TopId != Guid.Empty)
            {

                var l1 = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程资源 && o.TopId == TopId && o.State < State.停用).ToList();

                var GradeId = Guid.Parse(this.gradeList.SelectedValue);

                var catalogType = HomoryContext.Value.Catalog.SingleOrDefault(o => o.Id == GradeId).Type;

                var l2 = HomoryContext.Value.Catalog.Where(o => o.Type == catalogType && o.State < State.停用 && o.Id == ParentId).ToList();

                commentList.DataSource = l1.Union(l2).ToList();

                commentList.DataBind();

                commentList.Nodes[0].Checked = false;

                commentList.Nodes[0].Checkable = false;

                commentList.Nodes[0].Expanded = true;

                SourceLi.Visible = true;

            }
            else
            {
                SourceLi.Visible = false;
            }

        }

        protected void searchButton_Click(object sender, EventArgs e)
        {

           
            var source = LoadDataSource();
        }

        protected void period_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Id = Guid.Parse(this.period.SelectedValue);

            if (Id != Guid.Empty)
            {
                var type = (ClassType)HomoryContext.Value.Department.SingleOrDefault(o => o.Id == Id).ClassType;

                var CatalogType = ConvertFunc(type);
                if (CatalogType == CatalogType.年级_其他)
                {
                    var list1 = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.年级_小学).ToList();

                    var list2 = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.年级_初中).ToList();

                    this.gradeList.DataSource = list1.Union(list2).ToList();

                    this.gradeList.DataBind();

                    gradeList.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));

                    gradeList.SelectedIndex = 0;
                }
                else
                {
                    this.gradeList.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType).ToList();

                    this.gradeList.DataBind();

                    gradeList.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));

                    gradeList.SelectedIndex = 0;
                }
            }
            else
            {
                this.gradeList.DataSource = FindAges();

                this.gradeList.DataBind();

                gradeList.Items.Insert(0, new ListItem("全部", Guid.Empty.ToString()));

                gradeList.SelectedIndex = 0;
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
    }
}
