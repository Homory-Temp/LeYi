using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoSearch : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                search_content.Value = Request.QueryString["Content"];
                tree.DataSource = HomoryContext.Value.Catalog.Where(o => (o.Type == CatalogType.文章 || o.Type == CatalogType.课件 || o.Type == CatalogType.视频) && o.State < State.审核).ToList();
                tree.DataBind();
                tree.GetAllNodes().ToList().ForEach(o => o.Checked = true);
                tree.GetAllNodes().Where(o => o.Level < 1).ToList().ForEach(o => o.Expanded = true);
                reBind();
            }
        }

        protected void search_go_OnServerClick(object sender, EventArgs e)
        {
            reBind();
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void itemX_OnClick(object sender, EventArgs e)
        {
            var button = ((RadButton)sender);
            var list = new RadButton[] { s1, s2, s3 };
            foreach (var btn in list)
            {
                btn.Checked = btn.Value == button.Value;
            }
            reBind();
        }

        protected void result_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            reBind();
        }

        protected void reBind()
        {
            var content = search_content.Value.Trim();
            var source = HomoryContext.Value.ResourceMap;
            var catalogIdList = tree.GetAllNodes().Where(o => o.Checked).Select(o => Guid.Parse(o.Value)).ToList();
            var catalogs = catalogIdList.Join(HomoryContext.Value.Catalog, o => o, o => o.Id, (a, b) => b).ToList();
            var list = new List<ResourceMap>();
            list.AddRange(catalogIdList.Join(source.Where(o => o.State == State.启用 && o.OpenType == OpenType.不公开 && o.UserId == CurrentUser.Id), a => a, b => b.CatalogId, (a, b) => b).ToList());
            list.AddRange(catalogIdList.Join(source.Where(o => o.State == State.启用 && o.OpenType != OpenType.不公开), a => a, b => b.CatalogId, (a, b) => b).ToList());
            if (IsOnline)
            {
                var uid = CurrentUser.Id.ToString().ToUpper();
                list.AddRange(catalogs.Where(o => o.AuditUsers.ToUpper().Contains(uid.ToUpper())).Join(source.Where(o => o.State > State.启用), a => a.Id, b => b.CatalogId, (a, b) => b).ToList());
            }
            var finalSource = list;
            var final = finalSource.Where(o => o.Title.Contains(content)).ToList().OrderByDescending(o => o.Time).ToList();
            if (s2.Checked)
                result.DataSource = final.OrderByDescending(o => o.Stick).ThenByDescending(o => o.View);
            else if (s3.Checked)
                result.DataSource = final.OrderByDescending(o => o.Stick).ThenByDescending(o => o.Grade);
            else if (s1.Checked)
                result.DataSource = final.OrderByDescending(o => o.Stick).ThenByDescending(o => o.Time);
            total.InnerText = final.Count.ToString();
        }

        protected void commentList_NodeCheck(object sender, RadTreeNodeEventArgs e)
        {
            reBind();
        }

        protected void SetTop_Click(object sender, ImageClickEventArgs e)
        {
            var SourceId = Guid.Parse((sender as ImageButton).Attributes["SourceId"].ToString());
            var SourceObj = HomoryContext.Value.Resource.SingleOrDefault(o => o.Id == SourceId);
            SourceObj.Stick = System.Math.Abs((Byte)SourceObj.Stick - 1);
            HomoryContext.Value.SaveChanges();
            reBind();
        }

        protected void result_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            var control = (e.Item.FindControl("SetTop") as ImageButton);
            control.Attributes["onmouseover"] = "MouseOver('" + control.ClientID + "')";
            control.Attributes["onmouseout"] = "MouseOut('" + control.ClientID + "')";
            var ab = (e.Item.FindControl("ab") as HtmlAnchor).ClientID;
            var tip = (e.Item.FindControl("tip") as RadToolTip);
            tip.TargetControlID = ab;
        }

        protected void a_CheckedChanged(object sender, EventArgs e)
        {
            reBind();
        }

        protected void tb1_ServerClick(object sender, EventArgs e)
        {
            var rid = Guid.Parse((sender as HtmlAnchor).Attributes["match"]);
            var r = HomoryContext.Value.Resource.Single(o => o.Id == rid);
            r.State = State.启用;
            var ctrl = (sender as HtmlAnchor).NamingContainer.FindControl("reason") as RadTextBox;
            var ra = new ResourceAudit { Id = HomoryContext.Value.GetId(), Content = ctrl.Text, ResourceId = rid, Time = DateTime.Now, AuditState = 1, AuditUser = CurrentUser.Id };
            HomoryContext.Value.ResourceAudit.Add(ra);
            HomoryContext.Value.SaveChanges();
            reBind();
        }

        protected void tb2_ServerClick(object sender, EventArgs e)
        {
            var rid = Guid.Parse((sender as HtmlAnchor).Attributes["match"]);
            var r = HomoryContext.Value.Resource.Single(o => o.Id == rid);
            r.State = State.停用;
            var ctrl = (sender as HtmlAnchor).NamingContainer.FindControl("reason") as RadTextBox;
            var ra = new ResourceAudit { Id = HomoryContext.Value.GetId(), Content = ctrl.Text, ResourceId = rid, Time = DateTime.Now, AuditState = 4, AuditUser = CurrentUser.Id };
            HomoryContext.Value.ResourceAudit.Add(ra);
            HomoryContext.Value.SaveChanges();
            reBind();
        }

        protected void tb3_ServerClick(object sender, EventArgs e)
        {
            var rid = Guid.Parse((sender as HtmlAnchor).Attributes["match"]);
            var r = HomoryContext.Value.Resource.Single(o => o.Id == rid);
            r.OpenType = OpenType.不公开;
            r.State = State.启用;
            var ctrl = (sender as HtmlAnchor).NamingContainer.FindControl("reason") as RadTextBox;
            var ra = new ResourceAudit { Id = HomoryContext.Value.GetId(), Content = ctrl.Text, ResourceId = rid, Time = DateTime.Now, AuditState = 5, AuditUser = CurrentUser.Id };
            HomoryContext.Value.ResourceAudit.Add(ra);
            HomoryContext.Value.SaveChanges();
            reBind();
        }
    }
}
