using Models;
using System;
using System.Linq;

public partial class DepotSetting_CatalogEdit : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "CatalogId".Query().GlobalId();
            var item = DataContext.DepotCatalogLoad(Depot.Id).Single(o => o.Id == id);
            ordinal.Value = item.Ordinal;
            name.Text = item.Name;
        }
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().None())
        {
            NotifyError(ap, "请输入类别名称");
            return;
        }
        var id = "CatalogId".Query().GlobalId();
        var item = DataContext.DepotCatalogLoad(Depot.Id).Single(o => o.Id == id);
        var content = name.Text.Trim();
        var pinYin = DataContext.ToPinYin(content).Single();
        item.Name = content;
        item.PinYin = pinYin;
        item.Ordinal = ordinal.PeekValue(100);
        DataContext.SaveChanges();
        Response.Redirect("~/DepotSetting/Catalog?DepotId={0}&Initial={1}".Formatted(Depot.Id, item.ParentId));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        var id = "CatalogId".Query().GlobalId();
        var item = DataContext.DepotCatalogLoad(Depot.Id).Single(o => o.Id == id);
        Response.Redirect("~/DepotSetting/Catalog?DepotId={0}&Initial={1}".Formatted(Depot.Id, item.ParentId));
    }
}
