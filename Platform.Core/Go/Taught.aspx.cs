using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoTaught : HomoryCorePageWithGrid
    {
        private const string Right = "CourseTaught";

        protected override void CheckRight()
        {
            if (!IsMaster && !CurrentRights.Contains(PageRight))
            {
				Response.Redirect(Application["Core"] + "Go/Home", false);
			}
        }

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
            BindCombo();
            InitCombo();
            BindComboX();
            InitComboX();
            BindTree();
            InitTree();
            BindTreeX();
            InitTreeX();
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

        private void BindComboX()
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                comboX.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.ClassType != ClassType.其他)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            else
            {
                var c = CurrentCampus;
                comboX.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.Id == c.Id && o.ClassType != ClassType.其他)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            comboX.DataBind();
        }

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
        }

        private void InitComboX()
        {
            if (comboX.Items.Count <= 0) return;
            comboX.SelectedIndex = 0;
        }

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTree();
            InitTree();
            view.Rebind();
        }

        protected void comboX_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTreeX();
            InitTreeX();
            viewX.Rebind();
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
                        o => (((o.Type == DepartmentType.学校 && o.Id == c) || o.Type == DepartmentType.部门) && o.State < State.审核));
                tree.DataSource =
                    source.OrderBy(o => o.State)
                        .ThenBy(o => o.Ordinal)
                        .ThenBy(o => o.Name).ToList();
            }
            tree.DataBind();
        }

        private void BindTreeX()
        {
            if (comboX.SelectedIndex < 0)
            {
                treeX.DataSource = null;
            }
            else
            {
                var c = Guid.Parse(comboX.SelectedItem.Value);
                var source =
					HomoryContext.Value.Department.Where(
                        o => (o.Type == DepartmentType.学校 && o.State == State.启用 && o.Id == c) || (o.Type == DepartmentType.班级 && o.State == State.启用 && o.ClassType != ClassType.其他));
                if (CurrentRights.Contains(PageRight))
                {
                    treeX.DataSource =
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
                    treeX.DataSource = list;
                }
            }
            treeX.DataBind();
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

        protected void InitTreeX()
        {
            RadTreeNode node = null;
            if (treeX.Nodes.Count > 0 && treeX.Nodes[0].Nodes.Count > 0 && treeX.Nodes[0].Nodes[0].Nodes.Count > 0)
                node = treeX.Nodes[0].Nodes[0].Nodes[0];
            else if (treeX.Nodes.Count > 0 && treeX.Nodes[0].Nodes.Count > 0)
                node = treeX.Nodes[0].Nodes[0];
            else if (treeX.Nodes.Count > 0)
                node = treeX.Nodes[0];
            if (node == null) return;
            node.Expanded = true;
            node.ExpandParentNodes();
            node.Selected = true;
        }

        protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            tree.CollapseAllNodes();
            var node = e.Node;
            if (node.Level == 0 && node.Nodes.Count > 0)
                node = node.Nodes[0];
            node.Selected = true;
            node.ExpandParentNodes();
            node.Expanded = true;
            view.Rebind();
        }

        protected int GradeCount(ClassType classType)
        {
            switch ((int)classType)
            {
                case 1:
                    return 9;
                case 2:
                    return 3;
                case 3:
                    return 6;
                case 4:
                    return 3;
                case 6:
                    return 3;
                default:
                    return 0;
            }
        }

        protected string GenerateTreeName(Homory.Model.Department department, int index, int level)
        {
            return level == 1 ? GenGradeName(department) : department.DisplayName;
        }

        public static string[] J = { "初三", "初二", "初一" };
        public static string[] PJ = { "九年级", "八年级", "七年级", "六年级", "五年级", "四年级", "三年级", "二年级", "一年级" };
        public static string[] P = { "六年级", "五年级", "四年级", "三年级", "二年级", "一年级" };
        public static string[] K = { "大班", "中班", "小班" };
        public static string[] S = { "高三", "高二", "高一" };

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

        protected void treeX_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            treeX.CollapseAllNodes();
            var node = e.Node;
            while (node.Nodes.Count > 0)
                node = node.Nodes[0];
            node.Selected = true;
            node.ExpandParentNodes();
            node.Expanded = true;
            viewX.Rebind();
        }

        protected string GetIcon(ViewTeacher teacher)
        {
			var count = HomoryContext.Value.Taught.Count(o => o.UserId == teacher.Id && o.State == State.启用);
            return count == 0 ? "badge-default" : "badge-primary";
        }

        protected void view_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var departmentId = tree.SelectedNode == null || tree.SelectedNode.Level == 0 ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
				? HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId && o.Type == DepartmentUserType.部门主职教师)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.PriorOrdinal)
                    .ToList()
                : null;
            var stext = peek.Text.Trim();
            view.DataSource = list == null ? null : list.Where(o => o.Account.Contains(stext) || o.RealName.Contains(stext) || o.PinYin.Contains(stext) || o.Phone.Contains(stext) || o.IDCard.Contains(stext)).ToList();
        }

        protected void viewX_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var classId = treeX.SelectedNode == null || treeX.SelectedNode.Level != 2
                ? (Guid?)null
                : Guid.Parse(treeX.SelectedNode.Value);
            if (classId.HasValue)
            {
                var id = classId.Value;
                var pid = Guid.Parse(treeX.SelectedNode.ParentNode.Value);
                viewX.DataSource = HomoryContext.Value.ViewLearned.Where(o => o.DepartmentId == id || o.DepartmentId == pid).OrderBy(o => o.Ordinal).ToList();
                viewX.Visible = true;
            }
            else
            {
                viewX.DataSource = null;
                viewX.Visible = false;
            }
        }

        public bool CourseTaught(Entities db, Guid classId, Guid courseId, Guid userId, State state)
        {
            try
            {
                CourseUntaught(db, classId, courseId);
                var taught = new Taught
                {
                    DepartmentId = classId,
                    CourseId = courseId,
                    UserId = userId,
                    Time = DateTime.Now,
                    State = state
                };
                db.Taught.AddOrUpdate(taught);
                LogOp(OperationType.新增);
                db.SaveChanges();
                view.Rebind();
                viewX.Rebind();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CourseUntaught(Entities db, Guid classId, Guid courseId)
        {
            try
            {
                db.Taught.Where(o => o.DepartmentId == classId && o.CourseId == courseId && o.State == State.启用).Update(o => new Taught { State = State.历史 });
                LogOp(OperationType.删除);
                db.SaveChanges();
                view.Rebind();
                viewX.Rebind();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected string GetTeacher(Guid departmentId, Guid courseId)
        {
			var teacher = HomoryContext.Value.ViewTaught.FirstOrDefault(o => o.DepartmentId == departmentId && o.CourseId == courseId && o.State == State.启用);
            return teacher == null ? null : string.Format("[{0}]", teacher.UserName);
        }

        protected bool HasTeacher(Guid departmentId, Guid courseId)
        {
			var teacher = HomoryContext.Value.ViewTaught.FirstOrDefault(o => o.DepartmentId == departmentId && o.CourseId == courseId && o.State == State.启用);
            return teacher != null;
        }

        protected void view_OnItemDrop(object sender, RadListViewItemDragDropEventArgs e)
        {
            try
            {
                var target = e.DestinationHtmlElement;
                var classId = Guid.Parse(treeX.SelectedNode.Value);
                var courseId = Guid.Parse(target);
                var userId = Guid.Parse(e.DraggedItem.GetDataKeyValue("Id").ToString());
				CourseTaught(HomoryContext.Value, classId, courseId, userId, State.启用);
                Notify(panel, "操作成功", "success");
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        protected void viewX_OnItemDrop(object sender, RadListViewItemDragDropEventArgs e)
        {
            try
            {
                var target = e.DestinationHtmlElement;
                var classId = Guid.Parse(treeX.SelectedNode.Value);
                var userId = Guid.Parse(target);
                var courseId = Guid.Parse(e.DraggedItem.GetDataKeyValue("CourseId").ToString());
				CourseTaught(HomoryContext.Value, classId, courseId, userId, State.启用);
                Notify(panel, "操作成功", "success");
            }
// ReSharper disable EmptyGeneralCatchClause
            catch
// ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        protected void toDel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            var classId = Guid.Parse(treeX.SelectedNode.Value);
            var courseId = Guid.Parse(((ImageButton) sender).CommandArgument);
			CourseUntaught(HomoryContext.Value, classId, courseId);
            Notify(panel, "操作成功", "success");
        }

        protected override string PageRight
        {
            get { return Right; }
        }

        protected void peek_Search(object sender, SearchBoxEventArgs e)
        {
            view.Rebind();
        }
    }
}
