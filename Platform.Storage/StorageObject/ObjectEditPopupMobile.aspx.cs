using Models;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;

public partial class ObjectEditPopupMobile : SingleStoragePageMobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        unit.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.单位).OrderBy(o => o.Name).ToList());
        specification.SourceBind(db.Value.StorageDictionaryGet(StorageId, DictionaryType.规格).OrderBy(o => o.Name).ToList());
        if (!IsPostBack)
        {
            var obj = db.Value.StorageObjectGetOne("Id".Query().GlobalId());
            new Control[] { ordinal, name, fixedSerial, low, high, unit, specification, note, code }.InitialValue(obj.Ordinal, obj.Name, obj.FixedSerial, obj.Low, obj.High, obj.Unit, obj.Specification, obj.Note, obj.Code);
            fixedSerial.Visible = obj.Fixed;
            var files = obj.Image.FromJson<Models.Image>().Images;
            var div = new[] { div1, div2, div3, div4 }.ToList();
            var img = new[] { img1, img2, img3, img4 }.ToList();
            for (var i = 0; i < files.Count; i++)
            {
                img[i].ImageUrl = files[i];
                div[i].Visible = true;
            }
            image.InitialFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : 1;
            image.MaxFileInputsCount = div.Where(o => o.Visible == false).Count() == 0 ? 0 : div.Where(o => o.Visible == false).Count();
            tree.DataSource = db.Value.StorageCatalog.Where(o => o.StorageId == StorageId && o.State < State.删除).ToList();
            tree.DataBind();
            tree.EmbeddedTree.ExpandAllNodes();
            tree.SelectedValue = obj.CatalogId.ToString();
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
        var cv = tree.SelectedValue.GlobalId();
        var obj = db.Value.StorageObjectGetOne("Id".Query().GlobalId());
        db.Value.StorageObjectEdit(StorageId, "Id".Query().GlobalId(), name.Text, unit.Text, specification.Text, fixedSerial.Text, low.Value(0.00M), high.Value(0.00M), image, ordinal.Value(100), note.Text, code.Text);
        if (cv != obj.CatalogId)
        {
            obj.CatalogId = cv;
            foreach (var sf in db.Value.StorageFlow.Where(o => o.ObjectId == obj.Id).ToList())
            {
                sf.CatalogId = cv;
            }
        }
        db.Value.StorageSave();
        ap.Script("ok();");
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

    protected void unit_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        unit.DataSource = db.Value.StorageDictionaryGet(StorageId, DictionaryType.单位).OrderBy(o => o.Name).ToList().Where(o => o.Name.ToLower().Contains(e.FilterString.ToLower()) || o.PinYin.ToLower().Contains(e.FilterString.ToLower()));
    }

    protected void specification_DataSourceSelect(object sender, Telerik.Web.UI.SearchBoxDataSourceSelectEventArgs e)
    {
        specification.DataSource = db.Value.StorageDictionaryGet(StorageId, DictionaryType.规格).OrderBy(o => o.Name).ToList().Where(o => o.Name.ToLower().Contains(e.FilterString.ToLower()) || o.PinYin.ToLower().Contains(e.FilterString.ToLower()));
    }
}
