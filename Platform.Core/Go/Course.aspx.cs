using Homory.Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoCourse : HomoryCorePageWithGrid
    {
        private const string Right = "Course";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LogOp(OperationType.查询);
            }
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grid.DataSource = HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程 && o.State < State.删除).OrderBy(o => o.Ordinal).ToList();
        }

        protected void gridX_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
        {
            foreach (var command in e.Commands)
            {
                var values = command.NewValues;
                if (NotSet(values, "Name"))
                    continue;
                var ordinal = Get(values, "Ordinal", 99);
                var name = Get(values, "Name", string.Empty);
                var state = Get(values, "State", State.启用);
                switch (command.Type)
                {
                    case GridBatchEditingCommandType.Insert:
                        var range = bool.Parse(values["Range"].ToString());
                        CourseAdd(HomoryContext.Value, name, state, ordinal, range);
                        LogOp(OperationType.新增);
                        break;
                    case GridBatchEditingCommandType.Update:
                        {
                            var id = Get(values, "Id", Guid.Empty);
                            CourseUpdate(HomoryContext.Value, id, state, ordinal);
                            LogOp(state);
                        }
                        break;
                }
            }
            grid.Rebind();
            Notify(panel, "操作成功", "success");
        }

        public bool CourseAdd(Entities db, string name, State state, int ordinal, bool range)
        {
            try
            {
                var course = new Catalog
                {
                    Id = db.GetId(),
                    ParentId = HomoryCoreConstant.CourseOtherId,
                    TopId = range ? Guid.Empty : (Guid?)null,
                    Name = name,
                    State = state,
                    Ordinal = ordinal,
                    Type = CatalogType.课程
                };
                var ex = db.Catalog.SingleOrDefault(o => o.Name == name && o.Type == CatalogType.课程);
                if (ex == null)
                {
                    db.Catalog.Add(course);
                    db.SaveChanges();
                }
                else
                {
                    ex.State = state;
                    ex.Ordinal = ordinal;
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CourseUpdate(Entities db, Guid id, State state, int ordinal)
        {
            try
            {
                var course = db.Catalog.Single(o => o.Id == id);
                course.State = state;
                course.Ordinal = ordinal;
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
