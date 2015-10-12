using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
	public partial class GoAssess : HomoryCorePageWithGrid
    {
        private const string Right = "Assess";

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
            grade.DataSource = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_初中 || o.Type == CatalogType.年级_高中 || o.Type == CatalogType.年级_幼儿园)).OrderBy(o => o.Ordinal).ToList();
            grade.DataBind();
            course.DataSource = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.课程).OrderBy(o => o.State).ThenBy(o => o.Ordinal).ToList();
            course.DataBind();
            grade.SelectedIndex = 0;
            course.SelectedIndex = 0;
            BindGrid();
        }

        protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grid.Rebind();
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            BindGrid();
        }

        protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            var gradeId = Guid.Parse(grade.SelectedItem.Value);
            var courseId = Guid.Parse(course.SelectedItem.Value);
            foreach (var command in e.Commands)
            {
                var values = command.NewValues;
                if (NotSet(values, "Title"))
                    continue;
                var title = values["Title"].ToString();
                var ordinal = Get(values, "Ordinal", 99);
                var defaultState = HomoryContext.Value.AssessTable.Count(o => o.GradeId == gradeId && o.CourseId == courseId && o.State == State.启用) == 0 ? State.启用 : State.停用;
                var state = Get(values, "State", defaultState);
                switch (command.Type)
                {
                    case GridBatchEditingCommandType.Insert:
                        {
                            var newId = HomoryContext.Value.GetId();
                            HomoryContext.Value.AssessTable.Add(new AssessTable
                            {
                                Id = newId,
                                Title = title,
                                GradeId = gradeId,
                                CourseId = courseId,
                                Content = (new List<AssessItem>()).ToJson(),
                                State = state,
                                Ordinal = ordinal,
                                Time = DateTime.Now
                            });
                            if (state == State.启用)
                            {
                                HomoryContext.Value.AssessTable.Where(o => o.GradeId == gradeId && o.CourseId == courseId && o.Id != newId && o.State == State.启用).Update(o => new AssessTable
                                {
                                    State = State.停用,
                                });
                            }
                            HomoryContext.Value.SaveChanges();
                            LogOp(OperationType.新增);
                            break;
                        }
                    case GridBatchEditingCommandType.Update:
                        {
                            var id = Get(values, "Id", Guid.Empty);
                            HomoryContext.Value.AssessTable.Where(o => o.Id == id).Update(o => new AssessTable
                            {
                                Title = title,
                                Ordinal = ordinal,
                                State = state,
                            });
                            if (state == State.启用)
                            {
                                HomoryContext.Value.AssessTable.Where(o => o.GradeId == gradeId && o.CourseId == courseId && o.Id != id && o.State == State.启用).Update(o => new AssessTable
                                {
                                    State = State.停用,
                                });
                            }
                            HomoryContext.Value.SaveChanges();
                            LogOp(state);
                        }
                        break;
                }
            }
            Notify(panel, "操作成功", "success");
        }

        protected void BindGrid()
        {
            if (grade.SelectedItem != null && course.SelectedItem != null)
            {
                var gradeId = Guid.Parse(grade.SelectedItem.Value);
                var courseId = Guid.Parse(course.SelectedItem.Value);
                grid.DataSource = HomoryContext.Value.AssessTable.Where(o => o.CourseId == courseId && o.GradeId == gradeId && o.State < State.删除).ToList();
            }
            else
            {
                grid.DataSource = null;
            }
        }

        protected void grid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName != RadGrid.SelectCommandName) return;
            items.Visible = true;
            repeater.DataSource = new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            repeater.DataBind();
            var id = ((GridEditableItem) e.Item).GetDataKeyValue("Id").ToString();
            v.Value = id;
            var key = Guid.Parse(id);
            var assess = HomoryContext.Value.AssessTable.First(o => o.Id == key);
            var list = assess.Content.FromJson<List<AssessItem>>();
            for (var i = 0; i < list.Count; i++)
            {
                var name = repeater.Items[i].FindControl("in_name") as RadTextBox;
                var value = repeater.Items[i].FindControl("in_value") as RadNumericTextBox;
// ReSharper disable PossibleNullReferenceException
                name.Text = list[i].Name;
                value.Value = (double)list[i].Score;
// ReSharper restore PossibleNullReferenceException
			}
        }

        protected void buttonOk_Click(object sender, EventArgs e)
        {
            var id = Guid.Parse(v.Value);
            var list = new List<AssessItem>();
            foreach (RepeaterItem item in repeater.Items)
            {
                var name = ((RadTextBox) item.FindControl("in_name")).Text;
                var value = ((RadNumericTextBox) item.FindControl("in_value")).Value;
                if (!string.IsNullOrWhiteSpace(name) && value.HasValue)
                {
                    list.Add(new AssessItem { Name = name, Score = (decimal)value.Value });
                }
            }
            var content = list.ToJson();
            HomoryContext.Value.AssessTable.Where(o => o.Id == id).Update(o => new AssessTable
            {
                Content = content
            });
            HomoryContext.Value.SaveChanges();
            LogOp(OperationType.编辑);
            v.Value = string.Empty;
            items.Visible = false;
            grid.Rebind();
            Notify(panel, "操作成功", "success");
        }

        protected override string PageRight
        {
            get { return Right; }
        }
    }
}
