using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_ObjectEdit : SingleStorePage
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
            sp.Visible = CurrentStore.State != StoreState.食品;
            var oid = "ObjectId".Query().GlobalId();
            var obj = db.Value.StoreObject.Single(o => o.Id == oid);
            var cv = obj.CatalogId.ToString();
            var node = tree.EmbeddedTree.GetAllNodes().First(o => o.Value == cv);
            node.ExpandParentNodes();
            tree.SelectedValue = cv;
            ordinal.Value = obj.Ordinal;
            name.Text = obj.Name;
            if (unit.FindItemByText(obj.Unit) == null)
            {
                unit.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = obj.Unit, Value = obj.Unit });
            }
            unit.FindItemByText(obj.Unit).Selected = true;
            if (specification.FindItemByText(obj.Specification) == null)
            {
                specification.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = obj.Specification, Value = obj.Specification });
            }
            specification.FindItemByText(obj.Specification).Selected = true;
            var imgs = obj.Image.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < imgs.Length; i++)
            {
                new[] { p0, p1, p2, p3 }[i].Src = imgs[i];
            }
            content.Text = obj.Note;
        }
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        var oid = "ObjectId".Query().GlobalId();
        var obj = db.Value.StoreObject.Single(o => o.Id == oid);
        Response.Redirect("~/StoreAction/Object?StoreId={0}&CatalogId={1}".Formatted(StoreId, obj.CatalogId));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入物资名称", "error");
            return;
        }
        var id = Save();
        Response.Redirect("~/StoreAction/In?StoreId={0}&ObjectId={1}".Formatted(StoreId, id));
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入物资名称", "error");
            return;
        }
        Save();
        var oid = "ObjectId".Query().GlobalId();
        var obj = db.Value.StoreObject.Single(o => o.Id == oid);
        Response.Redirect("~/StoreAction/Object?StoreId={0}&CatalogId={1}".Formatted(StoreId, obj.CatalogId));
    }

    protected Guid Save()
    {
        var oid = "ObjectId".Query().GlobalId();
        var obj = db.Value.StoreObject.Single(o => o.Id == oid);
        var img = new[] { p0, p1, p2, p3 }.ToList();
        var photo = img.Count(o => !o.Src.Contains("/Content/Images/Transparent.png")) == 0 ? string.Empty : img.Where(o => !o.Src.Contains("/Content/Images/Transparent.png")).Select(o => o.Src).Aggregate("", (a, b) => a += "*" + b, o => o.Substring(1));
        obj.CatalogId = tree.SelectedValue.GlobalId();
        obj.Name = name.Text.Trim();
        obj.PinYin = db.Value.ToPinYin(name.Text.Trim()).Single();
        obj.Unit = unit.Text.Trim();
        obj.Specification = specification.Text.Trim();
        obj.Low = low.PeekValue(0.00M);
        obj.High = high.PeekValue(0.00M);
        obj.Image = photo;
        obj.Note = content.Text.Trim();
        obj.Ordinal = ordinal.PeekValue(100);
        obj.Code = code.Text.Trim();
        if (db.Value.StoreDictionary.Count(o => o.StoreId == StoreId && o.Type == DictionaryType.单位 && o.Name == unit.Text) == 0)
        {
            var dictionary = new StoreDictionary
            {
                StoreId = StoreId,
                Type = DictionaryType.单位,
                Name = unit.Text,
                PinYin = db.Value.ToPinYin(unit.Text).Single()
            };
            db.Value.StoreDictionary.Add(dictionary);
        }
        if (db.Value.StoreDictionary.Count(o => o.StoreId == StoreId && o.Type == DictionaryType.规格 && o.Name == specification.Text) == 0)
        {
            var dictionary = new StoreDictionary
            {
                StoreId = StoreId,
                Type = DictionaryType.规格,
                Name = specification.Text,
                PinYin = db.Value.ToPinYin(specification.Text).Single()
            };
            db.Value.StoreDictionary.Add(dictionary);
        }
        db.Value.SaveChanges();
        return obj.Id;
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
