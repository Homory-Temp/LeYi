using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_ObjectAdd : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            tree.DataBind();
            unit.DataSource = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.单位).OrderBy(o => o.PinYin).ToList();
            unit.DataBind();
            specification.DataSource = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == DictionaryType.规格).OrderBy(o => o.PinYin).ToList();
            specification.DataBind();
            var types = CurrentStore.Types;
            r1.Visible = types.Contains("0");
            r2.Visible = types.Contains("1");
            r3.Visible = types.Contains("2");
            new[] { t1, t2, t3 }[CurrentStore.DefaultType].Checked = true;
            if (!"CatalogId".Query().Null())
            {
                var node = tree.EmbeddedTree.GetAllNodes().First(o => o.Value == "CatalogId".Query());
                node.ExpandParentNodes();
                tree.SelectedValue = "CatalogId".Query();
            }
        }
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/StoreAction/Object?StoreId={0}&CatalogId={1}".Formatted(StoreId, "CatalogId".Query()));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        var id = Save();
        Response.Redirect("~/StoreAction/In?StoreId={0}&ObjectId={1}".Formatted(StoreId, id));
    }

    protected void goon_ServerClick(object sender, EventArgs e)
    {
        Save();
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Save();
        Response.Redirect("~/StoreAction/Object?StoreId={0}&CatalogId={1}".Formatted(StoreId, "CatalogId".Query()));
    }

    protected Guid Save()
    {
        var id = db.Value.GlobalId();
        db.Value.SaveChanges();
        return id;
    }

    protected void upload_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        var files = upload.UploadedFiles;
        var img = new[] { p0, p1, p2, p3 }.ToList();
        for (var i = 0; i < files.Count; i++)
        {
            for (var j = 0; j < img.Count; j++)
            {
                if (img[j].Src.Contains("/Content/Images/Transparent.png"))
                {
                    var path = "../Common/物资/图片/{0}{1}".Formatted(db.Value.GlobalId(), files[i].GetExtension());
                    files[i].SaveAs(Server.MapPath(path));
                    img[j].Src = path;
                    break;
                }
            }
            var count = img.Where(o => o.Src.Contains("/Content/Images/Transparent.png")).Count();
            upload.InitialFileInputsCount = count == 0 ? 0 : 1;
            clear.Visible = count < 4;
            imgRow.Visible = count < 4;
            upload.MaxFileInputsCount = count == 0 ? 0 : count;
        }
    }

    protected void clear_Click(object sender, EventArgs e)
    {
        var img = new[] { p0, p1, p2, p3 }.ToList();
        img.ForEach(o => o.Src = "../Content/Images/Transparent.png");
        upload.InitialFileInputsCount = 1;
        upload.MaxFileInputsCount = 4;
        clear.Visible = false;
        imgRow.Visible = false;
    }
}
