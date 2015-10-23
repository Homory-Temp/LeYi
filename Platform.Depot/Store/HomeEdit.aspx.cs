using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Store_HomeEdit : StorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "StoreId".Query().GlobalId();
            var item = db.Value.Store.Single(o => o.Id == id);
            ordinal.Value = item.Ordinal;
            name.Text = item.Name;
            view.SelectedIndex = view.FindItemIndexByValue(item.DefaultView.ToString());
            t1x.Visible = t1.Checked = item.Types.Contains("0");
            t2x.Visible = t2.Checked = item.Types.Contains("1");
            t3x.Visible = t3.Checked = item.Types.Contains("2");
            new[] { t1x, t2x, t3x }.ToList().ForEach(o => { if (o.Value == item.DefaultType.ToString()) o.Checked = true; });
            allowed.Visible = item.State != StoreState.固产;
        }
    }

    protected void edit_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.Trim().Null())
        {
            Notify(ap, "请输入仓库名称", "error");
            return;
        }
        if ("{0}{1}{2}".Formatted(t1.PeekValue(true), t2.PeekValue(true), t3.PeekValue(true)).Null())
        {
            Notify(ap, "请选择物资类型", "error");
            return;
        }
        if (new[] { t1x, t2x, t3x }.PeekValue(-1) == -1)
        {
            Notify(ap, "请选择默认物资类型", "error");
            return;
        }
        var id = "StoreId".Query().GlobalId();
        var item = db.Value.Store.Single(o => o.Id == id);
        item.Name = name.Text.Trim();
        item.Ordinal = ordinal.PeekValue(100);
        item.DefaultView = view.PeekValue(1);
        item.DefaultType = new[] { t1x, t2x, t3x }.PeekValue(1);
        item.Types = "{0}{1}{2}".Formatted(t1.PeekValue(true), t2.PeekValue(true), t3.PeekValue(true));
        db.Value.SaveChanges();
        Response.Redirect("~/Store/Home");
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Store/Home");
    }

    protected void t_CheckedChanged(object sender, EventArgs e)
    {
        t1x.Visible = t1.Checked;
        t2x.Visible = t2.Checked;
        t3x.Visible = t3.Checked;
        if (!t1.Checked)
            t1x.Checked = false;
        if (!t2.Checked)
            t2x.Checked = false;
        if (!t3.Checked)
            t3x.Checked = false;
    }

    protected void tx_CheckedChanged(object sender, EventArgs e)
    {
        if ((sender as RadButton).Checked)
            new RadButton[] { t1x, t2x, t3x }.Where(o => o.ID != (sender as RadButton).ID).ToList().ForEach(o => o.Checked = false);
    }
}
