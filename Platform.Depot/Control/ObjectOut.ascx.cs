using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Control_ObjectOut : DepotControlSingle
{
    public void LoadDefaults(InMemoryOut @out)
    {
        catalog.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
        catalog.DataBind();
        amount.Value = (double?)@out.Amount;
        amount.NumberFormat.DecimalDigits = Depot.Featured(DepotType.小数数量库) ? 2 : 0;
        reason.Text = @out.Reason;
        if (@out.CatalogId.HasValue && @out.CatalogId.Value != Guid.Empty)
        {
            var catalogId = @out.CatalogId.Value;
            var node = catalog.EmbeddedTree.FindNodeByValue(catalogId.ToString());
            node.Selected = true;
            node.ExpandParentNodes();
            catalog.SelectedValue = catalogId.ToString();
            var source = DataContext.DepotObjectLoad(Depot.Id, @out.CatalogId).Where(o => o.Amount > 0);
            obj.DataSource = source.ToList();
            obj.DataBind();
            if (@out.ObjectId.HasValue && @out.ObjectId.Value != Guid.Empty)
            {
                var oid = @out.ObjectId.Value;
                var so = DataContext.DepotObject.Single(o => o.Id == oid);
                unit.Text = so.Unit;
                brand.Text = so.Brand;
                specification.Text = so.Specification;
                stored.Text = so.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
                obj.SelectedIndex = obj.FindItemIndexByValue(@out.ObjectId.ToString());
                if (so.Single)
                {
                    amount.Visible = false;
                    ordinalList.Visible = true;
                    ordinalList.DataSource = so.DepotInX.ToList().Where(o => o.AvailableAmount == 1).Select(o => o.Ordinal).OrderBy(o => o).ToList();
                    ordinalList.DataBind();
                    ordinalList.Items.ToList().ForEach(o => { if (@out.Ordinals.Contains(int.Parse(o.Text))) o.Checked = true; });
                }
                else
                {
                    amount.Visible = true;
                    ordinalList.Visible = false;
                }
            }
        }
        if (!"ObjectId".Query().None())
        {
            catalog.Enabled = false;
            obj.Enabled = false;
        }
    }

    public InMemoryOut PeekValue()
    {
        var result = new InMemoryOut();
        result.Reason = reason.Text;
        result.CatalogId = catalog.SelectedValue.None() ? (Guid?)null : catalog.SelectedValue.GlobalId();
        result.ObjectId = obj.SelectedValue == null || obj.Text.None() ? (Guid?)null : obj.SelectedValue.GlobalId();
        result.Amount = amount.Value.HasValue ? (decimal)amount.Value.Value : (decimal?)null;
        result.Ordinals = ordinalList.Items.Where(o => o.Checked).Select(o => int.Parse(o.Text)).ToList();
        return result;
    }

    public int ItemIndex
    {
        get; set;
    }

    protected void catalog_EntryAdded(object sender, Telerik.Web.UI.DropDownTreeEntryEventArgs e)
    {
        var catalogId = e.Entry.Value.GlobalId();
        var source = DataContext.DepotObjectLoad(Depot.Id, catalogId);
        obj.DataSource = source.ToList();
        obj.DataBind();
        obj.ClearSelection();
        obj.Text = string.Empty;
    }

    protected void obj_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (obj.SelectedValue != null && !obj.Text.None())
        {
            var id = obj.SelectedValue.GlobalId();
            var so = DataContext.DepotObject.Single(o => o.Id == id);
            unit.Text = so.Unit;
            brand.Text = so.Brand;
            specification.Text = so.Specification;
            stored.Text = so.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
            if (so.Single)
            {
                amount.Visible = false;
                ordinalList.Visible = true;
                ordinalList.DataSource = so.DepotInX.ToList().Where(o => o.AvailableAmount == 1).Select(o => o.Ordinal).OrderBy(o => o).ToList();
                ordinalList.DataBind();
            }
            else
            {
                amount.Visible = true;
                ordinalList.Visible = false;
            }
        }
    }
}
