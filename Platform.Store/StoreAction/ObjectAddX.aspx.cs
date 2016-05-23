using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreAction_ObjectAddX : SingleStorePage
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
        Response.Redirect("~/StoreAction/In?StoreId={0}".Formatted(StoreId));
    }

    protected void in_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入物资名称", "error");
            return;
        }
        if (new[] { t1, t2, t3 }.Count(o => o.Checked == true) == 0)
        {
            Notify(ap, "请选择物资类型", "error");
            return;
        }
        var r = Save();
        if (r == Guid.Empty)
        {
            Notify(ap, "同级分类下请勿创建同名物资", "error");
            return;
        }
        Response.Redirect("~/StoreAction/In?StoreId={0}".Formatted(StoreId));
    }

    protected void goon_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入物资名称", "error");
            return;
        }
        if (new[] { t1, t2, t3 }.Count(o => o.Checked == true) == 0)
        {
            Notify(ap, "请选择物资类型", "error");
            return;
        }
        var r = Save();
        if (r == Guid.Empty)
        {
            Notify(ap, "同级分类下请勿创建同名物资", "error");
            return;
        }
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入物资名称", "error");
            return;
        }
        if (new[] { t1, t2, t3 }.Count(o => o.Checked == true) == 0)
        {
            Notify(ap, "请选择物资类型", "error");
            return;
        }
        var r = Save();
        if (r == Guid.Empty)
        {
            Notify(ap, "同级分类下请勿创建同名物资", "error");
            return;
        }
        Response.Redirect("~/StoreAction/In?StoreId={0}".Formatted(StoreId));
    }

    protected Guid Save()
    {
        var test = name.Text.Trim();
        var testx = tree.SelectedValue.GlobalId();
        if (db.Value.StoreObject.Count(x => x.Name == test && x.State < 2 && x.CatalogId == testx) > 0)
        {
            return Guid.Empty;
        }
        var id = db.Value.GlobalId();
        var img = new[] { p0, p1, p2, p3 }.ToList();
        var photo = img.Count(o => !o.Src.Contains("/Content/Images/Transparent.png")) == 0 ? string.Empty : img.Where(o => !o.Src.Contains("/Content/Images/Transparent.png")).Select(o => o.Src).Aggregate("", (a, b) => a += "*" + b, o => o.Substring(1));
        var obj = new StoreObject
        {
            Id = id,
            CatalogId = tree.SelectedValue.GlobalId(),
            SourceCatalogId = null,
            Name = name.Text.Trim(),
            PinYin = db.Value.ToPinYin(name.Text.Trim()).Single(),
            Single = t3.Checked,
            Consumable = t1.Checked,
            Fixed = false,
            Serial = string.Empty,
            Unit = unit.Text.Trim(),
            Specification = specification.Text.Trim(),
            Low = low.PeekValue(0.00M),
            High = high.PeekValue(0.00M),
            Image = photo,
            Note = content.Text.Trim(),
            TimeNode = DateTime.Today.ToTimeNode(),
            Time = DateTime.Today,
            OperationUserId = CurrentUser,
            OperationTime = DateTime.Now,
            Ordinal = ordinal.PeekValue(100),
            State = 1,
            Code = code.Text.Trim(),
            Amount = 0.00M,
            Money = 0.00M
        };
        db.Value.StoreObject.Add(obj);
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
