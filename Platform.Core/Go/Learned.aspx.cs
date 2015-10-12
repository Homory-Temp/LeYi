using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoLearned : HomoryCorePageWithGrid
    {
        private const string Right = "CourseLearned";

        protected override void CheckRight()
        {
            if (!IsMaster && !CurrentRights.Contains(PageRight))
            {
				Response.Redirect(Application["Core"] + "Go/Home", false);
			}
        }

        private void BindCombo()
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.ClassType != ClassType.其他)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            else
            {
                var c = CurrentCampus;
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.Id == c.Id && o.ClassType != ClassType.其他)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            combo.DataBind();
        }

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTree();
            InitTree();
            view.Rebind();
        }

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            LoadInit();
            LogOp(OperationType.查询);
        }

        private void LoadInit()
        {
            BindCombo();
            InitCombo();
            BindTree();
            InitTree();
        }

        private void BindTree()
        {
            if (combo.SelectedIndex < 0)
            {
                tree.DataSource = null;
            }
            else
            {
                var c = Guid.Parse(combo.SelectedItem.Value);
                var source =
                    HomoryContext.Value.Department.Where(
                        o => (o.Type == DepartmentType.学校 && o.State == State.启用 && o.Id == c && o.ClassType != ClassType.其他) || (o.Type == DepartmentType.班级 && o.State == State.启用));
                if (CurrentRights.Contains(PageRight))
                {
                    tree.DataSource =
                        source.Where(o => o.Level != 1)
                            .OrderBy(o => o.State)
                            .ThenBy(o => o.Ordinal)
                            .ThenBy(o => o.Name).ToList()
                            .Union(source.Where(o => o.Level == 1).OrderBy(o => o.State).ThenByDescending(o => o.Ordinal).ThenBy(o => o.Name))
                            .ToList();
                }
                else
                {
                    var list = new List<Department>();
                    var d = CurrentUser.DepartmentUser.Where(o => o.State == State.启用 && o.Type == DepartmentUserType.班级班主任).Select(o => o.DepartmentId).ToList();
                    foreach (var dItem in d)
                    {
                        var found = source.SingleOrDefault(o => o.Id == dItem);
                        if (list.Count(o => o.Id == dItem) == 0)
                        {
                            list.Add(found);
                        }
                        // ReSharper disable PossibleNullReferenceException
                        if (found.DepartmentParent != null)
                        // ReSharper restore PossibleNullReferenceException
                        {
                            var found2 = source.SingleOrDefault(o => o.Id == found.DepartmentParent.Id);
                            if (found2 != null && list.Count(o => o.Id == found2.Id) == 0)
                            {
                                list.Add(found2);
                            }
                            // ReSharper disable PossibleNullReferenceException
                            if (found2.DepartmentParent != null)
                            // ReSharper restore PossibleNullReferenceException
                            {
                                var found3 = source.SingleOrDefault(o => o.Id == found2.DepartmentParent.Id);
                                if (found3 != null && list.Count(o => o.Id == found3.Id) == 0)
                                {
                                    list.Add(found3);
                                }
                            }
                        }
                    }
                    tree.DataSource = list;
                }
            }
            tree.DataBind();
        }

        private void InitTree()
        {
            RadTreeNode node = null;
            if (tree.Nodes.Count > 0 && tree.Nodes[0].Nodes.Count > 0)
                node = tree.Nodes[0].Nodes[0];
            else if (tree.Nodes.Count > 0)
                node = tree.Nodes[0];
            if (node == null) return;
            node.Expanded = true;
            node.ExpandParentNodes();
            node.Selected = true;
        }

        public static string[] J = { "初三", "初二", "初一" };
        public static string[] PJ = { "九年级", "八年级", "七年级", "六年级", "五年级", "四年级", "三年级", "二年级", "一年级" };
        public static string[] P = { "六年级", "五年级", "四年级", "三年级", "二年级", "一年级" };
        public static string[] S = { "高三", "高二", "高一" };
        public static string[] K = { "大班", "中班", "小班" };

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

        protected string GenGradeName(Homory.Model.Department d)
        {
            int index = d.Ordinal - __Year - 1;
            var pdt = d.DepartmentRoot.ClassType;
            switch (pdt)
            {
                case ClassType.九年一贯制:
                    return index < PJ.Length && index > -1 ? PJ[index] : string.Empty;
                case ClassType.初中:
                    return index < J.Length && index > -1 ? J[index] : string.Empty;
                case ClassType.小学:
                    return index < P.Length && index > -1 ? P[index] : string.Empty;
                case ClassType.幼儿园:
                    return index < K.Length && index > -1 ? K[index] : string.Empty;
                case ClassType.高中:
                    return index < S.Length && index > -1 ? S[index] : string.Empty;
            }
            return string.Empty;
        }

        protected string GenerateTreeName(Department department, int index, int level)
        {
            var part1 = level == 1 ? GenGradeName(department) : department.DisplayName;
            var part2 = level == 1
                ? (department.Learned.Count(o => o.State == State.启用) == 0
                    ? " [未选择]" : string.Empty) : (department.Learned.Count(o => o.State == State.启用) > 0
                    ? " ★" : string.Empty);
            return string.Format("{0}{1}", part1, part2);
        }

        protected string FormatTreeNode(dynamic department)
        {
            return department.State == State.启用 ? "ui green circle icon" : "ui red circle icon";
        }

        protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            e.Node.Selected = true;
            e.Node.Expanded = true;
            view.Rebind();
        }

        protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (tree.SelectedNode != null && tree.SelectedNode.Level > 0)
            {
                view.Visible = true;
                if (tree.SelectedNode.Level == 1)
                {
                    view.DataSource = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.课程 && o.TopId == null && o.Name != "综合").OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
                }
                else
                {
                    view.DataSource = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.课程 && o.TopId == Guid.Empty && o.Name != "综合").OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
                }
            }
            else
            {
                view.DataSource = null;
            }
        }

        protected string HandleButton(Catalog course)
        {
            var id = Guid.Parse(tree.SelectedNode.Value);
            return course.Learned.Count(o => o.State == State.启用 && o.DepartmentId == id) == 0 ? "btn btn-primary" : "btn btn-info";
        }

        protected string Iconed(Catalog course)
        {
            var id = Guid.Parse(tree.SelectedNode.Value);
            var count = course.Learned.Count(o => o.State == State.启用 && o.DepartmentId == id);
            return string.Format("{0} {1}", count == 0 ? "×" : "√", course.Name);
        }

        protected void OnClick(object sender, EventArgs e)
        {
            var button = (sender as RadButton);
// ReSharper disable PossibleNullReferenceException
            var courseId = Guid.Parse(button.CommandArgument);
// ReSharper restore PossibleNullReferenceException
            var classId = Guid.Parse(tree.SelectedNode.Value);
			CourseLearned(HomoryContext.Value, classId, courseId);
            RebindBatch();
            view.Rebind();
			
        }

        protected void RebindExpanded()
        {
            var expanded = tree.GetAllNodes().Where(o => o.Expanded).Select(o => o.Value).ToList();
            BindTree();
            foreach (
                var toExpand in
                    expanded.Select(value => tree.GetAllNodes().SingleOrDefault(o => o.Value == value))
                        .Where(toExpand => toExpand != null))
            {
                toExpand.Expanded = true;
            }
        }

        protected void RebindBatch()
        {
            var selected = tree.SelectedNode.Value;
            RebindExpanded();
            tree.GetAllNodes().Single(o => o.Value == selected).Selected = true;
        }

        public bool CourseLearned(Entities db, Guid classId, Guid courseId)
        {
            try
            {
                if (db.Learned.Count(o => o.DepartmentId == classId && o.CourseId == courseId) > 0)
                {
                    var learned = db.Learned.First(o => o.DepartmentId == classId && o.CourseId == courseId);
                    learned.State = learned.State == State.启用 ? State.删除 : State.启用;
                    LogOp(learned.State);
                }
                else
                {
                    var learned = new Learned
                    {
                        DepartmentId = classId,
                        CourseId = courseId,
                        State = State.启用
                    };
                    db.Learned.Add(learned);
                    LogOp(OperationType.新增);
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected override string PageRight
        {
            get { return Right; }
        }
    }
}
