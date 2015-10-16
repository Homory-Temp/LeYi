using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreSetting_Dictionary : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            d5.Visible = d4.Visible = ds4.Visible = CurrentStore.State != StoreState.食品;
        }
    }

    protected void d_ServerClick(object sender, EventArgs e)
    {
        new[] { d1, d2, d3, d4, d5 }.ToList().ForEach(o => 
        {
            if ((sender as HtmlInputButton).ID == o.ID)
            {
                o.Attributes["class"] = "btn btn-warning dictionary";
                dh.Value = o.ID.Replace("d", "");
            }
            else
            {
                o.Attributes["class"] = "btn btn-info dictionary";
            }
        });
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var type = (DictionaryType)int.Parse(dh.Value);
        var source = db.Value.StoreDictionary.Where(o => o.StoreId == StoreId && o.Type == type).ToList();
        view.DataSource = source;
        pager.Visible = source.Count > pager.PageSize;
    }

    protected void add_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入要添加的基础数据", "error");
            return;
        }
        var type = (DictionaryType)int.Parse(dh.Value);
        var content = name.Text.Trim();
        if (db.Value.StoreDictionary.Count(o => o.StoreId == StoreId && o.Type == type && o.Name == content) == 0)
        {
            var dictionary = new StoreDictionary
            {
                StoreId = StoreId,
                Type = type,
                Name = name.Text.Trim(),
                PinYin = db.Value.ToPinYin(name.Text.Trim()).Single()
            };
            db.Value.StoreDictionary.Add(dictionary);
            db.Value.SaveChanges();
            view.Rebind();
        }
        name.Text = string.Empty;
        Notify(ap, "基础数据添加成功", "success");
    }

    protected void remove_ServerClick(object sender, EventArgs e)
    {
        var content = (sender as HtmlInputButton).Attributes["match"];
        var type = (DictionaryType)int.Parse(dh.Value);
        var dictionary = db.Value.StoreDictionary.Single(o => o.StoreId == StoreId && o.Type == type && o.Name == content);
        db.Value.StoreDictionary.Remove(dictionary);
        db.Value.SaveChanges();
        view.Rebind();
        Notify(ap, "基础数据删除成功", "success");
    }
}
