using System;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using Homory.Model;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.UI.WebControls;
using EntityFramework.Extensions;

namespace Go
{
    public partial class GoStudent : HomoryCorePageWithGrid
    {
        private const string Right = "Student";

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
                if (!CurrentRights.Contains(HomoryCoreConstant.RightMoveUser))
                {
                    Session[HomoryCoreConstant.SessionStudentsId] = null;
                    grid.MasterTableView.Columns[0].Visible = false;
                }
                LogOp(OperationType.查询);
            }
        }

        protected List<Guid> ActionStudents
        {
            get
            {
                if (Session[HomoryCoreConstant.SessionStudentsId] == null)
                    Session[HomoryCoreConstant.SessionStudentsId] = new List<Guid>();
                return Session[HomoryCoreConstant.SessionStudentsId] as List<Guid>;
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
            grid.Rebind();
            view.Rebind();
        }

        private void LoadInit()
        {
            loading.InitialDelayTime = int.Parse("Busy".FromWebConfig());
         
            BindCombo();
            InitCombo();
            BindTree();
            InitTree();
        }

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
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

        protected string GenerateTreeName(Homory.Model.Department department, int index, int level)
        {
            try
            {
                if (level == 1)
                    return GenGradeName(department);
                var count = department.DepartmentUser.Count(
                    o =>
                        o.Type == DepartmentUserType.班级学生 && o.State == State.启用);
                return count > 0
                    ? string.Format("{0} [{1}]", department.DisplayName, count
                        )
                    : department.DisplayName;
            }
            catch
            {
                return string.Format("{0}-{1}-{2}", department.DisplayName, index, level);
            }
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
            if (tree.Nodes.Count > 0 && tree.Nodes[0].Nodes.Count > 0 && tree.Nodes[0].Nodes[0].Nodes.Count > 0)
                node = tree.Nodes[0].Nodes[0].Nodes[0];
            else if (tree.Nodes.Count > 0 && tree.Nodes[0].Nodes.Count > 0)
                node = tree.Nodes[0].Nodes[0];
            else if (tree.Nodes.Count > 0)
                node = tree.Nodes[0];
            if (node == null) return;
            node.Expanded = true;
            node.ExpandParentNodes();
            node.Selected = true;
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var departmentId = tree.SelectedNode == null || tree.SelectedNode.Level < 2 ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? HomoryContext.Value.ViewStudent.Where(o => o.DepartmentId == departmentId)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.Ordinal)
                    .ToList()
                : null;
            var query = peek.Text;
            peek.Visible = grid.Visible = departmentId.HasValue;
            grid.DataSource = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.UniqueId != null && o.UniqueId.Contains(query)) || o.PinYin.Contains(query) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
        }

        public bool StudentAdd(Entities db, Guid campusId, Guid classId, int ordinal, string name, string account, string passwordInitial, State state, string uniqueId, string idCard, bool? gender, DateTime? birthday, string nationality, string birthplace, string address, string charger, string chargerContact)
        {
            try
            {
                string key, salt;
                var password = HomoryCryptor.Encrypt(passwordInitial, out key, out salt);
                if (HomoryContext.Value.Student.Count(o => o.IDCard == idCard && o.UniqueId == uniqueId && o.User.State == State.启用) == 0)
                {
                    var user = new User
                    {
                        Id = db.GetId(),
                        Account = account,
                        RealName = name,
                        DisplayName = name,
                        Stamp = Guid.NewGuid(),
                        Type = UserType.学生,
                        Password = password,
                        PasswordEx = null,
                        CryptoKey = key,
                        CryptoSalt = salt,
                        Icon = "~/Common/默认/用户.png",
                        State = state,
                        Ordinal = ordinal,
                        Description = null
                    };
                    var userStudent = new Homory.Model.Student
                    {
                        Id = user.Id,
                        UniqueId = uniqueId,
                        Gender = gender,
                        Birthday = birthday,
                        Birthplace = birthplace,
                        Address = address,
                        Nationality = nationality,
                        IDCard = idCard,
                        Charger = charger,
                        ChargerContact = chargerContact
                    };
                    var relation = new DepartmentUser
                    {
                        DepartmentId = classId,
                        UserId = user.Id,
                        TopDepartmentId = campusId,
                        Type = DepartmentUserType.班级学生,
                        State = State.启用,
                        Ordinal = 0,
                        Time = DateTime.Now
                    };
                    db.User.Add(user);
                    db.Student.Add(userStudent);
                    db.DepartmentUser.Add(relation);
                }
                else
                {
                    var nowU = HomoryContext.Value.Student.Single(o => o.IDCard == idCard && o.UniqueId == uniqueId && o.User.State == State.启用);
                    var nowQ = HomoryContext.Value.DepartmentUser.Where(o => o.DepartmentId == classId && o.UserId == nowU.Id && o.Type == DepartmentUserType.班级学生);
                    if (nowQ.Count() == 0)
                    {
                        var relation = new DepartmentUser
                        {
                            DepartmentId = classId,
                            UserId = nowU.Id,
                            TopDepartmentId = campusId,
                            Type = DepartmentUserType.班级学生,
                            State = State.启用,
                            Ordinal = 0,
                            Time = DateTime.Now
                        };
                        db.DepartmentUser.Add(relation);
                    }
                    else
                    {
                        HomoryContext.Value.DepartmentUser.Where(o => o.DepartmentId == classId && o.UserId == nowU.Id && o.Type == DepartmentUserType.班级学生).Update(o => new DepartmentUser { Time = DateTime.Now, State = State.启用 });
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool StudentUpdate(Entities db, Guid id, int ordinal, string name, string account, State state, string uniqueId, string idCard, bool? gender, DateTime? birthday, string nationality, string birthplace, string address, string charger, string chargerContact)
        {
            try
            {
                if (db.User.Count(o => o.Id != id && o.Account == account) > 0)
                    return false;
                var user = db.User.Single(o => o.Id == id);
                user.Account = account;
                user.RealName = name;
                user.DisplayName = name;
                user.Stamp = Guid.NewGuid();
                user.State = state;
                user.Ordinal = ordinal;
                var userStudent = db.Student.Single(o => o.Id == id);
                userStudent.UniqueId = uniqueId;
                userStudent.Gender = gender;
                userStudent.Birthday = birthday;
                userStudent.Birthplace = birthplace;
                userStudent.Address = address;
                userStudent.Nationality = nationality;
                userStudent.IDCard = idCard;
                userStudent.Charger = charger;
                userStudent.ChargerContact = chargerContact;
                var r = db.DepartmentUser.FirstOrDefault(o => o.UserId == id && o.Type == DepartmentUserType.班级学生);
                if (r != null)
                {
                    r.Ordinal = ordinal;
                    r.State = state == State.启用 ? State.启用 : State.历史;
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            var campusId = Guid.Parse(tree.SelectedNode.ParentNode.ParentNode.Value);
            var classId = Guid.Parse(tree.SelectedNode.Value);
            dynamic result = new ExpandoObject();
            result.Ok = true;
            result.Message = string.Empty;
            foreach (var command in e.Commands)
            {
                var values = command.NewValues;
                if (NotSet(values, "RealName") || NotSet(values, "IDCard") || NotSet(values, "Ordinal"))
                    continue;
                var idCard = Get(values, "IDCard", (string)null);
                var name = values["RealName"].ToString();
                if (string.IsNullOrWhiteSpace(idCard))
                {
                    result.Message += string.Format("姓名为\"{0}\"的学生由于身份证号不正确未保存。", name);
                    result.Ok = false;
                    continue;
                }
                var ordinal = Get(values, "Ordinal", 50);
                var state = Get(values, "State", State.启用);
                var gender = Get(values, "Gender", (bool?)null);
                var birthday = Get(values, "Birthday", (DateTime?)null);
                var uniqueId = Get(values, "UniqueId", (string)null);
                var nationality = Get(values, "Nationality", (string)null);
                var birthplace = Get(values, "Birthplace", (string)null);
                var address = Get(values, "Address", (string)null);
                var charger = Get(values, "Charger", (string)null);
                var chargerContact = Get(values, "ChargerContact", (string)null);
                switch (command.Type)
                {
                    case GridBatchEditingCommandType.Insert:
                        {
                            StudentAdd(HomoryContext.Value, campusId, classId, ordinal, name, idCard, idCard.Substring(12),
                                state, uniqueId, idCard,
                                gender, birthday, nationality, birthplace, address, charger, chargerContact);
                            LogOp(OperationType.新增);
                            break;
                        }
                    case GridBatchEditingCommandType.Update:
                        {
                            var id = Get(values, "Id", Guid.Empty);
                            if (!StudentUpdate(HomoryContext.Value, id, ordinal, name, idCard,
                                state, uniqueId, idCard,
                                gender, birthday, nationality, birthplace, address, charger, chargerContact))
                            {
                                result.Ok = false;
                                result.Message = "学生的身份证号不能重复，请检查！";
                            }
                            LogOp(state);
                            break;
                        }
                }
            }
            RebindBatch();
            Notify(panel, result.Ok ? "操作成功" : result.Message, result.Ok ? "success" : "warn");
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

        protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            tree.CollapseAllNodes();
            var node = e.Node;
            while (node.Nodes.Count > 0)
                node = node.Nodes[0];
            node.Selected = true;
            node.ExpandParentNodes();
            node.Expanded = true;
            grid.Rebind();
            view.Rebind();
        }

        protected void peek_Search(object sender, SearchBoxEventArgs e)
        {
            grid.Rebind();
         
        }

        public bool StudentMove(Entities db, Guid id, Guid sourceDepartmentId, Guid targetDepartmentId, Guid targetCampusId)
        {
            try
            {
                var relation = db.DepartmentUser.Where(o => o.UserId == id && o.DepartmentId == sourceDepartmentId && o.Type == DepartmentUserType.班级学生 && o.State == State.启用).Update(o =>
                new DepartmentUser { State = State.历史 });
                var newRelation = new DepartmentUser
                {
                    DepartmentId = targetDepartmentId,
                    UserId = id,
                    TopDepartmentId = targetCampusId,
                    Type = DepartmentUserType.班级学生,
                    State = State.启用,
                    Time = DateTime.Now,
                    Ordinal = 0
                };
                db.DepartmentUser.AddOrUpdate(newRelation);
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

        protected void btnImport_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Go/Import", false);
        }

        protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            try
            {
                view.DataSource = ActionStudents.Join(HomoryContext.Value.ViewStudent.Where(o => o.State < State.删除), o => o, o => o.Id, (a, b) => b).OrderBy(o => o.RealName).ToList();
            }
            catch
            {
                view.DataSource = null;
            }
        }

        protected void actSel_CheckedChanged(object sender, EventArgs e)
        {
            var id = Guid.Parse((sender as CheckBox).Attributes["ItemID"]);
            var done = (sender as CheckBox).Checked;
            if (done && !ActionStudents.Contains(id))
                ActionStudents.Add(id);
            else if (!done && ActionStudents.Contains(id))
                ActionStudents.Remove(id);
            grid.Rebind();
            view.Rebind();
        }

        protected bool ContainsAllActSel()
        {
            var departmentId = tree.SelectedNode == null || tree.SelectedNode.Level < 2 ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? HomoryContext.Value.ViewStudent.Where(o => o.DepartmentId == departmentId)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.Ordinal)
                    .ToList()
                : null;
            var query = peek.Text;
            peek.Visible = grid.Visible = departmentId.HasValue;
            var source = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.UniqueId != null && o.UniqueId.Contains(query)) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            return source.All(o => ActionStudents.Contains(o.Id)) && source != null;
        }

        protected void actSel_CheckedChangedX(object sender, EventArgs e)
        {
            var done = (sender as CheckBox).Checked;
            var departmentId = tree.SelectedNode == null || tree.SelectedNode.Level < 2 ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? HomoryContext.Value.ViewStudent.Where(o => o.DepartmentId == departmentId)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.Ordinal)
                    .ToList()
                : null;
            var query = peek.Text;
            peek.Visible = grid.Visible = departmentId.HasValue;
            var source = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.UniqueId != null && o.UniqueId.Contains(query)) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            if (done)
            {
                ActionStudents.AddRange(source.Select(o => o.Id).Where(o => !ActionStudents.Contains(o)).ToList());
            }
            else
            {
                ActionStudents.RemoveAll(x => source.Select(o => o.Id).Contains(x));
            }
            grid.Rebind();
            view.Rebind();
        }

        protected void actionButton_Click(object sender, EventArgs e)
        {
            var gid = Guid.Parse((sender as RadButton).Value);
            if (ActionStudents.Contains(gid))
            {
                ActionStudents.Remove(gid);
                grid.Rebind();
                view.Rebind();
            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightMoveUser))
            {
                var node = tree.GetAllNodes().SingleOrDefault(o => o.Selected == true && o.Level == 2);
                if (node != null)
                {
                    var targetCampusNode = node.ParentNode;
                    while (targetCampusNode.Level > 0)
                    {
                        targetCampusNode = targetCampusNode.ParentNode;
                    }
                    var targetCampusId = Guid.Parse(targetCampusNode.Value);
                    var targetDepartmentId = Guid.Parse(node.Value);
                    foreach (var asobj in ActionStudents.Join(HomoryContext.Value.ViewStudent.Where(o => o.State < State.删除), o => o, o => o.Id, (a, b) => b).OrderBy(o => o.RealName).ToList())
                    {
                        if (asobj.DepartmentId == targetDepartmentId) continue;
                        StudentMove(HomoryContext.Value, asobj.Id, asobj.DepartmentId.Value, targetDepartmentId, targetCampusId);
                        LogOp(OperationType.编辑);
                    }
                    Session[HomoryCoreConstant.SessionStudentsId] = new List<Guid>();
                    RebindBatch();
                    grid.Rebind();
                    view.Rebind();
                    Notify(panel, "操作成功", "success");
                }
                else
                {
                    Notify(panel, "学生只能在班级间调动", "warn");
                }
            }
            else
            {
                Notify(panel, "无权限调动学生", "warn");
            }
        }
    }
}
