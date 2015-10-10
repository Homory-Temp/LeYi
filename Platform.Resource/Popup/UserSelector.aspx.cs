using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;

namespace Popup
{
	public partial class CampusSelector : HomoryResourcePage
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tree.DataSource = CurrentRights.Contains("Global") ? HomoryContext.Value.Department.Where(o => o.State < State.审核 && (o.Type == DepartmentType.学校 || o.Type == DepartmentType.部门)).OrderBy(o => o.Ordinal).ToList() : HomoryContext.Value.Department.Where(o => o.State < State.审核 && o.TopId == CurrentCampus.Id && (o.Type == DepartmentType.学校 || o.Type == DepartmentType.部门)).OrderBy(o => o.Ordinal).ToList();
                tree.DataBind();
                RadTreeNode __n = null;
                var dept = HomoryContext.Value.ViewTeacher.FirstOrDefault(o => o.Id == Initial && (o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.部门主职教师) && o.State < State.审核);
                if (dept != null)
                {
                    __n = tree.GetAllNodes().FirstOrDefault(o => o.Value.Equals(dept.DepartmentId.ToString(), StringComparison.OrdinalIgnoreCase));
                }
                if (__n == null)
                {
                    for (var i = 0; i < tree.Nodes.Count; i++)
                    {
                        if (tree.Nodes[i].Nodes.Count > 0)
                        {
                            __n = tree.Nodes[i].Nodes[0];
                            break;
                        }
                    }
                }
                if (__n != null)
                {
                    __n.Selected = true;
                    __n.ExpandParentNodes();
                }
                view.Rebind();
                panel.ResponseScripts.Add("$('#v').val('" + Initial.ToString() + "');");
            }
        }

        protected Guid Initial
        {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["Id"]))
                    return Guid.Empty;
                else
                    return Guid.Parse(Request.QueryString["Id"]);
            }
        }

        protected override bool ShouldOnline
		{
			get { return true; }
		}

        protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var id = tree.SelectedNode == null ? (Guid?)null : Guid.Parse(tree.SelectedNode.Value);
            if (id == null)
                view.DataSource = null;
            else
            {
                var gid = id.Value;

                var source = HomoryContext.Value.ViewTeacher.Where(o => o.DepartmentId == gid && (o.Type == DepartmentUserType.借调后部门主职教师 || o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.部门兼职教师) && o.State == State.启用);
                var text = peek.Text.Trim();
                var ds = source.Where(o => o.RealName.Contains(text) || o.PinYin.Contains(text) || o.Phone.Contains(text) || o.IDCard.Contains(text)).ToList();
                view.DataSource = ds;
            }
        }

        protected void tree_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            tree.CollapseAllNodes();
            if (e.Node.Level == 0 && e.Node.Nodes.Count > 0)
            {
                e.Node.Nodes[0].Selected = true;
                e.Node.Nodes[0].ExpandParentNodes();
                e.Node.Nodes[0].ExpandChildNodes();
                e.Node.Nodes[0].Expanded = true;
            }
            else
            {
                e.Node.Selected = true;
                e.Node.ExpandParentNodes();
                e.Node.ExpandChildNodes();
                e.Node.Expanded = true;
            }
            view.Rebind();
        }

        protected void peek_Search(object sender, SearchBoxEventArgs e)
        {
            view.Rebind();
        }
    }
}
