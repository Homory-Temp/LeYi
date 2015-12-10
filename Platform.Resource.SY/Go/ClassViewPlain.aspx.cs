using Homory.Model;
using System;
using System.Linq;
using System.Text;

namespace Go
{
    public partial class GoViewPlain : System.Web.UI.Page
    {
        protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cg.Visible = CanCombineCourse() || CanCombineGrade();
                tag.Visible = CanCombineTags();
                var url = string.Format("../Document/web/PdfViewer.aspx?Id={0}&Random={1}", Request.QueryString["Id"],
                    Guid.NewGuid());
                publish_preview_pdf.Attributes["src"] = url;
                catalog.Visible = CurrentResource.Type == ResourceType.文章 && CurrentResource.ResourceCatalog.Count(y => y.State < State.审核 && y.Catalog.State < State.审核 && y.Catalog.Type == CatalogType.文章) > 0;
                var p =
                    TargetUser.Resource.Where(
                        o => o.State == State.启用 && o.Type == CurrentResource.Type && o.Time > CurrentResource.Time)
                        .OrderByDescending(o => o.Time).FirstOrDefault();
                HomoryContext.Value.ST_Resource(CurrentResource.Id, ResourceOperationType.View, 1);
                CurrentResource.View += 1;
                HomoryContext.Value.SaveChanges();
            }
        }

        private Resource _resource;

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

        protected void publish_attachment_list_OnNeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
        {
            var resource = CurrentResource;
            var files = HomoryContext.Value.Resource.First(o => o.Id == resource.Id).ResourceAttachment.OrderBy(o => o.Id).ToList();
            publish_attachment_list.DataSource = files;
            pppp1.Visible = pppp2.Visible = HomoryContext.Value.Resource.First(o => o.Id == resource.Id).ResourceAttachment.Count > 0;
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
	}
}
