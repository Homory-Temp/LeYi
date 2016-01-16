using Models;
using System;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_Object : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree0.Nodes[0].Text = "全部类别{0}".Formatted(DataContext.DepotObjectLoad(Depot.Id, null, true).Count().EmptyWhenZero());
            tree.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList().Where(o => o.Code != "*Homory:Null*").ToList();
            tree.DataBind();
            depts.DataSource = DataContext.DepotObjectLoad(Depot.Id, null).Select(o => o.Department).Distinct().OrderBy(o => o).ToList();
            depts.DataBind();
            depts.Visible = deptsSpan.Visible = !Depot.Featured(DepotType.幼儿园);
            if (!"CatalogId".Query().None())
            {
                var cid = "CatalogId".Query();
                if (tree.GetAllNodes().Count(o => o.Value == cid) == 1)
                {
                    var node = tree.GetAllNodes().Single(o => o.Value == cid);
                    node.Selected = true;
                    node.ExpandParentNodes();
                    node.ExpandChildNodes();
                    tree0.Nodes[0].Selected = false;
                }
            }
            if (Depot.DefaultObjectView == "Simple".GetFirstChar())
            {
                view_simple.Attributes["class"] = "btn btn-warning";
                view_photo.Attributes["class"] = "btn btn-info";
            }
            else
            {
                view_simple.Attributes["class"] = "btn btn-info";
                view_photo.Attributes["class"] = "btn btn-warning";
            }
            if (!"Search".Query().None())
            {
                toSearch.Text = Server.UrlDecode("Search".Query());
                view.Rebind();
            }
        }
    }

    public void MakeThumNail(string originalImagePath, string thumNailPath, int width, int height, string model)
    {
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
        int thumWidth = width; 
        int thumHeight = height;  
        int x = 0;
        int y = 0;
        int originalWidth = originalImage.Width; 
        int originalHeight = originalImage.Height; 
        switch (model)
        {
            case "HW":  
                break;
            case "W":     
                thumHeight = originalImage.Height * width / originalImage.Width;
                break;
            case "H":   
                thumWidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut":
                if ((double)originalImage.Width / (double)originalImage.Height > (double)thumWidth / (double)thumHeight)
                {
                    originalHeight = originalImage.Height;
                    originalWidth = originalImage.Height * thumWidth / thumHeight;
                    y = 0;
                    x = (originalImage.Width - originalWidth) / 2;
                }
                else
                {
                    originalWidth = originalImage.Width;
                    originalHeight = originalWidth * height / thumWidth;
                    x = 0;
                    y = (originalImage.Height - originalHeight) / 2;
                }
                break;
            default:
                break;
        }
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(thumWidth, thumHeight);
        System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(bitmap);
        graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        graphic.Clear(System.Drawing.Color.Transparent);
        graphic.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumWidth, thumHeight), new System.Drawing.Rectangle(x, y, originalWidth, originalHeight), System.Drawing.GraphicsUnit.Pixel);
        try
        {
            bitmap.Save(thumNailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            graphic.Dispose();
        }
    }

    protected string GenAutoImage(object url)
    {
        if (url.None())
            return "../Content/Images/Transparent.png";
        else
        {
            var path = Server.MapPath(url.ToString());
            var pathx = path.Substring(0, path.LastIndexOf(".")) + ".done" + path.Substring(path.LastIndexOf("."));
            if (!System.IO.File.Exists(pathx))
            {
                MakeThumNail(path, pathx, 0, 158, "H");
            }
            return url.ToString().Substring(0, url.ToString().LastIndexOf(".")) + ".done" + url.ToString().Substring(url.ToString().LastIndexOf("."));
        }
    }

    protected string DName(Guid? did)
    {
        if (did == null)
            return "";
        else
            return DataContext.Depot.SingleOrDefault(o => o.Id == did).Name;
    }

    protected string CountTotal(DepotObject obj)
    {
        var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
        var noOut = query.Count() > 0 ? query.Where(o => o.Type == UseType.借用).Sum(o => o.Amount - o.ReturnedAmount) : 0;
        return (obj.Amount + noOut).ToAmount(Depot.Featured(DepotType.小数数量库));
    }

    protected bool IsSimple
    {
        get
        {
            return view_photo.Attributes["class"] != "btn btn-warning";
        }
    }

    protected Guid? CurrentNode
    {
        get
        {
            return tree.SelectedNode == null ? (Guid?)null : tree.SelectedValue.GlobalId();
        }
    }

    protected void manage_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotSetting/Catalog?DepotId={0}".Formatted(Depot.Id));
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectAdd?DepotId={0}&CatalogId={1}".Formatted(Depot.Id, CurrentNode));
    }

    protected void tree0_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree.SelectedNode != null)
            tree.SelectedNode.Selected = false;
        view.CurrentPageIndex = 0;
        view.Rebind();
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (tree0.SelectedNode != null)
            tree0.SelectedNode.Selected = false;
        tree.GetAllNodes().Where(o => o.ParentNode == e.Node.ParentNode).ToList().ForEach(o => o.Expanded = false);
        e.Node.Expanded = true;
        view.CurrentPageIndex = 0;
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var node = CurrentNode;
        var source = DataContext.DepotObjectLoad(Depot.Id, node.HasValue ? node.Value.GlobalId() : (Guid?)null, true);
        if (!toSearch.Text.None())
        {
            source = source.Where(o => o.Name.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.PinYin.ToLower().Contains(toSearch.Text.Trim().ToLower()) || o.Code.ToLower() == toSearch.Text.Trim().ToLower()).ToList();
        }
        if (depts.SelectedIndex >= 0)
        {
            var dept = depts.SelectedItem.Text;
            source = source.Where(o => o.Department == dept).ToList();
        }
        if (only.Checked)
        {
            source = source.Where(o => o.Fixed == true).ToList();
        }
        view.DataSource = source.OrderByDescending(o => o.AutoId).ToList();
        pager.Visible = source.Count() > pager.PageSize;
    }

    protected void view_simple_ServerClick(object sender, EventArgs e)
    {
        if (view_simple.Attributes["class"] == "btn btn-info")
        {
            view_simple.Attributes["class"] = "btn btn-warning";
            view_photo.Attributes["class"] = "btn btn-info";
            view.Rebind();
        }
    }

    protected void view_photo_ServerClick(object sender, EventArgs e)
    {
        if (view_photo.Attributes["class"] == "btn btn-info")
        {
            view_simple.Attributes["class"] = "btn btn-info";
            view_photo.Attributes["class"] = "btn btn-warning";
            view.Rebind();
        }
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/In?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        var objId = (sender as HtmlInputButton).Attributes["match"].GlobalId();
        var isVirtual = Depot.Featured(DepotType.固定资产库);
        var catalogId = DataContext.DepotObjectCatalog.Single(o => o.ObjectId == objId && o.IsLeaf == true && o.IsVirtual == isVirtual).CatalogId;
        Response.Redirect("~/DepotAction/ObjectEdit?DepotId={0}&ObjectId={1}&CatalogId={2}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"], catalogId));
    }

    protected void delete_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectRemove?DepotId={0}&ObjectId={1}&CatalogId={2}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"], CurrentNode.HasValue ? CurrentNode.Value : Guid.Empty));
    }

    protected void use_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Use?DepotId={0}&ObjectId={1}&UseType=2".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void usex_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Use?DepotId={0}&ObjectId={1}&UseType=1".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected void out_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/Out?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void only_CheckedChanged(object sender, EventArgs e)
    {
        view.Rebind();
    }

    protected string ToLine(string value)
    {
        var sb = new StringBuilder();
        int xno = int.Parse(line_no.Value);
        for (var i = 0; i < value.Length; i++)
        {
            sb.Append(value[i]);
            if (i % xno == 0 && i > 0)
            {
                sb.Append("<br/ >");
            }
        }
        if (sb.ToString().EndsWith("<br />"))
            sb = sb.Remove(sb.ToString().LastIndexOf("<br />"), 6);
        return sb.ToString();
    }

    protected void fix_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotQuery/ObjectToFixed?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"]));
    }

    protected void clear_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectClear?DepotId={0}&ObjectId={1}&CatalogId={2}".Formatted(Depot.Id, (sender as HtmlInputButton).Attributes["match"], CurrentNode.HasValue ? CurrentNode.Value : Guid.Empty));
    }
}
