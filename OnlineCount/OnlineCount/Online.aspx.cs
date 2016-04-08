using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Online : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (DepartmentId == "4006")
            {
                Load4006();
            }
            else if (DepartmentId == "4007")
            {
                Load4007();
            }
        }
    }

    protected void Load4006()
    {
        var db = new C6Entities();
        var department = db.Department.SingleOrDefault(o => o.DeptDelFlag == 0 && o.DeptID == DepartmentId);
        var children = db.Department.Where(o => o.DeptDelFlag == 0 && o.DeptParentID == DepartmentId);
        var from = new DateTime(int.Parse(Year), int.Parse(Month), 1);
        var to = from.AddMonths(1);
        var data = db.C____OnlineCountStatistics.Where(o => o.Time > from && o.Time < to).ToList();
        var sb = new StringBuilder();
        var users = 0;
        var counter = 0;
        var total = new int[Days.Count * 2];
        sb.AppendFormat("{0} {1}年{2}月 在线统计\r\n", department.DeptName, Year, Month);
        sb.Append("\t");
        foreach (var day in Days)
        {
            sb.AppendFormat("{0}月{1}日\t\t", day.Month.ToString().PadLeft(2, '0'), day.Day.ToString().PadLeft(2, '0'));
        }
        sb.Append("\r\n\t");
        foreach (var day in Days)
        {
            sb.AppendFormat("{0}\t{1}\t", "11:00", "16:00");
        }
        sb.Append("\r\n");
        foreach (var child in children)
        {
            var relations = db.RelationshipUsers.Where(o => o.DelFlag == "0" && o.DeptID == child.DeptID && o.RelaPrimary == 1);
            if (relations.Count() == 0)
                continue;
            sb.AppendFormat("【{0}】", child.DeptName);
            sb.Append("\r\n");
            foreach (var relation in relations)
            {
                var user = db.Users.SingleOrDefault(o => o.SysFlag == 0 && o.UserID == relation.UserID);
                if (user == null)
                    continue;
                users++;
                counter = 0;
                var me = 0;
                sb.AppendFormat("{0}\t", user.UserName);
                foreach (var day in Days)
                {
                    var middle = new DateTime(day.Year, day.Month, day.Day, 12, 0, 0);
                    if (data.Count(o => o.Time > day && o.Time < middle && o.Online == 1 && o.Id == user.UserID) > 0)
                    {
                        sb.AppendFormat("{0}\t", "√");
                        total[counter]++;
                        me++;
                    }
                    else
                    {
                        sb.Append("\t");
                    }
                    counter++;
                    if (data.Count(o => o.Time > middle && o.Time < day.AddDays(1) && o.Online == 1 && o.Id == user.UserID) > 0)
                    {
                        sb.AppendFormat("{0}\t", "√");
                        total[counter]++;
                        me++;
                    }
                    else
                    {
                        sb.Append("\t");
                    }
                    counter++;
                }
                sb.AppendFormat("{0}\r\n", me);
            }
        }
        sb.AppendFormat("<{0}人>\t", users);
        for (var i = 0; i < Days.Count; i++)
        {
            sb.AppendFormat("{0}\t", total[i]);
        }
        table.InnerText = sb.ToString();
    }

    protected void Load4007()
    {
        var db = new C6Entities();
        var department = db.Department.SingleOrDefault(o => o.DeptDelFlag == 0 && o.DeptID == DepartmentId);
        var children = db.Department.Where(o => o.DeptDelFlag == 0 && o.DeptParentID == DepartmentId);
        var from = new DateTime(int.Parse(Year), int.Parse(Month), 1);
        var to = from.AddMonths(1);
        var data = db.C____OnlineCountStatistics.Where(o => o.Time > from && o.Time < to).ToList();
        var sb = new StringBuilder();
        var users = 0;
        var counter = 0;
        var total = new int[Days.Count * 2];
        sb.AppendFormat("{0} {1}年{2}月 在线统计\r\n", department.DeptName, Year, Month);
        sb.Append("\t");
        foreach (var day in Days)
        {
            sb.AppendFormat("{0}月{1}日\t\t", day.Month.ToString().PadLeft(2, '0'), day.Day.ToString().PadLeft(2, '0'));
        }
        sb.Append("\r\n\t");
        foreach (var day in Days)
        {
            sb.AppendFormat("{0}\t{1}\t", "11:00", "16:00");
        }
        sb.Append("\r\n");
        foreach (var child in children)
        {
            sb.AppendFormat("【{0}】\r\n", child.DeptName);
            var inners = db.Department.Where(o => o.DeptDelFlag == 0 && o.DeptParentID == child.DeptID);
            foreach (var inner in inners)
            {
                var relations = db.RelationshipUsers.Where(o => o.DelFlag == "0" && o.DeptID == inner.DeptID && o.RelaPrimary == 1);
                if (relations.Count() == 0)
                    continue;
                sb.AppendFormat("  **【{0}】", inner.DeptName);
                sb.Append("\r\n");
                foreach (var relation in relations)
                {
                    var user = db.Users.SingleOrDefault(o => o.SysFlag == 0 && o.UserID == relation.UserID);
                    if (user == null)
                        continue;
                    users++;
                    counter = 0;
                    var me = 0;
                    sb.AppendFormat("{0}\t", user.UserName);
                    foreach (var day in Days)
                    {
                        var middle = new DateTime(day.Year, day.Month, day.Day, 12, 0, 0);
                        if (data.Count(o => o.Time > day && o.Time < middle && o.Online == 1 && o.Id == user.UserID) > 0)
                        {
                            sb.AppendFormat("{0}\t", "√");
                            total[counter]++;
                            me++;
                        }
                        else
                        {
                            sb.Append("\t");
                        }
                        counter++;
                        if (data.Count(o => o.Time > middle && o.Time < day.AddDays(1) && o.Online == 1 && o.Id == user.UserID) > 0)
                        {
                            sb.AppendFormat("{0}\t", "√");
                            total[counter]++;
                            me++;
                        }
                        else
                        {
                            sb.Append("\t");
                        }
                        counter++;
                    }
                    sb.AppendFormat("{0}\r\n", me);
                }
            }
        }
        sb.AppendFormat("<{0}人>\t", users);
        for (var i = 0; i < Days.Count; i++)
        {
            sb.AppendFormat("{0}\t", total[i]);
        }
        table.InnerText = sb.ToString();
    }

    protected string DepartmentId
    {
        get
        {
            return Request.QueryString["D"];
        }
    }

    protected string Year
    {
        get
        {
            return Request.QueryString["Y"];
        }
    }

    protected string Month
    {
        get
        {
            return Request.QueryString["M"].PadLeft(2, '0');
        }
    }

    protected List<DateTime> Days
    {
        get
        {
            var list = new List<DateTime>();
            var from = new DateTime(int.Parse(Year), int.Parse(Month), 1);
            var to = from.AddMonths(1);
            while (from < to)
            {
                list.Add(from);
                from = from.AddDays(1);
            }
            return list;
        }
    }
}
