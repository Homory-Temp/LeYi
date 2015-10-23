using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_ObjectUseBody : SingleStoreControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    public void LoadDefaults(CachedUse use)
    {
        tid.Value = use.UserTarget;
        var catalogs = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
        if (CurrentStore.State == StoreState.食品)
        {
            catalogs.RemoveAll(o => o.ParentId == null && o.Name != use.UserTarget);
            catalog.DataSource = catalogs;
            catalog.DataBind();
            catalog.EmbeddedTree.Nodes[0].Expanded = true;
        }
        else
        {
            catalog.DataSource = catalogs;
            catalog.DataBind();
        }
        amount.Value = (double?)use.Amount;
        note.Text = use.Note;
        if (use.CatalogId.HasValue && use.CatalogId.Value != Guid.Empty)
        {
            var catalogId = use.CatalogId.Value;
            var node = catalog.EmbeddedTree.FindNodeByValue(catalogId.ToString());
            node.Selected = true;
            node.ExpandParentNodes();
            catalog.SelectedValue = catalogId.ToString();
            var c = db.Value.StoreCatalog.Single(o => o.Id == catalogId);
            var list = new List<Guid>();
            AddChildren(list, c);
            obj.DataSource = list.Join(db.Value.StoreObject, o => o, o => o.CatalogId, (a, b) => b).OrderBy(o => o.Ordinal).ToList();
            obj.DataBind();
            if (use.ObjectId.HasValue && use.ObjectId.Value != Guid.Empty)
            {
                var oid = use.ObjectId.Value;
                var so = db.Value.StoreObject.Single(o => o.Id == oid);
                unit.Text = so.Unit;
                specification.Text = so.Specification;
                stored.Text = so.Amount.ToAmount();
                obj.SelectedIndex = obj.FindItemIndexByValue(use.ObjectId.ToString());
                if (so.Consumable)
                {
                    act.DataSource = new[] { "领用" };
                    act.DataBind();
                }
                else
                {
                    act.DataSource = new[] { "借用", "领用" };
                    act.DataBind();
                }
                if (!use.Type.Null())
                {
                    act.FindItemByText(use.Type).Selected = true;
                }
            }
        }
    }

    public CachedUse PeekValue()
    {
        var use = tid.Value;
        var result = new CachedUse();
        result.UserTarget = use;
        result.CatalogId = catalog.SelectedValue == null ? (Guid?)null : catalog.SelectedValue.GlobalId();
        result.ObjectId = obj.SelectedValue == null || obj.Text.Null() ? (Guid?)null : obj.SelectedValue.GlobalId();
        result.Amount = amount.Value.HasValue ? (decimal)amount.Value.Value : (decimal?)null;
        result.Note = note.Text;
        result.Type = act.SelectedIndex == -1 ? "" : act.SelectedItem.Text;
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
        if (obj.SelectedValue != null)
        {
            var id = obj.SelectedValue.GlobalId();
            var so = db.Value.StoreObject.Single(o => o.Id == id);
            unit.Text = so.Unit;
            specification.Text = so.Specification;
            stored.Text = so.Amount.ToAmount();
            if (so.Consumable)
            {
                act.DataSource = new[] { "领用" };
                act.DataBind();
            }
            else
            {
                act.DataSource = new[] { "借用", "领用" };
                act.DataBind();
            }
            act.SelectedIndex = 0;
            //amount.Value = (double)so.Amount;
        }
    }
}
