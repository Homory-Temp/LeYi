using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_ObjectEdit : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
            tree.DataBind();
            unit.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.单位).ToList();
            unit.DataBind();
            age.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.年龄段).ToList();
            age.DataBind();
            agex.Visible = Depot.Featured(DepotType.幼儿园);
            specification.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.规格).ToList();
            specification.DataBind();
            brand.DataSource = DataContext.DepotDictionaryLoad(Depot.Id, DictionaryType.品牌).ToList();
            brand.DataBind();
            var oid = "ObjectId".Query().GlobalId();
            var obj = DataContext.DepotObject.Single(o => o.Id == oid);
            var cid = "CatalogId".Query();
            var node = tree.EmbeddedTree.GetAllNodes().First(o => o.Value == cid);
            node.ExpandParentNodes();
            node.Selected = true;
            tree.SelectedValue = node.Value;
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
            if (brand.FindItemByText(obj.Brand) == null)
            {
                brand.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = obj.Brand, Value = obj.Brand });
            }
            brand.FindItemByText(obj.Brand).Selected = true;
            try
            {
                if (age.FindItemByText(obj.Age) == null)
                {
                    age.Items.Add(new Telerik.Web.UI.RadComboBoxItem { Text = obj.Age, Value = obj.Age });
                }
                age.FindItemByText(obj.Age).Selected = true;
            }
            catch
            { }
            var imgs = new string[] { obj.ImageA, obj.ImageB, obj.ImageC, obj.ImageD };
            for (var i = 0; i < imgs.Length; i++)
            {
                if (!imgs[i].None())
                    new[] { p0, p1, p2, p3 }[i].Src = imgs[i];
            }
            var img = new[] { p0, p1, p2, p3 }.ToList();
            var count = img.Where(o => o.Src.Contains("/Content/Images/Transparent.png")).Count();
            upload.InitialFileInputsCount = count == 0 ? 0 : 1;
            clear.Visible = count < 4;
            imgRow.Visible = count < 4;
            upload.MaxFileInputsCount = count == 0 ? 0 : count;
            content.Text = obj.Note;
        }
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Object?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, "CatalogId".Query()));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().None())
        {
            NotifyError(ap, "请输入物资名称");
            return;
        }
        if (tree.SelectedValue.None())
        {
            NotifyError(ap, "请选择物资类别");
        }
        var id = Save();
        Response.Redirect("~/DepotAction/In?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, id));
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().None())
        {
            NotifyError(ap, "请输入物资名称");
            return;
        }
        if (tree.SelectedValue.None())
        {
            NotifyError(ap, "请选择物资类别");
        }
        Save();
        Response.Redirect("~/DepotAction/Object?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, "CatalogId".Query()));
    }

    protected Guid Save()
    {
        var oid = "ObjectId".Query().GlobalId();
        var img = new[] { p0, p1, p2, p3 }.ToList();
        var photo = img.Count(o => !o.Src.Contains("/Content/Images/Transparent.png")) == 0 ? new string[] { } : img.Where(o => !o.Src.Contains("/Content/Images/Transparent.png")).Select(o => o.Src).ToArray();
        var ids = new List<Guid>();
        var node = tree.EmbeddedTree.SelectedNode;
        ids.Add(node.Value.GlobalId());
        while (node.ParentNode != null)
        {
            node = node.ParentNode;
            ids.Insert(0, node.Value.GlobalId());
        }
        DataContext.DepotObjectEditX(oid, ids, Depot.Id, name.Text.Trim(), "", "", brand.Text.Trim(), "", unit.Text.Trim(), specification.Text.Trim(), low.PeekValue(0.00M), high.PeekValue(0.00M), photo.Length > 0 ? photo[0] : "", photo.Length > 1 ? photo[1] : "", photo.Length > 2 ? photo[2] : "", photo.Length > 3 ? photo[3] : "", content.Text.Trim(), ordinal.PeekValue(100), age.Text.Trim());
        return oid;
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
                    var path = "../Common/物资/图片/{0}{1}".Formatted(DataContext.GlobalId(), files[i].GetExtension());
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
