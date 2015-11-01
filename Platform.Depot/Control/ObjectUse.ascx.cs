using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Control_ObjectUse : DepotControlSingle
{
    public void LoadDefaults(InMemoryUse use)
    {
        catalog.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
        catalog.DataBind();
        amount.Value = (double?)use.Amount;
        amount.NumberFormat.DecimalDigits = Depot.Featured(DepotType.小数数量库) ? 2 : 0;
        age.Text = use.Age;
        place.Text = use.Place;
        note.Text = use.Note;
        if (use.CatalogId.HasValue && use.CatalogId.Value != Guid.Empty)
        {
            var catalogId = use.CatalogId.Value;
            var node = catalog.EmbeddedTree.FindNodeByValue(catalogId.ToString());
            node.Selected = true;
            node.ExpandParentNodes();
            catalog.SelectedValue = catalogId.ToString();
            var source = DataContext.DepotObjectLoad(Depot.Id, use.CatalogId);
            obj.DataSource = source.ToList();
            obj.DataBind();
            if (use.ObjectId.HasValue && use.ObjectId.Value != Guid.Empty)
            {
                var oid = use.ObjectId.Value;
                var so = DataContext.DepotObject.Single(o => o.Id == oid);
                unit.Text = so.Unit;
                brand.Text = so.Brand;
                specification.Text = so.Specification;
                stored.Text = so.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
                obj.SelectedIndex = obj.FindItemIndexByValue(use.ObjectId.ToString());
                if (so.Single)
                {
                    amount.Visible = false;
                    ordinalList.Visible = true;
                    ordinalList.DataSource = so.DepotInX.ToList().Where(o => o.AvailableAmount == 1).Select(o => o.Ordinal).OrderBy(o => o).ToList();
                    ordinalList.DataBind();
                    ordinalList.Items.ToList().ForEach(o => { if (use.Ordinals.Contains(int.Parse(o.Text))) o.Checked = true; });
                }
                else
                {
                    amount.Visible = true;
                    ordinalList.Visible = false;
                }
                if (so.Consumable)
                {
                    act.DataSource = new[] { "领用" };
                    act.DataBind();
                }
                else if (so.Single)
                {
                    act.DataSource = new[] { "借用" };
                    act.DataBind();
                }
                else
                {
                    act.DataSource = new[] { "借用", "领用" };
                    act.DataBind();
                }
                if (!"ObjectId".Query().None())
                {
                    act.FindItemByText("UseType".Query().Equals("1") ? "借用" : "领用").Selected = true;
                }
                if (use.Type.HasValue)
                {
                    act.FindItemByText(use.Type.Value.ToString()).Selected = true;
                }
            }
        }
        if (!"ObjectId".Query().None())
        {
            catalog.Enabled = false;
            obj.Enabled = false;
        }
    }

    public InMemoryUse PeekValue()
    {
        var result = new InMemoryUse();
        result.Age = age.Text;
        result.Place = place.Text;
        result.CatalogId = catalog.SelectedValue.None() ? (Guid?)null : catalog.SelectedValue.GlobalId();
        result.ObjectId = obj.SelectedValue == null || obj.Text.None() ? (Guid?)null : obj.SelectedValue.GlobalId();
        result.Amount = amount.Value.HasValue ? (decimal)amount.Value.Value : (decimal?)null;
        result.Note = note.Text;
        result.Ordinals = ordinalList.Items.Where(o => o.Checked).Select(o => int.Parse(o.Text)).ToList();
        result.Type = act.SelectedIndex == -1 ? (UseType?)null : (UseType)Enum.Parse(typeof(UseType), act.SelectedItem.Text);
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
            if (so.Consumable)
            {
                act.Items.Clear();
                act.DataSource = new[] { "领用" };
                act.DataBind();
            }
            else if (so.Single)
            {
                act.Items.Clear();
                act.DataSource = new[] { "借用" };
                act.DataBind();
            }
            else
            {
                act.Items.Clear();
                act.DataSource = new[] { "借用", "领用" };
                act.DataBind();
            }
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
            act.SelectedIndex = 0;
        }
    }
}
