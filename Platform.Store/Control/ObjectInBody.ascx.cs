using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_ObjectInBody : SingleStoreControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public void LoadDefaults(CachedIn @in)
    {
        tid.Value = @in.TargetId.ToString();
        var target = db.Value.StoreTarget.Single(o => o.Id == @in.TargetId);
        var catalogs = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
        if (CurrentStore.State == StoreState.食品)
        {
            catalogs.RemoveAll(o => o.ParentId == null && o.Name != target.UsageTarget);
            catalog.DataSource = catalogs;
            catalog.DataBind();
            catalog.EmbeddedTree.Nodes[0].Expanded = true;
        }
        else
        {
            catalog.DataSource = catalogs;
            catalog.DataBind();
        }
        amount.Value = (double?)@in.Amount;
        perPrice.Value = (double?)@in.SourcePerPrice;
        fee.Value = (double?)@in.Fee;
        money.Value = (double?)@in.Money;
        place.Text = @in.Place;
        note.Text = @in.Note;
        time.SelectedDate = (@in.TimeNode.HasValue ? @in.TimeNode.Value : target.TimeNode).ToTime();
        if (@in.CatalogId.HasValue && @in.CatalogId.Value != Guid.Empty)
        {
            var catalogId = @in.CatalogId.Value;
            var node = catalog.EmbeddedTree.FindNodeByValue(catalogId.ToString());
            node.Selected = true;
            node.ExpandParentNodes();
            catalog.SelectedValue = catalogId.ToString();
            var c = db.Value.StoreCatalog.Single(o => o.Id == catalogId);
            var list = new List<Guid>();
            AddChildren(list, c);
            obj.DataSource = list.Join(db.Value.StoreObject, o => o, o => o.CatalogId, (a, b) => b).OrderBy(o => o.Ordinal).ToList();
            obj.DataBind();
            if (@in.ObjectId.HasValue && @in.ObjectId.Value != Guid.Empty)
            {
                var oid = @in.ObjectId.Value;
                var so = db.Value.StoreObject.Single(o => o.Id == oid);
                unit.Text = so.Unit;
                specification.Text = so.Specification;
                stored.Text = so.Amount.ToAmount();
                obj.SelectedIndex = obj.FindItemIndexByValue(@in.ObjectId.ToString());
            }
        }
    }

    public CachedIn PeekValue()
    {
        var targetId = tid.Value.GlobalId();
        var result = new CachedIn();
        result.TargetId = targetId;
        result.CatalogId = catalog.SelectedValue == null ? (Guid?)null : catalog.SelectedValue.GlobalId();
        result.ObjectId = obj.SelectedValue == null || obj.Text.Null() ? (Guid?)null : obj.SelectedValue.GlobalId();
        result.TimeNode = time.SelectedDate.HasValue ? time.SelectedDate.Value.ToTimeNode() : db.Value.StoreTarget.Single(o => o.Id == targetId).TimeNode;
        result.Amount = amount.Value.HasValue ? (decimal)amount.Value.Value : (decimal?)null;
        result.SourcePerPrice = perPrice.Value.HasValue ? (decimal)perPrice.Value.Value : (decimal?)null;
        result.Fee = fee.Value.HasValue ? (decimal)fee.Value.Value : (decimal?)null;
        result.Money = money.Value.HasValue ? (decimal)money.Value.Value : (decimal?)null;
        result.Place = place.Text;
        result.Note = note.Text;
        return result;
    }

    public int ItemIndex
    {
        get; set;
    }

    protected void catalog_EntryAdded(object sender, Telerik.Web.UI.DropDownTreeEntryEventArgs e)
    {
        var catalogId = e.Entry.Value.GlobalId();
        var catalog = db.Value.StoreCatalog.Single(o => o.Id == catalogId);
        var list = new List<Guid>();
        AddChildren(list, catalog);
        obj.DataSource = list.Join(db.Value.StoreObject, o => o, o => o.CatalogId, (a, b) => b).OrderBy(o => o.Ordinal).ToList();
        obj.DataBind();
        obj.ClearSelection();
        obj.Text = string.Empty;
    }

    protected void AddChildren(List<Guid> list, StoreCatalog catalog)
    {
        list.Add(catalog.Id);
        foreach (var child in catalog.ChildrenStoreCatalog)
        {
            AddChildren(list, child);
        }
    }

    protected void obj_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (obj.SelectedValue != null && !obj.Text.Null())
        {
            var id = obj.SelectedValue.GlobalId();
            var so = db.Value.StoreObject.Single(o => o.Id == id);
            unit.Text = so.Unit;
            specification.Text = so.Specification;
            stored.Text = so.Amount.ToAmount();
        }
    }
}
