using Homory.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Popup_GroupManageX : HomoryResourcePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			InitializeHomoryPage();
		}
	}

	protected void InitializeHomoryPage()
	{
		var id = Guid.Parse(Request.QueryString["Id"]);
		var g = HomoryContext.Value.Group.First(o => o.Id == id);

		name.Text = g.Name;
		intro.Text = g.Introduction;

		publish_course.DataSource =
			HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && o.Type == CatalogType.课程 && o.Name != "综合")
				.OrderBy(o => o.State)
				.ThenBy(o => o.Ordinal)
				.ToList();
		publish_course.DataBind();
		if (g.CourseId.HasValue)
			publish_course.SelectedValue = g.CourseId.Value.ToString();

        List<Catalog> qList;
        switch (CurrentCampus.ClassType)
        {
            case ClassType.九年一贯制:
                qList = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_初中 || o.Type == CatalogType.年级_其他)).ToList().Select(o => new Catalog { Id = o.Id, Name = o.Name, Ordinal = o.Ordinal, Type = o.Type, ParentId = o.ParentId, State = o.State, TopId = o.TopId }).ToList();
                break;
            case ClassType.初中:
                qList = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_初中 || o.Type == CatalogType.年级_其他)).ToList();
                break;
            case ClassType.小学:
                qList = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_其他)).ToList();
                break;
            case ClassType.幼儿园:
                qList = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_幼儿园 || o.Type == CatalogType.年级_其他)).ToList();
                break;
            case ClassType.高中:
                qList = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_高中 || o.Type == CatalogType.年级_其他)).ToList();
                break;
            default:
                qList = HomoryContext.Value.Catalog.Where(o => o.State < State.审核 && (o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_其他 || o.Type == CatalogType.年级_初中 || o.Type == CatalogType.年级_幼儿园 || o.Type == CatalogType.年级_高中)).ToList();
                break;
        }
        publish_grade.DataSource = qList.OrderBy(o => o.Ordinal).ToList();
        publish_grade.DataBind();
        if (g.GradeId.HasValue)
			publish_grade.SelectedValue = g.GradeId.Value.ToString();


		if (!g.Icon.Equals("~/Common/默认/群组.png"))
		{
			var dir = new DirectoryInfo(Server.MapPath("~/GroupIcon"));
			var files = dir.GetFiles("*.png").Select(o => "~/GroupIcon/" + o.Name).OrderBy(o => o).ToList();
			var index = files.IndexOf(g.Icon);
			icons.SelectedIndexes.Add(index);
		}

	}

	protected void icons_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
	{
		var id = Guid.Parse(Request.QueryString["Id"]);
		var g = HomoryContext.Value.Group.First(o => o.Id == id);

		var dir = new DirectoryInfo(Server.MapPath("~/GroupIcon"));
		var files = dir.GetFiles("*.png").Select(o => "~/GroupIcon/" + o.Name).OrderBy(o => o).ToList();
		icons.DataSource = files;
	}

	protected override bool ShouldOnline
	{
		get { return false; }
	}

	protected void btnCreate_ServerClick(object sender, EventArgs e)
	{
		var id = Guid.Parse(Request.QueryString["Id"]);
		var g = HomoryContext.Value.Group.First(o => o.Id == id);
		var dir = new DirectoryInfo(Server.MapPath("~/GroupIcon"));
		var files = dir.GetFiles("*.png").Select(o => "~/GroupIcon/" + o.Name).OrderBy(o => o).ToList();
		g.Icon = icons.SelectedItems.Count == 0 ? "~/Common/默认/群组.png" : files[icons.SelectedIndexes[0]];
		g.Introduction = intro.Text;
		g.Name = name.Text;
		if (publish_course.SelectedIndex > 0)
			g.CourseId = Guid.Parse(publish_course.SelectedItem.Value);
		if (publish_grade.SelectedIndex > 0)
			g.GradeId = Guid.Parse(publish_grade.SelectedItem.Value);
		HomoryContext.Value.SaveChanges();
		panel.ResponseScripts.Add("RadCloseRebind();");
	}
}
