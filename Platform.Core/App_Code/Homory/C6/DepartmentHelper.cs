using JHSoft.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public class DepartmentHelper
{
    private static DBOperator db = DBOperatorFactory.GetDBOperator();

    // 新增学校
    public static void InsertCampus(string unitId, string name, int ordinal)
    {
        var id = JHSoft.Departments.IDCreator.CreateID("Department");
        var id_s = JHSoft.Departments.IDCreator.CreateID("Station");
        var param = new object[] { id, name, ordinal, unitId };
        db.ExecProcReDataSet("__InsertCampus", param);
        var objs = new object[] { "dept", "Department", "DeptID", id, "DeptParentID", "", "DeptDelFlag=0", "DeptSort" };
        db.ExecProcReDataSet("pt_SortInsert", objs);
        var param_s = new object[] { id_s, name, ordinal, unitId };
        db.ExecProcReDataSet("__InsertStation", param_s);
    }

    // 更新学校
    public static void UpdateCampus(string unitId, string name, int ordinal, Homory.Model.State state)
    {
        var param = new object[] { unitId, name, ordinal, state < Homory.Model.State.审核 ? 0 : 1 };
        db.ExecProcReDataSet("__UpdateCampus", param);
        db.ExecProcReDataSet("__UpdateStation", param);
    }

    // 新增部门
    public static void InsertDepartment(string pId, string meId, string name, int ordinal)
    {
        var id = JHSoft.Departments.IDCreator.CreateID("Department");
        var param = new object[] { id, name, ordinal, pId, meId };
        db.ExecProcReDataSet("__InsertDepartment", param);
        var objs = new object[] { "dept", "Department", "DeptID", id, "DeptParentID", "", "DeptDelFlag=0", "DeptSort" };
        db.ExecProcReDataSet("pt_SortInsert", objs);
    }

    // 更新部门
    public static void UpdateDepartment(string name, int ordinal, Homory.Model.State state, string meId, string pId = "")
    {
        var param = new object[] { name, ordinal, pId, meId, state < Homory.Model.State.审核 ? 0 : 1 };
        var id = db.ExecProcReobject("__UpdateDepartment", param).ToString();
        var objs = new object[] { "dept", "Department", "DeptID", id, "DeptParentID", "", "DeptDelFlag=0", "DeptSort" };
        db.ExecProcReDataSet("pt_SortInsert", objs);
    }
}
