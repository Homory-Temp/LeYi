using Models;
using System;
using System.IO;
using System.Linq;

public partial class ObjectAddPopup : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            unit.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.单位).OrderBy(o => o.Name).ToList());
            specification.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.规格).OrderBy(o => o.Name).ToList());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            trtr.Style.Add("display", "none");
        }
    }

    protected void ok_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写物资名称")) return;
        var div = new[] { div1, div2, div3, div4 }.ToList();
        var img = new[] { img1, img2, img3, img4 }.ToList();
        Image image = new Image();
        for (var i = 0; i < div.Count; i++)
        {
            if (div[i].Visible)
                image.Images.Add(img[i].ImageUrl);
        }
        db.Value.StorageObjectAdd(StorageId, "Id".Query().GlobalId(), name.Text, unit.Text, specification.Text, consumable.Checked, @single.Checked || @fixed.Checked, @fixed.Checked, fixedSerial.Text, low.Value(0.00M), high.Value(0.00M), image, CurrentUser, ordinal.Value(100), DateTime.Now, note.Text, code.Text);
        db.Value.StorageSave();
        ap.Script("ok();");
    }

    protected void ok_in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (name.MissingText("请填写物资名称")) return;

        if(@fixed.Checked)
            if (fixedSerial.MissingText("请填写固定资产编号")) return;

        var div = new[] { div1, div2, div3, div4 }.ToList();
        var img = new[] { img1, img2, img3, img4 }.ToList();
        Image image = new Image();
        for (var i = 0; i < div.Count; i++)
        {
            if (div[i].Visible)
                image.Images.Add(img[i].ImageUrl);
        }
        var gid = db.Value.StorageObjectAdd(StorageId, "Id".Query().GlobalId(), name.Text, unit.Text, specification.Text, consumable.Checked, (@fixed.Checked || @single.Checked), @fixed.Checked, fixedSerial.Text, low.Value(0.00M), high.Value(0.00M), image, CurrentUser, ordinal.Value(100), DateTime.Now, note.Text, code.Text);
        db.Value.StorageSave();
        if (gid == Guid.Empty)
            ap.Script("ok();");
        else
            ap.Script("top.location.href = '../StorageIn/InObject?StorageId={0}&ObjectId={1}';".Formatted(StorageId, gid));
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }

    protected void image_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        var files = image.UploadedFiles;
        var div = new[] { div1, div2, div3, div4 }.ToList();
        var img = new[] { img1, img2, img3, img4 }.ToList();
        for (var i = 0; i < files.Count; i++)
        {
            for (var j = 0; j < div.Count; j++)
            {
                if (div[j].Visible == false)
                {
                    var path = "~/Upload/{0}{1}".Formatted(db.Value.GlobalId(), files[i].GetExtension());
                    files[i].SaveAs(Server.MapPath(path));
                    img[j].ImageUrl = path;
                    div[j].Visible = true;
                    break;
                }
            }
            image.InitialFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : 1;
            image.MaxFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : div.Where(o => o.Visible == false).Count();
        }
    }

    protected void btn1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var file = Server.MapPath(img1.ImageUrl);
        File.Delete(file);
        div1.Visible = false;
        var div = new[] { div1, div2, div3, div4 }.ToList();
        image.InitialFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : 1;
        image.MaxFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : div.Where(o => o.Visible == false).Count();
    }

    protected void btn2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var file = Server.MapPath(img2.ImageUrl);
        File.Delete(file);
        div2.Visible = false;
        var div = new[] { div1, div2, div3, div4 }.ToList();
        image.InitialFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : 1;
        image.MaxFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : div.Where(o => o.Visible == false).Count();
    }

    protected void btn3_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var file = Server.MapPath(img3.ImageUrl);
        File.Delete(file);
        div3.Visible = false;
        var div = new[] { div1, div2, div3, div4 }.ToList();
        image.InitialFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : 1;
        image.MaxFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : div.Where(o => o.Visible == false).Count();
    }

    protected void btn4_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var file = Server.MapPath(img4.ImageUrl);
        File.Delete(file);
        div4.Visible = false;
        var div = new[] { div1, div2, div3, div4 }.ToList();
        image.InitialFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : 1;
        image.MaxFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : div.Where(o => o.Visible == false).Count();
    }
}
