using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoTeacher : HomoryCorePageWithGrid
    {
        private const string Right = "Teacher";

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
                    Session[HomoryCoreConstant.SessionTeachersId] = null;
                    grid.MasterTableView.Columns[0].Visible = false;
                }
                LogOp(OperationType.查询);
            }
        }

        protected bool CanMove
        {
            get
            {
                return CurrentRights.Contains(HomoryCoreConstant.RightMoveUser);
            }
        }

        protected string ForceTreeName(Homory.Model.Department department)
        {
            var count = HomoryContext.Value.DepartmentUser.Count(
                o =>
                    (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.部门兼职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.State == State.启用 && o.DepartmentId == department.Id && o.User.State < State.审核);
            return count > 0
                ? string.Format("{0} [{1}]", department.DisplayName, count
                    )
                : department.DisplayName;
        }

        protected List<Guid> ActionTeachers
        {
            get
            {
                if (Session[HomoryCoreConstant.SessionTeachersId] == null)
                    Session[HomoryCoreConstant.SessionTeachersId] = new List<Guid>();
                return Session[HomoryCoreConstant.SessionTeachersId] as List<Guid>;
            }
        }

        private void BindCombo()
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            else
            {
                var c = CurrentCampus;
                combo.DataSource = HomoryContext.Value.Department.Where(o => (o.Type == DepartmentType.学校 && o.State < State.审核 && o.Id == c.Id)).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ThenBy(o => o.Name).ToList();
            }
            combo.DataBind();
        }

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindTree();
            InitTree();
            grid.Rebind();
            gridX.Rebind();
            grid_b.Rebind();
            grid_view.Rebind();
            view.Rebind();
        }

        private void LoadInit()
        {
            BindCombo();
            InitCombo();
            BindTree();
            InitTree();
            BindIMP();
        }

        private void BindIMP()
        {
            btnImport.Visible = tree.SelectedNode != null && tree.SelectedNode.Level > 0;
        }

        private void InitCombo()
        {
            if (combo.Items.Count <= 0) return;
            combo.SelectedIndex = 0;
        }

        protected string GenerateTreeName(Homory.Model.Department department, int index, int level)
        {
            try
            {
                if (level == 1)
                    return HomoryCoreConstant.GradeNames[index];
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
                                        o => (((o.Type == DepartmentType.学校 && o.Id == c) || o.Type == DepartmentType.部门) && o.State < State.审核));
                tree.DataSource =
                    source.OrderBy(o => o.State)
                        .ThenBy(o => o.Ordinal)
                        .ThenBy(o => o.Name).ToList();
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

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var departmentId = tree.SelectedNode == null ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? (tree.SelectedNode.Level == 0 ? HomoryContext.Value.ViewTeacher.Where(o => o.TopDepartmentId == departmentId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师))
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.PriorOrdinal)
                    .ToList() : HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师))
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.PriorOrdinal)
                    .ToList())
                : null;
            var query = peek.Text;
            peek.Visible = MainPanel.Visible = departmentId.HasValue;
            if (tree.SelectedNode != null)
            {
                peek.Visible = MainPanel.Visible = true;
            }
            grid.DataSource = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) || o.PinYin.Contains(query) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            grid.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = tree.SelectedNode != null && tree.SelectedNode.Level > 0;
        }

        protected void gridX_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var departmentId = tree.SelectedNode == null ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId && o.Type == DepartmentUserType.部门兼职教师)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.MinorOrdinal)
                    .ToList()
                : null;
            var query = peek.Text;
            peek.Visible = MainPanel.Visible = departmentId.HasValue;
            if (tree.SelectedNode != null)
            {
                peek.Visible = MainPanel.Visible = true;
            }
            gridX.DataSource = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) || o.PinYin.Contains(query) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            grid.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = tree.SelectedNode != null && tree.SelectedNode.Level > 0;
        }

        protected void grid_b_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var departmentId = tree.SelectedNode == null ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId && (o.Type == DepartmentUserType.借调前部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师))
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.MinorOrdinal)
                    .ToList()
                : null;
            var query = peek.Text;
            peek.Visible = MainPanel.Visible = departmentId.HasValue;
            if (tree.SelectedNode != null)
            {
                peek.Visible = MainPanel.Visible = true;
            }
            grid_b.DataSource = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) || o.PinYin.Contains(query) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            grid.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = tree.SelectedNode != null && tree.SelectedNode.Level > 0;
        }

        protected void grid_view_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var departmentId = tree.SelectedNode == null ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId && o.Type == DepartmentUserType.教师可查看部门)
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.MinorOrdinal)
                    .ToList()
                : null;
            var query = peek.Text;
            peek.Visible = MainPanel.Visible = departmentId.HasValue;
            if (tree.SelectedNode != null)
            {
                peek.Visible = MainPanel.Visible = true;
            }
            grid_view.DataSource = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) || o.PinYin.Contains(query) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            grid.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = tree.SelectedNode != null && tree.SelectedNode.Level > 0;
        }

        public bool TeacherMove(Entities db, Guid id, Guid sourceDepartmentId, Guid targetDepartmentId, Guid targetCampusId)
        {
            try
            {
                var relation = db.DepartmentUser.Where(o => o.UserId == id && o.State < State.删除 && o.DepartmentId == sourceDepartmentId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.借调前部门主职教师)).Update(o =>
                new DepartmentUser { State = State.删除 });
                if (!PermanentParttime)
                {
                    db.DepartmentUser.Where(o => o.UserId == id && (o.Type == DepartmentUserType.教师可查看部门 || o.Type == DepartmentUserType.部门兼职教师) && o.State < State.删除).Update(
                        o => new DepartmentUser { State = State.删除 });
                }
                var newRelation = new DepartmentUser
                {
                    DepartmentId = targetDepartmentId,
                    UserId = id,
                    TopDepartmentId = targetCampusId,
                    Type = DepartmentUserType.部门主职教师,
                    State = State.启用,
                    Ordinal = 0,
                    Time = DateTime.Now
                };
                db.DepartmentUser.AddOrUpdate(newRelation);
                db.SaveChanges();
                try { UserHelper.ResetUserFulltime(id.ToString().ToUpper(), targetDepartmentId.ToString().ToUpper(), 0, "1000", PermanentParttime ? 0 : 1); } catch { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TeacherParttime(Entities db, Guid id, Guid targetDepartmentId, Guid targetCampusId)
        {
            try
            {
                var newRelation = new DepartmentUser
                {
                    DepartmentId = targetDepartmentId,
                    UserId = id,
                    TopDepartmentId = targetCampusId,
                    Type = DepartmentUserType.部门兼职教师,
                    State = State.启用,
                    Ordinal = 50,
                    Time = DateTime.Now
                };
                db.DepartmentUser.AddOrUpdate(newRelation);
                db.SaveChanges();
                try { UserHelper.InsertUserParttime(id.ToString().ToUpper(), targetDepartmentId.ToString().ToUpper(), 50); } catch { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool? _permanentParttime;

        protected bool PermanentParttime
        {
            get
            {
                if (!_permanentParttime.HasValue)
                {
                    _permanentParttime = bool.Parse(HomoryContext.Value.ApplicationPolicy.Single(o => o.Name == "PermanentParttime" && o.ApplicationId == Guid.Empty).Value);
                }
                return _permanentParttime.Value;
            }
        }

        public bool TeacherAdd(Entities db, Guid campusId, Guid departmentId, int ordinal, string name, string phone, string passwordInitial, State state, string email, string idCard, bool? gender, DateTime? birthday, string nationality, string birthplace, string address, string account, bool? perstaff, bool sync, out Guid gid)
        {
            gid = db.GetId();
            try
            {
                string key, salt;
                var password = HomoryCryptor.Encrypt(passwordInitial, out key, out salt);
                var user = new User
                {
                    Id = gid,
                    Account = account,
                    RealName = name,
                    DisplayName = name,
                    Stamp = Guid.NewGuid(),
                    Type = UserType.教师,
                    Password = password,
                    PasswordEx = null,
                    CryptoKey = key,
                    CryptoSalt = salt,
                    Icon = "~/Common/默认/用户.png",
                    State = state,
                    Ordinal = ordinal,
                    Description = null
                };
                var userTeacher = new Homory.Model.Teacher
                {
                    Id = user.Id,
                    Phone = phone,
                    Email = email,
                    Gender = gender,
                    Birthday = birthday,
                    Birthplace = birthplace,
                    Address = address,
                    Nationality = nationality,
                    IDCard = idCard,
                    PerStaff = perstaff ?? true,
                    Sync = sync
                };
                var relation = new DepartmentUser
                {
                    DepartmentId = departmentId,
                    UserId = user.Id,
                    TopDepartmentId = campusId,
                    Type = DepartmentUserType.部门主职教师,
                    State = State.启用,
                    Ordinal = 0,
                    Time = DateTime.Now
                };
                db.User.Add(user);
                db.Teacher.Add(userTeacher);
                db.DepartmentUser.Add(relation);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TeacherUpdate(Entities db, Guid id, int ordinal, string name, string phone, State state, string email, string idCard, bool? gender, DateTime? birthday, string nationality, string birthplace, string address, string account, bool? perstaff, bool sync)
        {
            try
            {
                var user = db.User.First(o => o.Id == id);
                user.Account = account;
                user.RealName = name;
                user.DisplayName = name;
                user.Stamp = Guid.NewGuid();
                user.Ordinal = ordinal;
                if (user.State != State.内置)
                    user.State = state;
                var userTeacher = db.Teacher.First(o => o.Id == id);
                userTeacher.Email = email;
                userTeacher.Phone = phone;
                userTeacher.Gender = gender;
                userTeacher.Birthday = birthday;
                userTeacher.Birthplace = birthplace;
                userTeacher.Address = address;
                userTeacher.Nationality = nationality;
                userTeacher.IDCard = idCard;
                if (perstaff.HasValue)
                {
                    userTeacher.PerStaff = perstaff.Value;
                }
                userTeacher.Sync = sync;
                var r = db.DepartmentUser.FirstOrDefault(o => o.UserId == id && (o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.部门主职教师) && o.State < State.审核);
                if (r == null)
                {
                    r = db.DepartmentUser.FirstOrDefault(o => o.UserId == id && (o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.部门主职教师) && o.State < State.删除);
                    if (r == null)
                    {
                        r = db.DepartmentUser.FirstOrDefault(o => o.UserId == id && (o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.部门主职教师));
                    }
                }
                if (r != null)
                {
                    r.Ordinal = ordinal;
                    if (user.State != State.内置)
                        r.State = state;
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TeacherVisit(Entities db, Guid id, Guid targetDepartmentId, Guid targetCampusId)
        {
            try
            {
                var newRelation = new DepartmentUser
                {
                    DepartmentId = targetDepartmentId,
                    UserId = id,
                    TopDepartmentId = targetCampusId,
                    Type = DepartmentUserType.教师可查看部门,
                    State = State.启用,
                    Ordinal = 50,
                    Time = DateTime.Now
                };
                db.DepartmentUser.AddOrUpdate(newRelation);
                db.SaveChanges();
                try { UserHelper.InsertUserVisitable(id.ToString().ToUpper(), targetCampusId.ToString().ToUpper(), targetDepartmentId.ToString().ToUpper()); } catch { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected string GetInfo(ViewTeacher v)
        {
            var id = v.Id;
            var type = v.Type == DepartmentUserType.借调前部门主职教师 ? DepartmentUserType.借调后部门主职教师 : DepartmentUserType.借调前部门主职教师;
            var other = HomoryContext.Value.DepartmentUser.FirstOrDefault(o => o.UserId == id && o.Type == type && o.State == State.启用);
            if (other == null) return "未查询到相关借调信息";
            var departmentId = other.DepartmentId;
            var d = HomoryContext.Value.Department.First(o => o.Id == departmentId);
            var t = d.DepartmentRoot.DisplayName;
            return string.Format(v.Type == DepartmentUserType.借调前部门主职教师 ? "借出至“{0}-{1}”" : "由“{0}-{1}”借入", t, d.DisplayName);
        }

        protected void gridX_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightMoveUser))
            {
                var campusNode = tree.SelectedNode;
                while (campusNode.Level > 0)
                {
                    campusNode = campusNode.ParentNode;
                }
                var departmentId = Guid.Parse(tree.SelectedNode.Value);
                foreach (var command in e.Commands)
                {
                    var values = command.NewValues;
                    var ordinal = Get(values, "MinorOrdinal", 99);
                    var state = Get(values, "State", State.启用);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Update:
                            {
                                var id = Get(values, "Id", Guid.Empty);
                                HomoryContext.Value.DepartmentUser.Where(o => o.UserId == id && o.DepartmentId == departmentId && o.Type == DepartmentUserType.部门兼职教师).Update(
                                    o => new DepartmentUser { Ordinal = ordinal, State = state });
                                HomoryContext.Value.SaveChanges();
                                LogOp(state);
                                try { UserHelper.UpdateUserParttime(id.ToString().ToUpper(), departmentId.ToString().ToUpper(), ordinal, state); } catch { }
                                break;
                            }
                    }
                }
                RebindBatch();
                grid.Rebind();
                gridX.Rebind();
                grid_b.Rebind();
                grid_view.Rebind();
                view.Rebind();
                Notify(panel, "操作成功", "success");
            }
            else
            {
                Notify(panel, "无权限删除教师兼职", "warn");
            }
        }

        public bool TeacherBorrow(Entities db, Guid id, Guid sourceDepartmentId, Guid targetDepartmentId, Guid targetCampusId)
        {
            try
            {
                HomoryContext.Value.DepartmentUser.Where(o => o.UserId == id && o.State < State.删除 && (o.Type == DepartmentUserType.借调前部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.部门兼职教师 || o.Type == DepartmentUserType.教师可查看部门 || o.Type == DepartmentUserType.部门主职教师)).Update(o => new DepartmentUser { State = State.删除 });
                var topId = HomoryContext.Value.Department.First(o => o.Id == sourceDepartmentId).DepartmentRoot.Id;
                var newRelationX = new DepartmentUser
                {
                    DepartmentId = sourceDepartmentId,
                    UserId = id,
                    TopDepartmentId = topId,
                    Type = DepartmentUserType.借调前部门主职教师,
                    State = State.启用,
                    Ordinal = 0,
                    Time = DateTime.Now
                };
                var newRelation = new DepartmentUser
                {
                    DepartmentId = targetDepartmentId,
                    UserId = id,
                    TopDepartmentId = targetCampusId,
                    Type = DepartmentUserType.借调后部门主职教师,
                    State = State.启用,
                    Ordinal = 0,
                    Time = DateTime.Now
                };
                HomoryContext.Value.DepartmentUser.AddOrUpdate(newRelationX);
                HomoryContext.Value.DepartmentUser.AddOrUpdate(newRelation);
                HomoryContext.Value.SaveChanges();
                try { UserHelper.ResetUserFulltime(id.ToString().ToUpper(), targetDepartmentId.ToString().ToUpper(), 0, "1000", PermanentParttime ? 0 : 1); } catch { }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void grid_b_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightMoveUser))
            {
                var campusNode = tree.SelectedNode;
                while (campusNode.Level > 0)
                {
                    campusNode = campusNode.ParentNode;
                }
                var departmentId = Guid.Parse(tree.SelectedNode.Value);
                foreach (var command in e.Commands)
                {
                    var values = command.NewValues;
                    var state = Get(values, "QuitBorrow", State.启用);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Update:
                            {
                                var id = Get(values, "Id", Guid.Empty);
                                var found = HomoryContext.Value.DepartmentUser.FirstOrDefault(o => o.UserId == id && o.Type == DepartmentUserType.借调前部门主职教师 && o.State == State.启用);
                                var du = new DepartmentUser { DepartmentId = found.DepartmentId, Ordinal = found.Ordinal, State = State.启用, TopDepartmentId = found.TopDepartmentId, Type = DepartmentUserType.部门主职教师, UserId = id,
                                    Time = DateTime.Now
                                };
                                HomoryContext.Value.DepartmentUser.Where(o => o.UserId == id && o.State < State.删除 && (o.Type == DepartmentUserType.借调前部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.教师可查看部门 || o.Type == DepartmentUserType.部门兼职教师 || o.Type == DepartmentUserType.部门主职教师)).Update(o => new DepartmentUser { State = State.删除 });
                                HomoryContext.Value.DepartmentUser.AddOrUpdate(du);
                                HomoryContext.Value.SaveChanges();
                                
                                if (state == State.删除)
                                {
                                    try { UserHelper.ResetUserFulltime(id.ToString().ToUpper(), du.DepartmentId.ToString().ToUpper(), 0, "1000", PermanentParttime ? 0 : 1); } catch { }
                                }
                                break;
                            }
                    }
                }
                RebindBatch();
                grid.Rebind();
                gridX.Rebind();
                grid_b.Rebind();
                grid_view.Rebind();
                view.Rebind();
                Notify(panel, "操作成功", "success");
            }
            else
            {
                Notify(panel, "无权限借调教师", "warn");
            }
        }

        protected void grid_view_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                var campusNode = tree.SelectedNode;
                while (campusNode.Level > 0)
                {
                    campusNode = campusNode.ParentNode;
                }
                var departmentId = Guid.Parse(tree.SelectedNode.Value);
                foreach (var command in e.Commands)
                {
                    var values = command.NewValues;
                    var ordinal = Get(values, "MinorOrdinal", 99);
                    var state = Get(values, "State", State.启用);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Update:
                            {
                                var id = Get(values, "Id", Guid.Empty);
                                HomoryContext.Value.DepartmentUser.Where(o => o.UserId == id && o.DepartmentId == departmentId).Update(
                                    o => new DepartmentUser { Ordinal = ordinal, State = state });
                                HomoryContext.Value.SaveChanges();
                                LogOp(state);
                                if (state > State.启用)
                                {
                                    try { UserHelper.UpdateUserVisitable(id.ToString().ToUpper(), departmentId.ToString().ToUpper()); } catch { }
                                }
                                break;
                            }
                    }
                }
                Notify(panel, "操作成功", "success");
                RebindBatch();
                grid.Rebind();
                gridX.Rebind();
                grid_b.Rebind();
                grid_view.Rebind();
                view.Rebind();
            }
            else
            {
                Notify(panel, "无权限设定教师可访问部门", "warn");
            }
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            try
            {
                var campusNode = tree.SelectedNode;
                while (campusNode.Level > 0)
                {
                    campusNode = campusNode.ParentNode;
                }
                var campusId = Guid.Parse(campusNode.Value);
                var departmentId = Guid.Parse(tree.SelectedNode.Value);
                dynamic result = new ExpandoObject();
                result.Ok = true;
                result.Message = string.Empty;
                foreach (var command in e.Commands)
                {
                    var values = command.NewValues;
                    var state = Get(values, "State", State.启用);
                    if (NotSet(values, "RealName") || (NotSet(values, "Phone") && state < State.停用) || NotSet(values, "Account"))
                        continue;
                    var name = values["RealName"].ToString();
                    var account = values["Account"].ToString();
                    var phone = values["Phone"].ToString();
                    var mRegex = HomoryContext.Value.ApplicationPolicy.Single(o => o.Name == "UserPhoneRegex" && o.ApplicationId == Guid.Empty).Value;
                    if (!new Regex(mRegex).IsMatch(phone) && state < State.停用)
                    {
                        result.Message += string.Format("姓名为\"{0}\"的教师由于手机号码不正确未保存。", name);
                        result.Ok = false;
                        continue;
                    }
                    var ordinal = Get(values, "PriorOrdinal", 50);
                    var email = Get(values, "Email", (string)null);
                    var idCard = Get(values, "IDCard", (string)null);
                    if (!string.IsNullOrWhiteSpace(idCard) && idCard.Length != 18)
                    {
                        idCard = null;
                    }
                    var gender = Get(values, "Gender", (bool?)null);
                    var sync_ex = Get(values, "Sync", false);
                    var sync = sync_ex.HasValue ? sync_ex.Value : false;
                    var birthday = Get(values, "Birthday", (DateTime?)null);
                    var nationality = Get(values, "Nationality", (string)null);
                    var birthplace = Get(values, "Birthplace", (string)null);
                    var address = Get(values, "Address", (string)null);
                    var perstaff = Get(values, "PerStaff", (bool?)null);
                    switch (command.Type)
                    {
                        case GridBatchEditingCommandType.Insert:
                            {
                                Guid gid;
                                TeacherAdd(HomoryContext.Value, campusId, departmentId, ordinal, name, phone, "000000",
                                    state, email, idCard, gender, birthday, nationality, birthplace, address, account, perstaff, sync, out gid);
                                try { UserHelper.InsertUser(name, "C984AED014AEC7623A54F0591DA07A85FD4B762D", sync, state, account, departmentId.ToString().ToUpper(), gid.ToString().ToUpper(), phone, idCard, ordinal, 0, "1000"); } catch { Notify(panel, "办公平台教师新增失败", "warn"); }
                                LogOp(OperationType.新增);
                                break;
                            }
                        case GridBatchEditingCommandType.Update:
                            {
                                var id = Get(values, "Id", Guid.Empty);
                                TeacherUpdate(HomoryContext.Value, id, ordinal, name, phone,
                                    state, email, idCard, gender, birthday, nationality, birthplace, address, account, perstaff, sync);
                                try { UserHelper.UpdateUser(name, sync, state, account, id.ToString().ToUpper(), phone, idCard, ordinal); } catch { Notify(panel, "办公平台教师更新失败", "warn"); }
                                LogOp(state);
                                break;
                            }
                    }
                }
                RebindBatch();
                grid.Rebind();
                gridX.Rebind();
                grid_b.Rebind();
                grid_view.Rebind();
                view.Rebind();
                Notify(panel, result.Ok ? "操作成功" : result.Message, result.Ok ? "success" : "warn");
            }
            catch
            {
                Notify(panel, false ? "操作失败" : "教师数据保存失败！", "warn");
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

        protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            tree.CollapseAllNodes();
            var node = e.Node;
            if (node.Level == 0 && node.Nodes.Count > 0)
                node = node.Nodes[0];
            node.Selected = true;
            node.ExpandParentNodes();
            node.Expanded = true;
            grid.Rebind();
            gridX.Rebind();
            grid_b.Rebind();
            grid_view.Rebind();
            view.Rebind();
        }

        protected void peek_Search(object sender, SearchBoxEventArgs e)
        {
            grid.Rebind();
            gridX.Rebind();
            grid_b.Rebind();
            grid_view.Rebind();
         
        }

        protected override string PageRight
        {
            get { return Right; }
        }

        protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            try
            {
                view.DataSource = ActionTeachers.Join(HomoryContext.Value.ViewTeacher.Where(o => o.State < State.删除 && (o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.部门主职教师)), o => o, o => o.Id, (a, b) => b).OrderBy(o => o.RealName).ToList();
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
            if (done && !ActionTeachers.Contains(id))
                ActionTeachers.Add(id);
            else if (!done && ActionTeachers.Contains(id))
                ActionTeachers.Remove(id);
            grid.Rebind();
            view.Rebind();
        }

        protected bool ContainsAllActSel()
        {
            var departmentId = tree.SelectedNode == null ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? (tree.SelectedNode.Level == 0 ? HomoryContext.Value.ViewTeacher.Where(o => o.TopDepartmentId == departmentId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师))
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.PriorOrdinal)
                    .ToList() : HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师))
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.PriorOrdinal)
                    .ToList())
                : null;
            var query = peek.Text;
            peek.Visible = MainPanel.Visible = departmentId.HasValue;
            if (tree.SelectedNode != null)
            {
                peek.Visible = MainPanel.Visible = true;
            }
            var source = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            grid.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = tree.SelectedNode != null && tree.SelectedNode.Level > 0;
            return source.All(o => ActionTeachers.Contains(o.Id)) && source != null;
        }

        protected void actSel_CheckedChangedX(object sender, EventArgs e)
        {
            var done = (sender as CheckBox).Checked;
            var departmentId = tree.SelectedNode == null ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            var list = departmentId.HasValue
                ? (tree.SelectedNode.Level == 0 ? HomoryContext.Value.ViewTeacher.Where(o => o.TopDepartmentId == departmentId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师))
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.PriorOrdinal)
                    .ToList() : HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == departmentId && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师))
                    .OrderBy(o => o.State)
                    .ThenBy(o => o.PriorOrdinal)
                    .ToList())
                : null;
            var query = peek.Text;
            peek.Visible = MainPanel.Visible = departmentId.HasValue;
            if (tree.SelectedNode != null)
            {
                peek.Visible = MainPanel.Visible = true;
            }
            var source = departmentId.HasValue
                ? list.Where(
                    o =>
                        o.Account.Contains(query) || o.RealName.Contains(query) || (o.Email != null && o.Email.Contains(query)) ||
                        (o.IDCard != null && o.IDCard.Contains(query))).ToList()
                : null;
            if (done)
            {
                ActionTeachers.AddRange(source.Select(o => o.Id).Where(o => !ActionTeachers.Contains(o)).ToList());
            }
            else
            {
                ActionTeachers.RemoveAll(x => source.Select(o => o.Id).Contains(x));
            }
            grid.Rebind();
            view.Rebind();
        }

        protected void actionButton_Click(object sender, EventArgs e)
        {
            var gid = Guid.Parse((sender as RadButton).Value);
            if (ActionTeachers.Contains(gid))
            {
                ActionTeachers.Remove(gid);
                grid.Rebind();
                view.Rebind();
            }
        }

        protected void btnM_Click(object sender, EventArgs e)
        {
            var node = tree.GetAllNodes().SingleOrDefault(o => o.Selected == true && o.Level > 0);
            if (node != null)
            {
                var targetCampusNode = node.ParentNode;
                while (targetCampusNode.Level > 0)
                {
                    targetCampusNode = targetCampusNode.ParentNode;
                }
                var targetCampusId = Guid.Parse(targetCampusNode.Value);
                var targetDepartmentId = Guid.Parse(node.Value);
                foreach (var asobj in ActionTeachers.Join(HomoryContext.Value.ViewTeacher.Where(o => o.State < State.删除 && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师)), o => o, o => o.Id, (a, b) => b).OrderBy(o => o.RealName).ToList())
                {
                    if (asobj.DepartmentId == targetDepartmentId) continue;
                    TeacherMove(HomoryContext.Value, asobj.Id, asobj.DepartmentId, targetDepartmentId, targetCampusId);
                    LogOp(OperationType.编辑);
                }
                Session[HomoryCoreConstant.SessionTeachersId] = new List<Guid>();
                RebindBatch();
                grid.Rebind();
                gridX.Rebind();
                grid_b.Rebind();
                grid_view.Rebind();
                view.Rebind();
                Notify(panel, "操作成功", "success");
            }
            else
            {
                Notify(panel, "教师只能在部门间调动", "warn");
            }
        }

        protected void btnP_Click(object sender, EventArgs e)
        {
            var node = tree.GetAllNodes().SingleOrDefault(o => o.Selected == true && o.Level > 0);
            if (node != null)
            {
                var targetCampusNode = node.ParentNode;
                while (targetCampusNode.Level > 0)
                {
                    targetCampusNode = targetCampusNode.ParentNode;
                }
                var targetCampusId = Guid.Parse(targetCampusNode.Value);
                var targetDepartmentId = Guid.Parse(node.Value);
                foreach (var asobj in ActionTeachers)
                {
                    TeacherParttime(HomoryContext.Value, asobj, targetDepartmentId, targetCampusId);
                    LogOp(OperationType.新增);
                }
                Session[HomoryCoreConstant.SessionTeachersId] = new List<Guid>();
                RebindBatch();
                grid.Rebind();
                gridX.Rebind();
                grid_b.Rebind();
                grid_view.Rebind();
                view.Rebind();
                Notify(panel, "操作成功", "success");
            }
            else
            {
                Notify(panel, "教师只能在部门间兼职", "warn");
            }
        }

        protected void btnB_Click(object sender, EventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightMoveUser))
            {
                var node = tree.GetAllNodes().SingleOrDefault(o => o.Selected == true && o.Level > 0);
                if (node != null)
                {
                    var targetCampusNode = node.ParentNode;
                    while (targetCampusNode.Level > 0)
                    {
                        targetCampusNode = targetCampusNode.ParentNode;
                    }
                    var targetCampusId = Guid.Parse(targetCampusNode.Value);
                    var targetDepartmentId = Guid.Parse(node.Value);
                    foreach (var asobj in ActionTeachers.Join(HomoryContext.Value.ViewTeacher.Where(o => o.State < State.删除 && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师)), o => o, o => o.Id, (a, b) => b).OrderBy(o => o.RealName).ToList())
                    {
                        if (asobj.DepartmentId == targetDepartmentId) continue;
                        TeacherBorrow(HomoryContext.Value, asobj.Id, asobj.DepartmentId, targetDepartmentId, targetCampusId);
                        LogOp(OperationType.编辑);
                    }
                    Session[HomoryCoreConstant.SessionTeachersId] = new List<Guid>();
                    RebindBatch();
                    grid.Rebind();
                    gridX.Rebind();
                    grid_b.Rebind();
                    grid_view.Rebind();
                    view.Rebind();
                    Notify(panel, "操作成功", "success");
                }
                else
                {
                    Notify(panel, "教师只能在部门间借调", "warn");
                }
            }
            else
            {
                Notify(panel, "无权限借调教师", "warn");
            }
        }

        protected void btnV_Click(object sender, EventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightMoveUser))
            {
                var node = tree.GetAllNodes().SingleOrDefault(o => o.Selected == true && o.Level > 0);
                if (node != null)
                {
                    var targetCampusNode = node.ParentNode;
                    while (targetCampusNode.Level > 0)
                    {
                        targetCampusNode = targetCampusNode.ParentNode;
                    }
                    var targetCampusId = Guid.Parse(targetCampusNode.Value);
                    var targetDepartmentId = Guid.Parse(node.Value);
                    foreach (var asobj in ActionTeachers)
                    {
                        TeacherVisit(HomoryContext.Value, asobj, targetDepartmentId, targetCampusId);
                        LogOp(OperationType.新增);
                    }
                    Session[HomoryCoreConstant.SessionTeachersId] = new List<Guid>();
                    RebindBatch();
                    grid.Rebind();
                    gridX.Rebind();
                    grid_b.Rebind();
                    grid_view.Rebind();
                    view.Rebind();
                    Notify(panel, "操作成功", "success");
                }
                else
                {
                    Notify(panel, "教师只能设定部门可访", "warn");
                }
            }
            else
            {
                Notify(panel, "无权限设定教师可访", "warn");
            }
        }

        protected void btnR_Click(object sender, EventArgs e)
        {
            foreach (var asobj in ActionTeachers)
            {
                string k, v;
                var u = HomoryContext.Value.User.First(o => o.Id == asobj);
                u.Password = HomoryCryptor.Encrypt("000000", out k, out v);
                try { UserHelper.UpdateUserPassword(asobj.ToString().ToUpper(), "000000"); } catch { }
            }
            Session[HomoryCoreConstant.SessionTeachersId] = new List<Guid>();
            HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
            RebindBatch();
            grid.Rebind();
            gridX.Rebind();
            grid_b.Rebind();
            grid_view.Rebind();
            view.Rebind();
            Notify(panel, "操作成功", "success");
        }

        protected void btnB_Load(object sender, EventArgs e)
        {
            (sender as Button).Visible = CanMove;
        }

        protected void btnV_Load(object sender, EventArgs e)
        {
            (sender as Button).Visible = CanMove;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            var url = string.Format("../Patch/Import?CampusId={0}&DepartmentId={1}", tree.SelectedNode.ParentNode.Value, tree.SelectedNode.Value);
            panel.ResponseScripts.Add(string.Format("window.open('{0}', '_blank');", url));
        }
    }
}
