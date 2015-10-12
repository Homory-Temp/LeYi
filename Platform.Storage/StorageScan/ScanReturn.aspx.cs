using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ScanReturn : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScanOutListSingle.Clear();
        }
        code.Focus();
    }

    protected List<Guid> ScanOutListSingle
    {
        get
        {
            if (Session["ScanReturnD"] == null)
                Session["ScanReturnD"] = new List<Guid>();
            return Session["ScanReturnD"] as List<Guid>;
        }
    }

    protected void add_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var c = code.Text;
        code.Text = string.Empty;
        if (c.Length != 12)
            return;
        string type;
        Guid id;
        db.Value.FromQR(c, out type, out id);
        switch (type)
        {
            case "W":
                {
                    break;
                }
            case "D":
                {
                    ScanOutListSingle.Add(id);
                    viewD.Rebind();
                    break;
                }
        }
    }

    protected void viewD_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = ScanOutListSingle.Join(db.Value.查询_借还流, o => o, o => o.单入标识, (x, y) => y).Where(o => o.仓库标识 == StorageId).ToList();
        viewD.Source(source);
    }

    protected string GetAutoId(查询_借用单 sheet)
    {
        return db.Value.ToQR("W", db.Value.StorageObject.Single(o => o.Id == sheet.物品标识).AutoId);
    }

    protected string GetAutoId(StorageLendSingle sheet)
    {
        return db.Value.ToQR("D", sheet.StorageIn.StorageInSingle.Single(o => o.InOrdinal == sheet.Ordinal).AutoId);
    }

    protected bool InRecord()
    {
        if (!in_confirm.Checked) { in_confirm.ForeColor = Color.Red; return false; }
        apx.ResponseScripts.Add("scanDoX();");
        return true;
    }

    protected void out_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        if (!InRecord()) return;
    }

    protected void ap_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        foreach (var item in viewD.Items)
        {
            var id = item.GetDataKeyValue("单借标识").ToString().GlobalId();
            var @single = db.Value.查询_借还流.SingleOrDefault(o => o.单借标识 == id);
            var amountCtrl = (item.FindControl("amount") as RadNumericTextBox);
            var amount = amountCtrl.Value.HasValue ? (decimal)amountCtrl.Value.Value : 0;
            if (amount == 0)
                continue;
            var note = (item.FindControl("note") as RadTextBox).Text;
            db.Value.SetReturnSpecific(@single, CurrentUser, note ?? string.Empty);
        }
        db.Value.StorageSave();
        ScanOutListSingle.Clear();
        Response.Redirect("~/StorageQuery/QueryReturn?StorageId={0}".Formatted(StorageId));
    }

    protected void adv_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageScan/ScanReturnAdv?StorageId={0}".Formatted(StorageId));
    }
}
