using EntityFramework.Extensions;
using Homory.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.Editor;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoPublishing : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeHomoryPage();
                CreateDirectories();
            }
        }

        protected void CreateDirectories()
        {
            var path = string.Format("../Common/资源/{0}/附件", CurrentUser.Id.ToString().ToUpper());
            var dir = Server.MapPath(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            path = string.Format("../Common/资源/{0}/文章", CurrentUser.Id.ToString().ToUpper());
            dir = Server.MapPath(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            path = string.Format("../Common/资源/{0}/课件", CurrentUser.Id.ToString().ToUpper());
            dir = Server.MapPath(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            path = string.Format("../Common/资源/{0}/视频", CurrentUser.Id.ToString().ToUpper());
            dir = Server.MapPath(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            path = string.Format("../Common/资源/{0}/上传", CurrentUser.Id.ToString().ToUpper());
            dir = Server.MapPath(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        protected ResourceType ResourceType
        {
            get
            {
                switch (Request.QueryString["Type"])
                {
                    case "Courseware":
                        return ResourceType.课件;
                    case "Media":
                        return ResourceType.视频;
                    default:
                        return ResourceType.文章;
                }
            }
        }

        protected Resource CurrentResource
        {
            get
            {
                return CurrentUser.Resource.First(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == CurrentUser.Id);
            }
        }

        protected List<RadTreeNode> LoadCDSNodes(string sid)
        {
            var id = Guid.Parse(sid);
            var l = new List<RadTreeNode>();
            var catalog = HomoryContext.Value.Catalog.Where(o => o.ParentId == id && o.State < State.停用).ToList();
            foreach (var c in catalog)
            {
                l.Add(new RadTreeNode { Text = c.Name, Value = c.Id.ToString().ToUpper() });
            }
            foreach (var n in l)
            {
                n.Nodes.AddRange(LoadCDSNodes(n.Value));
            }
            return l;
        }

        public const string FORMAT_AUTHOR = "{0} - {1}";

        public List<ED> lll { get { return HomoryContext.Value.ViewTeacher.Where(o => o.State < State.审核 && (o.Type == DepartmentUserType.部门主职教师 || o.Type == DepartmentUserType.借调后部门主职教师) && o.TopDepartmentId == CurrentCampus.Id).ToList().Select(o => new ED { Id = o.Id, Name = string.Format(FORMAT_AUTHOR, o.RealName, o.Phone) }).ToList(); } }

        protected void InitializeHomoryPage()
        {
            if (!CurrentRights.Contains("OtherPublish"))
            {
                apxx.Visible = false;
            }
            if (CurrentUser.Resource.Count(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == CurrentUser.Id) != 0)
            {
                var r = CurrentResource;

                var path__ = Server.MapPath(CurrentResource.Preview);
                if (File.Exists(path__))
                {
                    FileInfo info = new FileInfo(path__);
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
                        publish_preview_player.Title = "请点击播放按钮预览。";
                        preview_timer.Enabled = false;
                    }
                    catch
                    {
                        publish_preview_player.Title = "正在转换视频格式，请稍候。。。（可尝试先发布资源）";
                        preview_timer.Enabled = true;
                    }
                }
                else
                {
                    publish_preview_player.Title = "正在转换视频格式，请稍候。。。（可尝试先发布资源）";
                    preview_timer.Enabled = true;
                }
                try
                {
                    bool yes = CurrentResource.Author.Equals(CurrentUser.Id.ToString(), StringComparison.OrdinalIgnoreCase);
                    btnMe.Checked = yes;
                    btnOher.Checked = !yes;
                    btnOher.Value = CurrentResource.Author;
                    if (yes)
                    {
                        publish_ohter_publish.Value = string.Format(FORMAT_AUTHOR, CurrentUser.RealName, CurrentUser.Teacher.Phone);
                    }
                    else
                    {
                        var ______id = Guid.Parse(CurrentResource.Author);
                        var ______user = HomoryContext.Value.ViewTeacher.First(o => o.Id == ______id);
                        publish_ohter_publish.Value = string.Format(FORMAT_AUTHOR, ______user.RealName, ______user.Phone);
                    }
                }
                catch
                {
                    publish_ohter_publish.Value = string.Format(FORMAT_AUTHOR, CurrentUser.RealName, CurrentUser.Teacher.Phone);
                    CurrentResource.Author = CurrentUser.Id.ToString();
                    HomoryContext.Value.SaveChanges();
                    btnMe.Checked = true;
                    btnOher.Checked = false;
                    btnOher.Value = CurrentUser.Id.ToString();
                }
                publish_title_content.Value = r.Title;
                publish_tag_tags.DataSource =
                        HomoryContext.Value.ResourceTag.Where(o => o.ResourceId == r.Id && o.State == State.启用).Select(o => o.Tag).ToList();
                publish_tag_tags.DataBind();
                if (string.IsNullOrWhiteSpace(r.Preview))
                {
                    publish_preview_plain.Visible = false;
                    publish_preview_media.Visible = false;
                    publish_preview_empty.Visible = true;
                }
                else
                {
                    publish_preview_empty.Visible = false;
                    if (ResourceType == ResourceType.视频)
                    {
                        publish_preview_plain.Visible = false;
                        publish_preview_media.Visible = true;
                        publish_preview_player.Source = r.Preview;
                    }
                    else
                    {
                        publish_preview_plain.Visible = true;
                        publish_preview_media.Visible = false;
                        var url = string.Format("../Document/web/PdfViewer.aspx?Id={0}&Random={1}", r.Id, Guid.NewGuid());
                        publish_preview_pdf.Attributes["src"] = url;
                    }
                }
                publish_open_panel.Controls.OfType<RadButton>().First(o => o.Value == ((int)r.OpenType).ToString()).Checked = true;
                publish_editor_label.InnerText = string.Format("{0}内容：", ResourceType);
                publish_editor.Content = r.Content;
                publish_grade.SelectedValue = r.GradeId.ToString().ToUpper();
                periodx.SelectedDate = r.ResourceTime ?? DateTime.Today;
                var path = string.Format("../Common/资源/{0}/上传", CurrentUser.Id.ToString().ToUpper());
                publish_editor.SetPaths(new[] { path }, EditorFileTypes.All, EditorFileOptions.All);
                List<Catalog> cds = new List<Catalog>();
                switch (ResourceType)
                {
                    case ResourceType.视频:
                        publish_catalog.EmbeddedTree.Nodes.AddRange(LoadCDSNodes("10A37221-02C5-48D8-A82C-DA62A3386C0B"));
                        publish_catalog.ExpandAllDropDownNodes();
                        var catalogValueMedia = r.ResourceCatalog.First(o => o.Catalog.Type == CatalogType.视频 && o.State == State.启用).CatalogId.ToString();
                        publish_catalog.SelectedValue = catalogValueMedia.ToUpper();
                        break;
                    case ResourceType.文章:
                        publish_catalog.EmbeddedTree.Nodes.AddRange(LoadCDSNodes("023CAF84-4F7B-4777-ABEB-66137B4E71FD"));
                        publish_catalog.ExpandAllDropDownNodes();
                        var catalogValueArticle = r.ResourceCatalog.First(o => o.Catalog.Type == CatalogType.文章 && o.State == State.启用).CatalogId.ToString();
                        publish_catalog.SelectedValue = catalogValueArticle.ToUpper();
                        break;
                    case ResourceType.课件:
                        publish_catalog.EmbeddedTree.Nodes.AddRange(LoadCDSNodes("C7F16CCC-19EB-4363-8D24-7285F43C910F"));
                        publish_catalog.ExpandAllDropDownNodes();
                        var catalogValueCourseware = r.ResourceCatalog.First(o => o.Catalog.Type == CatalogType.课件 && o.State == State.启用).CatalogId.ToString();
                        publish_catalog.SelectedValue = catalogValueCourseware.ToUpper();
                        break;
                    default:
                        publish_catalog_panel.Visible = false;
                        break;
                }
                popup_import.NavigateUrl = string.Format("../Popup/PublishImport.aspx?Type={0}", Request.QueryString["Type"]);
                popup_attachment.NavigateUrl = string.Format("../Popup/PublishAttachment.aspx?Type={0}", Request.QueryString["Type"]);
                return;
            }
            var resource = new Resource
            {
                Id = HomoryContext.Value.GetId(),
                UserId = CurrentUser.Id,
                Type = ResourceType,
                OpenType = OpenType.互联网,
                FileType = ResourceFileType.Word,
                Title = string.Empty,
                Author = CurrentUser.Id.ToString(),
                State = State.审核,
                GradeId = Guid.Parse("A3757840-9DF7-4370-8151-FAD39B44EF6A"),
                Time = DateTime.Now
            };
            HomoryContext.Value.Resource.Add(resource);
            HomoryContext.Value.SaveChanges();
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        protected void publish_tag_add_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(publish_tag_content.Text.Trim()))
                return;
            var rt = new ResourceTag
            {
                Tag = publish_tag_content.Text.Trim(),
                ResourceId = CurrentResource.Id,
                State = State.启用
            };
            HomoryContext.Value.ResourceTag.AddOrUpdate(rt);
            HomoryContext.Value.SaveChanges();
            publish_tag_content.Text = string.Empty;
            publish_tag_tags.DataSource =
                    HomoryContext.Value.ResourceTag.Where(o => o.ResourceId == CurrentResource.Id && o.State == State.启用).Select(o => o.Tag).ToList();
            publish_tag_tags.DataBind();
        }

        protected void publish_tag_delete_OnServerClick(object sender, EventArgs e)
        {
            var resource = CurrentResource;
            var tag = ((HtmlAnchor)sender).InnerText;
            HomoryContext.Value.ResourceTag.Where(o => o.Tag == tag && o.ResourceId == resource.Id).Delete();
            HomoryContext.Value.SaveChanges();
            publish_tag_tags.DataSource =
                    HomoryContext.Value.ResourceTag.Where(o => o.ResourceId == resource.Id && o.State == State.启用).Select(o => o.Tag).ToList();
            publish_tag_tags.DataBind();
        }

        protected void OnOpenClick(object sender, EventArgs e)
        {
            var type = (OpenType)int.Parse(((RadButton)sender).Value);
            CurrentResource.OpenType = type;
            HomoryContext.Value.SaveChanges();
        }

        protected ResourceLogType TeacherOperationType
        {
            get
            {
                var type = Request.QueryString["Type"];
                switch (type)
                {
                    case "Media":
                        return ResourceLogType.发布视频;
                    case "Courseware":
                        return ResourceLogType.发布课件;
                    default:
                        return ResourceLogType.发布文章;
                }
            }
        }

        protected void publish_attachment_list_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsOnline || CurrentUser.Resource.Count(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == CurrentUser.Id) == 0)
                return;
            var resource = CurrentResource;
            var files = HomoryContext.Value.Resource.First(o => o.Id == resource.Id).ResourceAttachment.OrderBy(o => o.Id).ToList();
            publish_attachment_list.DataSource = files;
        }

        protected void publish_attachment_delete_OnClick(object sender, ImageClickEventArgs e)
        {
            var id = Guid.Parse(((ImageButton)sender).CommandArgument);
            var a = HomoryContext.Value.ResourceAttachment.First(o => o.Id == id);
            HomoryContext.Value.ResourceAttachment.Remove(a);
            HomoryContext.Value.SaveChanges();
            publish_attachment_list.Rebind();
        }

        protected void pubish_publish_go_OnClick(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(publish_title_content.Value))
            {
                publish_publish_panel.ResponseScripts.Add("popNotify();");
                return;
            }
            var resource = CurrentResource;
            var catalog = HomoryContext.Value.ResourceCatalog.Where(o => o.ResourceId == resource.Id && o.Catalog.Type == CatalogType.文章 && o.State < State.删除).FutureCount();
            var catalogX = HomoryContext.Value.ResourceCatalog.Where(o => o.ResourceId == resource.Id && o.Catalog.Type == CatalogType.视频 && o.State < State.删除).FutureCount();
            var ccc = CurrentResource.CourseId.HasValue ? 1 : 0;
            var ggc = CurrentResource.GradeId.HasValue ? 1 : 0;
            switch (resource.Type)
            {
                case ResourceType.文章:
                    {
                        if (catalog.Value == 0 || (ccc + ggc) < 2)
                        {
                            publish_publish_panel.ResponseScripts.Add("popNotify();");
                            return;
                        }
                        break;
                    }
                case ResourceType.课件:
                case ResourceType.试卷:
                    {
                        if ((ccc + ggc) < 2)
                        {
                            publish_publish_panel.ResponseScripts.Add("popNotify();");
                            return;
                        }
                        break;
                    }
                case ResourceType.视频:
                    {
                        if (catalogX.Value == 0 || (ccc + ggc) < 2)
                        {
                            publish_publish_panel.ResponseScripts.Add("popNotify();");
                            return;
                        }
                        break;
                    }
            }
            if (string.IsNullOrWhiteSpace(resource.Content) && string.IsNullOrWhiteSpace(resource.Preview))
            {
                publish_publish_panel.ResponseScripts.Add("popNotify();");
                return;
            }
            if (resource.PrizeRange != null && resource.PrizeLevel != null)
            {
                var credit =
                    HomoryContext.Value.PrizeCredit.SingleOrDefault(o => o.PrizeRange == resource.PrizeRange && o.PrizeLevel == resource.PrizeLevel);
                if (credit != null)
                {
                    resource.Credit = credit.Credit;
                    LogOp(ResourceLogType.个人积分, resource.Credit);
                }
            }
            resource.Title = publish_title_content.Value;
            resource.Content = publish_editor.Content;
            resource.Time = DateTime.Now;
            resource.State = State.启用;
            resource.UserId = Guid.Parse(resource.Author);
            resource.CampusId = CurrentCampus.Id;
            HomoryContext.Value.SaveChanges();
            LogOp(TeacherOperationType);
            Response.Redirect(string.Format("../Go/{1}?Id={0}", resource.Id, resource.Type == Homory.Model.ResourceType.视频 ? "ViewVideo" : "ViewPlain"), false);
        }

        protected void publish_attachment_list_panel_OnAjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }

        protected override bool ShouldOnline
        {
            get { return true; }
        }

        public class ED
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        protected void author_pub_DataSourceSelect(object sender, SearchBoxDataSourceSelectEventArgs e)
        {
            RadSearchBox searchBox = (RadSearchBox)sender;
            searchBox.DataSource = lll.Where(o => o.Name.Contains(e.FilterString)).ToList();
            searchBox.DataBind();
        }

        protected void author_pub_Search(object sender, SearchBoxEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Value))
            {
                CurrentResource.Author = e.Value;
                HomoryContext.Value.SaveChanges();
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
                    publish_preview_player.Title = "请点击播放按钮预览。";
                    preview_timer.Enabled = false;
                }
                catch
                {
                }
            }
        }

        protected void btnMe_Click(object sender, EventArgs e)
        {
            if (!CurrentResource.Author.Equals(CurrentUser.Id.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                CurrentResource.Author = CurrentUser.Id.ToString();
                HomoryContext.Value.SaveChanges();
                publish_ohter_publish.Value = string.Format(FORMAT_AUTHOR, CurrentUser.RealName, CurrentUser.Teacher.Phone);
            }
            btnOher.Value = CurrentResource.Author;
            btnMe.Checked = true;
            btnOher.Checked = false;
        }

        protected void apxx_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "")
                return;
            if (e.Argument == "0")
            {
                if (!CurrentResource.Author.Equals(CurrentUser.Id.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    CurrentResource.Author = CurrentUser.Id.ToString();
                    HomoryContext.Value.SaveChanges();
                    publish_ohter_publish.Value = string.Format(FORMAT_AUTHOR, CurrentUser.RealName, CurrentUser.Teacher.Phone);
                }
            }
            else
            {
                var gid = Guid.Parse(e.Argument);
                var ______user = HomoryContext.Value.ViewTeacher.First(o => o.Id == gid);
                CurrentResource.Author = ______user.Id.ToString();
                HomoryContext.Value.SaveChanges();
                publish_ohter_publish.Value = string.Format(FORMAT_AUTHOR, ______user.RealName, ______user.Phone);
            }
            btnOher.Value = CurrentResource.Author;
            btnMe.Checked = CurrentResource.Author.Equals(CurrentUser.Id.ToString(), StringComparison.OrdinalIgnoreCase);
            btnOher.Checked = !btnMe.Checked;
        }

        protected void publish_catalog_EntryAdded(object sender, DropDownTreeEntryEventArgs e)
        {
            HomoryContext.Value.Database.BeginTransaction();
            HomoryContext.Value.ResourceCatalog.Where(o => o.ResourceId == CurrentResource.Id).Delete();
            var rc = new ResourceCatalog
            {
                ResourceId = CurrentResource.Id,
                CatalogId = Guid.Parse(e.Entry.Value),
                State = State.启用
            };
            HomoryContext.Value.ResourceCatalog.AddOrUpdate(rc);
            HomoryContext.Value.Database.CurrentTransaction.Commit();
            HomoryContext.Value.SaveChanges();
        }

        protected void publish_grade_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var r = CurrentResource;
            if (string.IsNullOrEmpty(e.Value))
            {
                r.GradeId = null;
            }
            else
            {
                r.GradeId = Guid.Parse(e.Value);
            }
            HomoryContext.Value.SaveChanges();
        }

        protected void periodx_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            var r = CurrentResource;
            r.ResourceTime = periodx.SelectedDate;
            HomoryContext.Value.SaveChanges();
        }
    }
}
