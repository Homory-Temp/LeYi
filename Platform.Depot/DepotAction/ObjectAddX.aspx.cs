using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

public partial class DepotAction_ObjectAddX : DepotPageSingle
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
            deptRow.Visible = Depot.Featured(DepotType.固定资产库) && !Depot.Featured(DepotType.幼儿园);
            if (Depot.Featured(DepotType.固定资产库))
            {
                r1.Visible = r2.Visible = r3.Visible = false;
                r4.Visible = r5.Visible = true;
            }
            else
            {
                var types = Depot.ObjectTypes;
                r1.Visible = types.Contains(t1.Value.GetFirstChar());
                r2.Visible = types.Contains(t2.Value.GetFirstChar());
                r3.Visible = types.Contains(t3.Value.GetFirstChar());
                new[] { t1, t2, t3 }.ToList().ForEach(o => { if (o.Value.GetFirstChar() == Depot.DefaultObjectType) o.Checked = true; });
                r4.Visible = false;
                r5.Visible = false;
            }
            if (!"CatalogId".Query().None())
            {
                var node = tree.EmbeddedTree.GetAllNodes().First(o => o.Value == "CatalogId".Query());
                node.ExpandParentNodes();
                tree.SelectedValue = "CatalogId".Query();
            }
        }
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/In?DepotId={0}".Formatted(Depot.Id));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().None())
        {
            NotifyError(ap, "请输入物资名称");
            return;
        }
        if (new[] { t1, t2, t3 }.Count(o => o.Checked == true) == 0)
        {
            NotifyError(ap, "请选择物资类型");
            return;
        }
        if (tree.SelectedValue.None())
        {
            NotifyError(ap, "请选择物资类别");
        }
        var id = Save();
        Response.Redirect("~/DepotAction/In?DepotId={0}".Formatted(Depot.Id));
    }

    protected void goon_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().None())
        {
            NotifyError(ap, "请输入物资名称");
            return;
        }
        if (new[] { t1, t2, t3 }.Count(o => o.Checked == true) == 0)
        {
            NotifyError(ap, "请选择物资类型");
            return;
        }
        if (tree.SelectedValue.None())
        {
            NotifyError(ap, "请选择物资类别");
        }
        Save();
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().None())
        {
            NotifyError(ap, "请输入物资名称");
            return;
        }
        if (new[] { t1, t2, t3 }.Count(o => o.Checked == true) == 0)
        {
            NotifyError(ap, "请选择物资类型");
            return;
        }
        if (tree.SelectedValue.None())
        {
            NotifyError(ap, "请选择物资类别");
        }
        Save();
        Response.Redirect("~/DepotAction/In?DepotId={0}".Formatted(Depot.Id));
    }

    protected Guid Save()
    {
        var id = DataContext.GlobalId();
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
        if (Depot.Featured(DepotType.固定资产库))
        {
            DataContext.DepotObjectAdd(id, ids, Depot.Id, name.Text.Trim(), t4.Checked, false, true, "", "", brand.Text.Trim(), "", unit.Text.Trim(), specification.Text.Trim(), low.PeekValue(0.00M), high.PeekValue(0.00M), photo.Length > 0 ? photo[0] : "", photo.Length > 1 ? photo[1] : "", photo.Length > 2 ? photo[2] : "", photo.Length > 3 ? photo[3] : "", content.Text.Trim(), ordinal.PeekValue(100), age.Text.Trim(), true, dept.Text.Trim());
        }
        else
        {
            DataContext.DepotObjectAdd(id, ids, Depot.Id, name.Text.Trim(), t3.Checked, t1.Checked, false, "", "", brand.Text.Trim(), "", unit.Text.Trim(), specification.Text.Trim(), low.PeekValue(0.00M), high.PeekValue(0.00M), photo.Length > 0 ? photo[0] : "", photo.Length > 1 ? photo[1] : "", photo.Length > 2 ? photo[2] : "", photo.Length > 3 ? photo[3] : "", content.Text.Trim(), ordinal.PeekValue(100), age.Text.Trim(), false, dept.Text.Trim());
        }
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
