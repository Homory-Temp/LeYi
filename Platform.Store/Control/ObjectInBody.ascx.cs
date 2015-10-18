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
            var catalogs = db.Value.StoreCatalog.Where(o => o.StoreId == StoreId && o.State < 2).OrderBy(o => o.Ordinal).ToList();
            var target = db.Value.StoreTarget.Single(o => o.Id == TargetId);
            if (CurrentStore.State == Models.StoreState.食品)
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
        }
    }

    public decimal Amount
    {
        get
        {
            return amount.PeekValue(0);
        }
        set
        {
            amount.Value = (double)value;
        }
    }

    public decimal SourcePerPrice
    {
        get
        {
            return perPrice.PeekValue(0.00M);
        }
        set
        {
            perPrice.Value = (double)value;
        }
    }

    public decimal Fee
    {
        get
        {
            return fee.PeekValue(0.00M);
        }
        set
        {
            fee.Value = (double)value;
        }
    }

    public decimal Money
    {
        get
        {
            return money.PeekValue(0.00M);
        }
        set
        {
            money.Value = (double)value;
        }
    }

    public string Place
    {
        get
        {
            return place.Text;
        }
        set
        {
            place.Text = value;
        }
    }

    public string Note
    {
        get
        {
            return note.Text;
        }
        set
        {
            note.Text = value;
        }
    }

    public int ItemIndex
    {
        get; set;
    }

    public Guid TargetId
    {
        get; set;
    }

    public Guid ObjectId
    {
        get
        {
            return obj.SelectedValue.GlobalId();
        }
    }

    protected void catalog_EntryAdded(object sender, Telerik.Web.UI.DropDownTreeEntryEventArgs e)
    {
        var catalogId = e.Entry.Value.GlobalId();
        var catalog = db.Value.StoreCatalog.Single(o => o.Id == catalogId);
        var list = new List<Guid>();
        AddChildren(list, catalog);
        obj.DataSource = list.Join(db.Value.StoreObject, o => o, o => o.CatalogId, (a, b) => b).OrderBy(o => o.Ordinal).ToList();
        obj.DataBind();
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
        }
        else
        {
            unit.Text = "";
            specification.Text = "";
            stored.Text = "";
        }
    }
}
