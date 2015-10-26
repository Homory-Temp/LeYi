using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Control_ObjectIn : DepotControlSingle
{
    public void LoadDefaults(InMemoryIn @in)
    {
        orderId.Value = @in.OrderId.ToString();
        var order = DataContext.DepotOrder.Single(o => o.Id == @in.OrderId);
        catalog.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id);
        catalog.DataBind();
        age.Text = @in.Age;
        place.Text = @in.Place;
        amount.Value = (double?)@in.Amount;
        priceSet.Value = (double?)@in.PriceSet;
        money.Value = (double?)@in.Money;
        note.Text = @in.Note;
        time.SelectedDate = @in.Time;
        if (@in.CatalogId.HasValue && @in.CatalogId.Value != Guid.Empty)
        {
            var catalogId = @in.CatalogId.Value;
            var node = catalog.EmbeddedTree.FindNodeByValue(catalogId.ToString());
            node.Selected = true;
            node.ExpandParentNodes();
            catalog.SelectedValue = catalogId.ToString();
            var source = DataContext.DepotObjectLoad(Depot.Id, @in.CatalogId);
            obj.DataSource = source.ToList();
            obj.DataBind();
            if (@in.ObjectId.HasValue && @in.ObjectId.Value != Guid.Empty)
            {
                var oid = @in.ObjectId.Value;
                var so = DataContext.DepotObject.Single(o => o.Id == oid);
                unit.Text = so.Unit;
                specification.Text = so.Specification;
                stored.Text = so.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
                obj.SelectedIndex = obj.FindItemIndexByValue(@in.ObjectId.ToString());
            }
            age.Text = @in.Age;
        }
    }

    public InMemoryIn PeekValue()
    {
        var oid = orderId.Value.GlobalId();
        var result = new InMemoryIn();
        result.OrderId = oid;
        result.CatalogId = catalog.SelectedValue == null ? (Guid?)null : catalog.SelectedValue.GlobalId();
        result.ObjectId = obj.SelectedValue == null || obj.Text.None() ? (Guid?)null : obj.SelectedValue.GlobalId();
        result.Time = time.SelectedDate.HasValue ? time.SelectedDate.Value : DataContext.DepotOrder.Single(o => o.Id == oid).RecordTime;
        result.Amount = amount.Value.HasValue ? (decimal)amount.Value.Value : (decimal?)null;
        result.PriceSet = priceSet.Value.HasValue ? (decimal)priceSet.Value.Value : (decimal?)null;
        result.Money = money.Value.HasValue ? (decimal)money.Value.Value : (decimal?)null;
        result.Age = age.Text;
        result.Place = place.Text;
        result.Note = note.Text;
        return result;
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
            specification.Text = so.Specification;
            stored.Text = so.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
        }
    }
}
