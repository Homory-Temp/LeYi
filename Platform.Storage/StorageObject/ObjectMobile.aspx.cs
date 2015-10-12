using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;

public partial class Object : SingleStoragePageMobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //tree.SourceBind(db.Value.StorageCatalogGet(StorageId).OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList());
            //tree.InitialTree(0, 2);
            list.Rebind();
            //if (!"TargetId".Query().Null())
            //{
            //    target_content.Visible = true;
            //    target_content.Text = "已选择购置单，单号：{0}".Formatted(db.Value.StorageTargetGetOne("TargetId".Query().GlobalId()).Number);
            //}
        }
        code.Focus();
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        list.Rebind();
    }

    protected void list_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        //if (tree.SelectedValue.Null())
        //    list.Source(null);
        //else
        //    list.Source(db.Value.StorageCatalogObjectGetEx(StorageId, tree.SelectedValue.GlobalId()).Where(o => o.Name.ToLower().Contains(search.Text.ToLower()) || o.PinYin.ToLower().Contains(search.Text.ToLower()) || o.Note.ToLower().Contains(search.Text.ToLower())));

        try

        {
            var c = code.Text;
            code.Text = string.Empty;
            if (c.Length != 12)
                return;
            string type;
            Guid id;
            db.Value.FromQR(c, out type, out id);
            switch (type)
            {
                case "W":
                    {
                        list.Source(db.Value.StorageObject.Where(o => o.Id == id).ToList());
                        break;
                    }
                case "D":
                    {
                        var s = db.Value.StorageInSingle.Single(o => o.Id == id);
                        list.Source(db.Value.StorageObject.Where(o => o.Id == s.ObjectId).ToList());
                        break;
                    }
            }
        }
        catch
        {
            list.Source(null);
        }


    }

    //protected void listX_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    //{
    //    if (tree.SelectedValue.Null())
    //        listX.Source(null);
    //    else
    //        listX.Source(db.Value.StorageCatalogObjectGetEx(StorageId, tree.SelectedValue.GlobalId()).Where(o => o.Name.ToLower().Contains(search.Text.ToLower()) || o.PinYin.ToLower().Contains(search.Text.ToLower()) || o.Note.ToLower().Contains(search.Text.ToLower())));
    //}

    //protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    //ap.Script("w_add('{0}');".Formatted(tree.SelectedValue));
    //}

    protected void edit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_edit('{0}');".Formatted(sender.ButtonArgs()));
    }

    protected void remove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("w_remove('{0}');".Formatted(sender.ButtonArgs()));
    }

    //protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    //{
    //    e.Node.Expanded = true;
    //    list.Rebind();
    //}

    //protected void ap_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
    //{
    //    if (e.Argument == "Rebind")
    //    {
    //        tree.RebindTreeCallback(db.Value.StorageCatalogGet(StorageId).OrderBy(o => o.Ordinal).ThenBy(o => o.Name).ToList());
    //        list.Rebind();
          
    //    }
    //}

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if ("TargetId".Query().Null())
            Response.Redirect("~/StorageIn/InObject?StorageId={0}&ObjectId={1}".Formatted(StorageId, sender.ButtonArgs()));
        else
            Response.Redirect("~/StorageIn/InDoing?StorageId={0}&ObjectId={1}&TargetId={2}".Formatted(StorageId, sender.ButtonArgs(), "TargetId".Query()));
    }

    protected void consume_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if ("UserId".Query().Null())
            Response.Redirect("~/StorageConsume/ConsumeDoing?StorageId={0}&ObjectId={1}".Formatted(StorageId, sender.ButtonArgs()));
        else
            Response.Redirect("~/StorageConsume/ConsumeDoing?StorageId={0}&ObjectId={1}&UserId={2}".Formatted(StorageId, sender.ButtonArgs(), "UserId".Query()));
    }

    protected void out_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var obj = db.Value.StorageObjectGetOne(sender.ButtonArgs().GlobalId());
        if ("UserId".Query().Null())
            Response.Redirect("~/StorageOut/OutDoing{2}?StorageId={0}&ObjectId={1}".Formatted(StorageId, sender.ButtonArgs(), obj.Single ? "S" : "M"));
        else
            Response.Redirect("~/StorageOut/OutDoing{2}?StorageId={0}&ObjectId={1}&UserId={2}".Formatted(StorageId, sender.ButtonArgs(), obj.Single ? "S" : "M", "UserId".Query()));
    }

    protected void lend_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var obj = db.Value.StorageObjectGetOne(sender.ButtonArgs().GlobalId());
        if ("UserId".Query().Null())
            Response.Redirect("~/StorageLend/LendDoing{2}?StorageId={0}&ObjectId={1}".Formatted(StorageId, sender.ButtonArgs(), obj.Single ? "S" : "M"));
        else
            Response.Redirect("~/StorageLend/LendDoing{2}?StorageId={0}&ObjectId={1}&UserId={2}".Formatted(StorageId, sender.ButtonArgs(), obj.Single ? "S" : "M", "UserId".Query()));
    }

    protected void return_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var obj = db.Value.StorageObjectGetOne(sender.ButtonArgs().GlobalId());
        if ("UserId".Query().Null())
            Response.Redirect("~/StorageReturn/ReturnObject?StorageId={0}&ObjectId={1}".Formatted(StorageId, sender.ButtonArgs()));
        else
            Response.Redirect("~/StorageReturn/ReturnDoing{3}?StorageId={0}&ObjectId={1}&UserId={2}".Formatted(StorageId, sender.ButtonArgs(), "UserId".Query(), obj.Single ? "S" : "M"));
    }

    protected void search_Search(object sender, SearchBoxEventArgs e)
    {
        list.Rebind();
    }

    protected void list_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        try
        {
            var item = e.Item as RadListViewDataItem;
            var label = item.FindControl("lbl").ClientID;
            var tooltip = item.FindControl("tip") as RadWindow;
            tooltip.OpenerElementID = label;
            tooltip.NavigateUrl = "../StorageObject/ObjectWindow?Id={0}".Formatted(item.GetDataKeyValue("Id"));
        }
        catch
        {

        }
    }

    //protected void listX_ItemDataBound(object sender, RadListViewItemEventArgs e)
    //{
    //    try
    //    {
    //        var item = e.Item as RadListViewDataItem;
    //        var label = item.FindControl("labelx").ClientID;

    //        var tooltip = item.FindControl("tip2") as RadToolTip;
    //        tooltip.TargetControlID = label;
            //var tooltip = item.FindControl("tip") as RadWindow;
            //tooltip.OpenerElementID = label;
            //tooltip.NavigateUrl = "../StorageObject/ObjectWindow?Id={0}".Formatted(item.GetDataKeyValue("Id"));
    //    }
    //    catch
    //    {

    //    }
    //}

    //protected void view_type_grid_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    list.Visible = true;
    //    listX.Visible = false;
    //}

    //protected void view_type_list_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    //{
    //    list.Visible = false;
    //    listX.Visible = true;
    //}

    protected void flow_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageScan/ScanQuery?Id={0}".Formatted(sender.ButtonArgs()));
    }
}
