using Models;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Check : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = db.Value.查询_盘库流.Where(o => o.仓库标识 == StorageId).ToList().Where(o =>
        (o.入库存放地.Contains(place.Text) || o.存放地.Contains(place.Text))
        &&
        (o.物品名称.Contains(name.Text) || o.物品拼音.Contains(name.Text))
        &&
        (o.责任人.Contains(people.Text) || o.责任人拼音.Contains(people.Text))
        ).GroupBy(o => o.入库标识);
        var list = new List<S_CheckObj>();
        foreach (var g in source)
        {
            var sco = new S_CheckObj();
            sco.Id = g.Key;
            sco.In = db.Value.StorageIn.Single(o => o.Id == g.Key);
            sco.Obj = sco.In.StorageObject;
            sco.Amount = g.Count();
            sco.People = g.First().责任人;
            sco.Items = new List<SI_CheckObj>();
            foreach (var v in g)
            {
                sco.Items.Add(new SI_CheckObj { Id = v.单计标识, Ordinal = v.编号, Place = v.存放地 });
            }
            list.Add(sco);
        }
        view.Source(list);
    }

    protected void query_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        sa.Checked = false;
        view.Rebind();
    }

    protected void sa_CheckedChanged(object sender, EventArgs e)
    {
        var c = sa.Checked;
        view.Items.ForEach(o =>
        {
            var x = (o as RadListViewDataItem).FindControl("s") as RadButton;
            x.Checked = c;
        });
    }

    protected void start_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var list = new List<Guid>();
        view.Items.ForEach(o =>
        {
            var x = (o as RadListViewDataItem).FindControl("s") as RadButton;
            if (x.Checked)
                list.Add(o.GetDataKeyValue("Id").ToString().GlobalId());
        });
        if (list.Count > 0)
        {
            //    Session["CheckList"] = list.ToJson();
            //    Response.Redirect("~/StorageCheck/CheckStart?StorageId={0}&Name={1}&Place={2}".Formatted(StorageId, Server.UrlEncode(name_ex.Text), Server.UrlEncode(place.Text)));
            //}
            //else
            //{
            //    Session["CheckList"] = null;

            IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageCheck/Check.table"));
            try
            {
                var table = engine.OpenXTablePortable<Guid, ToCheckTable>("ToCheckList");
                var ct = new ToCheckTable();
                ct.Id = db.Value.GlobalId();
                ct.Name = name_ex.Text;
                ct.UserId = CurrentUser;
                ct.Time = DateTime.Now;
                ct.TimeNode = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                ct.List = list;
                ct.Place = place.Text;
                table[ct.Id] = ct;
                engine.Commit();
            }
            finally
            {
                engine.Close();
            }
        }
        //pkList.Rebind();
        Response.Redirect("~/StorageHome/Home?StorageId={0}".Formatted(StorageId));
    }

    protected string Agg(List<int> list)
    {
        var sb = new StringBuilder();
        foreach (var i in list.OrderBy(o => o))
        {
            sb.Append(i.ToString());
            sb.Append("、");
        }
        return sb.ToString(0, sb.Length - 1);
    }

    protected void view_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
        var item = e.Item as RadListViewDataItem;
        var rp = item.FindControl("rp") as Repeater;
        var inId = item.GetDataKeyValue("Id").ToString().GlobalId();
        var x = db.Value.查询_盘库流.Where(o => o.入库标识 == inId).ToList().Where(o =>
        (o.入库存放地.Contains(place.Text) || o.存放地.Contains(place.Text))
        &&
        (o.物品名称.Contains(name.Text) || o.物品拼音.Contains(name.Text))
        &&
        (o.责任人.Contains(people.Text) || o.责任人拼音.Contains(people.Text))
        ).GroupBy(o => o.存放地);
        var list = new List<StoragePlaced>();
        foreach (var g in x)
        {
            list.Add(new StoragePlaced { Place = g.Key, Ordinals = g.Select(o => o.编号).ToList() });
        }
        rp.DataSource = list;
        rp.DataBind();
    }

    protected void pkList_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageCheck/Check.table"));
        var l = new List<ToCheckTable>();
        try
        {
            var table = engine.OpenXTablePortable<Guid, ToCheckTable>("ToCheckList");
            l = table.Where(o => o.Value.UserId == CurrentUser).OrderByDescending(o => o.Value.Time).Select(o => o.Value).ToList();
        }
        finally
        {
            engine.Close();
        }
        //pkList.DataSource = l;
    }
}
