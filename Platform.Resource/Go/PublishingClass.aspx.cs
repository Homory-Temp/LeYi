using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EntityFramework.Extensions;
using Homory.Model;
using Telerik.Web.UI;
using Telerik.Web.UI.Editor;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoPublishingClass : System.Web.UI.Page
    {
        protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeHomoryPage();
                CreateDirectories();
            }
        }

        protected Guid UserId
        {
            get
            {
                return Guid.Parse(Request.QueryString["UserId"]);
            }

        }

        protected bool IsOnline
        {
            get { return !string.IsNullOrEmpty(Request.QueryString["UserId"]); }
        }

        protected Resource R(object id)
        {
            var gid = Guid.Parse(id.ToString());
            return HomoryContext.Value.Resource.Single(o => o.Id == gid);
        }

        protected User U(object id)
        {
            var gid = Guid.Parse(id.ToString());
            return HomoryContext.Value.User.Single(o => o.Id == gid);
        }

        protected string UC(object id)
        {
            var gid = Guid.Parse(id.ToString());
            var user = HomoryContext.Value.User.Single(o => o.Id == gid);
            return user.DepartmentUser.Count(o => o.Type == DepartmentUserType.部门主职教师) > 0 ? "[" + user.DepartmentUser.Single(o => o.Type == DepartmentUserType.部门主职教师).TopDepartment.Name + "]" : "";
        }

        protected User CurrentUser
        {
            get
            {
                return HomoryContext.Value.User.Single(o => o.Id == UserId);
            }
        }

        private Department _campus;

        protected Department CurrentCampus
        {
            get
            {
                if (_campus != null) return _campus;
                var department =
                    CurrentUser.DepartmentUser.SingleOrDefault(o => o.Type == DepartmentUserType.部门主职教师 && o.State == State.启用);
                if (department == null) return null;
                _campus = department.TopDepartment;
                return _campus;
            }
        }

        private bool? _isMaster;

        protected bool IsMaster
        {
            get
            {
                if (!_isMaster.HasValue)
                {
                    _isMaster = CurrentUser != null &&
                                CurrentUser.DepartmentUser.Count(o => o.Type == DepartmentUserType.班级班主任 && o.State == State.启用) > 0;
                }
                return _isMaster.Value;
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
            path = string.Format("../Common/资源/{0}/试卷", CurrentUser.Id.ToString().ToUpper());
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
                var type = comboResType.SelectedItem.Value;
                switch (type)
                {
                    case "3":
                        return ResourceType.课件;
                    case "4":
                        return ResourceType.试卷;
                    case "1":
                        return ResourceType.视频;
                    case "2":
                        return ResourceType.文章;
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
        protected void InitializeHomoryPage()
        {
            Session["__r"] = false;
            if (Session["ClassResType"]!=null)
            {
                comboResType.SelectedIndex = comboResType.Items.FindItemIndexByValue(InnerType);
            }
            else
            {
                Session["ClassResType"] = "1";
            }
            Session["__r"] = true;
            var UserId = CurrentUser.Id;
            Resource r;

            if (HomoryContext.Value.Resource.Count(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == UserId && o.AssistantType == 1) != 0)
            {
                r = HomoryContext.Value.Resource
                    .First(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == UserId && o.AssistantType == 1);

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

            }
            else
            {
                r = new Resource
                {
                    Id = HomoryContext.Value.GetId(),
                    UserId = CurrentUser.Id,
                    Type = ResourceType,
                    OpenType = OpenType.互联网,
                    FileType = ResourceFileType.Word,
                    Title = string.Empty,
                    Author = CurrentUser.RealName,
                    State = State.审核,
                    Time = DateTime.Now,
                    AssistantType = 1
                };
                HomoryContext.Value.Resource.Add(r);
                HomoryContext.Value.SaveChanges();
            }


            publish_title_content.Value = r.Title;
            publish_tag_tags.DataSource =
                    HomoryContext.Value.ResourceTag.Where(o => o.ResourceId == r.Id && o.State == State.启用).Select(o => o.Tag).ToList();
            publish_tag_tags.DataBind();
            publish_prize_range.SelectedValue = r.PrizeRange.HasValue ? ((int)r.PrizeRange.Value).ToString() : "0";
            publish_prize_level.SelectedValue = r.PrizeLevel.HasValue ? ((int)r.PrizeLevel.Value).ToString() : "0";
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
            publish_editor_label.InnerText = string.Format("{0}简介：", ResourceType);
            publish_editor.Content = r.Content;
            var path = string.Format("../Common/资源/{0}/上传", CurrentUser.Id.ToString().ToUpper());
            publish_editor.SetPaths(new[] { path }, EditorFileTypes.All, EditorFileOptions.All);
            popup_import.NavigateUrl = string.Format("../Popup/PublishImportClass.aspx?UserId={1}&Type={0}", TeacherOperationType.ToString(), UserId);
            popup_attachment.NavigateUrl = string.Format("../Popup/PublishAttachmentClass.aspx?UserId={1}&Type={0}", TeacherOperationType.ToString(), UserId);
            return;
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

        protected void publish_prize_range_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "0")
            {
                CurrentResource.PrizeRange = null;
                CurrentResource.PrizeLevel = null;
                publish_prize_level.SelectedIndex = 0;
            }
            else
            {
                var range = (ResourcePrizeRange)(int.Parse(e.Value));
                CurrentResource.PrizeRange = range;
            }
            HomoryContext.Value.SaveChanges();
        }

        protected void publish_prize_level_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.Value == "0")
            {
                CurrentResource.PrizeRange = null;
                CurrentResource.PrizeLevel = null;
                publish_prize_range.SelectedIndex = 0;
            }
            else
            {
                var level = (ResourcePrizeLevel)(int.Parse(e.Value));
                CurrentResource.PrizeLevel = level;
            }
            HomoryContext.Value.SaveChanges();
        }

        protected void publish_editor_timer_OnTick(object sender, EventArgs e)
        {
            if (publish_editor.Content.Length == 0) return;
            CurrentResource.Content = publish_editor.Content;
            HomoryContext.Value.SaveChanges();
        }

        protected void CatalogChozen(DropDownTreeEntryEventArgs e, State state)
        {
            var rc = new ResourceCatalog
            {
                ResourceId = CurrentResource.Id,
                CatalogId = Guid.Parse(e.Entry.Value),
                State = state
            };
            HomoryContext.Value.ResourceCatalog.AddOrUpdate(rc);
            HomoryContext.Value.SaveChanges();
        }

        protected ResourceLogType TeacherOperationType
        {
            get
            {
                var type = comboResType.SelectedItem.Value;
                switch (type)
                {
                    case "1":
                        return ResourceLogType.发布视频;
                    case "3":
                        return ResourceLogType.发布课件;
                    case "4":
                        return ResourceLogType.发布试卷;
                    default:
                        return ResourceLogType.发布文章;
                }
            }
        }

        protected string InnerType
        {
            get
            {
                return Session["ClassResType"].ToString();
            }
        }

        private int? __year;

        protected int __Year
        {
            get
            {
                if (!__year.HasValue)
                {
                    __year = int.Parse(HomoryContext.Value.Dictionary.Single(o => o.Key == "SchoolYear").Value);
                }
                return __year.Value;
            }
            set
            {
                __year = value;
            }
        }

        protected void publish_attachment_list_OnNeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            if (!IsOnline || CurrentUser.Resource.Count(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == CurrentUser.Id && o.AssistantType == 1) == 0)
                return;
            var resource = CurrentResource;
            var files = HomoryContext.Value.Resource.Single(o => o.Id == resource.Id).ResourceAttachment.OrderBy(o => o.Id).ToList();
            publish_attachment_list.DataSource = files;
        }

        protected void publish_attachment_delete_OnClick(object sender, ImageClickEventArgs e)
        {
            var id = Guid.Parse(((ImageButton)sender).CommandArgument);
            var a = HomoryContext.Value.ResourceAttachment.Single(o => o.Id == id);
            HomoryContext.Value.ResourceAttachment.Remove(a);
            HomoryContext.Value.SaveChanges();
            publish_attachment_list.Rebind();
        }

        protected int GradeCount(ClassType classType)
        {
            switch ((int)classType)
            {
                case 1:
                    return 9;
                case 2:
                    return 3;
                case 3:
                    return 6;
                case 4:
                    return 3;
                case 6:
                    return 3;
                default:
                    return 0;
            }
        }

        public static Guid[] J = { Guid.Parse("8444E587-8CB6-4D3E-AFBA-08D1C78F2F5B"), Guid.Parse("08DDE587-8CFA-4C95-B63B-08D1C78F124D"), Guid.Parse("B25DE587-8C11-4B58-A5FE-08D1C78EFA2E") };
        public static Guid[] PJ = { Guid.Parse("8444E587-8CB6-4D3E-AFBA-08D1C78F2F5B"), Guid.Parse("08DDE587-8CFA-4C95-B63B-08D1C78F124D"), Guid.Parse("B25DE587-8C11-4B58-A5FE-08D1C78EFA2E"), Guid.Parse("BE0AE587-8CC5-40D7-B3AE-08D1C78EE12F"), Guid.Parse("18C4E587-8C47-4FEC-9FEA-08D1C78EC896"), Guid.Parse("9785E587-8C4D-49AD-B774-08D1C78EB002"), Guid.Parse("58ABE587-8C48-443E-AE1A-08D1C78E9641"), Guid.Parse("8B26E587-8CA9-4D15-B328-08D1C78E7D23"), Guid.Parse("077EE587-8CDA-44A9-9EA0-08D1C78E61D5") };
        public static Guid[] P = { Guid.Parse("BE0AE587-8CC5-40D7-B3AE-08D1C78EE12F"), Guid.Parse("18C4E587-8C47-4FEC-9FEA-08D1C78EC896"), Guid.Parse("9785E587-8C4D-49AD-B774-08D1C78EB002"), Guid.Parse("58ABE587-8C48-443E-AE1A-08D1C78E9641"), Guid.Parse("8B26E587-8CA9-4D15-B328-08D1C78E7D23"), Guid.Parse("077EE587-8CDA-44A9-9EA0-08D1C78E61D5") };
        public static Guid[] S = { Guid.Parse("0E667D0D-0BF5-4D18-9B4D-9096EDBEF969"), Guid.Parse("AC02A035-24F2-41F5-BA6B-178ED4348009"), Guid.Parse("85AD57A8-B503-4B83-99B4-9040DADC1B22") };
        public static Guid[] K = { Guid.Parse("A00D96D1-6A53-427F-B253-D92AE9E86802"), Guid.Parse("E1DD20C7-E15A-4F6E-BC18-24801292FE29"), Guid.Parse("C0CE6BA0-DD24-4316-A6C2-B998353CABAD") };

        protected Guid? GenGradeId(Department d)
        {
            int index = d.Ordinal - __Year - 1;
            var pdt = d.DepartmentRoot.ClassType;
            switch (pdt)
            {
                case ClassType.九年一贯制:
                    return index < PJ.Length && index > -1 ? PJ[index] : (Guid?)null;
                case ClassType.初中:
                    return index < J.Length && index > -1 ? J[index] : (Guid?)null;
                case ClassType.小学:
                    return index < P.Length && index > -1 ? P[index] : (Guid?)null;
                case ClassType.幼儿园:
                    return index < K.Length && index > -1 ? K[index] : (Guid?)null;
                case ClassType.高中:
                    return index < S.Length && index > -1 ? S[index] : (Guid?)null;
            }
            return null;
        }

        protected void pubish_publish_go_OnClick(object sender, ImageClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(publish_title_content.Value))
            {
                publish_publish_panel.ResponseScripts.Add("popNotify();");
                return;
            }
            var resource = CurrentResource;
            var deptCatalogId = Guid.Parse(Request.QueryString["GradeId"]);
            var dept = HomoryContext.Value.Department.Single(o => o.Id == deptCatalogId);
            var gradeCatalogId = GenGradeId(dept);
            var courseCatalogId = Guid.Parse(Request.QueryString["CourseId"]);
            var catalogId = Guid.Parse("45265E53-2D6A-40D4-BC50-F6BEE5FCD8EF");
            HomoryContext.Value.ResourceCatalog.Add(new ResourceCatalog { CatalogId = catalogId, ResourceId = resource.Id, State = State.启用 });
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
                }
            }
            resource.Title = publish_title_content.Value;
            resource.Content = publish_editor.Content;
            resource.Time = DateTime.Now;
            resource.State = State.启用;
            resource.CourseId = courseCatalogId;
            resource.GradeId = gradeCatalogId;
            resource.AssistantType = 1;
            resource.CampusId = CurrentCampus.Id;
            HomoryContext.Value.SaveChanges();
            Response.Redirect(string.Format("../Go/{1}?Id={0}", resource.Id, resource.Type == Homory.Model.ResourceType.视频 ? "ClassViewVideo" : "ClassViewPlain"), false);
        }

        protected void publish_attachment_list_panel_OnAjaxRequest(object sender, AjaxRequestEventArgs e)
        {

        }
        protected void comboResType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["ClassResType"] = comboResType.SelectedItem.Value;
            if (Session["__r"] != null && bool.Parse(Session["__r"].ToString()))
                Response.Redirect(Request.Url.PathAndQuery);
            //var keys = Request.QueryString.AllKeys;
            //if (keys.Contains("SelType"))
            //{
            //    var url = "?";
            //    foreach (var k in keys)
            //    {
            //        if (k == "SelType")
            //            continue;
            //        url += string.Format("{0}={1}&", k, Request.QueryString[k]);
            //    }
            //    this.publish_publish_panel.ResponseScripts.Add("top.location.href = '" + url + "&SelType=" + comboResType.SelectedItem.Value + "';");
            //    return;
            //}
            //var urlx = "top.location.href = '" + Request.Url.PathAndQuery + "&SelType=" + comboResType.SelectedItem.Value + "';";
            //this.publish_publish_panel.ResponseScripts.Add(urlx);


            /*

            Resource r;

            if (HomoryContext.Value.Resource.Count(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == UserId) != 0)
            {
                r = HomoryContext.Value.Resource
                    .First(o => o.State == State.审核 && o.Type == ResourceType && o.UserId == UserId);
            }
            else
            {
                r = new Resource
                    {
                        Id = HomoryContext.Value.GetId(),
                        UserId = CurrentUser.Id,
                        Type = ResourceType,
                        OpenType = OpenType.互联网,
                        FileType = ResourceFileType.Word,
                        Title = string.Empty,
                        Author = CurrentUser.RealName,
                        State = State.审核,
                        Time = DateTime.Now
                    };
                HomoryContext.Value.Resource.Add(r);
                HomoryContext.Value.SaveChanges();
            }

            publish_title_content.Value = r.Title;
            publish_tag_tags.DataSource =
                    HomoryContext.Value.ResourceTag.Where(o => o.ResourceId == r.Id && o.State == State.启用).Select(o => o.Tag).ToList();
            publish_tag_tags.DataBind();
            publish_prize_range.SelectedValue = r.PrizeRange.HasValue ? ((int)r.PrizeRange.Value).ToString() : "0";
            publish_prize_level.SelectedValue = r.PrizeLevel.HasValue ? ((int)r.PrizeLevel.Value).ToString() : "0";
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
            publish_editor_label.InnerText = string.Format("{0}简介：", ResourceType);
            publish_editor.Content = r.Content;
            var path = string.Format("../Common/资源/{0}/上传", CurrentUser.Id.ToString().ToUpper());
            publish_editor.SetPaths(new[] { path }, EditorFileTypes.All, EditorFileOptions.All);
            popup_import.NavigateUrl = string.Format("../Popup/PublishImportClass.aspx?UserId={1}&Type={0}", TeacherOperationType.ToString(), UserId);
            popup_attachment.NavigateUrl = string.Format("../Popup/PublishAttachmentClass.aspx?UserId={1}&Type={0}", TeacherOperationType.ToString(), UserId);

            wPanel.RaisePostBackEvent("");

            // 根据选择的类别跳转到本页 Type=?
            //var type = Request.QueryString["Type"];
            //switch (type)
            //{
            //    case "Media":
            //        return TeacherOperationType.Media;
            //    case "Courseware":
            //        return TeacherOperationType.Courseware;
            //    case "Paper":
            //        return TeacherOperationType.Paper;
            //    default:
            //        return TeacherOperationType.Article;
            //}
             */
        }
        protected void wPanel_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {

            popup_import.NavigateUrl = string.Format("../Popup/PublishImportClass.aspx?UserId={1}&Type={0}", TeacherOperationType.ToString(), UserId);
            popup_attachment.NavigateUrl = string.Format("../Popup/PublishAttachmentClass.aspx?UserId={1}&Type={0}", TeacherOperationType.ToString(), UserId);
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
    }
}
