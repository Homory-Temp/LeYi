using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Store_HomeAdd : StorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void add_ServerClick(object sender, EventArgs e)
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
        var store = new Store
        {
            Id = db.Value.GlobalId(),
            Name = name.Text.Trim(),
            CampusId = CurrentCampus,
            Ordinal = ordinal.PeekValue(100),
            DefaultView = view.PeekValue(1),
            DefaultType = new[] { t1x, t2x, t3x }.PeekValue(1),
            Types = "{0}{1}{2}".Formatted(t1.PeekValue(true), t2.PeekValue(true), t3.PeekValue(true)),
            State = StoreState.启用
        };
        db.Value.Store.Add(store);
        var role = new StoreRole
        {
            Id = db.Value.GlobalId(),
            StoreId = store.Id,
            Name = "{0}管理组".Formatted(store.Name),
            Right = "*",
            Ordinal = 0,
            State = 0
        };
        db.Value.StoreRole.Add(role);
        role.User.Add(db.Value.GetUser(CurrentUser));
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
