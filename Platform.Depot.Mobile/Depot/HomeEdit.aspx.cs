using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Depot_HomeEdit : DepotPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var id = "DepotId".Query().GlobalId();
            var item = DataContext.Depot.Single(o => o.Id == id);
            ordinal.Value = item.Ordinal;
            name.Text = item.Name;
            view.SelectedIndex = view.Items.Single(o => o.Value.GetFirstChar() == item.DefaultObjectView).Index;
            t1x.Visible = t1.Checked = item.ObjectTypes.Contains("C");
            t2x.Visible = t2.Checked = item.ObjectTypes.Contains("U");
            t3x.Visible = t3.Checked = item.ObjectTypes.Contains("S");
            new[] { t1x, t2x, t3x }.ToList().ForEach(o => { if (o.Value.GetFirstChar() == item.DefaultObjectType) o.Checked = true; });
        }
    }

    protected void edit_ServerClick(object sender, EventArgs e)
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
        var id = "DepotId".Query().GlobalId();
        DataContext.DepotEdit(id, name.Text.Trim(), ordinal.PeekValue(100), view.PeekValue("Simple", false).GetFirstChar(), new[] { t1x, t2x, t3x }.PeekRadioValue(string.Empty), "{0}{1}{2}".Formatted(t1.PeekValue(true).GetFirstChar(), t2.PeekValue(true).GetFirstChar(), t3.PeekValue(true).GetFirstChar()));
        Response.Redirect("~/Depot/Home");
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
