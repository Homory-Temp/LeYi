using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoGrade : HomoryCorePageWithGrid
    {
        private const string Right = "Grade";

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
            years.Value = __Year;
            toYear.Value = years.Value + 1;
        }

        protected void g1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new { 名称 = "小班", 入学时间 = string.Format("{0}年09月", __Year), 毕业时间 = string.Format("{0}年07月", __Year + 3) });
            list.Add(new { 名称 = "中班", 入学时间 = string.Format("{0}年09月", __Year - 1), 毕业时间 = string.Format("{0}年07月", __Year + 2) });
            list.Add(new { 名称 = "大班", 入学时间 = string.Format("{0}年09月", __Year - 2), 毕业时间 = string.Format("{0}年07月", __Year + 1) });
            g1.DataSource = list;
        }

        protected void g2_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new { 名称 = "初一", 入学时间 = string.Format("{0}年09月", __Year), 毕业时间 = string.Format("{0}年07月", __Year + 3) });
            list.Add(new { 名称 = "初二", 入学时间 = string.Format("{0}年09月", __Year - 1), 毕业时间 = string.Format("{0}年07月", __Year + 2) });
            list.Add(new { 名称 = "初三", 入学时间 = string.Format("{0}年09月", __Year - 2), 毕业时间 = string.Format("{0}年07月", __Year + 1) });
            g2.DataSource = list;
        }

        protected void g3_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new { 名称 = "一年级", 入学时间 = string.Format("{0}年09月", __Year), 毕业时间 = string.Format("{0}年07月", __Year + 6) });
            list.Add(new { 名称 = "二年级", 入学时间 = string.Format("{0}年09月", __Year - 1), 毕业时间 = string.Format("{0}年07月", __Year + 5) });
            list.Add(new { 名称 = "三年级", 入学时间 = string.Format("{0}年09月", __Year - 2), 毕业时间 = string.Format("{0}年07月", __Year + 4) });
            list.Add(new { 名称 = "四年级", 入学时间 = string.Format("{0}年09月", __Year - 3), 毕业时间 = string.Format("{0}年07月", __Year + 3) });
            list.Add(new { 名称 = "五年级", 入学时间 = string.Format("{0}年09月", __Year - 4), 毕业时间 = string.Format("{0}年07月", __Year + 2) });
            list.Add(new { 名称 = "六年级", 入学时间 = string.Format("{0}年09月", __Year - 5), 毕业时间 = string.Format("{0}年07月", __Year + 1) });
            g3.DataSource = list;
        }

        protected void g4_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new { 名称 = "一年级", 入学时间 = string.Format("{0}年09月", __Year), 毕业时间 = string.Format("{0}年07月", __Year + 9) });
            list.Add(new { 名称 = "二年级", 入学时间 = string.Format("{0}年09月", __Year - 1), 毕业时间 = string.Format("{0}年07月", __Year + 8) });
            list.Add(new { 名称 = "三年级", 入学时间 = string.Format("{0}年09月", __Year - 2), 毕业时间 = string.Format("{0}年07月", __Year + 7) });
            list.Add(new { 名称 = "四年级", 入学时间 = string.Format("{0}年09月", __Year - 3), 毕业时间 = string.Format("{0}年07月", __Year + 6) });
            list.Add(new { 名称 = "五年级", 入学时间 = string.Format("{0}年09月", __Year - 4), 毕业时间 = string.Format("{0}年07月", __Year + 5) });
            list.Add(new { 名称 = "六年级", 入学时间 = string.Format("{0}年09月", __Year - 5), 毕业时间 = string.Format("{0}年07月", __Year + 4) });
            list.Add(new { 名称 = "七年级", 入学时间 = string.Format("{0}年09月", __Year - 6), 毕业时间 = string.Format("{0}年07月", __Year + 3) });
            list.Add(new { 名称 = "八年级", 入学时间 = string.Format("{0}年09月", __Year - 7), 毕业时间 = string.Format("{0}年07月", __Year + 2) });
            list.Add(new { 名称 = "九年级", 入学时间 = string.Format("{0}年09月", __Year - 8), 毕业时间 = string.Format("{0}年07月", __Year + 1) });
            g4.DataSource = list;
        }

        protected void g5_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(new { 名称 = "高一", 入学时间 = string.Format("{0}年09月", __Year), 毕业时间 = string.Format("{0}年07月", __Year + 3) });
            list.Add(new { 名称 = "高二", 入学时间 = string.Format("{0}年09月", __Year - 1), 毕业时间 = string.Format("{0}年07月", __Year + 2) });
            list.Add(new { 名称 = "高三", 入学时间 = string.Format("{0}年09月", __Year - 2), 毕业时间 = string.Format("{0}年07月", __Year + 1) });
            g5.DataSource = list;
        }

        private int? __year;

        protected int __Year
        {
            get
            {
                if (!__year.HasValue)
                {
                    __year = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "SchoolYear").Value);
                }
                return __year.Value;
            }
            set
            {
                __year = value;
            }
        }

        protected override string PageRight
        {
            get { return Right; }
        }

        protected void btnSaveX_Click(object sender, EventArgs e)
        {
            var set = HomoryContext.Value.Dictionary.Single(o => o.Key == "SchoolYear");
            set.Value = years.Value.Value.ToString();
            int obsolete = int.Parse(years.Value.Value.ToString());
            var grades = HomoryContext.Value.Department.Where(o => o.Type == DepartmentType.班级 && o.Level == 1 && o.Ordinal == obsolete);
            List<Department> toObsolete = new List<Department>();
            foreach (var dept in grades)
            {
                    toObsolete.AddRange(dept.DepartmentChildren);
            }
            foreach (var c in toObsolete)
            {
                HomoryContext.Value.DepartmentUser.Where(o => (o.Type == DepartmentUserType.班级学生 || o.Type == DepartmentUserType.年级行政领导 || o.Type == DepartmentUserType.班级班主任) && o.State == State.启用 && o.DepartmentId == c.Id).Update(o => new DepartmentUser { State = State.历史 });
                HomoryContext.Value.Learned.Where(o => o.State == State.启用 && o.DepartmentId == c.Id).Update(o => new Learned { State = State.历史 });
                HomoryContext.Value.Taught.Where(o => o.State == State.启用 && o.DepartmentId == c.Id).Update(o => new Taught { State = State.历史 });
            }
            LogOp(OperationType.编辑);
            HomoryContext.Value.SaveChanges();
            toYear.Value = (int)(years.Value + 1);
            __year = null;
            g1.Rebind();
            g2.Rebind();
            g3.Rebind();
            g4.Rebind();
            Notify(panel, "操作成功", "success");
        }
    }
}
