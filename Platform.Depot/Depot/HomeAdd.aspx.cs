using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Depot_HomeAdd : DepotPage
{
    protected void add_ServerClick(object sender, EventArgs e)
    {
        if (name.Text.None())
        {
            NotifyError(ap, "请输入仓库名称");
            return;
        }
        if ("{0}{1}{2}".Formatted(t1.PeekValue(true), t2.PeekValue(true), t3.PeekValue(true)).None())
        {
            NotifyError(ap, "请选择物资类型");
            return;
        }
        if (new[] { t1x, t2x, t3x }.PeekRadioValue(string.Empty).None())
        {
            NotifyError(ap, "请选择默认物资类型");
            return;
        }
        DataContext.DepotAdd(name.Text.Trim(), DepotUser.CampusId, DepotUser.Id, ordinal.PeekValue(100), view.PeekValue("Simple", false), new[] { t1x, t2x, t3x }.PeekRadioValue(string.Empty), "{0}{1}{2}".Formatted(t1.PeekValue(true).GetFirstChar(), t2.PeekValue(true).GetFirstChar(), t3.PeekValue(true).GetFirstChar()), DepotType.通用库, State.启用);
        Response.Redirect("~/Depot/DepotHome");
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Depot/Home");
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
