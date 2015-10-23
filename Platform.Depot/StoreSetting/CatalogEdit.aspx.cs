using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class StoreSetting_CatalogEdit : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "CatalogId".Query().GlobalId();
            var item = db.Value.StoreCatalog.Single(o => o.Id == id);
            ordinal.Value = item.Ordinal;
            name.Text = item.Name;
        }
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入类别名称", "error");
            return;
        }
        var id = "CatalogId".Query().GlobalId();
        var item = db.Value.StoreCatalog.Single(o => o.Id == id);
        var content = name.Text.Trim();
        var pinYin = db.Value.ToPinYin(content).Single();
        if (CurrentStore.State == StoreState.食品 && item.ParentId == null)
        {
            var dictionary = db.Value.StoreDictionary.Single(o => o.StoreId == StoreId && o.Type == DictionaryType.使用对象 && o.Name == item.Name);
            db.Value.StoreDictionary.Remove(dictionary);
            var dictionary2 = db.Value.StoreDictionary.Single(o => o.StoreId == StoreId && o.Type == DictionaryType.年龄段 && o.Name == item.Name);
            db.Value.StoreDictionary.Remove(dictionary2);
            var _dictionary = new StoreDictionary
            {
                StoreId = StoreId,
                Type = DictionaryType.使用对象,
                Name = content,
                PinYin = pinYin
            };
            db.Value.StoreDictionary.Add(_dictionary);
            var _dictionary2 = new StoreDictionary
            {
                StoreId = StoreId,
                Type = DictionaryType.年龄段,
                Name = content,
                PinYin = pinYin
            };
            db.Value.StoreDictionary.Add(_dictionary2);
        }
        item.Name = content;
        item.PinYin = pinYin;
        item.Ordinal = ordinal.PeekValue(100);
        db.Value.SaveChanges();
        Response.Redirect("~/StoreSetting/Catalog?StoreId={0}&Initial={1}".Formatted(StoreId, item.ParentId));
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        var id = "CatalogId".Query().GlobalId();
        var item = db.Value.StoreCatalog.Single(o => o.Id == id);
        Response.Redirect("~/StoreSetting/Catalog?StoreId={0}&Initial={1}".Formatted(StoreId, item.ParentId));
    }
}
