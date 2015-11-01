﻿using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Control_ObjectIn : DepotControlSingle
{
    public void LoadDefaults(InMemoryIn @in)
    {
        catalog.DataSource = DataContext.DepotCatalogTreeLoad(Depot.Id).ToList();
        catalog.DataBind();
        age.Text = @in.Age;
        place.Text = @in.Place;
        amount.Value = (double?)@in.Amount;
        amount.NumberFormat.DecimalDigits = Depot.Featured(DepotType.小数数量库) ? 2 : 0;
        priceSet.Value = (double?)@in.PriceSet;
        money.Value = (double?)@in.Money;
        note.Text = @in.Note;
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
                brand.Text = so.Brand;
                specification.Text = so.Specification;
                stored.Text = so.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
                obj.SelectedIndex = obj.FindItemIndexByValue(@in.ObjectId.ToString());
            }
        }
        if (!"ObjectId".Query().None())
        {
            catalog.Enabled = false;
            obj.Enabled = false;
        }
    }

    public int ItemIndex
    {
        get; set;
    }

    public InMemoryIn PeekValue()
    {
        var result = new InMemoryIn();
        result.CatalogId = catalog.SelectedValue.None() ? (Guid?)null : catalog.SelectedValue.GlobalId();
        result.ObjectId = obj.SelectedValue == null || obj.Text.None() ? (Guid?)null : obj.SelectedValue.GlobalId();
        result.Amount = amount.Value.HasValue ? (decimal)amount.Value.Value : (decimal?)null;
        result.PriceSet = priceSet.Value.HasValue ? (decimal)priceSet.Value.Value : (decimal?)null;
        result.Money = money.Value.HasValue ? (decimal)money.Value.Value : (decimal?)null;
        result.Age = age.Text;
        result.Place = place.Text;
        result.Note = note.Text;
        result.ResponsibleId = people.SelectedValue.None() ? (Guid?)null : people.SelectedValue.GlobalId();
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
            brand.Text = so.Brand;
            specification.Text = so.Specification;
            stored.Text = so.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
            var last = so.DepotInX.OrderByDescending(o => o.AutoId).FirstOrDefault();
            if (last != null)
            {
                priceSet.Value = (double)last.PriceSet;
                age.Text = last.Age;
                place.Text = last.Place;
            }
        }
    }

    protected void people_Load(object sender, EventArgs e)
    {
        people.Items.Clear();
        people.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem { Text = "", Value = "", Selected = true });
        people.DataSource = DataContext.DepotUserLoad(Depot.CampusId).ToList();
        people.DataBind();
    }
}
