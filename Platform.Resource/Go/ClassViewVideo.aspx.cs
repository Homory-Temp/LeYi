using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Homory.Model;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;
using System.Text;

namespace Go
{
	public partial class GoViewVideo :  System.Web.UI.Page
	{
        protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

                var path = Server.MapPath(CurrentResource.Preview);
                bool yes = false;
                if (File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    try
                    {
                        var s = info.OpenWrite();
                        try
                        {
                            s.Close();
                        }
                        catch
                        {
                        }
                        yes = true;
                    }
                    catch
                    {
                    }
                }

                if (!yes)
                {
                    ni.Visible = true;
                    ri.Visible = false;
                }
                else
                {
                    ni.Visible = false;
                    ri.Visible = true;
                    preview_timer.Enabled = false;
                }


                cg.Visible = CanCombineCourse() || CanCombineGrade();
                tag.Visible = CanCombineTags();
                player.Video = CurrentResource.Preview;
                catalog.Visible = CurrentResource.Type == ResourceType.视频 && CurrentResource.ResourceCatalog.Count(y => y.State < State.审核 && y.Catalog.State < State.审核 && y.Catalog.Type == CatalogType.视频) > 0;
                HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.View, 1);
				CurrentResource.View += 1;
				HomoryContext.Value.SaveChanges();
			}
		}

		private Resource _resource;

        protected void publish_attachment_list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            var resource = CurrentResource;
            var files = HomoryContext.Value.Resource.First(o => o.Id == resource.Id).ResourceAttachment.OrderBy(o => o.Id).ToList();
            publish_attachment_list.DataSource = files;
            pppp1.Visible = pppp2.Visible = HomoryContext.Value.Resource.First(o => o.Id == resource.Id).ResourceAttachment.Count > 0;
        }

        protected static string QueryType(CatalogType type)
		{
			switch (type)
			{
				case CatalogType.年级_幼儿园:
                case CatalogType.年级_初中:
                case CatalogType.年级_小学:
                case CatalogType.年级_高中:
					{
						return "Grade";
					}
				case CatalogType.课程:
					{
						return "Course";
					}
				default:
					{
						return "Catalog";
					}
			}
		}

        protected bool CanCombineGrade()
        {
            return CurrentResource.GradeId.HasValue;
        }

        protected string CombineGrade()
        {
            return CanCombineGrade() ? string.Format("年级：<a target='_blank' href='../Go/Search?{1}={2}'>{0}</a>", HomoryContext.Value.Catalog.First(o => o.Id == CurrentResource.GradeId).Name, QueryType(HomoryContext.Value.Catalog.First(o => o.Id == CurrentResource.GradeId).Type), CurrentResource.GradeId) : "";
        }

        protected bool CanCombineCourse()
        {
            return CurrentResource.CourseId.HasValue;
        }

        protected string CombineCourse()
        {
            return CanCombineCourse() ? string.Format("学科：<a target='_blank' href='../Go/Search?{1}={2}'>{0}</a>", HomoryContext.Value.Catalog.First(o => o.Id == CurrentResource.CourseId).Name, QueryType(CatalogType.课程), CurrentResource.CourseId) : "";
        }

        protected bool CanCombineTags()
        {
            return CurrentResource.ResourceTag.Count(o => o.State < State.审核) > 0;
        }

        protected string CombineTags()
        {
            var format = "、<a target='_blank' href='../Go/Search?Content={1}'>{0}</a>";
            var sb = new StringBuilder();
            foreach (var t in CurrentResource.ResourceTag.Where(o => o.State < State.审核).ToList())
                sb.Append(string.Format(format, t.Tag, Server.UrlEncode(t.Tag)));
            return CanCombineTags() ? sb.ToString().Substring(1) : "";
        }


        protected Func<string, ResourceCatalog, string> Combine = (a, o) => string.Format("{0}<a target='_blank' href='../Go/Search?{2}={3}'>{1}</a>、", a, o.Catalog.Name, QueryType(o.Catalog.Type), o.CatalogId);

		protected Resource CurrentResource
		{
			get
			{
				if (_resource == null)
				{
					var id = Guid.Parse(Request.QueryString["Id"]);
					_resource = HomoryContext.Value.Resource.First(o => o.Id == id);
				}
				return _resource;
			}
		}

		private User _user;

		protected User TargetUser
		{
			get
			{
				if (_user == null)
				{
					_user = CurrentResource.User;
				}
				return _user;
			}
		}

        protected void preview_timer_Tick(object sender, EventArgs e)
        {
            var path = Server.MapPath(CurrentResource.Preview);
            if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);
                try
                {
                    var s = info.OpenWrite();
                    try
                    {
                        s.Close();
                    }
                    catch
                    {
                    }
                    Response.Redirect(Request.Url.PathAndQuery.ToString(), true);
                }
                catch
                {
                }
            }
        }

    }
}
