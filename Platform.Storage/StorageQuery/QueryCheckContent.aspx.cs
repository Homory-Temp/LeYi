using Models;
using STSdb4.Data;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class QueryCheckContent : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "Id".Query().GlobalId();
            IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageCheck/Check.table"));
            try
            {
                var table = engine.OpenXTablePortable<Guid, CheckTable>("CheckList");
                var obj = table[id];
                name_ex.Text = obj.Name;
                kcuf.Value = obj.ContentItem;
                kcufX.Value = obj.ContentResult;
                view.Rebind();
            }
            finally
            {
                engine.Close();
            }
        }
    }

    protected bool CDC(Guid id)
    {
        var list = kcufX.Value.FromJson<Dictionary<Guid, bool>>();
        return list[id];
    }

    protected Color CDCColor(Guid id)
    {
        var list = kcufX.Value.FromJson<Dictionary<Guid, bool>>();
        return list[id] ? Color.Red : Color.Green;
    }

    protected void view_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        view.Source(kcuf.Value.FromJson<List<查询_盘库流>>());
    }

    protected string GP(查询_盘库流 f)
    {
        return db.Value.StorageObjectGetOne(f.物品标识).GeneratePath();
    }
}
