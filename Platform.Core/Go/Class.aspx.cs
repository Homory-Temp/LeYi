using System;
using System.Data.Entity.Migrations;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoClass : HomoryCorePageWithGrid
    {
        private const string Right = "Class";

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
            BindTreeX();
        }

        private void BindCombo()
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.ClassType != ClassType.其他 && o.ClassType != ClassType.无)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            else
            {
                var c = CurrentCampus;
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.ClassType != ClassType.其他 && o.ClassType != ClassType.无 && o.Id == c.Id)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            combo.DataBind();
        }

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
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
                        o => (o.Type == DepartmentType.学校 && o.ClassType != ClassType.无 && o.ClassType != ClassType.其他 && o.State < State.审核 && o.Id == c) || (o.Type == DepartmentType.班级 && o.State < State.删除));
                tree.DataSource =
                    source.Where(o => o.Level != 1)
                        .OrderBy(o => o.State)
                        .ThenBy(o => o.Ordinal)
                        .ThenBy(o => o.Name).ToList()
                        .Union(source.Where(o => o.Level == 1).OrderBy(o => o.State).ThenByDescending(o => o.Ordinal).ThenBy(o => o.Name))
                        .ToList();
            }
            tree.DataBind();
        }

        private void InitTree()
        {
            if (tree.Nodes.Count <= 0) return;
            tree.Nodes[0].Expanded = true;
            tree.Nodes[0].Selected = true;
            AutoGenerateGrades();
            grid.Rebind();
            gridX.Rebind();
        }

        private void BindTreeX()
        {
            var node = tree.SelectedNode;
            while (node.Level > 0)
                node = node.ParentNode;
            var id = Guid.Parse(node.Value);
            var source = HomoryContext.Value.Department.Where(o => (((o.Type == DepartmentType.学校 && o.Id == id) || o.Type == DepartmentType.部门) && o.State < State.审核));
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

        public static string[] J = { "初三", "初二", "初一" };
        public static string[] PJ = { "九年级", "八年级", "七年级", "六年级", "五年级", "四年级", "三年级", "二年级", "一年级" };
        public static string[] P = { "六年级", "五年级", "四年级", "三年级", "二年级", "一年级" };
        public static string[] S = { "高三", "高二", "高一" };
        public static string[] K = { "大班", "中班", "小班" };

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

        protected void AutoGenerateGrades()
        {
            var parentId = tree.SelectedNode == null || tree.SelectedNode.Level != 0 ? (Guid?)(null) : Guid.Parse(tree.SelectedNode.Value);
            if (!parentId.HasValue) return;
            ClassType classType = HomoryContext.Value.Department.First(o => o.Id == parentId.Value).ClassType;
            var startYear = __Year - GradeCount(classType) + 1;
            for (var i = 0; i < GradeCount(classType); i++)
            {
                var j = startYear + i;
                var ding = j + GradeCount(classType);
                if (HomoryContext.Value.Department.Count(o => o.Type == DepartmentType.班级 && o.ParentId == parentId.Value && o.Ordinal == ding) == 0)
                {
                    HomoryContext.Value.Department.Add(new Homory.Model.Department
                    {
                        Id = HomoryContext.Value.GetId(),
                        Name = string.Format("{0}届", ding),
                        DisplayName = string.Format("{0}届", ding),
                        ParentId = parentId,
                        TopId = parentId.Value,
                        Level = 1,
                        Hidden = false,
                        Ordinal = ding,
                        State = State.启用,
                        Type = DepartmentType.班级,
                        ClassType = ClassType.无,
                        BuildType = BuildType.无,
                        Code = string.Empty
                    });
                    LogOp(OperationType.新增);
                }
                else
                {
                    HomoryContext.Value.Department.Where(o => o.Type == DepartmentType.班级 && o.ParentId == parentId.Value && o.Ordinal == ding).Update(o => new Homory.Model.Department { State = State.启用 });
                }
            }
            HomoryContext.Value.Department.Where(o => o.Type == DepartmentType.班级 && o.ParentId == parentId.Value && o.Ordinal <= __Year).Update(
                o => new Homory.Model.Department { State = State.删除 });
            HomoryContext.Value.SaveChanges();
            RebindBatch();
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

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var parentId = tree.SelectedNode == null || tree.SelectedNode.Level != 0 ? (Guid?)(null) : Guid.Parse(tree.SelectedNode.Value);
            grid.DataSource = parentId.HasValue ?
                HomoryContext.Value.Department.Where(o => o.ParentId == parentId.Value && o.Type == DepartmentType.班级 && o.State < State.审核).OrderByDescending(o => o.Ordinal).ToList() : null;
            grid.Visible = parentId.HasValue;
            gridXX.Visible = !grid.Visible && !gridX.Visible;
        }

        protected string GenerateTreeName(Homory.Model.Department department, int index, int level)
        {
            var part1 = level == 1 ? GenGradeName(department) : department.DisplayName;
            var part2 = level > 1
                ? (department.DepartmentUser.Count(o => o.Type == DepartmentUserType.班级班主任 && o.State == State.启用) == 0
                    ? string.Empty
                    : string.Format(" [{0}]", department.DepartmentUser.First(o => o.Type == DepartmentUserType.班级班主任 && o.State == State.启用).User.RealName))
                : string.Empty;
            return string.Format("{0}{1}", part1, part2);
        }

        protected string GenerateGridName(Homory.Model.Department department)
        {
            var part1 = GenGradeName(department);
            return part1;
        }

        protected void gridX_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var parentId = tree.SelectedNode == null || tree.SelectedNode.Level != 1 ? (Guid?)(null) : Guid.Parse(tree.SelectedNode.Value);
            gridX.DataSource = parentId.HasValue
                ? HomoryContext.Value.Department.Where(o => o.State < State.删除 && o.ParentId == parentId.Value && o.Type == DepartmentType.班级)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.Ordinal)
                    .ToList()
                : null;
            gridX.Visible = parentId.HasValue;
            gridXX.Visible = !grid.Visible && !gridX.Visible;
        }

        protected void gridX_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            var parentId = Guid.Parse(tree.SelectedNode.Value);
            var campusId = Guid.Parse(tree.SelectedNode.ParentNode.Value);
            var level = tree.SelectedNode.Level + 1;
            foreach (var command in e.Commands)
            {
                var values = command.NewValues;
                if (NotSet(values, "Ordinal"))
                    continue;
                var ordinal = Get(values, "Ordinal", 1);
                var name = string.Format("（{0}）班", ordinal);
                var state = Get(values, "State", State.启用);
                var displayName = Get(values, "DisplayName", name);
                switch (command.Type)
                {
                    case GridBatchEditingCommandType.Insert:
                        {
                            HomoryContext.Value.Department.Add(new Homory.Model.Department
                            {
                                Id = HomoryContext.Value.GetId(),
                                ParentId = parentId,
                                TopId = campusId,
                                Name = name,
                                DisplayName = displayName,
                                Level = level,
                                Hidden = false,
                                Ordinal = ordinal,
                                State = state,
                                Type = DepartmentType.班级,
                                Code = string.Empty,
                                BuildType = BuildType.无,
                                ClassType = ClassType.无
                            });
                            HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
                        }
                    case GridBatchEditingCommandType.Update:
                        {
                            var id = Get(values, "Id", Guid.Empty);
                            HomoryContext.Value.Department.Where(o => o.Id == id).Update(o => new Homory.Model.Department
                            {
                                Name = name,
                                DisplayName = name,
                                Ordinal = ordinal,
                                State = state,
                            });
                            HomoryContext.Value.SaveChanges();
                            LogOp(state);
                        }
                        break;
                }
            }
            RebindBatch();
            Notify(panel, "操作成功", "success");
        }

        protected string FormatTreeNode(dynamic department, int level)
        {
            return level < 2 ? "coreHidden" : (department.State == State.启用 ? "ui green circle icon" : "ui red circle icon");
        }

        protected void tree_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightMoveDepartment))
            {
                if (e.SourceDragNode == null || e.DestDragNode == null || e.SourceDragNode.ParentNode.Value == e.DestDragNode.Value) return;
                if (e.SourceDragNode.Level != 2 || e.DestDragNode.Level != 1) return;
                var id = Guid.Parse(e.SourceDragNode.Value);
                var parentId = Guid.Parse(e.DestDragNode.Value);
                var level = e.DestDragNode.Level + 1;
                HomoryContext.Value.Department.Where(o => o.Id == id).Update(o => new Homory.Model.Department
                {
                    ParentId = parentId,
                    Level = level
                });
                HomoryContext.Value.SaveChanges();
                LogOp(OperationType.编辑);
                RebindMove(e.SourceDragNode.ParentNode.Value, e.SourceDragNode.Value, e.DestDragNode.Value);
            }
            else
            {
                Notify(panel, "无权限调动班级", "warn");
            }
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

        protected void RebindMove(string sourceParent, string source, string target)
        {
            RebindExpanded();
            tree.GetAllNodes().Single(o => o.Value == sourceParent).Expanded = true;
            tree.GetAllNodes().Single(o => o.Value == target).Expanded = true;
            tree.GetAllNodes().Single(o => o.Value == source).Expanded = true;
            tree.GetAllNodes().Single(o => o.Value == source).Selected = true;
            gridX.Rebind();
        }

        protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            tree.CollapseAllNodes();
            e.Node.Selected = true;
            e.Node.ExpandParentNodes();
            e.Node.Expanded = true;
            AutoGenerateGrades();
            grid.Rebind();
            gridX.Rebind();
            LoadCharging();
            BindTreeX();
            view.Rebind();
        }

        protected void LoadCharging()
        {
            if (tree.SelectedNode.Level != 2) return;
            var classId = Guid.Parse(tree.SelectedNode.Value);
            var du = HomoryContext.Value.DepartmentUser.SingleOrDefault(o => o.DepartmentId == classId && o.State == State.启用 && o.Type == DepartmentUserType.班级班主任);
            if (du == null)
            {
                charging.Visible = false;
                charging.Text = string.Empty;
                charging.CommandArgument = string.Empty;
            }
            else
            {
                charging.Visible = true;
                var user = du.User;
                charging.Text = user.RealName;
                charging.CommandArgument = user.Id.ToString();
            }
        }

        protected void treeX_OnNodeClick(object sender, RadTreeNodeEventArgs e)
        {
            e.Node.Selected = true;
            e.Node.Expanded = true;
            view.Rebind();
        }

        protected void view_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var node = tree.SelectedNode;
            if (node != null)
            {
                var id = Guid.Parse(node.Value);
                var topId = HomoryContext.Value.Department.First(o => o.Id == id).TopId;
                var source =
                    HomoryContext.Value.ViewTeacher.Where(
                        o => o.TopDepartmentId == topId && o.Type == DepartmentUserType.部门主职教师 && o.State == State.启用);
                var text = peek.Text.Trim();
                var ds = source.Where(o => o.RealName.Contains(text) || o.Phone.Contains(text) || o.IDCard.Contains(text) || o.PinYin.Contains(text)).ToList();
                view.DataSource = ds;
            }
        }

        protected void charger_OnClick(object sender, EventArgs e)
        {
            var button = sender as RadButton;
            if (button == null) return;
            var classId = Guid.Parse(tree.SelectedNode.Value);
            var campusNode = tree.SelectedNode;
            while (campusNode.Level > 0)
            {
                campusNode = campusNode.ParentNode;
            }
            var campusId = Guid.Parse(campusNode.Value);
            var userId = Guid.Parse(button.CommandArgument);
            HomoryContext.Value.DepartmentUser.Where(o => o.DepartmentId == classId && o.State < State.删除 && o.Type == DepartmentUserType.班级班主任)
            .Update(o => new DepartmentUser { State = State.历史 });
            var cc = new DepartmentUser
            {
                DepartmentId = classId,
                UserId = userId,
                State = State.启用,
                Ordinal = 0,
                Type = DepartmentUserType.班级班主任,
                Time = DateTime.Now,
                TopDepartmentId = campusId
            };
            HomoryContext.Value.DepartmentUser.AddOrUpdate(cc);
            HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
            HomoryContext = new Lazy<Entities>(() => new Entities());
            RebindBatch();
            LoadCharging();
            view.Rebind();
        }

        protected void charging_OnClick(object sender, EventArgs e)
        {
            var classId = Guid.Parse(tree.SelectedNode.Value);
            HomoryContext.Value.DepartmentUser.Where(o => o.DepartmentId == classId && o.State < State.删除 && o.Type == DepartmentUserType.班级班主任)
                .Update(o => new DepartmentUser { State = State.历史 });
            LogOp(OperationType.编辑);
            HomoryContext.Value.SaveChanges();
            RebindBatch();
            LoadCharging();
            view.Rebind();
        }

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTree();
            InitTree();
            grid.Rebind();
            gridX.Rebind();
            view.Rebind();
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
