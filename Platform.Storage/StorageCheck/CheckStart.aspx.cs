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

public partial class CheckStart : SingleStoragePageMobile
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["CheckList"].Null())
            {
                Response.Redirect("~/StorageCheck/ToCheck?StorageId={0}".Formatted(StorageId));
                return;
            }
            else
            {
                name_ex.Text = "Name".Query(true);
                var list = Session["CheckList"].ToString().FromJson<List<Guid>>();
                var place = "Place".Query(true);
                var source = list.Join(db.Value.查询_盘库流, o => o, o => o.入库标识, (x, y) => y).ToList().Where(o => (o.入库存放地.Contains(place) || o.存放地.Contains(place))).ToList();
                var listX = source.Select(o => o.单计标识).ToList();
                kcuf.Value = listX.ToJson();
                var dict = new Dictionary<Guid, bool>();
                foreach (var s in source)
                {
                    dict.Add(s.单计标识, false);
                }
                kcufX.Value = dict.ToJson();
                grid.Rebind();
            }
        }
        code.Focus();
    }

    protected void back_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Session["CheckList"] = null;
        Response.Redirect("~/StorageHome/HomeMobile?StorageId={0}".Formatted(StorageId));
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var c = code.Text;
        code.Text = string.Empty;
        if (c.Length != 12)
            return;
        string type;
        Guid id;
        db.Value.FromQR(c, out type, out id);
        switch (type)
        {
            case "D":
                {
                    var list = kcufX.Value.FromJson<Dictionary<Guid, bool>>();
                    list[id] = true;
                    kcufX.Value = list.ToJson();
                    grid.Rebind();
                    break;
                }
        }
    }

    protected void save_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageCheck/Check.table"));
        try
        {
            var table = engine.OpenXTablePortable<Guid, CheckTable>("CheckList");
            var ct = new CheckTable();
            ct.Id = db.Value.GlobalId();
            ct.Name = name_ex.Text;
            ct.OperationUserId = CurrentUser;
            ct.Time = DateTime.Now;
            ct.TimeNode = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            var list = kcuf.Value.FromJson<List<Guid>>();
            if (list == null || list.Count == 0)
            {
                grid.Source(null);
                return;
            }
            var source = list.Join(db.Value.查询_盘库流, o => o, o => o.单计标识, (x, y) => y).OrderBy(o => o.物品名称).ThenBy(o => o.编号).ToList();
            ct.ContentItem = source.ToJson();
            ct.ContentResult = kcufX.Value;
            table[ct.Id] = ct;
            engine.Commit();
        }
        finally
        {
            engine.Close();
        }
        Response.Redirect("~/StorageHome/HomeMobile?StorageId={0}".Formatted(StorageId));
    }

    protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        var list = kcuf.Value.FromJson<List<Guid>>();
        if (list == null || list.Count == 0)
        {
            grid.Source(null);
            return;
        }
        var source = list.Join(db.Value.查询_盘库流, o => o, o => o.单计标识, (x, y) => y).OrderBy(o => o.物品名称).ThenBy(o => o.编号).ToList();
        grid.Source(source);
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
}
